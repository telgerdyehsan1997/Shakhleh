namespace Domain
{
    using Olive;
    using Olive.Csv;
    using Olive.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;

    public class RowQuotaImportService
    {
        static IDatabase Database => Context.Current.Database();

        ImportQueueItem QueueItem;
        List<ImportError> Errors;
        IDictionary<IEntity, int> Indexes;
        List<RowQuota> RowImportItems;
        List<RowQuota> SavedItems;
        List<RowQuota> DeletingItems;

        RowQuotaImportService()
        {
            QueueItem = Database.GetList<ImportQueueItem>(i => i.StatusId == ImportStatus.Pending
                                   && i.TypeId == ImportType.RowQuota.ID
                                   && i.IsArchive == false)
                .WithMin(p => p.UploadDate).GetAwaiter().GetResult();

            Errors = new List<ImportError>();
            Indexes = new Dictionary<IEntity, int>();
            RowImportItems = new List<RowQuota>();
            SavedItems = new List<RowQuota>();
            DeletingItems = new List<RowQuota>();
        }

        /// <summary>
        /// A background task will run this method to get and process pending import queue items. 
        /// </summary>
        public static async Task ProcessNext()
        {
            var service = new RowQuotaImportService();

            if (await service.UpdateToProcessing() == false) return;

            await service.Process();
        }

        async Task<bool> UpdateToProcessing()
        {
            if (QueueItem == null) return false;

            await QueueItem.UpdateStatus(ImportStatus.Processing);

            return true;
        }

        async Task Process()
        {
            await ImportItems();
            try
            {
                await Save();
                var queueList = (await Database.GetList<ImportQueueItem>(i => i.TypeId == ImportType.RowQuota.ID
                                   && i.IsArchive == false && i.StatusId != ImportStatus.Failed).OrderBy(x => x.UploadDate));

                if (queueList.Skip(1).Any())
                {
                    await Database.Update(queueList.FirstOrDefault(), x => x.IsArchive = true);
                }
            }
            catch (Exception ex)
            {
                await AddError($"Could not process item. {ex.Message}");
            }
        }

        async Task ImportItems()
        {
            try
            {
                var importFile = CsvReader.Read(await QueueItem.File.GetContentTextAsync(), isFirstRowHeaders: true);

                var missingHeaders = await ValidateColumns(importFile.Columns);

                if (missingHeaders.HasValue())
                    throw new Exception($"The following columns are missing: {missingHeaders}");

                var rows = importFile.GetRows();

                await rows.DoAsync(async (row, index) => await ImportRow(row, index + 2));
            }
            catch (Exception ex)
            {
                await AddError($"Could not process item. {ex.Message}");
            }
        }

        async Task<string> ValidateColumns(DataColumnCollection columns)
        {
            var columnNames = columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

            var names = new List<string> {
                "Commodity Code",
                "Quota Number",
                "Preference",
                "Countries",
            };

            return names.Except(x => columnNames.Contains(x)).ToList().ToString(", ");
        }

        async Task ImportRow(DataRow row, int index)
        {
            try
            {
                var item = await GetImportItem(row);

                RowImportItems.Add(item);
                Indexes[item] = index;
            }
            catch (Exception ex)
            {
                await AddError(ex.Message, index);
            }
        }

        async Task<RowQuota> GetImportItem(DataRow row)
        {
            var commodityCode = row["Commodity Code"].ToString();
            var quotaNumber = row["Quota Number"].ToString();
            var preference = row["Preference"].ToString();
            var countries = row["Countries"].ToString();


            if (commodityCode.IsEmpty() || commodityCode.Length < 8)
                throw new Exception($"Length of '{commodityCode}' is less than 8 character");

            var importcode = commodityCode.Substring(commodityCode.Length - 2, 2);
            if (int.TryParse(importcode, out int importcodeNumber))
                importcode = importcodeNumber.ToString();
            var exportcode = commodityCode.Substring(0, commodityCode.Length - 2);
            if (int.TryParse(exportcode, out int exportcodeNumber))
                exportcode = exportcodeNumber.ToString();
            
            if (exportcode.Length<8)
                exportcode=exportcode.Length switch
                {
                    7 => $"0{exportcode}",
                    6 => $"00{exportcode}",
                    _ => exportcode
                };

            if (importcode.Length < 2)
                importcode = importcode.Length switch
                {
                    1 => $"0{importcode}",
                    _ => importcode
                };

            var commodityCodeObject = await Database.FirstOrDefault<CommodityCode>(x => x.ExportCode == exportcode && x.ImportCode == importcode);
            if (commodityCodeObject == null)
                throw new Exception("There is no commodity code with this value");



            var itemsWithThisQuotaNumber = await Database.GetList<RowQuota>(x => x.QuotaNumber == quotaNumber);
            DeletingItems.AddRange(itemsWithThisQuotaNumber);

            var tempCountries = countries.Split(',');
            countries = string.Join(',', tempCountries.Distinct());
            return new RowQuota
            {
                CommodityCode = commodityCodeObject,
                Preference = preference.HasValue() ? preference.ToUpper() == "Y" : false,
                QuotaNumber = quotaNumber,
                Countries = countries,
            };
        }

        async Task Save()
        {
            using (var scope = Database.CreateTransactionScope())
            {
                var rowQuotas = await Database.Of<RowQuota>()
                    .GetList()
                    .Where(x => x.ID.IsNoneOf(RowImportItems.IDs().ToArray()))
                    .ToList();
                await RowImportItems.Do(i => TrySave(i, Indexes[i]));
                await Database.Delete(DeletingItems);
                if (Errors.None())
                {
                    //await Database.Update(rowQuotas, x => x.IsDeactivated = true);
                    await QueueItem.UpdateStatus(ImportStatus.Successful);
                }
                else
                {
                    await Database.Save(Errors);

                    if (SavedItems.None())
                        await QueueItem.UpdateStatus(ImportStatus.Failed);
                    else
                    {
                        var unMatchrowQuota = rowQuotas.Except(SavedItems);
                        //await Database.Update(unMatchrowQuota, x => x.IsDeactivated = true);
                        await QueueItem.UpdateStatus(ImportStatus.PartialSuccess);
                    }

                }

                scope.Complete();
            }
        }

        async Task TrySave(RowQuota rowQuota, int? line = null, List<RowQuota> toBeRemoved = null)
        {
            try
            {
                rowQuota = await Database.Save(rowQuota);
                SavedItems.Add(rowQuota);
            }
            catch (Exception e)
            {
                await AddError(e.Message, line);
            }
        }

        async Task AddError(string details, int? line = null)
        {
            Errors.Add(new ImportError
            {
                LineNumber = line,
                ImportQueueItem = QueueItem,
                ErrorReason = details
            });
        }
    }
}
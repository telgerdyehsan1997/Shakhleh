namespace Domain
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;
    using Olive;
    using Olive.Csv;
    using Olive.Entities;

    public class CommodityCodeImportService
    {
        static IDatabase Database => Context.Current.Database();

        ImportQueueItem QueueItem;
        List<ImportError> Errors;
        IDictionary<IEntity, int> Indexes;
        List<CommodityCode> RowImportItems;
        List<CommodityCode> SavedItems;

        CommodityCodeImportService()
        {
            QueueItem = Database.GetList<ImportQueueItem>(i => i.StatusId == ImportStatus.Pending
                                   && i.TypeId == ImportType.CommodityCode.ID
                                   && i.IsArchive == false)
                .WithMin(p => p.UploadDate).GetAwaiter().GetResult();

            Errors = new List<ImportError>();
            Indexes = new Dictionary<IEntity, int>();
            RowImportItems = new List<CommodityCode>();
            SavedItems = new List<CommodityCode>();
        }

        /// <summary>
        /// A background task will run this method to get and process pending import queue items. 
        /// </summary>
        public static async Task ProcessNext()
        {
            var service = new CommodityCodeImportService();

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
                var queueList = (await Database.GetList<ImportQueueItem>(i => i.TypeId == ImportType.CommodityCode.ID
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
                "Commodity Code (export)",
                "Commodity Code (import)",
                "Second quantity",
                "Third quantity",
                "VAT",
                "Full rate of duty",
                "Specific Rate",
                "MFN full rate",
                "MFN add duty",
                "N851 - PHC",
                "N852 - CED",
                "N853 - CVD",
                "ESA - TRQ",
                "FAR - PREF",
                "FAR - TRQ",
                "GSP - PREF",
                "GSP - TRQ",
                "IND - PREF",
                "IND - TRQ",
                "INO - PREF",
                "INO - TRQ",
                "GS+-PREF",
                "GS+-TRQ",
                "LDC - PREF",
                "LDC - TRQ",
                "ISR - PREF",
                "ISR - TRQ",
                "PAL - PREF",
                "PAL - TRQ",
                "SWI - PREF",
                "SWI - TRQ",
                "LIC99",
                "Control",
                "Quota",
                "Box 44(1)",
                "Box 44(2)",
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

        async Task<CommodityCode> GetImportItem(DataRow row)
        {
            var exportCode = row["Commodity Code (export)"].ToString();
            var importCode = row["Commodity Code (import)"].ToString();
            var secondQuantity = await SecondQuantityDescription.FindByQuantityCode(row["Second quantity"].ToStringOrEmpty());
            if (secondQuantity != null && secondQuantity.IsDeactivated)
                secondQuantity = null;
            var thirdQuantity = await SecondQuantityDescription.FindByQuantityCode(row["Third quantity"].ToStringOrEmpty());
            if (thirdQuantity != null && thirdQuantity.IsDeactivated)
                thirdQuantity = null;

            var vatList = new List<VATType>();

            var vatTypeData = row["VAT"].ToStringOrEmpty();

            if (vatTypeData.HasValue())
            {
                var vats = vatTypeData.Split(',');
                foreach (var vat in vats)
                {
                    var vatType = await VATType.FindByName(vat.TrimOrEmpty());
                    if (vatType == null && row["VAT"].ToStringOrEmpty().HasValue())
                        vatType = await Database.Save(new VATType { Name = row["VAT"].ToStringOrEmpty() });
                    vatList.Add(vatType);
                }
            }



            var rateOfDuty = row["Full rate of duty"].ToString().To<decimal>();
            var specificRate = row["Specific Rate"].ToString();
            var mfnFullRate = row["MFN full rate"].ToString();
            var mfnAddDuty = row["MFN add duty"].ToString();
            var chiPref = row["N851 - PHC"].ToString();
            var chiTrq = row["N852 - CED"].ToString();
            var esaPref = row["N853 - CVD"].ToString();
            var esaTrq = row["ESA - TRQ"].ToString();
            var farPref = row["FAR - PREF"].ToString();
            var farTrq = row["FAR - TRQ"].ToString();
            var gspPref = row["GSP - PREF"].ToString();
            var gspTrq = row["GSP - TRQ"].ToString();
            var indPref = row["IND - PREF"].ToString();
            var indTrq = row["IND - TRQ"].ToString();
            var inoPref = row["INO - PREF"].ToString();
            var inoTrq = row["INO - TRQ"].ToString();
            var gsPref = row["GS+-PREF"].ToString();
            var gsTrq = row["GS+-TRQ"].ToString();
            var ldcPref = row["LDC - PREF"].ToString();
            var lfcTrq = row["LDC - TRQ"].ToString();
            var isrPref = row["ISR - PREF"].ToString();
            var isrTrq = row["ISR - TRQ"].ToString();
            var palPref = row["PAL - PREF"].ToString();
            var palTrq = row["PAL - TRQ"].ToString();
            var swiPref = row["SWI - PREF"].ToString();
            var swiTrq = row["SWI - TRQ"].ToString();
            var lic99 = row["LIC99"].ToString().ToLower();
            var control = row["Control"].ToString().ToLower();

            var eUQuota = row["Quota"].ToString().ToLower();
            var eUQuotaRef = row["EU Quota Pref"].ToString().ToLower();
            var otherQuota = row["Other Quota"].ToString().ToLower();
            var box441 = row["Box 44(1)"].ToString().ToLower();
            var box442 = row["Box 44(2)"].ToString().ToLower();
          //  var dangerousGood = row["Dangerous Goods"].ToString().ToLower();

            var commodityCode = (await CommodityCode.FindByExportCodeAndImportCode(exportCode, importCode))?.Clone() ?? new CommodityCode();

            commodityCode.ExportCode = exportCode;
            commodityCode.ImportCode = importCode;
            commodityCode.SecondQuantity = secondQuantity;
            commodityCode.ThirdQuantity = thirdQuantity;
            commodityCode.FullRateOfDuty = rateOfDuty;
            commodityCode.SpecificRate = specificRate;
            commodityCode.MFNFullRate = mfnFullRate;
            commodityCode.MFNAdditionalDuty = mfnAddDuty;
            commodityCode.N851_PHC = chiPref.ToLower().IsAnyOf("true", "1", "yes", "y");
            commodityCode.N852_CED = chiTrq.ToLower().IsAnyOf("true", "1", "yes", "y");
            commodityCode.N853_CVD = esaPref.ToLower().IsAnyOf("true", "1", "yes", "y");
            commodityCode.ESA_TRQ = esaTrq;
            commodityCode.FAR_PREF = farPref;
            commodityCode.FAR_TRQ = farTrq;
            commodityCode.GSP_PREF = gspPref;
            commodityCode.GSP_TRQ = gspTrq;
            commodityCode.IND_PREF = indPref;
            commodityCode.IND_TRQ = indTrq;
            commodityCode.INO_PREF = inoPref;
            commodityCode.INO_TRQ = inoTrq;
            commodityCode.GS_PREF = gsPref;
            commodityCode.GS_TRQ = gsTrq;
            commodityCode.LDC_PREF = ldcPref;
            commodityCode.LDC_TRQ = lfcTrq;
            commodityCode.ISR_PREF = isrPref;
            commodityCode.ISR_TRQ = isrTrq;
            commodityCode.PAL_PREF = palPref;
            commodityCode.PAL_TRQ = palTrq;
            commodityCode.SWI_PREF = swiPref;
            commodityCode.SWI_TRQ = swiTrq;
            commodityCode.LIC99 = lic99.ToLower().IsAnyOf("true", "1", "yes", "y");
            commodityCode.Control = control.ToLower().IsAnyOf("true", "1", "yes", "y");
           // commodityCode.DangerousGoods = dangerousGood.IsAnyOf("true", "1", "yes", "y");

            commodityCode.EUQuota = eUQuota;
            commodityCode.EUQuotaPref = eUQuotaRef;
            commodityCode.OtherQuota = otherQuota.ToLower().IsAnyOf("true", "1", "yes", "y");
            commodityCode.Box44_1 = box441;
            commodityCode.Box44_2 = box442;
            commodityCode.IsDeactivated = false;
            commodityCode.VatTypesToSave = vatList;
            commodityCode.VATString = vatList.ToString(" | ");

            return commodityCode;
        }

        async Task Save()
        {
            using (var scope = Database.CreateTransactionScope())
            {
                var commodityCode = await Database.Of<CommodityCode>()
                    .Where(x => !x.IsDeactivated)
                    .GetList()
                    .Where(x => x.ID.IsNoneOf(RowImportItems.IDs().ToArray()))
                    .ToList();
                await RowImportItems.Do(i => TrySave(i, Indexes[i], commodityCode));

                if (Errors.None())
                {
                    await Database.Update(commodityCode, x => x.IsDeactivated = true);
                    await QueueItem.UpdateStatus(ImportStatus.Successful);
                }
                else
                {
                    await Database.Save(Errors);

                    if (SavedItems.None())
                        await QueueItem.UpdateStatus(ImportStatus.Failed);
                    else
                    {
                        var unMatchcommodityCode = commodityCode.Except(SavedItems);
                        await Database.Update(unMatchcommodityCode, x => x.IsDeactivated = true);
                        await QueueItem.UpdateStatus(ImportStatus.PartialSuccess);
                    }

                }

                scope.Complete();
            }
        }

        async Task TrySave(CommodityCode item, int? line = null, List<CommodityCode> toBeRemoved = null)
        {
            try
            {
                var vatsToSave = item.VatTypesToSave;
                item = await Database.Save(item);

                // Remove unselected Teams
                await Database.Delete((await item.MultipleVATLinks.GetList()).Where(x => vatsToSave.Lacks(x.Vattype)).ToList());

                // Save newly selected ones
                await vatsToSave.Except((await item.MultipleVATLinks.GetList()).Select(x => x.Vattype)).ToList()
                    .Do(x => Database.Save(new CommodityCodeMultipleVATLink { Commoditycode = item, Vattype = x }));

                SavedItems.Add(item);
            }
            catch (Exception e)
            {
                var notToDelete = toBeRemoved.FirstOrDefault(c => c.ExportCode == item.ExportCode && c.ImportCode == item.ImportCode);

                if (notToDelete != null)
                    toBeRemoved.Remove(notToDelete);

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
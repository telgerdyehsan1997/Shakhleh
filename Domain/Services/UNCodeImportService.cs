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

    public class UNCodeImportService
    {
        static IDatabase Database => Context.Current.Database();

        readonly ImportQueueItem QueueItem;
        readonly List<ImportError> Errors;
        readonly IDictionary<IEntity, int> Indexes;
        readonly List<UNCode> RowImportItems;
        readonly List<UNCode> SavedItems;

        UNCodeImportService()
        {
            QueueItem = Database.GetList<ImportQueueItem>(i => i.StatusId == ImportStatus.Pending
                                   && i.TypeId == ImportType.UnCodes.ID
                                   && i.IsArchive == false)
                .WithMin(p => p.UploadDate).GetAwaiter().GetResult();

            Errors = new List<ImportError>();
            Indexes = new Dictionary<IEntity, int>();
            RowImportItems = new List<UNCode>();
            SavedItems = new List<UNCode>();
        }

        /// <summary>
        /// A background task will run this method to get and process pending import queue items. 
        /// </summary>
        public static async Task ProcessNext()
        {
            var service = new UNCodeImportService();

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
                var queueList = (await Database.GetList<ImportQueueItem>(i => i.TypeId == ImportType.UnCodes.ID
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
            var columnNames = columns.Cast<DataColumn>().Select(x => x.ColumnName.ToUpper().Trim().Replace(" ", "")).ToArray();

            var names = new List<string> {
                "UN_No",
                "Name_and_description",
                "Nom_et_description",
                "Class",
                "Classification_code",
                "Packing_group",
                "Labels",
                "Special_provisions",
                "Limited_and_excepted_quantities_3.4",
                "Limited_and_excepted_quantities_3.5",
                "Packing_instructions",
                "Special_packing_provisions",
                "Mixed_packing_provisions",
                "Instructions",
                "Tank_code",
                "Vehicle_for_tank_carriage",
                "Transport_category",
                "Packages",
                "Bulk",
                "Loading_unloading_and_handling",
                "Operation",
                "Hazard_identification_No",
            };
            return names.Select(x => x.ToUpper().Trim().Replace(" ", "")).Except(x => columnNames.Contains(x.ToUpper().Trim().Replace(" ", ""))).ToList().ToString(", ");
        }

        async Task ImportRow(DataRow row, int index)
        {
            try
            {
                var item = await GetImportItem(row);
                if (item!=null)
                {
                    RowImportItems.Add(item);
                    Indexes[item] = index;
                }
            }
            catch (Exception ex)
            {
                await AddError(ex.Message, index);
            }
        }

        async Task<UNCode> GetImportItem(DataRow row)
        {
            var unNo = row["UN_No"].ToString();
            var nameAndDescription = row["Name_and_description"].ToString();
            var nomEtDescription = row["Nom_et_description"].ToString();
            var className = row["Class"].ToString();
            var classificationCode = row["Classification_code"].ToString();
            var packingGroup = row["Packing_group"].ToString();
            var labels = row["Labels"].ToString();
            var specialProvisions = row["Special_provisions"].ToString();
            var limitedAndExceptedQuantities34 = row["Limited_and_excepted_quantities_3.4"].ToString();
            var limitedAndExceptedQuantities35 = row["Limited_and_excepted_quantities_3.5"].ToString();
            var packingInstructions = row["Packing_instructions"].ToString();
            var specialPackingProvisions = row["Special_packing_provisions"].ToString();
            var mixedPackingProvisions = row["Mixed_packing_provisions"].ToString();
            var instructions = row["Instructions"].ToString();
            var tankCode = row["Tank_code"].ToString();
            var vehicleForTankCarriage = row["Vehicle_for_tank_carriage"].ToString();
            var transportCategoryTunnelRestrictionCode = row["Transport_category"].ToString();
            var packages = row["Packages"].ToString();
            var bulk = row["Bulk"].ToString();
            var loadingUnloadingAndHandling = row["Loading_unloading_and_handling"].ToString();
            var hazardIdentificationNo = row["Hazard_identification_No"].ToString();

            if (unNo.Length != 4 && int.TryParse(unNo, out _))
            {
                await AddError($"UN Number {unNo} must have 4 digit numeric value");
                return new UNCode();
            }

            var uncode = (await UNCode.FindByUnNumber(unNo))?.Clone() ?? new UNCode();

            uncode.UNNo = unNo;
            uncode.NameAndDescription = nameAndDescription;
            uncode.NomEtDescription = nomEtDescription;
            uncode.Class = className;
            uncode.ClassificationCode = classificationCode;
            uncode.PackingGroup = packingGroup;
            uncode.Labels = labels;
            uncode.SpecialProvisions = specialProvisions;
            uncode.LimitedAndExceptedQuantities3_4 = limitedAndExceptedQuantities34;
            uncode.LimitedAndExceptedQuantities3_5 = limitedAndExceptedQuantities35;
            uncode.PackingInstructions = packingInstructions;
            uncode.SpecialPackingProvisions = specialPackingProvisions;
            uncode.MixedPackingProvisions = mixedPackingProvisions;
            uncode.Instructions = instructions;
            uncode.TankCode = tankCode;
            uncode.VehicleForTankCarriage = vehicleForTankCarriage;
            uncode.TransportCategory_TunnelRestrictionCode = transportCategoryTunnelRestrictionCode;
            uncode.Packages = packages;
            uncode.Bulk = bulk;
            uncode.Loading_UnloadingAndHandling = loadingUnloadingAndHandling;
            uncode.HazardIdentificationNo = hazardIdentificationNo;

            return uncode;
        }

        async Task Save()
        {
            using (var scope = Database.CreateTransactionScope())
            {
                var uncode = await Database.Of<UNCode>()
                    .Where(x => !x.IsDeactivated)
                    .GetList()
                    .Where(x => x.ID.IsNoneOf(RowImportItems.IDs().ToArray()))
                    .ToList();
                await RowImportItems.Do(i => TrySave(i, Indexes[i], uncode));

                if (Errors.None())
                {
                    await Database.Update(uncode, x => x.IsDeactivated = true);
                    await QueueItem.UpdateStatus(ImportStatus.Successful);
                }
                else
                {
                    await Database.Save(Errors);

                    if (SavedItems.None())
                        await QueueItem.UpdateStatus(ImportStatus.Failed);
                    else
                    {
                        var unMatchUnCode = uncode.Except(SavedItems);
                        await Database.Update(unMatchUnCode, x => x.IsDeactivated = true);
                        await QueueItem.UpdateStatus(ImportStatus.PartialSuccess);
                    }

                }

                scope.Complete();
            }
        }

        async Task TrySave(UNCode item, int? line = null, List<UNCode> toBeRemoved = null)
        {
            try
            {
                item = await Database.Save(item);
                SavedItems.Add(item);
            }
            catch (Exception e)
            {
                var notToDelete = toBeRemoved.FirstOrDefault(c => c.UNNo == item.UNNo);

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
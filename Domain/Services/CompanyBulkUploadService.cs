namespace Domain
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Threading.Tasks;
    using Olive;
    using Olive.Csv;
    using Olive.Entities;

    public class CompanyBulkUploadService
    {
        static IDatabase Database => Context.Current.Database();

        ImportQueueItem QueueItem;
        List<ImportError> Errors;
        IDictionary<IEntity, int> Indexes;
        List<Company> RowImportItems;
        List<Company> SavedItems;

        CompanyBulkUploadService()
        {
            QueueItem = Database.GetList<ImportQueueItem>(i => i.StatusId == ImportStatus.Pending && i.TypeId == ImportType.Company.ID).WithMin(p => p.UploadDate).GetAwaiter().GetResult();
            Errors = new List<ImportError>();
            Indexes = new Dictionary<IEntity, int>();
            RowImportItems = new List<Company>();
            SavedItems = new List<Company>();
        }

        /// <summary>
        /// A background task will run this method to get and process pending import queue items. 
        /// </summary>
        public static async Task ProcessNext()
        {
            var service = new CompanyBulkUploadService();

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

            await Save();
        }

        async Task ImportItems()
        {
            try
            {
                var rows = CsvReader.Read(await QueueItem.File.GetContentTextAsync(), isFirstRowHeaders: true).GetRows();

                await rows.DoAsync(async (row, index) => await ImportRow(row, index + 2));
            }
            catch (Exception ex)
            {
                await AddError($"Could not process item. {ex.Message}");
            }
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

        async Task<Company> GetImportItem(DataRow row)
        {
            var type = await CompanyType.FindByName(row["Type"].ToString());
            var accountNumber = row["Customer account number"].ToString();
            var companyName = row["Company name"].ToString();
            //var country = await Country.FindByCode(row["Country"].ToString());  // this needs to change..
            var country = await Database.FirstOrDefault<Country>(c => c.Code == row["Country"].ToString());
            var postcode = row["Post code"].ToString();
            var addressLine1 = row["Address line 1"].ToString();
            var addressLine2 = row["Address line 2"].ToString();
            var town = row["Town/City"].ToString();
            var eori = row["EORI number"].ToString();
            var branchIdentifier = row["Branch identifier"].ToString();
            var aeoNumber = row["AEO number"].ToString();
            var tsp = row["TSP"].ToString();
            var cfsp = await CFSPType.Parse(row["CFSP"].ToString());
            var paymentCode = await PaymentType.FindByCode(row["Payment code"].ToString());
            var defermentNumber = row["Deferment number"].ToString();
            var representationType = row["Representation type"].ToString();
            var guaranteeNumber = row["Guarantee number"].ToString();
            var guaranteeType = row["Guarantee type"].ToString();
            var tin = row["TIN"].ToString();
            var pin = row["PIN"].ToString();

            if (Company.FindByDefermentNumberAndEORINumberAndNameAndPostcodeAndTown(defermentNumber, eori, companyName, postcode, town) != null)
            {
                return new Company
                {
                    Type = type,
                    CustomerAccountNumber = accountNumber,
                    Name = companyName,
                    Country = country,
                    Postcode = postcode,
                    AddressLine1 = addressLine1,
                    AddressLine2 = addressLine2,
                    Town = town,
                    EORINumber = eori,
                    BranchIdentifier = branchIdentifier,
                    AEONumber = aeoNumber,
                    TSP = tsp,
                    //CFSP = cfsp.To<bool>(),
                    PaymentType = paymentCode,
                    DefermentNumber = defermentNumber,
                    RepresentationType = representationType.To<bool>(),
                    GuaranteeNumber = guaranteeNumber,
                    GuaranteeType = guaranteeType,
                    TIN = tin,
                    PIN = pin,
                    IsCreatedFromAPI = false,
                    CFSPType = cfsp
                };
            }

            else return new Company();


        }

        async Task Save()
        {
            using (var scope = Database.CreateTransactionScope())
            {
                await RowImportItems.Do(i => TrySave(i, Indexes[i]));

                if (Errors.None())
                    await QueueItem.UpdateStatus(ImportStatus.Successful);

                else
                {
                    await Database.Save(Errors);

                    if (SavedItems.None())
                        await QueueItem.UpdateStatus(ImportStatus.Failed);

                    else
                        await QueueItem.UpdateStatus(ImportStatus.PartialSuccess);
                }

                scope.Complete();
            }
        }

        async Task TrySave(Company item, int? line = null)
        {
            try
            {
                await Database.Save(item);

                SavedItems.Add(item);
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

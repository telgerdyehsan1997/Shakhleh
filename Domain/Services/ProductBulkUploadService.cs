namespace Domain
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Threading.Tasks;
    using Olive;
    using Olive.Csv;
    using Olive.Entities;

    public class ProductBulkUploadService
    {
        static IDatabase Database => Context.Current.Database();

        ImportQueueItem QueueItem;
        List<ImportError> Errors;
        IDictionary<IEntity, int> Indexes;
        List<Product> RowImportItems;
        List<Product> SavedItems;

        ProductBulkUploadService()
        {
            QueueItem = Database.GetList<ImportQueueItem>(i => i.StatusId == ImportStatus.Pending && i.TypeId == ImportType.Product.ID).WithMin(p => p.UploadDate).GetAwaiter().GetResult();
            Errors = new List<ImportError>();
            Indexes = new Dictionary<IEntity, int>();
            RowImportItems = new List<Product>();
            SavedItems = new List<Product>();
        }

        /// <summary>
        /// A background task will run this method to get and process pending import queue items. 
        /// </summary>
        public static async Task ProcessNext()
        {
            var service = new ProductBulkUploadService();

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

                await rows.DoAsync(async (row, index) => await ImportRow(row, index + 2, QueueItem.Company));
            }
            catch (Exception ex)
            {
                await AddError($"Could not process item. {ex.Message}");
            }
        }

        async Task ImportRow(DataRow row, int index, Company company)
        {
            try
            {
                var item = await GetImportItem(row, company);

                RowImportItems.Add(item);
                Indexes[item] = index;
            }
            catch (Exception ex)
            {
                await AddError(ex.Message, index);
            }
        }

        async Task<Product> GetImportItem(DataRow row, Company company)
        {
            var productCode = row["Product code"].ToString();
            var productName = row["Product name"].ToString();
            var commodityCode = await CommodityCode.FindByExportCodeAndImportCode(row["Commodity code export"].ToString(), row["Commodity code import"].ToString());
            var additionalCode = row["Additional code"].ToString();
            var quota = row["Quota"].ToString();
            var vat = await VATType.FindByName(row["VAT"].ToString());
            var licenced = row["Licenced"].ToString().To<bool>();
            var exportLicence = row["Export licence"].ToString();

            var product = await Product.FindByCompanyAndCode(company, productCode);
            if (product == null)
            {
                return new Product
                {
                    Code = productCode,
                    Name = productName,
                    Company = company,
                    CommodityCode = commodityCode,
                    AdditionalCode = additionalCode,
                    Quota = quota,
                    VAT = vat,
                    Licenced = licenced,
                    ExportLicence = exportLicence,
                    IsCreatedFromAPI = false,
                };
            }
            else
            {
                var myProduct = await Database.Reload(product);
                await Database.Update(myProduct, x =>
                {
                    x.Name = productName;
                    x.CommodityCode = commodityCode;
                    x.AdditionalCode = additionalCode;
                    x.Quota = quota;
                    x.VAT = vat;
                    x.Licenced = licenced;
                    x.ExportLicence = exportLicence;
                    x.IsCreatedFromAPI = false;
                });
                return (await Database.Reload(myProduct)).Clone();
            }

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

        async Task TrySave(Product item, int? line = null)
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

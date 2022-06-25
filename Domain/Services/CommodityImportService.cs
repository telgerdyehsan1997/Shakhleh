using Olive;
using Olive.Csv;
using Olive.Entities;
using Olive.Entities.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{

    public interface ICommodityImportService
    {
        Task<bool> ProcessImport(ImportQueueItem item);
    }

    public class CommodityImportService : ICommodityImportService
    {
        static IDatabase Database => Context.Current.Database();

        ImportQueueItem QueueItem;
        List<ImportError> Errors;
        IDictionary<IEntity, int> Indexes;
        List<Commodity> RowImportItems;
        List<Commodity> SavedItems;

        public CommodityImportService()
        {
            Errors = new List<ImportError>();
            Indexes = new Dictionary<IEntity, int>();
            RowImportItems = new List<Commodity>();
            SavedItems = new List<Commodity>();
        }

        public async Task<bool> ProcessImport(ImportQueueItem item)
        {
            if (item.TypeId != ImportType.Commodity.ID)
                return false;
            this.QueueItem = item;

            if (await UpdateToProcessing() == false) return false;

            return await Process();
        }

        async Task<bool> UpdateToProcessing()
        {
            if (QueueItem == null) return false;

            await QueueItem.UpdateStatus(ImportStatus.Processing);

            return true;
        }

        async Task<bool> Process()
        {
            await ImportItems();

            return await Save();
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
                "Product Code",
                "Gross Weight",
                "Net Weight",
                "Second Quantity",
                "Third Quantity",
                "Value",
                "Number of packages for this commodity code (If known)",
                "Country Code",
                "Country Name",
                "Preference",
                "Preference Type",
                "Preference Certificate Number",
                "Are Goods Licencable",
                "Licence Name",
                "Licence Number",
                "Licence Status Code",
                "Quantity",
                "RPTID Code"
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

        async Task<Commodity> GetImportItem(DataRow row)
        {

            var product = await Product.FindByCompanyAndCode(this.QueueItem.Company, row["Product Code"].ToString().ToUpper());
            var grossWeight = row["Gross Weight"].ToString().To<double>();
            var netWeight = row["Net Weight"].ToString().To<double>();
            var secondQuantity = row["Second Quantity"].ToString().To<double>();
            var thirdQuantity = row["Third Quantity"].ToString().To<double>();
            var value = row["Value"].ToString().To<decimal>();
            var numberOfPackages = row["Number of packages for this commodity code (If known)"].ToString().To<int>();
            var country = await Country.FindByCodeAndName(row["Country Code"].ToString(), row["Country Name"].ToString());
            var hasPreference = row["Preference"].ToString().To<bool>();
            var preferenceType = await PreferenceType.FindByName(row["Preference Type"].ToString());
            var preferenceCertNumber = row["Preference Certificate Number"].ToString();
            var areGoodLicencable = row["Are Goods Licencable"].ToString().To<bool>();
            var licenceName = await Database.Of<Licence>()
                     .Where(x => x.LicenceName == row["Licence Name"].ToString())
                     .FirstOrDefault();
            var licenceNumber = row["Licence Number"].ToString();

            var licenceStatusCode = await Database.Of<LicenceStatusCode>()
                    .Where(x => x.StatusCode == row["Licence Status Code"].ToString())
                    .FirstOrDefault();

            var quantity = row["Quantity"].ToString().To<int>();
            var rptid = row["RPTID Code"].ToString();

            var commodity = new Commodity
            {
                Product = product,
                GrossWeight = grossWeight,
                NetWeight = netWeight,
                Value = value,
                NumberOfPackages = numberOfPackages,
                CountryOfDestination = country,
                HasPreference = hasPreference,
                GoodsLicencable = areGoodLicencable,
                RPTIDCode = rptid,
                Consignment = this.QueueItem.Consignment
            };

            if (product?.CommodityCode.SecondQuantity != null)
                commodity.SecondQuantity = secondQuantity;

            if (product?.CommodityCode.ThirdQuantity != null)
                commodity.ThirdQuantity = thirdQuantity;

            if (hasPreference)
            {
                commodity.PreferenceType = preferenceType;
                commodity.PreferenceCertificateNumber = preferenceCertNumber;
            }
            if (areGoodLicencable)
            {
                commodity.LicenceStatusCode = licenceStatusCode;
                commodity.LicenceType = licenceName;
                commodity.LicenceNumber = licenceNumber;
                if (licenceName.Quantity == true)
                    commodity.Quantity = quantity;
            }

            return commodity;
        }

        async Task<bool> Save()
        {
            using (var scope = Database.CreateTransactionScope())
            {
                await RowImportItems.Do(i => TrySave(i, Indexes[i]));

                if (Errors.None())
                {
                    await QueueItem.UpdateStatus(ImportStatus.Successful);
                }
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

            return Errors.None();
        }


        async Task TrySave(Commodity item, int? line = null)
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

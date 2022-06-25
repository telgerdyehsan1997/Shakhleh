using Olive;
using Olive.Csv;
using Olive.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class AuthorisedLocationsImportService
    {
        static IDatabase Database => Context.Current.Database();

        ImportQueueItem QueueItem;
        List<ImportError> Errors;
        IDictionary<IEntity, int> Indexes;
        List<AuthorisedLocation> RowImportItems;
        List<AuthorisedLocation> SavedItems;

        public AuthorisedLocationsImportService()
        {

            QueueItem = Task.Factory.RunSync(() => Database.GetList<ImportQueueItem>(i => i.StatusId == ImportStatus.Pending && i.TypeId == ImportType.AuthorisedLocation.ID).WithMin(p => p.UploadDate));
            Errors = new List<ImportError>();
            Indexes = new Dictionary<IEntity, int>();
            RowImportItems = new List<AuthorisedLocation>();
            SavedItems = new List<AuthorisedLocation>();
        }

        /// <summary>
        /// A background task will run this method to get and process pending import queue items. 
        /// </summary>
        public static async Task ProcessNext()
        {
            var service = new AuthorisedLocationsImportService();

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
                "Location name",
                "Customs identity",
                "NCTS code",
                "Authorisation number",
                "Email Addresses"
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
        async Task<AuthorisedLocation> GetImportItem(DataRow row)
        {

            var locationName = row["Location name"].ToString().ToUpper();
            var customsIdentity = row["Customs identity"].ToString().ToUpper();
            var nCTSCode = row["NCTS code"].ToString().ToUpper();
            var authorisationNumber = row["Authorisation number"].ToString().ToUpper();
            var email = row["Email Addresses"].ToString().ToUpper();
            var emails = email.Split(",");
            if (email.Contains("|"))
                throw new ValidationException("Please seperate emails using a ','.");
            if (emails.Any(e => !e.Contains("@")))
                throw new ValidationException("One or more emails isn't valid.");

            var authorisedLocation = (await AuthorisedLocation.FindByCustomsIdentity(customsIdentity))?.Clone() ?? new AuthorisedLocation();
            var transitOffice = await TransitOffice.FindByNCTSCode(nCTSCode);
            if (transitOffice == null)
                throw new ValidationException("No office of transit with given NCTS code found.");

            if (authorisedLocation.LocationName == locationName
                && authorisedLocation.CustomsIdentity == customsIdentity
                && authorisedLocation.TransitOfficeId == transitOffice
                && authorisedLocation.AuthorisationNumber == authorisationNumber
                && authorisedLocation.EmailAddresses == email.Remove(" "))
                throw new ValidationException("Duplicate entry.");

            authorisedLocation.LocationName = locationName;
            authorisedLocation.CustomsIdentity = customsIdentity;
            authorisedLocation.TransitOffice = transitOffice;
            authorisedLocation.AuthorisationNumber = authorisationNumber;
            authorisedLocation.EmailAddresses = email;
            return authorisedLocation;
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

        async Task TrySave(AuthorisedLocation item, int? line = null)
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

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

    public class RouteIterneriesImportService
    {
        static IDatabase Database => Context.Current.Database();

        readonly ImportQueueItem QueueItem;
        readonly List<ImportError> Errors;
        readonly IDictionary<IEntity, int> Indexes;
        readonly List<RouteItinerary> RowImportItems;
        readonly List<RouteItinerary> SavedItemsRouteItinerary;
        RouteIterneriesImportService()
        {
            QueueItem = Database.GetList<ImportQueueItem>(i => i.StatusId == ImportStatus.Pending
                                   && i.TypeId == ImportType.Itinerary
                                   && i.IsArchive == false)
                .WithMin(p => p.UploadDate).GetAwaiter().GetResult();

            Errors = new List<ImportError>();
            Indexes = new Dictionary<IEntity, int>();
            RowImportItems = new List<RouteItinerary>();
            SavedItemsRouteItinerary = new List<RouteItinerary>();
        }

        /// <summary>
        /// A background task will run this method to get and process pending import queue items. 
        /// </summary>
        public static async Task ProcessNext()
        {
            var service = new RouteIterneriesImportService();

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
                var queueList = (await Database.GetList<ImportQueueItem>(i => i.TypeId == ImportType.Itinerary
                                   && i.IsArchive == false && i.StatusId != ImportStatus.Failed).OrderBy(x => x.UploadDate));

                if (queueList.Skip(1).Any())
                {
                    await Database.Update(queueList.FirstOrDefault(), x => x.IsArchive = true);
                }
            }
            catch (Exception ex)
            {
                Log.For<DataRow>().Error(ex);
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
                Log.For<DataRow>().Error(ex);
                await AddError($"Could not process item. {ex.Message}");
            }
        }

        async Task<string> ValidateColumns(DataColumnCollection columns)
        {
            var columnNames = columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

            var names = new List<string> {
                "UK PORT",
                "NON UK PORT",
                "Destination",
                "Countries",
                "IsDefault"
            };

            return names.Except(x => columnNames.Contains(x)).ToList().ToString(", ");
        }

        async Task ImportRow(DataRow row, int index)
        {
            try
            {
                var item = await GetImportItem(row);
                if (item != null)
                {
                    RowImportItems.Add(item);
                    Indexes[item] = index;
                }
            }
            catch (Exception ex)
            {
                Log.For<DataRow>().Error(ex);
                await AddError(ex.Message, index);
            }
        }

        async Task<RouteItinerary> GetImportItem(DataRow row)
        {
            var isDefault = (row["IsDefault"].ToStringOrEmpty()).IsAnyOf("y", "true", "yes");

            var ukPort = await Port.GetPortByName(row["UK PORT"].ToString().ToUpper(), isUk: true);
            var nonUkport = await Port.GetPortByName(row["NON UK PORT"].ToString().ToUpper(), isUk: false);

            var destination = await Country.GetCountryByName(row["Destination"].ToString());
            var route = await Route.FindByNon_UKPortAndUKPort(nonUkport, ukPort);

            var countries = row["Countries"].ToStringOrEmpty().Split(",").ToList();


            if (ukPort == null || nonUkport == null)
            {
                await AddError("UK Port or Non UK port is missing");
                return null;
            }

            else if (route == null)
            {
                await AddError($"Route not found with UK Port: {ukPort?.PortCode} and Non UK Port: {nonUkport?.PortCode}");
                return null;
            }
            else if (destination == null)
            {
                await AddError($"Destination Country: {row["Destination"]} does not exists in system.");
                return null;
            }

            if (isDefault)
            {
                return new RouteItinerary
                {
                    UKCountry = ukPort.Country,
                    NonUKCountry = nonUkport.Country,
                    DestinationCountry = destination,
                    Route = route,
                    ImportCountries = countries,
                    HasDefault = true
                };
            }
            else
            {
                if (await RouteItinerary.FindByDesinationCountry(route, destination) != null)
                {
                    var routeItenary = (await RouteItinerary.FindByDesinationCountry(route, destination)).Clone();
                    routeItenary.ImportCountries = countries;
                    return routeItenary;
                }
                else
                {
                    return new RouteItinerary
                    {
                        UKCountry = ukPort.Country,
                        NonUKCountry = nonUkport.Country,
                        DestinationCountry = destination,
                        Route = route,
                        ImportCountries = countries
                    };
                }

            }
        }
        async Task<List<RouteItineraryCountry>> GetImportRouteItineraryCountryItem(RouteItinerary routeItinerary)
        {
            try
            {
                var routeItinerarycountries = new List<RouteItineraryCountry>();

                foreach (var country in routeItinerary.ImportCountries)
                {
                    if (country.HasValue())
                    {
                        var countryBycode = await Country.GetCountry(country);
                        if (countryBycode != null)
                        {
                            routeItinerarycountries.Add(new RouteItineraryCountry
                            {
                                Country = countryBycode,
                                RouteItinerary = routeItinerary,
                                Route = routeItinerary.Route
                            });
                        }
                    }
                }
                return routeItinerarycountries;
            }
            catch (Exception ex)
            {
                Log.For<RouteItineraryCountry>().Error(ex);
                throw;
            }


        }

        async Task Save()
        {
            try
            {
                using (var scope = Database.CreateTransactionScope())
                {
                    await RowImportItems.Do(i => TrySaveRouteItinerary(i, Indexes[i]));

                    if (Errors.None())
                    {
                        await QueueItem.UpdateStatus(ImportStatus.Successful);
                    }
                    else
                    {
                        await Database.Save(Errors);

                        if (SavedItemsRouteItinerary.None())
                            await QueueItem.UpdateStatus(ImportStatus.Failed);
                        else
                        {
                            await QueueItem.UpdateStatus(ImportStatus.PartialSuccess);
                        }
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                Log.For<RouteItinerary>().Error(ex);
                throw;
            }

        }

        async Task TrySaveRouteItinerary(RouteItinerary routeItinerary, int? line = null, List<RouteItinerary> toBeRemoved = null)
        {
            try
            {
                using (var scope = Database.CreateTransactionScope())
                {
                    var item = await Database.Save(routeItinerary);
                    if (item != null)
                    {
                        await Database.Delete(await item.RouteItineraryCountry.GetList());
                        item.ImportCountries = routeItinerary.ImportCountries;
                        var data = await GetImportRouteItineraryCountryItem(item);
                        await Database.Save(data);
                        SavedItemsRouteItinerary.Add(item);
                    }
                    scope.Complete();
                }

            }
            catch (Exception e)
            {
                var notToDelete = toBeRemoved?.FirstOrDefault(c => c.NonUKCountryId == routeItinerary.NonUKCountryId && c.UKCountryId == routeItinerary.UKCountryId);

                if (notToDelete != null)
                    toBeRemoved.Remove(notToDelete);

                Log.For<RouteItinerary>().Error(e);

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
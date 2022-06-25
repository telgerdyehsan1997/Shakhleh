using Domain;
using Microsoft.AspNetCore.Mvc;
using Olive;
using Olive.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{

    public interface IStatusManagementService
    {
        Task AutoClearConsignments();
    }
    public class StatusManagementService : IStatusManagementService
    {
        IDatabase Database;
        public StatusManagementService(IDatabase database)
        {
            Database = database;
        }

        public async Task AutoClearConsignments()
        {

            var timeToMinus = Settings.Current.TimeUntilCleared ?? 0;
            var consignments = await Database.Of<Consignment>()
                .Where(t => t.LastStatusUpdate <= LocalTime.Now.AddMinutes(-timeToMinus))
                .Where(t => t.ProgressId == Progress.Arrived && t.Shipment.TypeId == ShipmentType.IntoUk)
                .GetList()
                .ToList();

            foreach (var item in consignments)
            {
                //#140011: Background Task error - AutoCleared
                //Olive.Entities.ValidationException: Customer Reference may only contain alphanumeric characters.
                try
                {
                    //if (item.Shipment.IsOutUK)
                    //    continue;

                    await item.FlagAsCleared();
                }
                catch (ValidationException ex)
                {
                    Log.For(this).Error(ex);
                    continue;
                }
                catch (Exception ex)
                {
                    Log.For(this).Error(ex);
                    throw;
                }
            }
        }
    }
}

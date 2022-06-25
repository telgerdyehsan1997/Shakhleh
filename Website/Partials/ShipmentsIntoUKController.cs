using Domain;
using Olive;
using Olive.Entities;
using Olive.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vm = ViewModel;

namespace Controllers
{
    partial class ShipmentsIntoUKController
    {
        Olive.DatabaseFilters<Shipment> DatabaseFilters;
        public async Task<IEnumerable<Shipment>> FilterShipments(vm.CustomerShipmentInList info, Company company)
        {
            DatabaseFilters = new DatabaseFilters<Shipment>();
            DatabaseFilters.Add(x => x.CompanyId == company);
            DatabaseFilters.Add(x => x.TypeId == ShipmentType.IntoUk);

            if (Request.Query["Id"].ToStringOrEmpty().HasValue())
            {
                var parameters = await Database.Of<ConsignmentSearch>().Where(x => x.ID == new Guid(Request.Query["Id"])).FirstOrDefault();
                if (parameters != null)
                {
                    var shipmentIds = await Shipment.ShipmentList(parameters);
                    DatabaseFilters.Add(x => x.ID.IsAnyOf(shipmentIds));
                }
            }

            if (info.IsDeactivated.HasValue)
                DatabaseFilters.Add(x => x.IsDeactivated == info.IsDeactivated);

            if (info.ExpectedDate.HasValue)
                DatabaseFilters.Add(x => x.ExpectedDate >= info.ExpectedDate);

            if (info.ExpectedDateMax.HasValue)
                DatabaseFilters.Add(x => x.ExpectedDate <= info.ExpectedDateMax.EndOfDay());

            if (info.Date.HasValue)
                DatabaseFilters.Add(x => x.Date >= info.Date);

            if (info.DateMax.HasValue)
                DatabaseFilters.Add(x => x.Date <= info.DateMax.EndOfDay());

            if (info.Progress.Any())
                DatabaseFilters.Add(x => x.ProgressId.IsAnyOf(info.Progress));

            if (info.MyReferenceForCPInvoice.HasValue())
            {
                if (info.MyReferenceForCPInvoice.Length > 8)
                    info.MyReferenceForCPInvoice = info.MyReferenceForCPInvoice.Substring(0, info.MyReferenceForCPInvoice.Length - 5);
                DatabaseFilters.Add(s => s.MyReferenceForCPInvoice.Contains(info.MyReferenceForCPInvoice.Trim(), false));
            }

            if (info.VehicleNumber.HasValue())
                DatabaseFilters.Add(s => s.VehicleNumber.Contains(info.VehicleNumber.Trim(), false));

            if (info.TrailerNumber.HasValue())
                DatabaseFilters.Add(s => s.TrailerNumber.Contains(info.TrailerNumber.Trim(), false));

            if (info.IsNCTSShipmentOutConvertible != null)
                DatabaseFilters.Add(s => s.IsNCTSShipmentOutConvertible == info.IsNCTSShipmentOutConvertible);

            const int maxTrackingLength = 11;

            if (info.TrackingNumber.HasValue())
            {
                var trackingNumber = info.TrackingNumber.Trim();
                if (trackingNumber.Length > maxTrackingLength)
                {
                    if (trackingNumber.EndsWith("01"))
                        trackingNumber = trackingNumber.TrimEnd("01");
                    if (trackingNumber.EndsWith("02"))
                        trackingNumber = trackingNumber.TrimEnd("02");
                }

                DatabaseFilters.Add(n => n.TrackingNumber == trackingNumber);
            }
            var query = Database.Of<Shipment>().Where(DatabaseFilters).OrderBy(item => item.TrackingNumber);

            //info.Paging.TotalItemsCount = await query.Count();
            //query = query.Sort(info.Sort).Page(info.Paging);

            return await query.GetList();
        }
    }
}

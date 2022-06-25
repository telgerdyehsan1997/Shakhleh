using MSharp;

namespace Share.Shipment
{
    class ShipmentViewPage : RootPage
    {
        public ShipmentViewPage()
        {
            BrowserTitle("Shipments > View");
            Roles(AppRole.Admin, AppRole.Customer);
            Add<Modules.ShipmentDetailsView>();
            Add<Modules.ShipmentConsignmentsViewList>();
        }
    }
}
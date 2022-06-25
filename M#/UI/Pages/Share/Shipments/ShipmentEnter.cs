using MSharp;

namespace Share.Shipment
{
    class ShipmentEnterPage : RootPage
    {
        public ShipmentEnterPage()
        {
            BrowserTitle("Shipments > Enter");
            Roles(AppRole.Admin, AppRole.Customer);
            Add<Modules.ShipmentForm>();
        }
    }
}
using MSharp;

namespace Share.Shipment
{
    class ShipmentArchiveConfirmationPage : RootPage
    {
        public ShipmentArchiveConfirmationPage()
        {
            BrowserTitle("Shipments > Enter");
            Roles(AppRole.Admin, AppRole.Customer);
            Layout(Layouts.FrontEndModal);
            Add<Modules.ShipmentArchiveConfirmation>();
        }
    }
}
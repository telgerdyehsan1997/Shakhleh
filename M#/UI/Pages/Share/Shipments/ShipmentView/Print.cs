using MSharp;

namespace Share.Shipment.ShipmentView
{
    class PrintPage : SubPage<ShipmentViewPage>
    {
        public PrintPage()
        {
            BrowserTitle(cs("shipementDetailsPrintView.Shipment.TrackingNumber.ToString()"));

            Layout(Layouts.Print);
            Add<Modules.ShipementDetailsPrintView>();
            Add<Modules.ShipmentConsignmentPrintList>();
        }
    }
}
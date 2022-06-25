using MSharp;

namespace Share.Shipment
{
    class ConsignmentSearchEnterPage : RootPage
    {
        public ConsignmentSearchEnterPage()
        {
            BrowserTitle("Shipments > Consignments > Search");
            Roles(AppRole.Admin, AppRole.Customer);
            Layout(Layouts.FrontEndModal);
            Add<Modules.ConsignmentSearchForm>();
        }
    }
}
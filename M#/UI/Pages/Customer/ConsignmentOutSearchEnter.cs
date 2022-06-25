using MSharp;

namespace Customer
{
    class ConsignmentOutSearchEnterPage : RootPage
    {
        public ConsignmentOutSearchEnterPage()
        {
            BrowserTitle("Shipments > Consignments > Search");
            Roles(AppRole.Admin, AppRole.Customer);
            Layout(Layouts.FrontEndModal);
            Add<Modules.ConsignmentOutSearchForm>();
        }
    }
}
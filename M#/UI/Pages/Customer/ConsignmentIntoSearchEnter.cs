using MSharp;

namespace Customer
{
    class ConsignmentIntoSearchEnterPage : RootPage
    {
        public ConsignmentIntoSearchEnterPage()
        {
            BrowserTitle("Shipments > Consignments > Search");
            Roles(AppRole.Admin, AppRole.Customer);
            Layout(Layouts.FrontEndModal);
            Add<Modules.ConsignmentInSearchForm>();
        }
    }
}
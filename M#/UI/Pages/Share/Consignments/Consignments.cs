using MSharp;

namespace Share.Consignments
{
    class ConsignmentsPage : RootPage
    {
        public ConsignmentsPage()
        {
            BrowserTitle("Shipments > Consignments");
            Roles(AppRole.Admin, AppRole.Customer);
            Add<Modules.ConsignmentList>();
        }
    }
}
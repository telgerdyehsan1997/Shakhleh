using MSharp;

namespace Share.Consignments
{
    class ConsignmentEntryTypePage : SubPage<ConsignmentsPage>
    {
        public ConsignmentEntryTypePage()
        {
            BrowserTitle("Shipments > Consignments > Change Entry Type");
            Layout(Layouts.FrontEndModal);
            Roles(AppRole.Admin, AppRole.Customer);
            Add<Modules.ConsignmentEntryType>();
        }
    }
}
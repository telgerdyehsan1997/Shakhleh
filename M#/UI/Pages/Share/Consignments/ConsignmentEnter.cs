using MSharp;

namespace Share.Consignments
{
    class ConsignmentEnterPage : SubPage<ConsignmentsPage>
    {
        public ConsignmentEnterPage()
        {
            BrowserTitle("Shipments > Consignments > Enter");
            Roles(AppRole.Admin, AppRole.Customer);
            Add<Modules.ConsignmentForm>();
            OnStart(x =>
            {
                x.CSharp(@"await OnBound(info);");
               
            });
        }
    }
}
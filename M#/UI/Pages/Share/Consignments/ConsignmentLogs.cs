using MSharp;

namespace Share.Consignments
{
    class ConsignmentLogsPage : RootPage
    {
        public ConsignmentLogsPage()
        {
            BrowserTitle("Shipments > View > Consignment > Logs");
            Roles(AppRole.Admin, AppRole.Customer);
            Add<Modules.EADTransactionLogsList>();
        }
    }
}
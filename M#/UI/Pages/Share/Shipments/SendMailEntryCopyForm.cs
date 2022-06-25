using MSharp;

namespace Share.Shipment
{
    class SendMailEntryCopyFormPage : RootPage
    {
        public SendMailEntryCopyFormPage()
        {
            BrowserTitle("Shipments > Send > Copy Documents");
            Roles(AppRole.Admin, AppRole.Customer);
            Layout(Layouts.FrontEndModal);
            Add<Modules.SendMailEntryCopyForm>();
        }
    }
}
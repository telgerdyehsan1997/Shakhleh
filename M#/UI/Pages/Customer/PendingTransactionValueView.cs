using MSharp;

namespace Customer
{
    class PendingTransactionValueViewPage : RootPage
    {
        public PendingTransactionValueViewPage()
        {
            Layout(Layouts.FrontEndModal);
            Roles(AppRole.Customer);
            Add<Modules.CustomerPendingTransactionValueView>();
        }
    }
}
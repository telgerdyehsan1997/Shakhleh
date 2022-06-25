using MSharp;

namespace Admin.Accounting
{
    class BankDetailsPage : SubPage<Admin.AccountingPage>
    {
        public BankDetailsPage()
        {
            Add<Modules.BankDetailsForm>();
            BaseController("MFABaseController");
        }
    }
}
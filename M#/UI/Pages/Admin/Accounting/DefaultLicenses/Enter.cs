using MSharp;

namespace Admin.Accounting.DefaultLicenses
{
    class EnterPage : SubPage<Admin.Accounting.DefaultLicensesPage>
    {
        public EnterPage()
        {
            Add<Modules.DefaultLicenseForm>();
            BaseController("MFABaseController");
        }
    }
}
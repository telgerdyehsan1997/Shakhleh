using Modules;
using MSharp;

namespace Admin.Accounting
{
    class DefaultLicensesPage : SubPage<Admin.AccountingPage>
    {
        public DefaultLicensesPage()
        {
            Set(PageSettings.LeftMenu, nameof(AdminAccountingMenu));
            Add<Modules.DefaultLicensesList>();
            BaseController("MFABaseController");
        }
    }
}
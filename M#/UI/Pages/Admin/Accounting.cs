using Modules;
using MSharp;

namespace Admin
{
    class AccountingPage : SubPage<AdminPage>
    {
        public AccountingPage()
        {
            Roles(AppRole.Admin);
            
            Set(PageSettings.LeftMenu, nameof(AdminAccountingMenu));

            OnStart(x => x.Go<Accounting.VATRatesPage>().RunServerSide());

            BaseController("MFABaseController");
        }

    }
}
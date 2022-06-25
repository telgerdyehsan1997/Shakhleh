using MSharp;

namespace Admin.Accounting
{
    class ExchequerCodesPage : SubPage<Admin.AccountingPage>
    {
        public ExchequerCodesPage()
        {
            Add<Modules.ExchequerCodeList>();
            BaseController("MFABaseController");
        }
    }
}
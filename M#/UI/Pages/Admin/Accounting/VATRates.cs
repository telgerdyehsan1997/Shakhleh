using MSharp;

namespace Admin.Accounting
{
    class VATRatesPage : SubPage<Admin.AccountingPage>
    {
        public VATRatesPage()
        {
            Add<Modules.VATRatesList>();
            BaseController("MFABaseController");
        }
    }
}
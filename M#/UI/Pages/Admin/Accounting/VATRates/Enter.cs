using MSharp;

namespace Admin.Accounting.VATRates
{
    class EnterPage : SubPage<Accounting.VATRatesPage>
    {
        public EnterPage()
        {
            Add<Modules.VATRateForm>();
            BaseController("MFABaseController");
        }
    }
}
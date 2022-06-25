using MSharp;

namespace Admin.Company.Charge
{
    class EnterPage : SubPage<ChargesPage>
    {
        public EnterPage()
        {
            Add<Modules.ChargeForm>();
            BaseController("MFABaseController");
        }
    }
}
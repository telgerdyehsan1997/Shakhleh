using MSharp;

namespace Admin.Accounting.ExchequerCodes
{
    class EnterPage : SubPage<Admin.Accounting.ExchequerCodesPage>
    {
        public EnterPage()
        {
            Add<Modules.ExchequerCodeForm>();
            Layout(Layouts.FrontEndModal);
            BaseController("MFABaseController");
        }
    }
}
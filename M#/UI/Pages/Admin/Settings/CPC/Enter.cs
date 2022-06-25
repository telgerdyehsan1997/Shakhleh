using MSharp;

namespace Admin.Settings.CPC
{
    class EnterPage : SubPage<Settings.CPCPage>
    {
        public EnterPage()
        {
            Add<Modules.CPCDetails>();
            Add<Modules.CPCTaxLineList>();
        }
    }
}
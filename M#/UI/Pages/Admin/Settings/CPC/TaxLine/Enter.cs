using MSharp;

namespace Admin.Settings.CPC.TaxLine
{
    class EnterPage : SubPage<Admin.Settings.CPC.EnterPage>
    {
        public EnterPage()
        {
            Layout(Layouts.FrontEndModal);

            Add<Modules.CPCTaxLineForm>();
        }
    }
}
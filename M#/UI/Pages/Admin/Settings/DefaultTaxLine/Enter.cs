using MSharp;

namespace Admin.Settings.DefaultTaxLine
{
    class EnterPage : SubPage<Admin.Settings.DefaultTaxLinePage>
    {
        public EnterPage()
        {
            Layout(Layouts.FrontEndModal);

            Add<Modules.DefaultTaxLineForm>();
        }
    }
}
using MSharp;

namespace Admin.Settings.ExchangeRateFile
{
    class EnterPage : SubPage<Admin.Settings.ExchangeRateFilePage>
    {
        public EnterPage()
        {
            Layout(Layouts.FrontEnd);

            Add<Modules.ExchangerateFileForm>();
        }
    }
}
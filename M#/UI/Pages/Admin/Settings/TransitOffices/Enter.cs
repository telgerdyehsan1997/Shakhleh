using MSharp;

namespace Admin.Settings.TransitOffices
{
    class EnterPage : SubPage<Settings.TransitOfficesPage>
    {
        public EnterPage()
        {
            Add<Modules.TransitOfficeForm>();
        }
    }
}
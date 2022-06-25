using MSharp;

namespace Admin
{
    class CarriersPage : SubPage<SettingsPage>
    {
        public CarriersPage()
        {
            Add<Modules.CarrierList>();
        }
    }
}
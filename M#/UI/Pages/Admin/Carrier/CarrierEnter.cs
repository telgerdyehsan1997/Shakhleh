using MSharp;

namespace Admin.Carrier
{
    class CarrierEnterPage : SubPage<SettingsPage>
    {
        public CarrierEnterPage()
        {
            Add<Modules.CarrierForm>();
        }
    }
}
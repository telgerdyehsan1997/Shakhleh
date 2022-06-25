using MSharp;

namespace Admin.Settings
{
    class PaymentTypePage : SubPage<Admin.SettingsPage>
    {
        public PaymentTypePage()
        {
            Add<Modules.PaymentTypeList>();
        }
    }
}
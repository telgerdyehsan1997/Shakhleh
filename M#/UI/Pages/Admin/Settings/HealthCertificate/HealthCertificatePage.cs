using MSharp;

namespace Admin.Settings
{
    class HealthCertificatePage : SubPage<Admin.SettingsPage>
    {
        public HealthCertificatePage()
        {
            Add<Modules.HealthCertificateForm>();
            BaseController("MFABaseController");
        }
    }
}
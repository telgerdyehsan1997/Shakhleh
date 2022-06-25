using MSharp;

namespace Admin.Settings
{
    class HealthCertificateListPage : SubPage<Admin.SettingsPage>
    {
        public HealthCertificateListPage()
        {
            Add<Modules.HealthCertificateList>();
            BaseController("MFABaseController");
        }
    }
}
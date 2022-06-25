using MSharp;
using Domain;
using Modules;

namespace Admin
{
    public class SettingsPage : SubPage<AdminPage>
    {
        public SettingsPage()
        {
            Roles(AppRole.SuperAdmin);
            Set(PageSettings.LeftMenu, nameof(AdminSettingsMenu));

            OnStart(x => x.Go<Settings.GeneralPage>().RunServerSide());
        }
    }
}
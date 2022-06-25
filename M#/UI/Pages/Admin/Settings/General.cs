using MSharp;
using Domain;

namespace Admin.Settings
{
    public class GeneralPage : SubPage<SettingsPage>
    {
        public GeneralPage()
        {
            OnStart(x => x.Go<Admin.Settings.UsersPage>().RunServerSide());
        }
    }
}
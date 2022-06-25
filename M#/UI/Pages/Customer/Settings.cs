using MSharp;
using Domain;
using Modules;

namespace Customer
{
    public class SettingsPage : SubPage<CustomerPage>
    {
        public SettingsPage()
        {
            Roles(AppRole.Customer);
            Set(PageSettings.LeftMenu, nameof(CustomerSettingsMenu));

            OnStart(x =>
            {
                x.Go<Settings.RecordPage>().RunServerSide().Send("user", "User.GetId()");
            });
        }
    }
}
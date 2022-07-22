using MSharp;
using Domain;

namespace Admin
{
    public class FoodShopsPage : SubPage<AdminPage>
    {
        public FoodShopsPage()
        {
            Set(PageSettings.LeftMenu, "AdminSettingsMenu");

            OnStart(x => x.Go<Settings.GeneralPage>().RunServerSide());
        }
    }
}
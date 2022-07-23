using MSharp;
using Domain;

public class ShopUserPage : RootPage
{
    public ShopUserPage()
    {
        Roles(AppRole.ShopUser);

        Add<Modules.ShopUserMainMenu>();

        OnStart(x => x.Go<ShopUser.SettingsPage>().RunServerSide());
    }
}
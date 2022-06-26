using MSharp;
using Domain;

public class CustomerPage : RootPage
{
    public CustomerPage()
    {
        Roles(AppRole.Customer);

    }
}
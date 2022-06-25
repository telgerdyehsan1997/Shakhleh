using MSharp;
using Domain;

public class CustomerPage : RootPage
{
    public CustomerPage()
    {
        Roles(AppRole.Customer);

        OnStart(x =>
        {
            x.CSharp(@"if (CompanyUser!=null)
            {
              if (await Database.Of<BroadcastClientsMessage>().Any(x => x.User == User.GetId() && x.HasRead == false))
                {
                    return Redirect(Url.Index(""BroadcastMessage""));
                }
                if ((CompanyUser?.IsEAD == true))
                {
                    return Redirect(Url.Index(""ShipmentsIntoUK""));
                }
              else if (CompanyUser.IsAdmin == true || User.IsInRole(""Admin""))
               {
                 return Redirect(Url.Index(""CustomerSettings""));
               }
               else
               {
                    return Redirect(Url.Index(""GVMSBlank""));
               }
            }
            return new EmptyResult();");    
        });
    }
}
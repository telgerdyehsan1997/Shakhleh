using MSharp;
using Domain;
using Modules;

namespace Customer
{
    public class BroadcastMessagePage : RootPage
    {
        public BroadcastMessagePage()
        {
            Roles(AppRole.Customer,AppRole.Admin);
            Add<Modules.BroadcastClientsMessageList>();
        }
    }
}
using MSharp;

namespace Customer
{
    class DepositsPage : RootPage
    {
        public DepositsPage()
        {         
            Add<Modules.CustomerDepositView>();
            Add<Modules.CustomerDepositList>();          
        }
    }
}
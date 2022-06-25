using MSharp;

namespace Admin.Company.Deposits
{
    class EnterPage : SubPage<DepositsPage>
    {
        public EnterPage()
        {
            Add<Modules.CompanyDepositForm>();
        }
    }
}
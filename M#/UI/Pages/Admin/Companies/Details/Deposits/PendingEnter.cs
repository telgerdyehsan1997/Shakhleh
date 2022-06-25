using MSharp;

namespace Admin.Company.Deposits
{
    class PendingEnterPage : SubPage<DepositsPage>
    {
        public PendingEnterPage()
        {
            Layout(Layouts.FrontEndModal);
            Roles(AppRole.Admin);
            Add<Modules.CompanyPendingDepositForm>();
        }
    }
}
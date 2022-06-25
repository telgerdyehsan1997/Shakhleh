using MSharp;

namespace Admin.Company
{
    class AccountingInformationPage : SubPage<Admin.Company.DetailsPage>
    {
        public AccountingInformationPage()
        {
            Add<Modules.CompanyAccountingForm>();
            BaseController("MFABaseController");
        }
    }
}
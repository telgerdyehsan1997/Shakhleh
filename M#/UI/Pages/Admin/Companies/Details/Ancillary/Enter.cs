using MSharp;

namespace Admin.Company.Ancillary
{
    class EnterPage : SubPage<AncillariesPage>
    {
        public EnterPage()
        {
            Add<Modules.CompanyAncillaryForm>();
        }
    }
}
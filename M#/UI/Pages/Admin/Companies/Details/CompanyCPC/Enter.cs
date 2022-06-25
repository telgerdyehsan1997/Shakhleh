using MSharp;

namespace Admin.Company.CompanyCPC
{
    class EnterPage : SubPage<CompanyCPCPage>
    {
        public EnterPage()
        {
            Add<Modules.CompanyCPCForm>();
        }
    }
}
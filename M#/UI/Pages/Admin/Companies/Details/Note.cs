using MSharp;

namespace Admin.Company
{
    class NotePage : SubPage<CompaniesPage>
    {
        public NotePage()
        {
            Set(PageSettings.LeftMenu, "CompanyMenu");

            Add<Modules.NoteList>();
        }
    }
}
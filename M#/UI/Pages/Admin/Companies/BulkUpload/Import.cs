using MSharp;

namespace Admin.Company
{
    class BulkUploadListPage : SubPage<Admin.CompaniesPage>
    {
        public BulkUploadListPage()
        {
            Add<Modules.CompanyBulkUploadList>();
        }
    }
}
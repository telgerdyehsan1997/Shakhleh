using MSharp;

namespace Admin.Company
{
    class BulkUploadPage : SubPage<CompaniesPage>
    {
        public BulkUploadPage()
        {
            Add<Modules.CompanyBulkUpload>();
            Layout(Layouts.FrontEndModal);
        }
    }
}
using MSharp;

namespace Modules
{
    class CompanyBulkUpload : FormModule<Domain.ImportQueueItem>
    {
        public CompanyBulkUpload()
        {
            HeaderText("Bulk Upload");

            Field(x => x.File);

            AutoSet(x => x.Type).Value("ImportType.Company");

            Button("Cancel").OnClick(x => x.CloseModal());

            Button("Save").IsDefault().Icon(FA.Check)
            .OnClick(x =>
            {
                x.CSharp("info.Item.Type = ImportType.Company;");
                x.SaveInDatabase();
                x.GentleMessage("Saved successfully.");
                x.CloseModal();
            });
        }
    }
}
using MSharp;
using Domain;

namespace Modules
{
    class ProductBulkUpload : FormModule<Domain.ImportQueueItem>
    {
        public ProductBulkUpload()
        {
            HeaderText("Bulk Upload");

            Field(x => x.File);

            AutoSet(x => x.Type).Value("ImportType.Product");
            AutoSet(x => x.Company).Value("info.Company");

            ViewModelProperty<Company>("Company").FromRequestParam("company");

            Button("Cancel").OnClick(x => x.CloseModal());

            Button("Save").IsDefault().Icon(FA.Check)
            .OnClick(x =>
            {
                x.CSharp("info.Item.Type = ImportType.Product;");
                x.SaveInDatabase();
                x.GentleMessage("Saved successfully.");
                x.CloseModal();
            });
        }
    }
}
using MSharp;

namespace Modules
{
    class ImportRowQuotaForm : FormModule<Domain.ImportQueueItem>
    {
        public ImportRowQuotaForm()
        {
            Using("System.IO");
            HeaderText("Import")
                .SupportsAdd(true)
                .SupportsEdit(false);

            Field(x => x.File);

            AutoSet(x => x.Type).Value("ImportType.RowQuota");


            Button("Cancel").OnClick(x => x.CloseModal());

            Button("Save").IsDefault().Icon(FA.Check)
            .OnClick(x =>
            {
                x.SaveInDatabase();
                x.CloseModal(Refresh.Full);
            });          
        }
    }
}
using MSharp;

namespace Modules
{
    class ImportUNCodesForm : FormModule<Domain.ImportQueueItem>
    {
        public ImportUNCodesForm()
        {
            Using("System.IO");
            HeaderText("Upload UN Codes")
                .SupportsAdd(true)
                .SupportsEdit(false);

            Field(x => x.File);

            AutoSet(x => x.Type).Value("ImportType.UnCodes");


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
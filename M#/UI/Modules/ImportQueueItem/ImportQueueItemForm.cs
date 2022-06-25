using MSharp;

namespace Modules
{
    class ImportQueueItemForm : FormModule<Domain.ImportQueueItem>
    {
        public ImportQueueItemForm()
        {
            HeaderText("Upload Commodity Codes")
                .SupportsAdd(true)
                .SupportsEdit(false);

            Field(x => x.File);

            AutoSet(x => x.Type).Value("ImportType.CommodityCode");

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
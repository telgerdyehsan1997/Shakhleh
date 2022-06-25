using MSharp;

namespace Modules
{
    class ImportRoutingItineraryForm : FormModule<Domain.ImportQueueItem>
    {
        public ImportRoutingItineraryForm()
        {
            Using("System.IO");
            HeaderText("Upload Routing Itineraries")
                .SupportsAdd(true)
                .SupportsEdit(false);

            Field(x => x.File);

            AutoSet(x => x.Type).Value("ImportType.Itinerary");


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
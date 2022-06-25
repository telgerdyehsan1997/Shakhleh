using MSharp;

namespace Modules
{
    class ImportRoutingItineraryList : ListModule<Domain.ImportQueueItem>
    {
        public ImportRoutingItineraryList()
        {
            HeaderText("Routing Itineraries Imports")
                 .SortingStatement("item.UploadDate DESC");
            SourceCriteria("item.Type == ImportType.Itinerary");

            Search(x => x.UploadDate);
            Search(x => x.Status).AsRadioButtons(Arrange.Horizontal);
            SearchButton("Search").OnClick(x => x.ReturnView());

            Column(x => x.UploadDate);
            Column(x => x.File);

            Column(x => x.Status);

            LinkColumn("Errors").HeaderText("Errors").VisibleIf("await item.Errors.GetList().Any()")
                .OnClick(x => x.Go<Admin.Settings.Import.RouteItineraryErrorLogPage>().Send("item", "item.ID"));

            Button("New Import").IsDefault().Icon(FA.Plus)
                .OnClick(x => x.PopUp<Admin.Settings.Import.UploadRoutingItinerariesPage>());

        }
    }
}
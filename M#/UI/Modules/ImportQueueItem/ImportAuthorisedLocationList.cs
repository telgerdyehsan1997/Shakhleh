using MSharp;

namespace Modules
{
    class ImportAuthorisedLocationList : ListModule<Domain.ImportQueueItem>
    {
        public ImportAuthorisedLocationList()
        {
            HeaderText("Import History")
                .SortingStatement("item.UploadDate DESC");
            SourceCriteria("item.Type == ImportType.AuthorisedLocation");

            Search(x => x.UploadDate);
            Search(x => x.Status).AsRadioButtons(Arrange.Horizontal);
            SearchButton("Search").OnClick(x => x.ReturnView());

            Column(x => x.UploadDate);
            Column(x => x.File);

            Column(x => x.Status);

            LinkColumn("Errors").HeaderText("Errors").VisibleIf("await item.Errors.GetList().Any()")
                .OnClick(x => x.Go<Admin.Settings.Import.ErrorsPage>().Send("item", "item.ID"));

            Button("New Import").IsDefault().Icon(FA.Plus)
                .OnClick(x => x.PopUp<Admin.Settings.AuthorisedLocations.UploadPage>());
        }
    }
}
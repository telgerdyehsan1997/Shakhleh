using MSharp;

namespace Modules
{
    class ImportErrorList : BaseListModule<Domain.ImportError>
    {
        public ImportErrorList()
        {
            HeaderText("Import Errors")
               .PageSize("50")
               .SortingStatement("item.LineNumber")
               .DataSource("await info.ImportQueueItem.Errors.GetList()");

            Search(GeneralSearch.AllFields).Label("Find");
            SearchButton("Search").Icon(FA.Search).OnClick(x => x.ReturnView());

            Column(x => x.LineNumber);
            Column(x => x.ErrorReason);

            ViewModelProperty<Domain.ImportQueueItem>("ImportQueueItem").FromRequestParam("item");
        }
    }
}
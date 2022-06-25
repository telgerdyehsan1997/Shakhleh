using MSharp;

namespace Modules
{
    class CommodityImportErrorList : ListModule<Domain.ImportError>
    {
        public CommodityImportErrorList()
        {
            HeaderText("Import Errors")
                .PageSize("10")
                .SortingStatement("item.LineNumber")
                .DataSource("await info.ImportQueueItem.Errors.GetList()");

            Column(x => x.LineNumber);
            Column(x => x.ErrorReason);

            ViewModelProperty<Domain.ImportQueueItem>("ImportQueueItem").FromRequestParam("item");

            Button("Back")
                .OnClick(x => x.CloseModal());
        }
    }
}
using MSharp;

namespace Modules
{
    class UNCodesImportErrorList : ListModule<Domain.ImportError>
    {
        public UNCodesImportErrorList()
        {
            HeaderText("UN Codes Import Errors")
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
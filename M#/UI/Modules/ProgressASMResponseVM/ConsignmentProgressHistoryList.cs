using MSharp;

namespace Modules
{
    class ConsignmentProgressHistoryList : ListModule<Domain.ConsignmentProgressHistory>
    {
        public ConsignmentProgressHistoryList()
        {
            HeaderText("Progress History");
            DataSource("await info.Consignment.ProgressHistory.GetList()").SortingStatement("item.Date DESC");
            Column(x => x.Date).DisplayFormat("{0: dd/MM/yyyy HH:mm:ss}");
            Column(x => x.Progress);
            Column(x => x.User).DisplayExpression(@"@item.User?.Name");

            ViewModelProperty<Domain.Consignment>("Consignment").FromRequestParam("consignment");
        }
    }
}
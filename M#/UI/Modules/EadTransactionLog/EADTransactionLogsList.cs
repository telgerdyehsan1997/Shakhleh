using MSharp;
using Domain;

namespace Modules
{
    class EADTransactionLogsList : ListModule<Domain.EadTransactionLog>
    {
        public EADTransactionLogsList()
        {
            HeaderText("Logs");

            DataSource("await info.Consignment.Logs.GetList()");
            SortingStatement("item.Date DESC");

            EmptyMarkup("There are no logs to display.");

            Column(x => x.Date).DisplayFormat("{0: dd/MM/yyyy HH:mm:ss}");
            Column(x => x.Type);
            Column(x => x.File);

            Button("Back")
                .OnClick(x => x.ReturnToPreviousPage());

            ViewModelProperty<Consignment>("Consignment").FromRequestParam("consignment");
        }
    }
}
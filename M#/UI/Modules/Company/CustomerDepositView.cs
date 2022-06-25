using Domain;
using MSharp;
using System.Xml.Linq;
using Olive;

namespace Modules
{
    class CustomerDepositView : FormModule<Company>
    {
        public CustomerDepositView()
        {
            HeaderText("Deposits")
                .RequestParam("company");

            Link("ViewPendingTransaction")
                .Text("View pending transaction")
                .OnClick(x =>
                {
                    x.PopUp<Customer.PendingTransactionValueViewPage>().Pass("company").Send("deposit", "info.Item");
                });

            CustomField()
               .ItemCssClass("readonly-field")
                  .Label("Remaining Balance")
                  .ControlMarkup(@"@((await  info.Item.GetTotalRemainingBalance()).ToString(""c""))");

            CustomField().AfterControl("[#BUTTONS(ViewPendingTransaction)#]")
                .ItemCssClass("readonly-field")
                .Label("Pending Transaction Value")
              .ControlMarkup(@"@((await info.Item.GetPendingTransactionValue()).ToString(""c""))");

            CustomField()
              .ItemCssClass("readonly-field")
              .Label("Remaining Balance After Pending")
            .ControlMarkup(@"@((await info.Item.GetRemainingBalanceAfterPending()).ToString(""c""))");

        }
    }
}

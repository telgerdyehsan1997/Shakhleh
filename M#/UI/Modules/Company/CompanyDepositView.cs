using Domain;
using MSharp;
using System.Xml.Linq;
using Olive;

namespace Modules
{
    class CompanyDepositView : FormModule<Company>
    {
        public CompanyDepositView()
        {
            HeaderText("Deposits")
                .RequestParam("company")
                .IsViewComponent()
                .WrapInForm(false)
                .IsInUse();

            Link("ViewPendingTransaction")
                .Text("View pending transaction")
                .OnClick(x =>
                {
                    x.Go<Admin.Company.CompanyPendingTransactionValueViewPage>().Pass("company").SendReturnUrl();
                });

            CustomField()            
               .ItemCssClass("readonly-field")
                  .Label("Remaining Balance")
                  .ControlMarkup(@"@((await  info.Item.GetTotalRemainingBalance()).ToString(""c""))");

            CustomField()
               .ItemCssClass("readonly-field")
                  .Label("Overdraft Amount")
                  .BeforeControl("£")
                  .ControlMarkup(@"@(info.Item.OverdraftAmount.HasValue?info.Item.OverdraftAmount.Value:0)");

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

using Domain;
using MSharp;
using System.Xml.Linq;
using Olive;

namespace Modules
{
    class CompanyPendingTransactionValueView : ListModule<Domain.Deposit>
    {
        public CompanyPendingTransactionValueView()
        {
            HeaderText("Pending Deductions");
            DataSource("await Deposit.GetDepositList()");
            SourceCriteria("item.Company == info.Company");
            SortingStatement("item.DateAdded DESC | item.DateCreated DESC");

            Button("New Pending Transaction")
               .OnClick(x =>
                        x.If(AppRole.Admin, AppRole.Customer)
                        .PopUp<Admin.Company.Deposits.PendingEnterPage>()
                        .Pass("company")
                        .SendReturnUrl());

            Button("Approve Transaction")
             .OnClick(x =>
             {
                 x.CSharp(@" if (await info.Company.GetTotalRemainingBalance() <= 0)
                 {
                    Notify(""A positive Deposit value is required to approve a Pending Transaction"", obstruct: false);
                    await TryUpdateModelAsync(info);
                     return View(info);
                 }");
                 x.CSharp(@" foreach (var item in await info.SelectedItems.ToList())
                 {
                    if (item.Consignment.UCN.IsEmpty() && item.Consignment.Shipment.IsInUK && item.Consignment.IntoUKType == PortType.Inventory)
                        Notify($""{ item.Consignment} consignment doesn't have UCN Number."", ""error"");
                    else
                     await UpdateListPendingToWithdrawl(item.Consignment);
                  }");
                 x.GentleMessage("Transaction is being running in background, it will move accordingly once completed. Thank you!");
                 x.Reload();
             });



            this.ArchiveSearch();
            SearchButton("Search").Icon(FA.Search).OnClick(x => x.ReturnView());

            SelectCheckbox(true);
            CustomColumn("WithdrawalValue")
                .LabelText("Withdrawal")
                .DisplayExpression("@(item.Value)");

            Column(x => x.Consignment)
                .LabelText("Tracking Number");

            Column(x => x.Consignment.Shipment.MyReferenceForCPInvoice)
              .LabelText("Reference Number");

            Column(x => x.DateAdded)
               .DisplayFormat("{0:d}");



            ButtonColumn("Edit").HeaderText("Edit").GridColumnCssClass("actions").Icon(FA.Edit)
               .OnClick(x =>
                  x.If(AppRole.Admin)
                 .PopUp<Admin.Company.Deposits.PendingEnterPage>()
                 .Send("deposit", "item.ID").Pass("company")
                 .SendReturnUrl());


            this.ArchiveButtonColumn().ColumnVisibleIf(AppRole.SuperAdmin);

            ButtonColumn("@(item.Consignment.IsDeactivated ? \"Unarchive Consignment\":\"Archive Consignment\")").VisibleIf("item.ConsignmentId.HasValue")
               .HeaderText("Archive Consignment")
               .CssClass("c#:item.Consignment.IsDeactivated ? \"row-archived\":\"\"")
               .GridColumnCssClass("actions-sep")
               .Icon(FA.Archive)
               .OnClick(x =>
               {
                   x.PopUp<Share.Archive.ArchivePopUpPage>().Send("entity", "item.ID").Send("consignment", "item.ConsignmentId");
               });

            ViewModelProperty<Company>("Company").FromRequestParam("company");

            Inject("IEADShipmentService");


            OnControllerClassCode("Update Pending to Withdraw")
                .Code(@"public async Task UpdateListPendingToWithdrawl(Consignment consignment)
                {
                    try
                    {
                        await Database.Update(consignment, x =>
                        {
                            x.NeedToSendAmendment = true;
                            x.HasPrefrenceForSubdivision = true;
                        });
                        consignment = await Database.Reload(consignment);
                        await IEADShipmentService.Transmit(consignment);
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                  }
               ");
        }
    }
}

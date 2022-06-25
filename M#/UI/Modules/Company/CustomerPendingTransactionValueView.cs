using Domain;
using MSharp;
using System.Xml.Linq;
using Olive;

namespace Modules
{
    class CustomerPendingTransactionValueView : ListModule<Domain.Deposit>
    {
        public CustomerPendingTransactionValueView()
        {
            HeaderText("Pending Deductions");
            DataSource("await Deposit.GetDeactivatedDepositList(info.Company)");
            SortingStatement("item.DateAdded DESC | item.DateCreated DESC");

            Button("Approve Transaction")
            .OnClick(x =>
            {

                x.CSharp(@" if (await info.Company.GetTotalRemainingBalance() <= 0)
                 {
                    Notify(""A positive Deposit value is required to approve a Pending Transaction"", obstruct: false);
                    await TryUpdateModelAsync(info);
                     return JsonActions(info);
                 }");

                x.CSharp(@" foreach (var item in await info.SelectedItems.ToList())
                {
                    if (item.Consignment.UCN.IsEmpty() && item.Consignment.Shipment.IsInUK && await item.Consignment.Shipment.Route.UKPort?.PortsIntoUk.Any(x=>x.IntoUKTypeId != PortType.GVMS))
                        Notify($""{ item.Consignment} consignment doesn't have UCN Number."", ""error"");
                    else
                     await UpdateListPendingToWithdrawl(item.Consignment);
                }");
                x.GentleMessage("Transaction is being running in background, it will move accordingly once completed. Thank you!");
                x.CloseModal();
            });

            SelectCheckbox(true);

            CustomColumn("WithdrawalValue")
                .LabelText("Withdrawal")
                .DisplayExpression("£ @(item.Value)");

            Column(x => x.Consignment)
                .LabelText("Tracking Number");

            Column(x => x.Consignment.Shipment.MyReferenceForCPInvoice)
                .LabelText("Reference Number");

            Column(x => x.DateAdded)
               .DisplayFormat("{0:d}");

            Inject("IEADShipmentService");

            ViewModelProperty<Domain.Company>("Company").FromRequestParam("company");

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

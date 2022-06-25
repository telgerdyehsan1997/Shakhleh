using MSharp;

namespace Modules
{
    class DutyIsPayableView : FormModule<Domain.Consignment>
    {
        public DutyIsPayableView()
        {
            HeaderText("Duty is Payable on one or more of the commodity codes");

            DataSource("info.Consignment");
            ViewModelProperty<Domain.Consignment>("Consignment").FromRequestParam("consignment");
            ViewModelProperty<string>("Message").OnBound("info.Message = await info.Consignment.DutyMessage();");
            Inject("IEADShipmentService");

            CustomField().Label("").ItemCssClass("readonly-field").ControlMarkup("@info.Message");
            AutoSet(x => x.HasPrefrenceForSubdivision).Value("true");

            AutoSet(x => x.NeedToSendAmendment).Value("info.Consignment.NeedToSendAmendment == true && await info.Consignment.ProgressHistory.Any(x => x.ProgressId == Progress.ASMAccept)");

            Button("No")
                .OnClick(x => x.PopUp<Share.Commodities.DutyIsPayableHoldViewPage>().Send("consignment", "info.Consignment.ID"));

            Button("Yes")
                .OnClick(x =>
                {
                    x.RunInTransaction();
                    x.CSharp(@"try {await info.Consignment.ValidateCompletion(); }
                           catch (Exception ex) { return Notify(ex.Message, ""error""); }
                    ");
                    x.SaveInDatabase();
                    x.CSharp(@"if(info.Item.UCN.IsEmpty() && info.Item.Shipment.IsInUK && info.Item.IntoUKTypeId == PortType.Inventory)
                                  return Notify(""This consignment doesn't have UCN Number."", ""error"");");

                    x.CSharp(@"await IEADShipmentService.Transmit(info.Item, true);").ValidationError();
                    x.GentleMessage("Saved successfully.");
                    x.ReturnToPreviousPage();
                });

        }
    }
}
using MSharp;

namespace Modules
{
    class DutyIsPayableHoldView : FormModule<Domain.Consignment>
    {
        public DutyIsPayableHoldView()
        {
            HeaderText("Do you wish to send the entry to obtain the amount of duty payable?");
            Header("<p>This will place the entry on Hold pending further instructions you will be able to amend the entry whilst it remains in Hold.</p>");

            DataSource("info.Consignment");
            ViewModelProperty<Domain.Consignment>("Consignment").FromRequestParam("consignment");
            Inject("IEADShipmentService");

            Button("No")
                 .OnClick(x =>
                 {
                     x.RunInTransaction();
                     x.CSharp(@"try { await info.Consignment.FlagAsDraft();  }
                        catch(Exception ex) { return Notify(ex.Message, ""error""); }");
                     x.GentleMessage("Saved successfully.");
                     x.ReturnToPreviousPage();
                 });

            AutoSet(x => x.HasPrefrenceForSubdivision).Value("false");

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

                    x.CSharp(@"await IEADShipmentService.Transmit(info.Item, true);");
                    x.GentleMessage("Saved successfully.");
                    x.ReturnToPreviousPage();
                });


        }
    }
}
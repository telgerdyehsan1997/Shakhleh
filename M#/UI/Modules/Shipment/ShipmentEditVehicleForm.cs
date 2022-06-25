using MSharp;

namespace Modules
{
    class ShipmentEditVehicleForm : FormModule<Domain.Shipment>
    {
        public ShipmentEditVehicleForm()
        {
            HeaderText("Shipment details");

            Field(x => x.VehicleNumber);
            Field(x => x.TrailerNumber);

            Button("Cancel").OnClick(x => x.CloseModal());

            Button("Save").IsDefault().Icon(FA.Check)
            .OnClick(x =>
            {
                x.CSharp(@" try {await (await info.Item.Consignments.FirstOrDefault()).ValidateCompletion(); }
                        catch (Exception ex) { return Notify(ex.Message, ""error""); }");

                x.SaveInDatabase();
                //x.CSharp(@"await IEADShipmentService.UpdateTransmit(info.Item);");
                x.GentleMessage("Saved successfully.");
                x.CloseModal(Refresh.Full);
            });

            Inject("IEADShipmentService");
        }
    }
}
using Domain;
using MSharp;

namespace Modules
{
    class ShipmentDetailsView : ViewModule<Domain.Shipment>
    {
        public ShipmentDetailsView()
        {
            HeaderText("Shipment Details");
            this.AddDependency(typeof(IEADShipmentService));

            Field(x => x.Company).LabelText("Company name").DisplayExpression("@info.Shipment.Company.Name");
            Field(x => x.Company.Type).LabelText("Company type");
            Field(x => x.PrimaryContact);
            Field(x => x.NotifyAdditionalParty).LabelText("Notify additional parties");
            Field(x => x.Group).VisibleIf("info.Shipment.NotifyAdditionalParty == NotifyType.Group");
            Field(x => x.ContactName).VisibleIf("info.Shipment.NotifyAdditionalParty == NotifyType.SpecificContacts");
            Field(x => x.MyReferenceForCPInvoice)
                .LabelText("Customer Reference");
            Field(x => x.Type).DisplayExpression(@"@(info.Shipment.Type == ShipmentType.IntoUk ? ""Into UK"" : ""Out of UK"")");
            Field(x => x.VehicleNumber).AfterValue("[#BUTTONS(Edit)#]");
            Field(x => x.TrailerNumber);

            Field(x => x.ExpectedDate)
                .LabelText(@"@(info.Shipment.Type == ShipmentType.IntoUk ? ""Expected date of arrival"" : ""Expected date of departure"")");
            //Field(x => x.PortOfArrival)
            //    .VisibleIf("!info.Shipment.IsNCTSShipmentOutConvertible")
            //    .LabelText(@"@(info.Shipment.Type == ShipmentType.IntoUk ? ""Port of arrival"" : ""Port of departure"")");
            Field(x => x.Route).DisplayExpression("@item.RouteName");
            //.VisibleIf("info.Shipment.IsNCTSShipmentOutConvertible");
            Field(x => x.FirstBorderCrossing)
                .VisibleIf("info.Shipment.IsNCTSShipmentOutConvertible == true");
            Field(x => x.OfficeOfDestination);
            Field(x => x.SecondBorderCrossing)
                .VisibleIf("info.Shipment.IsNCTSShipmentOutConvertible == true");
            Field(x => x.ThirdBorderCrossing)
                .VisibleIf("info.Shipment.IsNCTSShipmentOutConvertible == true");
            Field(x => x.FourthBorderCrossing)
                .VisibleIf("info.Shipment.IsNCTSShipmentOutConvertible == true");
            Field(x => x.AuthorisedLocation).VisibleIf("info.Shipment.AuthorisedLocation != null");
            Field(x => x.ConsignmentsClearedDate).LabelText("Date/time cleared").VisibleIf("info.Shipment.ConsignmentsClearedDate != null");
            Field(x => x.UploadAttachments).LabelText("Attachment").DisplayExpression(@"@{
                 foreach (var upload in await item.UploadAttachments.GetList())
                 { 
                  <a href=""@upload.Attachment"" target=""_blank"">Download</a>
                 }
                }").AfterValue("[#BUTTONS(Upload)#]");


            Button("Upload")
                .Name("Upload")
                .Text("Upload")
                .OnClick(x => x.PopUp<Share.Shipment.ShipmentView.UploadAttachmentPage>().Send("shipment", "info.Shipment.ID"));


            Button("Edit")
                .VisibleIf("await info.Shipment.Consignments.Any(x=>x.ProgressId.IsAnyOf(Helper.Transmit))")
               .Name("Edit")
               .Text("Edit")
               .CssClass("vehicle-edit")
               .OnClick(x => x.PopUp<Share.Shipment.ShipmentView.EditVehicleNumberPage>().Send("item", "info.Shipment.ID"));

            Button("Send Copy Documents")
                .CssClass("float-right")
                .Text("Send Copy Documents")
                .OnClick(x => x.PopUp<Share.Shipment.SendMailEntryCopyFormPage>().Send("shipment", "info.Shipment.ID"));

            DataSource("info.Shipment");
            ViewModelProperty<Domain.Shipment>("Shipment").FromRequestParam("shipment");
        }
    }
}
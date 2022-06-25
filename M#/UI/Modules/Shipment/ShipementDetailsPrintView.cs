using Domain;
using MSharp;

namespace Modules
{
    class ShipementDetailsPrintView : ViewModule<Domain.Shipment>
    {
        public ShipementDetailsPrintView()
        {
            this.AddDependency(typeof(IEADShipmentService));

            var leftBox = Box("LeftBox", BoxTemplate.WrapperDiv).CssClass("section");
            var rightBox = Box("RightBox", BoxTemplate.WrapperDiv).CssClass("section");

            HeaderText("@info.Shipment.TrackingNumber");

            Field(x => x.Company).LabelText("Company name").DisplayExpression("@info.Shipment.Company.Name")
                .Box(leftBox);
            Field(x => x.Company.Type).LabelText("Company type")
                .Box(leftBox);
            Field(x => x.PrimaryContact)
                .Box(leftBox);
            Field(x => x.NotifyAdditionalParty).LabelText("Notify additional parties")
                .Box(leftBox);
            Field(x => x.Group).VisibleIf("info.Shipment.NotifyAdditionalParty == NotifyType.Group")
                .Box(leftBox);
            Field(x => x.ContactName).VisibleIf("info.Shipment.NotifyAdditionalParty == NotifyType.SpecificContacts")
                .Box(leftBox);
            Field(x => x.MyReferenceForCPInvoice)
                .LabelText("Customer Reference")
                .Box(leftBox);
            Field(x => x.Type).DisplayExpression(@"@(info.Shipment.Type == ShipmentType.IntoUk ? ""Into UK"" : ""Out of UK"")")
                .Box(leftBox);
            Field(x => x.VehicleNumber)
                .Box(leftBox);
            Field(x => x.TrailerNumber)
                .Box(leftBox);

            Field(x => x.ExpectedDate)
                .LabelText(@"@(info.Shipment.Type == ShipmentType.IntoUk ? ""Expected date of arrival"" : ""Expected date of departure"")")
                .Box(rightBox);
            //Field(x => x.PortOfArrival)
            //    .Box(rightBox)
            //    .VisibleIf("!info.Shipment.IsNCTSShipmentOutConvertible")
            //    .LabelText(@"@(info.Shipment.Type == ShipmentType.IntoUk ? ""Port of arrival"" : ""Port of departure"")");
            Field(x => x.Route)
                .Box(rightBox);
            //.VisibleIf("info.Shipment.IsNCTSShipmentOutConvertible");
            Field(x => x.FirstBorderCrossing)
                .Box(rightBox)
                .VisibleIf("info.Shipment.IsNCTSShipmentOutConvertible == true");
            Field(x => x.OfficeOfDestination)
                .Box(rightBox);
            Field(x => x.SecondBorderCrossing)
                .Box(rightBox)
                .VisibleIf("info.Shipment.IsNCTSShipmentOutConvertible == true");
            Field(x => x.ThirdBorderCrossing)
                .Box(rightBox)
                .VisibleIf("info.Shipment.IsNCTSShipmentOutConvertible == true");
            Field(x => x.FourthBorderCrossing)
                .Box(rightBox)
                .VisibleIf("info.Shipment.IsNCTSShipmentOutConvertible == true");
            Field(x => x.AuthorisedLocation).VisibleIf("info.Shipment.AuthorisedLocation != null")
                .Box(rightBox);
            Field(x => x.ConsignmentsClearedDate).LabelText("Date/time cleared").VisibleIf("info.Shipment.ConsignmentsClearedDate != null")
                .Box(rightBox);
            Field(x => x.Progress.AdminDisplay).VisibleIf(AppRole.Admin)
                .LabelText("Progress")
                .Box(rightBox);
            Field(x => x.Progress.ClientDisplay).VisibleIf(AppRole.Customer)
                .LabelText("Progress")
                .Box(rightBox);

            DataSource("info.Shipment");
            ViewModelProperty<Domain.Shipment>("Shipment").FromRequestParam("shipment");
        }
    }
}
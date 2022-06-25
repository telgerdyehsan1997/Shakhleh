using Domain;
using MSharp;
using Olive;
using Olive.Entities;

namespace Modules
{
    class ShipmentArchiveLogForm : FormModule<ArchiveLog>
    {
        public ShipmentArchiveLogForm()
        {
            Header(@"<h2>Archive</h2>");

            Field(x => x.Reason)
             .Label("Please explain why")
             .Mandatory();

            Field(x => x.TrackingNumber)
                  .Label("Please enter a replacement Tracking Number");


            Button("Cancel")
                .OnClick(x => x.CloseModal());

            Button("Archive")
                .IsDefault()
                .Icon(FA.Check)
                .OnClick(x =>
                {
                    x.If(@"info.TrackingNumber.IsEmpty()")
                      .MessageBox("Without a replacement Tracking number you cannot archive the shipment due to declarations status with Customs")
                      .AndExit();

                    x.If(@"!await Database.Of<Shipment>().Any(x => x.TrackingNumber == info.TrackingNumber.Trim() && !x.IsDeactivated)")
                        .MessageBox(cs("$\"{ info.TrackingNumber} does not exist\""))
                        .AndExit();

                    x.If(@"await Database.Of<Shipment>().Any(x => x.TrackingNumber == info.TrackingNumber.Trim() && x.ID == (info.Entity as GuidEntity))")
                      .MessageBox("The Replacement Tracking Number cannot be the same as the original Tracking Number.")
                      .AndExit();

                    x.CSharp(@"var shipment = await Database.Of<Shipment>().Where(x => x.ID == (info.Entity as GuidEntity)).FirstOrDefault();
                               var shipmentType = info.TrackingNumber.StartsWith(""R"") ? ShipmentType.IntoUk : ShipmentType.OutOfUk;");

                    x.CSharp(@"if (shipmentType != shipment.Type)
                    {
                            if (shipment.Type == ShipmentType.IntoUk)
                            {
                                Notify(""Please provide a replacement Tracking Number that follows the prefix of an imported Shipment"");
                                return JsonActions(info);
                            }
                           else
                           {
                                Notify(""Please provide a replacement Tracking Number that follows the prefix of an exported Shipment"");
                                return JsonActions(info);
                           }
                    }");

                    x.CloseModal(Refresh.Full);
                });

            ViewModelProperty<IArchivable>("Entity")
                .FromRequestParam("entity");

        }
    }
}
using MSharp;

namespace Modules
{
    class ShipmentArchiveConfirmation : FormModule<Domain.Shipment>
    {
        public ShipmentArchiveConfirmation()
        {
            HeaderText("Are you sure you wish to archive this shipment as the entry has been arrived with Customs");

            ViewModelProperty<Domain.Shipment>("Shipment")
                .FromRequestParam("shipment");

            Button("Yes")
              .OnClick(x =>
               {
                   x.PopUp<Share.Archive.ShipmentArchivePage>()
                   .Send("entity", "info.Shipment.ID");
               });

            Button("No")
                .OnClick(x => x.CloseModal());

        }
    }
}
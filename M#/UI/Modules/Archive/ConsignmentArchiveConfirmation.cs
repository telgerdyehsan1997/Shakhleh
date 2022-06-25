using MSharp;

namespace Modules
{
    class ConsignmentArchiveConfirmation : FormModule<Domain.Consignment>
    {
        public ConsignmentArchiveConfirmation()
        {
            HeaderText("Are you sure you wish to archive this consignment as the entry has been arrived with Customs");

            ViewModelProperty<Domain.Consignment>("Consignment")
                .FromRequestParam("consignment");

            Button("Yes")
              .OnClick(x =>
              {
                  x.PopUp<Share.Archive.ShipmentArchivePage>()
                  .Send("entity", "info.Consignment.ID");
              });

            Button("No")
                .OnClick(x => x.CloseModal());


        }
    }
}
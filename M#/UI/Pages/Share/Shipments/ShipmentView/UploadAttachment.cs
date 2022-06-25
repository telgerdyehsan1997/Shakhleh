using MSharp;

namespace Share.Shipment.ShipmentView
{
    class UploadAttachmentPage : SubPage<ShipmentViewPage>
    {
        public UploadAttachmentPage()
        {
            Roles(AppRole.Admin, AppRole.Customer);
            Layout(Layouts.FrontEndModal);
            Add<Modules.ShipmentAttachmentUploadForm>();
        }
    }
}
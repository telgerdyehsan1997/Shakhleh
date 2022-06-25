using MSharp;

namespace Domain
{
    class UploadAttachment : EntityType
    {
        public UploadAttachment()
        {
            SecureFile("Attachment");
            Associate<Shipment>("Shipment");
        }
    }
}
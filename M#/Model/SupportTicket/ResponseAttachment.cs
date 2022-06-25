using MSharp;

namespace Domain
{
    class ResponseAttachment : EntityType
    {
        public ResponseAttachment()
        {
            SecureFile("Attachment");
            Associate<Response>("Response");
        }
    }
}
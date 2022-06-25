using MSharp;

namespace Domain
{
    class BannerMessage : EntityType
    {
        public BannerMessage()
        {
            this.Archivable();
            BigString("Message");
        }
    }
}
using MSharp;

namespace Domain
{
    class Food : EntityType
    {
        public Food()
        {
            this.Archivable();

            String("Name").Mandatory();
            String("Description");

            SecureImage("Image");
            Int("Price");

            Associate<Shop>("Shop");
        }
    }
}

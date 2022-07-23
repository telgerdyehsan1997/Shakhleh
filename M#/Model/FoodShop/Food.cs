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
            Money("Price").IsCurrency(false);

            Associate<Shop>("Shop");
        }
    }
}

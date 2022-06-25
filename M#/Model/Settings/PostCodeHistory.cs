using MSharp;

namespace Domain
{
    public class PostCodeHistory : EntityType
    {
        public PostCodeHistory()
        {
            String("PostCode").Mandatory();
            BigString("Result");
            DateTime("Added on").Mandatory();
        }
    }
}
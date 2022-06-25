using MSharp;

namespace Domain
{
    class LrnToDriverMessage : EntityType
    {
        public LrnToDriverMessage()
        {
            String("Mobile number").Accepts(TextPattern.IntegerText_digitsOnly);            
            Associate<Country>("Country");
            DateTime("Date").Default(cs("LocalTime.Now")).Mandatory();
            Bool("Sent").Mandatory();
            Bool("Expired").Mandatory();
            String("Response Code");
        }
    }
}
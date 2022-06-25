using MSharp;

namespace Domain
{
    class VATRate : EntityType
    {
        public VATRate()
        {
            DateTime("Valid From").HasTime(false).Mandatory();
            String("Name").Mandatory();
            Int("RateS").Mandatory().IsPercentage();
            Int("RateZ").Mandatory().IsPercentage();
            Int("RateA").Mandatory().IsPercentage();
            BigString("Statement");
            this.Archivable();
        }
    }
}
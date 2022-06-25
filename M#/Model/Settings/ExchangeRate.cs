using MSharp;

namespace Domain
{
    class ExchangeRate : EntityType
    {
        public ExchangeRate()
        {
            Associate<Currency>("Currency").Mandatory().DatabaseIndex();
            Date("From").Mandatory();
            Date("To").Mandatory();
            Date("Updated date");

            String("Country/Territory").Mandatory();
            Decimal("Rate").Mandatory().Scale(4);
            Associate<ExchangeRateFile>("File").Mandatory();
            DefaultToString = Property("Currency");
        }
    }
}
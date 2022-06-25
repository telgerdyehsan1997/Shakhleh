using MSharp;

namespace Domain
{
    class ExchangeRateFile : EntityType
    {
        public ExchangeRateFile()
        {
            String("Name").Mandatory();
            String("URL");
            SecureFile("File");
            DateTime("Submit date").Mandatory().Default(cs("LocalTime.Now"));
        }
    }
}
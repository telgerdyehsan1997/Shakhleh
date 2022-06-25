using MSharp;

namespace Domain
{
    public class Settings : EntityType
    {
        public Settings()
        {
            PluralName("Settings").TableName("Settings").InstanceAccessors("Current");

            String("Name").Mandatory();
            Int("Password reset ticket expiry minutes").Mandatory();
            Int("Cache version").Mandatory();
            Int("Into UK tracking number").Default("1").DefaultFormatString("{0:000000}");
            Int("Out of UK tracking number").Default("1").DefaultFormatString("{0:000000}");
            DateTime("Date suffixes were last reset");
            Associate<Company>("Default declarant");

            String("IntoUK Document Code");
            String("IntoUK Document Status");
            String("IntoUK Document Reference");

            Int("Time until cleared");

            Bool("Send NCTS message via ASM").Mandatory(value: false);

            BigString("Bankers");
            String("Sort code");
            String("Account no");
            String("IBAN");
            String("BIC");
            Bool("Activate UCN");
            String("Channelports CFSP shipment number").Unique();
            Int("NCTS high value threshold");

            Int("CFSP shipment number").Default("1").DefaultFormatString("{0:00000}");

            String("CFSP Monthly Report Recipients");
            String("CP CFSP Month End Email Address");

        }
    }
}
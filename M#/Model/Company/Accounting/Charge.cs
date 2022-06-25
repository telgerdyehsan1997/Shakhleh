using MSharp;

namespace Domain
{
    class Charge : EntityType
    {
        public Charge()
        {
            this.Archivable();

            String("Name").Mandatory();

            Date("Valid from");
            Associate<Company>("Company");
            Associate<ChargeCurrencyOption>("Currency").Mandatory();

            Bool("Is Default").Mandatory();
            Bool("Is Yearly").Mandatory()
                .TrueText("Yearly")
                .FalseText("Monthly");

            Decimal("License Fee").Mandatory();
            Int("Free Consignments").Mandatory().Title("Free Consignments");
            Decimal("Price Per Additional Consignment").Mandatory();
            Decimal("Price Per Commodity").Mandatory().Default("0");
        }
    }
}
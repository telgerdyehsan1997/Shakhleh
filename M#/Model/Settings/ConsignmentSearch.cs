using MSharp;

namespace Domain
{
    class ConsignmentSearch : EntityType
    {
        public ConsignmentSearch()
        {

            String("Consignment number");
            String("LRN");
            String("EADMRN");

            Associate<Company>("UK trader");
            Associate<Company>("Declarant");
            Associate<Company>("Guarantor");

            Double("Total gross weight min").Max(99999999999.99).Scale(2);
            Double("Total gross weight max").Max(99999999999.99).Scale(2);

            String("Invoice number").Max(35);
            Decimal("Total value min").Max(9999999999.99).Scale(2);
            Decimal("Total value max").Max(9999999999.99).Scale(2);

            String("Commodity Code");
            String("UCR");
            Associate<Company>("Partner");
            Int("Total packages min").Min(1);
            Int("Total packages max").Min(1);

            Double("Total net weight min").Max(99999999999.99).Scale(3);
            Double("Total net weight max").Max(99999999999.99).Scale(3);

            Associate<Currency>("Invoice currency");
            Associate<Progress>("Progress");

            Associate<User>("User").Mandatory();
            Bool("Is Ncts").Mandatory().Default("c#: false");
            Bool("Is IntoUk").Mandatory().Default("c#: false");

            DateTime("DateCons");
            DateTime("DateMaxCons");
            DateTime("ExpectedDateCons");
            DateTime("ExpectedDateMaxCons");


        }
    }
}

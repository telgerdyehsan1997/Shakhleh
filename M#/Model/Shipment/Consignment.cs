using MSharp;

namespace Domain
{
    class Consignment : EntityType
    {
        public Consignment()
        {
            this.Archivable();

            Int("Id number");
            Associate<Shipment>("Shipment").Mandatory().DatabaseIndex();
            DefaultToString = String("Consignment number").Mandatory().Unique().DatabaseIndex();
            Associate<Company>("UK trader").Mandatory().DatabaseIndex();
            Associate<Company>("Partner").Mandatory().DatabaseIndex();
            Associate<Company>("Declarant").Mandatory().DatabaseIndex();
            Associate<Company>("Guarantor").DatabaseIndex();
            Int("Total packages").Mandatory().Min(1);
            Double("Total gross weight").Max(99999999999.99).Scale(2);
            Double("Total net weight").Max(99999999999.99).Scale(3);
          
            Associate<Currency>("Invoice currency");
            Decimal("Total value").Max(9999999999.99).Scale(2);
            String("UCR").Mandatory().Unique();
            InverseAssociate<Commodity>("Commodities", "Consignment");
            Bool("Use special CPC").Mandatory().Default("false");
            Associate<CPC>("Special CPC");
            Bool("Is importer paying the freight").Mandatory().Default("false");
            Associate<Currency>("Freight currency");
            Money("Freight amount").IsCurrency(false);
            Bool("Is importer paying insurance charges").Mandatory().Default("false");
            Associate<Currency>("Insurance currency");
            Money("Insurance amount").IsCurrency(false);
            Associate<Progress>("Progress").Mandatory().Default("c#:Progress.Draft").DatabaseIndex();
            String("EAD MRN");
            InverseAssociate<EadTransactionLog>("Logs", "Consignment");
            Double("Box 63 non_EU").Max(100).IsPercentage();
            Double("Box 68 EU").IsPercentage();
            InverseAssociate<ConsignmentDocument>("Documents", "Consignment");
            Bool("NeedToSendAmendment").Mandatory();

            // For NCTS Shipment Out API
            String("LRN").Unique().Max(22);
            Bool("Only 1 Commodity").Mandatory();
            Associate<TermOfSale>("Terms of Sale");
            Associate<DDPType>("DDP options");
            Double("VAT");
            String("Customer status label")
                .CalculatedFrom("GetCustomerStatusLabel()");
            String("Admin status label")
                .CalculatedFrom("GetAdminStatusLabel()");

            String("Route");
            DateTime("LastStatusUpdate");

            String("Entry reference").Unique();
            DateTime("Cleared date");
            Bool("Is Invoiced").Mandatory();
            DateTime("Invoice date");

            Int("Transmit retries").Mandatory();
            Bool("Has prefrence for subdivision");
            Decimal("Invoice amount");
            String("Correlation id");
            String("ICS MRN number");
            Bool("ICS api processed").Mandatory();
            Bool("CFSP UCR Updated").Mandatory();

            String("UCN");
            String("CFSP Shipment Number");

            Bool("HasFullCustomDetails")
                .Mandatory()
                .Default("true");

            InverseAssociate<ConsignmentProgressHistory>("ProgressHistory", "Consignment");
            Associate<GuaranteeLength>("Guarantee length");
            Associate<PortType>("IntoUKType");

            String("Sequence number").Max(18);
            Bool("Use EIDR").Mandatory();

            Bool("Has shipment File Delivery").Mandatory();

            String("Invoice number").Max(35).Mandatory();
            String("Second invoice number").Max(35);
            String("Third invoice number").Max(35);
            String("Fourth invoice number").Max(35);

            Bool("Has cfsp report generate").Mandatory();

            String("Total vat");
            String("Total other");
            String("Total vat paid");
            String("Total duty");
        }
    }
}
using MSharp;

namespace Domain
{
    class Commodity : EntityType
    {
        public Commodity()
        {
            Associate<Consignment>("Consignment").OnDelete(CascadeAction.CascadeDelete).DatabaseIndex();
            Associate<Product>("Product").DatabaseIndex();
            Associate<VATType>("VAT");
            Double("Gross weight").Scale(2);
            Double("Net weight").Scale(3);
            Double("Second quantity").Scale(3);
            Double("Third quantity").Scale(3);
            Decimal("Value").Scale(2);
            Int("Number of packages");
            Associate<Country>("Country of destination").Mandatory().DatabaseIndex();
            Bool("Has preference").TrueText("Yes").FalseText("No");
            Associate<PreferenceType>("Preference type");
            String("Preference certificate number");
            Bool("Goods licencable").Mandatory();
            Associate<Licence>("Licence type");
            String("Licence number");
            DateTime("SubmitDate").Mandatory().Default(cs("LocalTime.Now"));
            Associate<LicenceStatusCode>("Licence status code");
            Int("Quantity").Min(1);
            String("RPTID code");

            Bool("Is duty payable").Mandatory();
            Bool("Are the good hazardous?").Mandatory();
            String("UN Code");
            String("Description of goods");

            Bool("Need PHYTO Document Number");
            Bool("Need IPAFF Document Number").Mandatory();
            String("PHYTO Document Number");
            String("IPAFF Document Number");
            Bool("Is apply for all");
            Bool("Have health certificate number").Mandatory();
            InverseAssociate<HealthCertificateNumber>("Health certificate", "Commodity");
        }
    }
}
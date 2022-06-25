using MSharp;

namespace Domain
{
    class Product : EntityType
    {
        public Product()
        {
            this.Archivable();

            String("Code").Unique().Mandatory();
            String("Name").Mandatory();
            Associate<CommodityCode>("Commodity code").Mandatory();
            String("Additional code").MinLength(4).Max(4).Accepts(TextPattern.IntegerText_digitsOnly);
            String("Quota").MinLength(6).Max(6).Accepts(TextPattern.IntegerText_digitsOnly);
            Associate<VATType>("VAT");
            Bool("Licenced").Mandatory(value: false);
            String("Export licence");
            String("UN Dangerous goods code").MinLength(4).Max(4).Accepts(TextPattern.IntegerText_digitsOnly);
            Associate<Company>("Company");

            Bool("Is created from API").Mandatory().Default("false");

            ToStringExpression("Name + \" - \" + Code + \" - \" + CommodityCode");
        }
    }
}
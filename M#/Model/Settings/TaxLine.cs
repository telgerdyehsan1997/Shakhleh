using MSharp;

namespace Domain
{
    class TaxLine : EntityType
    {
        public TaxLine()
        {
            Abstract();

            String("Type").Mandatory().Calculated().Getter("TaxType + TaxTypeSuffix");
            String("Tax Type").MinLength(1).Max(1).Mandatory();
            String("Tax Type Suffix").Mandatory();
            String("Base Amount");
            String("Base Quantity");
            String("Rate");
            String("Override");
            String("Amount");
            String("MoP");
            this.Archivable();
        }
    }
}
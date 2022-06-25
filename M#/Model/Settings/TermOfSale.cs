using MSharp;

namespace Domain
{
    class TermOfSale : EntityType
    {
        public TermOfSale()
        {
            String("Name").Mandatory().Unique();
            BigString("Description");
            Bool("Box 45").Mandatory().TrueText("A0000").FalseText("B0000");
            Bool("Freight charge").Mandatory();
            Double("Value for VAT").Mandatory();
            Bool("Is DDP").Mandatory(value: false);
            //Bool("Default TCPM import").Mandatory();
            //Bool("Default TCPM export").Mandatory();
            this.Archivable();
        }
    }
}
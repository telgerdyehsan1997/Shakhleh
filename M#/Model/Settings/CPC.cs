using MSharp;

namespace Domain
{
    class CPC : EntityType
    {
        public CPC()
        {
            Associate<ShipmentType>("Type").Mandatory();
            DefaultSort = String("CPC Number").Name("Number").MinLength(7).Max(7).Mandatory();
            String("CPC description").Mandatory();
            String("Box 44");
            String("Box 47A");
            String("Box 47c1");
            String("Box 47 (c)").Name("Box47C");
            Bool("Manual").Mandatory(value: false);
            Bool("Override EORI").Mandatory(value: false);
            String("EORI");
            Bool("No tax line").Mandatory(value: false);
            ToStringExpression("Number");
            this.Archivable();

            InverseAssociate<CPCTaxLine>("Tax Lines", inverseOf: "CPC");
        }
    }
}
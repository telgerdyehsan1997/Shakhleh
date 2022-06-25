using MSharp;

namespace Domain
{
    class Port : EntityType
    {
        public Port()
        {
            String("Port name").Mandatory().Unique();
            String("Port code").Mandatory().MinLength(3).Max(3);
            Int("Transport mode").Mandatory().Default("3").Min(0).Max(9);
            Bool("Non-UK").Mandatory(value: false);
            Associate<TransitOffice>("TransitOffice").Mandatory();
            String("UKBF email").Accepts(TextPattern.EmailAddress);
            InverseAssociate<PortTypeIntoUk>("PortsIntoUk", "Port");
            String("Into UK Value").Max(1).Mandatory();
            Associate<PortType>("Out Of UK Type").Mandatory();
            String("Out of UK Value").Max(1).Mandatory();
            Associate<Country>("Country").DatabaseIndex();
            String("DTI Badge").Max(3).MinLength(3);
            String("Titled use port code").MinLength(3).Max(3);
            String("Titled location code").MinLength(3).Max(3);

            this.Archivable();
        }
    }
}
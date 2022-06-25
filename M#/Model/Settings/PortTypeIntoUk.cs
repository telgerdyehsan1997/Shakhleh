using MSharp;

namespace Domain
{
    class PortTypeIntoUk : EntityType
    {
        public PortTypeIntoUk()
        {
            Associate<Port>("Port").Mandatory();
            Associate<PortType>("Into UK Type").Mandatory();

        }
    }
}
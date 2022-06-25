using MSharp;

namespace Domain
{
    class ConsignmentDocument : EntityType
    {
        public ConsignmentDocument()
        {
            SecureFile("File").Mandatory();
            String("Name").Mandatory();
            DateTime("Date Recieved").Mandatory();
            Associate<Consignment>("Consignment").Mandatory().OnDelete(CascadeAction.CascadeDelete);
            String("Trader reference");
            Bool("Is Manual").Mandatory();
            Bool("Is Sent").Mandatory();
        }
    }
}
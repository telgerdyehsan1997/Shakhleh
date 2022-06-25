using MSharp;

namespace Domain
{
    class ConsignmentProgressHistory : EntityType
    {
        public ConsignmentProgressHistory()
        {
            DateTime("Date").Mandatory().Default(cs("LocalTime.Now"));
            Associate<Consignment>("Consignment").Mandatory();
            Associate<Progress>("Progress").Mandatory();
            Associate<User>("User");
        }
    }
}
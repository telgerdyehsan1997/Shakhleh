using MSharp;

namespace Domain
{
    class ReportErrorLog : EntityType
    {
        public ReportErrorLog()
        {
            DateTime("Recieved Date").Mandatory().Default(cs("LocalTime.Now"));
            String("FileName").Mandatory().Unique();
            String("Error").Max(int.MaxValue).Mandatory();
            String("StackTrace").Max(int.MaxValue).Mandatory(false);
            String("Location").Mandatory();
            Associate<Shipment>("Shipment");
        }
    }
}
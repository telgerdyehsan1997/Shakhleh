using MSharp;

namespace Domain
{
    class InvoiceJobStatus : EntityType
    {
        public InvoiceJobStatus()
        {
            InstanceAccessors("NotStarted" , "InProgress" , "Done" , "Error");
            String("Name").Mandatory().Unique();
        }
    }
}
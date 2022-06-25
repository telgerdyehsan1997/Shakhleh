using MSharp;

namespace Domain
{
    class GVMSStatus : EntityType
    {
        public GVMSStatus()
        {         
            IsEnumReference();
            InstanceAccessors("Transmitted",
                              "Pending",
                              "Rejected",
                              "Successful"
                              );
            LogEvents(false);

            String("System Name").Mandatory().Unique();          
        }
    }
}
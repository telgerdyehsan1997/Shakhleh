using MSharp;

namespace Domain
{
    class Progress : EntityType
    {
        public Progress()
        {
            TableName("Progress");
            IsEnumReference();
            InstanceAccessors("Draft",
                              "ReadyToTransmit",
                              "ReadyToTransmitAPI",
                              "ASMAccept",
                              "ASMReject",
                              "AwaitingArrival",
                              "AwaitingDeparture",
                              "ProcessingErrorArrival",
                              "ProcessingErrorDeparture",
                              "Arrived",
                              "WithCustoms",
                              "QueriedArrived",
                              "QueriedWithCustoms",
                              "Cleared",
                              "Cancelled",
                              "ManualGenereal",
                              "ManualRoute",
                              "ManualCPC",
                              "ManualLicense",
                              "ManualGenerealASMAccepted",
                              "ManualRouteASMAccepted",
                              "ManualCPCASMAccepted",
                              "ManualLicenseASMAccepted",
                              "ManualGenerealASMRejected",
                              "ManualRouteASMRejected",
                              "ManualCPCASMRejected",
                              "ManualLicenseASMRejected",
                              "InternalError",
                              "Partial",
                              "WithImporter",
                              "DutyPayment",
                              "EntryControlled",
                              "ManualQuota",
                              "LeftCountry"
                              );
            LogEvents(false);

            String("System Name").Mandatory().Unique();
            String("Client Display").Mandatory();
            String("Admin Display").Mandatory();
            Int("Weight").Mandatory();
            Bool("IsManual").Mandatory();
            Bool("AdminEditable").Mandatory();
            Bool("CustomerEditable").Mandatory();
            Bool("Recieve email notification customer").Mandatory();
            Bool("Recieve email notification channelport").Mandatory();
            Bool("Do not recieve email notification customer").Mandatory();
            Bool("Do not recieve email notification channelport").Mandatory();
        }
    }
}
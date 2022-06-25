using MSharp;

namespace Domain
{
    public class ImportStatus : EntityType
    {
        public ImportStatus()
        {
            InstanceAccessors("Failed", "Pending", "Processing", "Successful", "Partial success")
         .IsEnumReference()
                .LogEvents(false);

            String("Name").Mandatory();
        }
    }
}
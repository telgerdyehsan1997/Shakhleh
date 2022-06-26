using MSharp;

namespace Domain
{
    public class ImportQueueItem : MSharp.EntityType
    {
        public ImportQueueItem()
        {
            LogEvents(false);

            SecureFile("Import file").Name("File").Mandatory().ValidExtensions(".csv");
            DateTime("Date").Name("UploadDate").Mandatory().Default("c#:LocalTime.Now");
            Bool("Is Archive").Mandatory();
            Associate<ImportStatus>("Import status").Name("Status").Mandatory().Default("Pending");
            InverseAssociate<ImportError>("Errors", inverseOf: "ImportQueueItem");
            Associate<ImportType>("Type");
        }
    }
}
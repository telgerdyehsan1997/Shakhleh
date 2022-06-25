using MSharp;

namespace Domain
{
    public class ImportError : EntityType
    {
        public ImportError()
        {
            LogEvents(false);

            Associate<ImportQueueItem>("Import queue item").Mandatory().DatabaseIndex();
            BigString("Error reason").Mandatory();
            Int("Line number");
        }
    }
}
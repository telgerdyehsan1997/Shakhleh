using MSharp;

namespace Domain
{
    class TransitOfficeFile : EntityType
    {
        public TransitOfficeFile()
        {
            SecureFile("File").Mandatory().ValidExtensions(".csv"); ;
            DateTime("Date").Mandatory().Default("c#:LocalTime.Now");
            InverseAssociate<TransitOfficeFileError>("Errors", "TransitOfficeFile");
            Associate<TransitOfficeFileStatus>("Status").Mandatory().Default("c#:TransitOfficeFileStatus.Failed");
        }
    }
}
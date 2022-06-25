using MSharp;

namespace Domain
{
    class TransitOfficeFileError : EntityType
    {
        public TransitOfficeFileError()
        {
            Int("Row number");
            BigString("Error reason").Mandatory();
            Associate<TransitOfficeFile>("Transit Office File").DatabaseIndex();
        }
    }
}
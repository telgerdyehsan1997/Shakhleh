using MSharp;

namespace Domain
{
    class ExchequerCode : EntityType
    {
        public ExchequerCode()
        {
            String("Nominal code").Mandatory();
            String("Cost Centre").Mandatory();
            String("Department").Mandatory();
        }
    }
}
using MSharp;

namespace Domain
{
    class TransactionLog : EntityType
    {
        public TransactionLog()
        {
            Abstract();
            Date("Date").Mandatory().Default("c#:LocalTime.Now");
            Int("Type").CSharpTypeName("LogType").Mandatory()
                .Attributes("[EnumDataConverter(\"Domain.LogType\")]");
            SecureFile("File").Mandatory();
        }
    }
}
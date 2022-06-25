using MSharp;

namespace Domain
{
    class MFAMessage : EntityType
    {
        public MFAMessage()
        {
            Associate<User>("User").Mandatory();
            String("MFA Key").Max(50).Mandatory();
            String("To").Max(50).Mandatory();
            DateTime("Date").Default(cs("LocalTime.Now")).Mandatory();
            Bool("Sent").Mandatory();
            Bool("Expired").Mandatory();
            String("Response Code");
        }
    }
}
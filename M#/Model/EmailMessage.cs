using MSharp;

namespace Domain
{
    public class EmailMessage : EntityType
    {
        public EmailMessage()
        {
            Cachable(false)
                 .SoftDelete()
                .Implements("Olive.Email.IEmailMessage");

            BigString("Body").Mandatory();
            DateTime("Sendable date").Mandatory().Default("c#:LocalTime.Now").DefaultFormatString("g");
            Bool("Html").Mandatory();
            String("From address");
            String("From name");
            String("Reply to address");
            String("Reply to name");
            String("Subject").Mandatory();
            String("To").Mandatory();
            BigString("Attachments");
            String("Bcc").Max(int.MaxValue);
            String("Cc").Max(int.MaxValue);
            Int("Retries").Mandatory();
            String("VCalendar view");
            Bool("Enable ssl");
            String("Username");
            String("Password");
            String("Smtp hHost");
            Int("Smtp port");
        }
    }
}

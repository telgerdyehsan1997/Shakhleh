using MSharp;

namespace Domain
{
    class BroadcastClientsMessage : EntityType
    {
        public BroadcastClientsMessage()
        {
            Associate<BroadcastMessage>("Message")
                .Mandatory();

            Associate<Person>("User")
                .Mandatory();

            Associate<Company>("Company");

            DateTime("Date recieved")
                .Mandatory()
                .Default("c#: LocalTime.Now")
                .DatabaseIndex();

            Bool("Has read")
                .Mandatory()
                .TrueText("Read")
                .FalseText("Unread")
                .NullText("All");
        }
    }
}
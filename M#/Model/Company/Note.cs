using MSharp;

namespace Domain
{
    class Note : EntityType
    {
        public Note()
        {
            DateTime("Date/time").Name("DateAndtime").Mandatory().Default("c#:LocalTime.Now");
            Associate<User>("Added by");
            BigString("Description").Mandatory();
            Associate<Company>("Company").DatabaseIndex();
        }
    }
}

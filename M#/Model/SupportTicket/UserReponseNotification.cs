using MSharp;

namespace Domain
{
    class UserReponseNotification : EntityType
    {
        public UserReponseNotification()
        {
            Associate<Response>("Response").Mandatory();
            Associate<User>("User").Mandatory().DatabaseIndex();
            Bool("Has seen").Mandatory().Default("false");
            DateTime("Date").Default("c#: LocalTime.Now").DatabaseIndex();
        }
    }
}
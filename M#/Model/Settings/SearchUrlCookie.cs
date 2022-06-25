using MSharp;

namespace Domain
{
    class SearchUrlCookie : EntityType
    {
        public SearchUrlCookie()
        {
            String("Url").Mandatory().Max(20000);
            Associate<User>("User").Mandatory();
            Bool("Is Ncts").Mandatory().Default("c#: false");
            Bool("Is IntoUk").Mandatory().Default("c#: false");

        }
    }
}

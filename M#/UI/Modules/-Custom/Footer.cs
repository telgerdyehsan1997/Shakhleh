using Domain;
using MSharp;

namespace Modules
{
    public class Footer : GenericModule
    {
        const string DEVELOPER = "http://www.geeks.ltd.uk";
        const string LINKED_IN = "http://www.linkedin.com/company/geeks-ltd";
        const string EMAIL = "http://www.geeks.ltd.uk/software-development-quote.html";
        const string TWITTER = "https://twitter.com/GeeksLtd";
        const string FACEBOOK = "https://www.facebook.com/geeksltd";

        public Footer()
        {
            IsInUse().IsViewComponent()
                .Using("Olive.Security")
                .RootCssClass("website-footer")
                .Markup(@"");


        }
    }
}
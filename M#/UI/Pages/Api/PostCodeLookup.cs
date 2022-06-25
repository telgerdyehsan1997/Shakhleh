using MSharp;

namespace Api
{
    public class PostCodeLookupPage : RootPage
    {
        public PostCodeLookupPage()
        {
            Roles(AppRole.Admin, AppRole.Customer);
            Add<Modules.PostCodeLookupApiModule>();
        }
    }
}
using MSharp;

namespace Share.Utils
{
    class AddCompanyPage : RootPage
    {
        public AddCompanyPage()
        {
            Layout(Layouts.FrontEndModal);
            Add<Modules.AddCompany>();
        }
    }
}
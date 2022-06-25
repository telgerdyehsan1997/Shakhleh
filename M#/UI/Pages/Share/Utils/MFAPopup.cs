using MSharp;

namespace Share.Utils
{
    class MFAPopUpPage : RootPage
    {
        public MFAPopUpPage()
        {
            Layout(Layouts.FrontEndModal);
            Add<Modules.MFAForm>();
        }
    }
}
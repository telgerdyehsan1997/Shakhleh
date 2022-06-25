using MSharp;

namespace Share.Utils
{
    class AddContactPage : RootPage
    {
        public AddContactPage()
        {
            Layout(Layouts.FrontEndModal);
            Add<Modules.ShipmentContactForm>();
        }
    }
}

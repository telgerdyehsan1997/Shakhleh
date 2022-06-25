using MSharp;

namespace Admin.Carrier
{
    class AddCarrierPage : RootPage
    {
        public AddCarrierPage()
        {
            Layout(Layouts.FrontEndModal);
            Add<Modules.AddCarrierForm>();
        }
    }
}
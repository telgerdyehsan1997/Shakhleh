using MSharp;

namespace Share.Shipment
{
    class EORIValidatorPage : RootPage
    {
        public EORIValidatorPage()
        {
            Layout(Layouts.FrontEndModal);
            Add<Modules.EORIValidatorForm>();
        }
    }
}
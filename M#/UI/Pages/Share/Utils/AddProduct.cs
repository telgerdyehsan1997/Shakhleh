using MSharp;

namespace Share.Utils
{
    class AddProductPage : RootPage
    {
        public AddProductPage()
        {
            Layout(Layouts.FrontEndModal);
            Add<Modules.CommodityProductForm>();
        }
    }
}

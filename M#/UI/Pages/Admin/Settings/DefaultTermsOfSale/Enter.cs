using MSharp;

namespace Admin.Settings.DefaultTermsOfSale
{
    class EnterPage : SubPage<Admin.Settings.DefaultTermsOfSalePage>
    {
        public EnterPage()
        {
            Add<Modules.TermsOfSaleForm>();
        }
    }
}

using MSharp;

namespace Share.Commodities
{
    class InvoiceDeclarationRulesPage : RootPage
    {
        public InvoiceDeclarationRulesPage()
        {
            BrowserTitle("Shipments> Consignments > Commodities");
            Layout(Layouts.FrontEndModal);
            Roles(AppRole.Admin, AppRole.Customer);
            Add<Modules.InvoiceDeclarationRulesView>();
        }
    }
}
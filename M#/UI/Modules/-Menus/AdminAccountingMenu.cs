using MSharp;

namespace Modules
{
    class AdminAccountingMenu : MenuModule
    {
        public AdminAccountingMenu()
        {
            IsViewComponent().RootCssClass("navbar navbar-light").UlCssClass("nav flex-column w-100");
            Item("Bank Details").OnClick(x => x.Go<Admin.Accounting.BankDetailsPage>());

            Item("Default Licenses").OnClick(x => x.Go<Admin.Accounting.DefaultLicensesPage>());
            Item("Exchequer Codes").OnClick(x => x.Go<Admin.Accounting.ExchequerCodesPage>());
            Item("Invoices").OnClick(x => x.Go<Admin.Accounting.InvoicesPage>());
            Item("VAT Rates").OnClick(x => x.Go<Admin.Accounting.VATRatesPage>());
        }
    }
}
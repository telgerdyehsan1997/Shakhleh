using MSharp;
using Domain;

namespace Modules
{
    public class AdminSettingsMenu : MenuModule
    {
        public AdminSettingsMenu()
        {
            IsViewComponent().RootCssClass("navbar navbar-light").UlCssClass("nav flex-column w-100");

            Item("Ancillaries")
                .OnClick(x => x.Go<Admin.Settings.AncillariesPage>());

            Item("Authorised Locations")
                .OnClick(x => x.Go<Admin.Settings.AuthorisedLocationsPage>());

            Item("ASM File Error Log")
                .OnClick(x => x.Go<Admin.Settings.FileErroLogPage>());

            Item("Banner Message")
               .OnClick(x => x.Go<Admin.Dashboard.BannerMessageListPage>());

            Item("Carriers")
               .OnClick(x => x.Go<Admin.CarriersPage>());

            Item("Countries")
                .OnClick(x => x.Go<Admin.Settings.CountriesPage>());
          
            Item("Commodity Codes")
                .OnClick(x => x.Go<Admin.Settings.CommodityCodesPage>());

            Item("Commodity Code Imports")
                .OnClick(x => x.Go<Admin.Settings.ImportPage>());

            Item("CPC")
                .OnClick(x => x.Go<Admin.Settings.CPCPage>());

            Item("Currencies")
                .OnClick(x => x.Go<Admin.Settings.CurrenciesPage>());


            Item("Default Terms of Sale")
                .OnClick(x => x.Go<Admin.Settings.DefaultTermsOfSalePage>());

            Item("Default Tax Lines")
                .OnClick(x => x.Go<Admin.Settings.DefaultTaxLinePage>());

            Item("Email Templates")
                .OnClick(x => x.Go<Admin.Settings.EmailTemplatesPage>());

            Item("Exchange Rates")
                .OnClick(x => x.Go<Admin.Settings.ExchangeRateFilePage>());

            Item("Global Settings")
                .OnClick(x => x.Go<Admin.Settings.GlobalPage>());

            Item("Health Certificate")
                .OnClick(x => x.Go<Admin.Settings.HealthCertificateListPage>());

            Item("Licences")
                .OnClick(x => x.Go<Admin.Settings.LicencesPage>());

            Item("Offices of Transit")
                .OnClick(x => x.Go<Admin.Settings.TransitOfficesPage>());

            Item("Ports")
                .OnClick(x => x.Go<Admin.Settings.PortsPage>());

            Item("Payment Types")
                .OnClick(x => x.Go<Admin.Settings.PaymentTypePage>());

            Item("Routes").OnClick(x => x.Go<Admin.Settings.RoutesPage>());

            Item("Routing Itineraries Import")
                .OnClick(x => x.Go<Admin.Settings.RoutingItinerariesImportPage>());

            Item("Second Quantity Descriptions")
                .OnClick(x => x.Go<Admin.Settings.SecondQuantityDescriptionsPage>());

            var emailmenu = Item("Status Email Notifications")
                             .CssClass("has-submenu");

            emailmenu.SubItem("Channelports")
                .OnClick(x => x.Go<Admin.Settings.StatusEmailNotificationsPage>());
            emailmenu.SubItem("Customer")
                .OnClick(x => x.Go<Admin.Settings.StatusEmailNotificationsCustomerPage>());


            Item("Users")
               .OnClick(x => x.Go<Admin.Settings.UsersPage>());

            Item("UN Codes")
                .OnClick(x => x.Go<Admin.Settings.UNCodesPage>());

            Item("UN Codes Imports")
                .OnClick(x => x.Go<Admin.Settings.UNCodeImportsPage>());

            Item("Row Quotas")
                .OnClick(x => x.Go<Admin.Settings.RowQuotasPage>());

            Item("Row Quotas Imports")
                .OnClick(x => x.Go<Admin.Settings.RowQuotaImportsPage>());
        }
    }
}
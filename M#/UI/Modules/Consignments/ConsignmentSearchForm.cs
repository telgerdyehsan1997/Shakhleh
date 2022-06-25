using Domain;
using MSharp;

namespace Modules
{
    class ConsignmentSearchForm : FormModule<Domain.ConsignmentSearch>
    {
        public ConsignmentSearchForm()
        {
            HeaderText("Consignment- Into/Out of UK");

            Field(x => x.ConsignmentNumber);
            Field(x => x.UKTrader).DataSource("await (info.CompanyUser != null ?  info.CompanyUser.Company.GetTraderAssociatedCompanies() :  new Company().GetTraderAssociatedCompanies())").AsAutoComplete();
            Field(x => x.Declarant).DataSource("await (info.CompanyUser != null ?  info.CompanyUser.Company.GetDeclarentAssociatedCompanies() : new Company().GetDeclarentAssociatedCompanies())").AsAutoComplete();

            var weight = Box("Total gross weight", BoxTemplate.HeaderBox);

            Field(x => x.TotalGrossWeightMin)
                .Label("Min")
                .Box(weight);

            Field(x => x.TotalGrossWeightMax)
                .Label("Max")
                .Box(weight);

            Field(x => x.InvoiceNumber);

            var totalValue = Box("Total value", BoxTemplate.HeaderBox);

            Field(x => x.TotalValueMin)
                .Label("Min")
                .Box(totalValue);

            Field(x => x.TotalValueMax)
                .Label("Max")
                .Box(totalValue);


            Field(x => x.CommodityCode);

            Field(x => x.UCR)
                .Label("Declaration Unique Consignment Reference (DUCR)");

            Field(x => x.Partner).DataSource("await (info.CompanyUser != null ?  info.CompanyUser.Company.GetPartnerAssociatedCompanies() : new Company().GetPartnerAssociatedCompanies())").AsAutoComplete();


            var totalPackages = Box("Total packages", BoxTemplate.HeaderBox);

            Field(x => x.TotalPackagesMin)
                .Label("Min")
                .Box(totalPackages);

            Field(x => x.TotalPackagesMax)
                .Label("Max")
                .Box(totalPackages);

            var totalNetweight = Box("Total net weight", BoxTemplate.HeaderBox);

            Field(x => x.TotalNetWeightMin)
                .Label("Min")
                .Box(totalNetweight);

            Field(x => x.TotalNetWeightMax)
                .Label("Max")
                .Box(totalNetweight);


            Field(x => x.InvoiceCurrency).AsAutoComplete();
            Field(x => x.Progress).AsAutoComplete();


            Field(x => x.DateCons).ControlCssClass("text-hidden");
            Field(x => x.DateMaxCons).ControlCssClass("text-hidden");
            Field(x => x.ExpectedDateCons).ControlCssClass("text-hidden");
            Field(x => x.ExpectedDateMaxCons).ControlCssClass("text-hidden");

            AutoSet(x => x.IsNcts).Value("false");
            AutoSet(x => x.IsIntoUk).Value("true");

            AutoSet(x => x.User).Value("Context.Current.User().ExtractUser<User>()");
            Button("Search").Icon(FA.Search)
               .OnClick(x =>
               {
                   x.SaveInDatabase();
                   x.CSharp(@"string url = ConsignmentSearch.CreateCookieHistory(info.Item.ID.ToString());
                             Redirect(url);");
                   x.CloseModal(Refresh.Ajax);
               }).IsDefault(true);

            Button("Cancel").OnClick(x => x.CloseModal());
            Button("Clear").OnClick(x =>
            {
                x.CSharp("await ConsignmentSearch.ClearCookieHistory(isNcts: false, isIntoUk: true );");
                x.Go<Admin.ShipmentPage>();
                x.CloseModal();

            });

            ViewModelProperty<CompanyUser>("CompanyUser").Code("info.CompanyUser = await GetCompanyUser();");

            OnBeforeSave("No search iteams").Code(@"if (info.Item.CheckConsigmentFiled(item))
                    {
                        scope.Complete();
                        await ConsignmentSearch.ClearCookieHistory(isNcts: false, isIntoUk: true );
                        return true;
                    }");


            LoadJavascriptModule("scripts/components/consignment-search-form.js");

        }
    }
}
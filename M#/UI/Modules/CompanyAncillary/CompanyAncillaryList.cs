using MSharp;

namespace Modules
{
    class CompanyAncillaries : BaseListModule<Domain.Ancillary>
    {
        public CompanyAncillaries()
        {
            HeaderText("Ancillaries").DataSource("await info.Company.Ancillaries.GetList()");

            Search(GeneralSearch.AllFields).Label("Find");
            this.ArchiveSearch();
            SearchButton("Search").Icon(FA.Search).OnClick(x => x.ReturnView());

            Column(x => x.Country.Code).LabelText("Country");
            Column(x => x.FreightChargePerTonne);
            Column(x => x.FullLoadFreightCharge);
            Column(x => x.ValueForVAT);


            ButtonColumn("Edit").HeaderText("Edit").GridColumnCssClass("actions").Icon(FA.Edit)
                .OnClick(x => x.Go<Admin.Company.Ancillary.EnterPage>().Send("item", "item.ID").Send("company", "info.Company.ID").SendReturnUrl());

            this.ArchiveButtonColumn("Ancillary").ColumnVisibleIf(AppRole.SuperAdmin);

            Button("Add Bespoke Rate")
                .OnClick(x => x.Go<Admin.Company.Ancillary.EnterPage>().Send("company", "info.Company.ID").SendReturnUrl());

            //Button("Use Bespoke Rates").IsDefault().VisibleIf("await info.Company.Ancillaries.GetList().None()")
            //    .OnClick(x =>
            //    {
            //        x.CSharp("await info.Company.GenerateBespokeRates();");
            //        x.RefreshPage();
            //    });

            ViewModelProperty<Domain.Company>("Company").FromRequestParam("company");
        }
    }
}
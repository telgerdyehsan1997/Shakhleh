using MSharp;

namespace Modules
{
    class VATRatesList : MFABaseList<Domain.VATRate>
    {
        public VATRatesList()
        {
            HeaderText("VAT Rates");
            SortingStatement("ValidFrom DESC");

            this.ArchiveSearch().DefaultValueExpression("false");
            Search(GeneralSearch.AllFields).Label("Find:");
            SearchButton("Search").IsDefault().Icon(FA.Search)
                .OnClick(x => x.ReturnView());

            Column(x => x.ValidFrom);
            Column(x => x.Name).LabelText("VAT Rate Name");
            Column(x => x.RateZ).LabelText("VAT Rate for Z");
            Column(x => x.RateS).LabelText("VAT Rate for S");
            Column(x => x.RateA).LabelText("VAT Rate for A");
            Column(x => x.Statement);

            ButtonColumn("Edit").HeaderText("Edit").GridColumnCssClass("actions").Icon(FA.Edit)
            .OnClick(x => x.Go<Admin.Accounting.VATRates.EnterPage>().Send("item", "item.ID").SendReturnUrl());

            this.ArchiveButtonColumn("VAT Rate");

            Button("New VAT rate").Icon(FA.Plus)
            .OnClick(x => x.Go<Admin.Accounting.VATRates.EnterPage>().SendReturnUrl());
        }
    }
}
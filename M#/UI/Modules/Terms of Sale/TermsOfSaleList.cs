using MSharp;

namespace Modules
{
    class TermsOfSaleList : BaseListModule<Domain.TermOfSale>
    {
        public TermsOfSaleList()
        {
            HeaderText("Default Terms of Sale");
            this.ArchiveSearch();
            SearchButton("Search")
                .IsDefault()
                .OnClick(x => x.ReturnView());

            Column(x => x.Name).LabelText("Terms of sale");
            Column(x => x.Description);
            Column(x => x.Box45).DisplayExpression(@"@(item.Box45 ? ""A0000"" : ""B0000"")");
            Column(x => x.FreightCharge).DisplayExpression(@"@(item.FreightCharge ? ""Yes"" : ""No"")");
            Column(x => x.ValueForVAT);
            //Column(x => x.DefaultTCPMImport).DisplayExpression(@"@(item.ValueForVAT ? ""Yes"" : ""No"")");
            //Column(x => x.DefaultTCPMExport).DisplayExpression(@"@(item.ValueForVAT ? ""Yes"" : ""No"")");

            ButtonColumn("Edit").HeaderText("Edit").GridColumnCssClass("actions").Icon(FA.Edit)
                .OnClick(x => x.Go<Admin.Settings.DefaultTermsOfSale.EnterPage>().Send("item", "item.ID").SendReturnUrl());

            this.ArchiveButtonColumn();

            Button("New Terms of Sale").Icon(FA.Plus)
                .OnClick(x => x.Go<Admin.Settings.DefaultTermsOfSale.EnterPage>().SendReturnUrl());
        }
    }
}
using MSharp;

namespace Modules
{
    class CPCTaxLineList : BaseListModule<Domain.CPCTaxLine>
    {
        public CPCTaxLineList()
        {
            DataSource("info.CPC != null ? await info.CPC.TaxLines.GetList() : Enumerable.Empty<CPCTaxLine>()");
            VisibleIf("info.CPC != null");
            ViewModelProperty<Domain.CPC>("CPC").FromRequestParam("item");

            HeaderText("CPC Tax Lines").SortingStatement("item.Type");
            
            Column(x => x.Type);
            Column(x => x.BaseAmount);
            Column(x => x.BaseQuantity);
            Column(x => x.Rate);
            Column(x => x.Override);
            Column(x => x.Amount);
            Column(x => x.MoP);
            ButtonColumn("Edit").HeaderText("Edit").GridColumnCssClass("actions").Icon(FA.Edit)
                .OnClick(x => x.PopUp<Admin.Settings.CPC.TaxLine.EnterPage>().Send("item", "item.ID").SendReturnUrl());
            LinkColumn("Delete").HeaderText("Delete").CssClass("btn").GridColumnCssClass("actions").Icon(FA.Times)
                .ConfirmQuestion("Are you sure you want to delete this Tax Line?")
                .OnClick(x =>
                {
                    x.DeleteItem();
                    x.RefreshPage();
                });

            Button("New CPC Tax Line").Icon(FA.Plus)
                .OnClick(x => x.PopUp<Admin.Settings.CPC.TaxLine.EnterPage>()
                                    .Send("cpc", "info.CPC.ID")
                                    .SendReturnUrl());
        }
    }
}
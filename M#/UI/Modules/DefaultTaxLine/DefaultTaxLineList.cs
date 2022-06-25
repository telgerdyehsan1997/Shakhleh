using MSharp;

namespace Modules
{
    class DefaultTaxLineList : BaseListModule<Domain.DefaultTaxLine>
    {
        public DefaultTaxLineList()
        {
            HeaderText("Default Tax Lines").SortingStatement("item.Type");

            Search(GeneralSearch.AllFields).Label("Find:");
            Search(x => x.IsDeactivated).Label("Status").Control(ControlType.HorizontalRadioButtons).DefaultValueExpression("false");
            SearchButton("Search").Icon(FA.Search).CssClass("float-right").OnClick(x => x.ReturnView());

            Column(x => x.Type);
            Column(x => x.BaseAmount);
            Column(x => x.BaseQuantity);
            Column(x => x.Rate);
            Column(x => x.Override);
            Column(x => x.Amount);
            Column(x => x.MoP);
            ButtonColumn("Edit").HeaderText("Edit").GridColumnCssClass("actions").Icon(FA.Edit)
                .OnClick(x => x.PopUp<Admin.Settings.DefaultTaxLine.EnterPage>().Send("item", "item.ID").SendReturnUrl());
            this.ArchiveButtonColumn("CPC");

            Button("New Default Tax Line").Icon(FA.Plus)
                .OnClick(x => x.PopUp<Admin.Settings.DefaultTaxLine.EnterPage>()
                                    .SendReturnUrl());
        }
    }
}
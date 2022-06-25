using MSharp;

namespace Modules
{
    class CPCList : BaseListModule<Domain.CPC>
    {
        public CPCList()
        {
            HeaderText("CPC").SortingStatement("item.Number");

            Search(GeneralSearch.AllFields).Label("Find:");
            Search(x => x.IsDeactivated).Label("Status").Control(ControlType.HorizontalRadioButtons).DefaultValueExpression("false");
            SearchButton("Search").Icon(FA.Search).CssClass("float-right").OnClick(x => x.ReturnView());

            Column(x => x.Number);
            Column(x => x.CPCDescription);
            Column(x => x.Type);
            Column(x => x.Box44);
            Column(x => x.Manual);

            ButtonColumn("Edit").HeaderText("Edit").GridColumnCssClass("actions").Icon(FA.Edit)
                .OnClick(x => x.Go<Admin.Settings.CPC.EnterPage>().Send("item", "item.ID").SendReturnUrl());
            this.ArchiveButtonColumn("CPC");

            Button("New CPC").Icon(FA.Plus)
                .OnClick(x => x.Go<Admin.Settings.CPC.EnterPage>().SendReturnUrl());
        }
    }
}
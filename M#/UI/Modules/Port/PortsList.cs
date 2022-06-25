using MSharp;

namespace Modules
{
    class PortsList : BaseListModule<Domain.Port>
    {
        public PortsList()
        {
            HeaderText("Ports");

            Search(GeneralSearch.AllFields).Label("Find");
            Search(x => x.IsDeactivated).Label("Status").Control(ControlType.HorizontalRadioButtons).DefaultValueExpression("false");
            SearchButton("Search").Icon(FA.Search).CssClass("float-right").OnClick(x => x.ReturnView());

            Column(x => x.PortName);
            Column(x => x.TransportMode);
            Column(x => x.PortCode);
            Column(x => x.Non_UK);
            CustomColumn("NCTSCode").DisplayExpression("@item.TransitOffice?.NCTSCode").HeaderTemplate("NCTS Code");
            Column(x => x.UKBFEmail);
            Column(x => x.PortsIntoUk).DisplayExpression(@"@string.Join("" | "", await item.PortsIntoUk.GetList().Select(x => x.IntoUKType.Name).ToList())").LabelText("Into UK Type");
            Column(x => x.IntoUKValue);
            Column(x => x.OutOfUKType);
            Column(x => x.OutOfUKValue);
            Column(x => x.DTIBadge).HeaderTemplate("DTI Badge");

            ButtonColumn("Edit").HeaderText("Edit").GridColumnCssClass("actions").Icon(FA.Edit)
                .OnClick(x => x.Go<Admin.Settings.Ports.EnterPage>().Send("item", "item.ID").SendReturnUrl());
            this.ArchiveButtonColumn("Port");

            Button("New Port").Icon(FA.Plus)
                .OnClick(x => x.Go<Admin.Settings.Ports.EnterPage>().SendReturnUrl());
        }
    }
}
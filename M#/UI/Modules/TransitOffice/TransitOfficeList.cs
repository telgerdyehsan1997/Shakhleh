using MSharp;

namespace Modules
{
    class TransitOfficeList : BaseListModule<Domain.TransitOffice>
    {
        public TransitOfficeList()
        {
            HeaderText("Offices of Transit");

            Search(GeneralSearch.AllFields).Label("Find");
            Search(x => x.IsDeactivated).Label("Status").Control(ControlType.HorizontalRadioButtons).DefaultValueExpression("false");
            SearchButton("Search").Icon(FA.Search).CssClass("float-right").OnClick(x => x.ReturnView());

            Column(x => x.CountryCode);
            Column(x => x.CountryName);
            Column(x => x.NCTSCode);
            Column(x => x.UsualName);
            Column(x => x.Departure);
            Column(x => x.Destination);
            Column(x => x.Transit);
            Column(x => x.NearestOffice);
            Column(x => x.TransitOfficeAlias).LabelText("Alias");

            ButtonColumn("Edit").HeaderText("Edit").GridColumnCssClass("actions").Icon(FA.Edit)
                .OnClick(x => x.Go<Admin.Settings.TransitOffices.EnterPage>().Send("item", "item.ID").SendReturnUrl());

            this.ArchiveButtonColumn("Transit Office");

            Button("Bulk Import").OnClick(x => x.Go<Admin.Settings.TransitOffices.BulkImportPage>().SendReturnUrl());

            Button("New Office of Transit").Icon(FA.Plus)
                .OnClick(x => x.Go<Admin.Settings.TransitOffices.EnterPage>().SendReturnUrl());



        }
    }
}
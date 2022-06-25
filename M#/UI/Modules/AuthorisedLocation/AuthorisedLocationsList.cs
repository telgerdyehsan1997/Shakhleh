using MSharp;

namespace Modules
{
    class AuthorisedLocationsList : ListModule<Domain.AuthorisedLocation>
    {
        public AuthorisedLocationsList()
        {

            HeaderText("Authorised Locations");

            Search(GeneralSearch.AllFields).Label("Find");
            Search(x => x.IsDeactivated).Control(ControlType.HorizontalRadioButtons).Label("Status").DefaultValueExpression("false");
            SearchButton("Search").Icon(FA.Search).OnClick(x => x.ReturnView());

            Column(x => x.LocationName);
            Column(x => x.CustomsIdentity);
            Column(x => x.TransitOffice).LabelText("NCTS code").DisplayExpression("@item.TransitOffice.NCTSCode");
            Column(x => x.AuthorisationNumber);
            Column(x => x.EmailAddresses);
            LinkColumn("GuaranteeLength").HeaderText("Guarantee length").Text(@"@await item.GuaranteeLengths.Where(x=>!x.IsDeactivated).Count()")
              .OnClick(x =>
              {
                  x.Go<Admin.Settings.GuaranteeLengthPage>().Send("location", "item.ID").SendReturnUrl();
              });
            ButtonColumn("Edit").HeaderText("Edit").GridColumnCssClass("actions").Icon(FA.Edit)
                .OnClick(x => x.Go<Admin.Settings.AuthorisedLocations.EnterPage>().Send("item", "item.ID").SendReturnUrl());

            this.ArchiveButtonColumn();

            Button("Import")
                .OnClick(x => x.Go<Admin.Settings.AuthorisedLocations.ImportPage>().SendReturnUrl());

            Button("New Authorised location").Icon(FA.Plus)
                .OnClick(x => x.Go<Admin.Settings.AuthorisedLocations.EnterPage>().SendReturnUrl());
        }
    }
}
using MSharp;

namespace Modules
{
    class AuthorisedLocationGuaranteeLengthList : ListModule<Domain.GuaranteeLength>
    {
        public AuthorisedLocationGuaranteeLengthList()
        {
            HeaderText("Guarantee Length");

            DataSource("await info.Location.GuaranteeLengths.OrderBy(t => t.Length).GetList()");

            this.ArchiveSearch().DefaultValueExpression("false");

            Search(GeneralSearch.AllFields).Label("Find:");
            SearchButton("Search")
                .OnClick(x => x.ReturnView());

            Column(x => x.Length).HeaderTemplate("Valid for (days)");

            ButtonColumn("Edit").HeaderText("Actions").GridColumnCssClass("actions").Icon(FA.Edit)
                .OnClick(x => x.PopUp<Admin.Settings.GuaranteeLenghtEnterPage>().Send("item", "item.ID"));

            this.ArchiveButtonColumn("Guarantee Length")
                    .VisibleIf("item.Length!=8");


            ViewModelProperty<Domain.AuthorisedLocation>("Location").FromRequestParam("location");

            Button("New Guarantee Length")
                .OnClick(x => x.PopUp<Admin.Settings.GuaranteeLenghtEnterPage>().Pass("location"));
        }
    }
}
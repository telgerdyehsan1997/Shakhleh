using MSharp;

namespace Modules
{
    class LicencesList : ListModule<Domain.Licence>
    {
        public LicencesList()
        {
            HeaderText("Licences")
                .PageSize(10)
                .Sortable();

            Search(GeneralSearch.AllFields).Label("Find");
            this.ArchiveSearch();
            SearchButton("Search").Icon(FA.Search).CssClass("float-right").OnClick(x => x.ReturnView());

            Column(x => x.LicenceName);
            Column(x => x.Type);
            Column(x => x.LicenceType);
            Column(x => x.RPTID);
            Column(x => x.LicenceIdentifier);

            ButtonColumn("Edit").HeaderText("Edit").GridColumnCssClass("actions").Icon(FA.Edit)
                .OnClick(x => x.Go<Admin.Settings.Licences.EnterPage>().Send("item", "item.ID").SendReturnUrl());
            this.ArchiveButtonColumn("Licence");

            Button("New Licence").Icon(FA.Plus)
                .OnClick(x => x.Go<Admin.Settings.Licences.EnterPage>().SendReturnUrl());
        }
    }
}
using MSharp;

namespace Modules
{
    class LicenceStatusCodeList : ListModule<Domain.LicenceStatusCode>
    {
        public LicenceStatusCodeList()
        {

            HeaderText("Licence Status Codes");
            PageSize(10);
            Sortable();

            this.ArchiveSearch();
            SearchButton("Search").Icon(FA.Search).CssClass("float-right").OnClick(x => x.ReturnView());

            Column(x => x.StatusCode);
            Column(x => x.Type).DisplayExpression(cs(@"(item.IsForShipmentsInAndOutOfUK?""Both"":item.Type.ToString())"));
            Column(x => x.LicenceType);
            Column(x => x.Description);
            ButtonColumn("Edit").HeaderText("Actions").GridColumnCssClass("actions").Icon(FA.Edit)
                .OnClick(x => x.Go<Admin.Settings.Licences.StatusCodeEnterPage>()
                .Send("item", "item.ID").SendReturnUrl());

            this.ArchiveButtonColumn();
            Button("New Licence Status Code").Icon(FA.Plus)
                .OnClick(x => x.Go<Admin.Settings.Licences.StatusCodeEnterPage>()
                .SendReturnUrl());
        }
    }
}
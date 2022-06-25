using MSharp;

namespace Modules
{
    class CompanyUsersList : ListModule<Domain.CompanyUser>
    {
        public CompanyUsersList()
        {
            HeaderText("Company Users").DataSource(" (await info.Company.CompanyUsers.OrderBy(x => x.FirstName).GetList())");

            Search(GeneralSearch.AllFields).Label("Find");
            this.ArchiveSearch();
            SearchButton("Search").Icon(FA.Search).OnClick(x => x.ReturnView());

            Column(x => x.FirstName);
            Column(x => x.LastName);
            Column(x => x.Email).LabelText("Email address");
            Column(x => x.TelephoneNumber);
            Column(x => x.MobileNumber);
            Column(x => x.Notes);
            Column(x => x.AccountsDepartment);
            Column(x => x.IsAdmin)
                .HeaderTemplate("Customer Admin");

            Column(x => x.RecievesCFSPReport)
               .HeaderTemplate("Recieves CFSP Report");

            ButtonColumn("Edit").HeaderText("Edit").GridColumnCssClass("actions").Icon(FA.Edit)
                .OnClick(x => x.Go<Admin.Company.CompanyUser.EnterPage>().Send("item", "item.ID").Send("company", "info.Company.ID").SendReturnUrl());

            this.ArchiveButtonColumn("Company user");

            Button("New Company User").IsDefault().Icon(FA.Plus).VisibleIf("info.Company.Type != CompanyType.Other")
                .OnClick(x => x.Go<Admin.Company.CompanyUser.EnterPage>().Send("company", "info.Company.ID").SendReturnUrl());

            ViewModelProperty<Domain.Company>("Company").FromRequestParam("company");
        }
    }
}
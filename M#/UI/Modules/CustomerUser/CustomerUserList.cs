using Domain;
using MSharp;

namespace Modules
{
    public class CustomerUserList : ListModule<Domain.CompanyUser>
    {
        public CustomerUserList()
        {
            HeaderText("Company Users")
                .DataSource("(await (await Database.FirstOrDefault<CompanyUser>(x => x.ID == Guid.Parse(User.GetId()))).Company.CompanyUsers.GetList())");

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
                .OnClick(x => x.Go<Customer.Settings.Users.EnterPage>().Send("item", "item.ID").Send("company", "info.Company.ID").SendReturnUrl());

            this.ArchiveButtonColumn("Company user").VisibleIf("item.ID!=Guid.Parse(User.GetId())");

            Button("New Company User").IsDefault().Icon(FA.Plus)
                .OnClick(x => x.Go<Customer.Settings.Users.EnterPage>().Send("company", "info.Company.ID").SendReturnUrl());

            ViewModelProperty<Domain.Company>("Company")
                .OnBound("info.Company = (await Database.FirstOrDefault<CompanyUser>(x => x.ID == Guid.Parse(User.GetId()))).Company;");


        }
    }
}
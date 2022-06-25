using MSharp;

namespace Modules
{
    class CustomerContactList : BaseListModule<Domain.Contact>
    {
        public CustomerContactList()
        {
            HeaderText("Contacts")
                .DataSource("(await (await Database.FirstOrDefault<CompanyUser>(x => x.ID == Guid.Parse(User.GetId()))).Company.Contacts.GetList())");

            Search(GeneralSearch.AllFields).Label("Find");
            this.ArchiveSearch();
            SearchButton("Search").Icon(FA.Search).OnClick(x => x.ReturnView());

            Column(x => x.FirstName);
            Column(x => x.LastName);
            Column(x => x.Email).LabelText("Email address");
            Column(x => x.TelephoneNumber);
            Column(x => x.MobileNumber);
            Column(x => x.Notes);

            ButtonColumn("Edit").HeaderText("Edit").GridColumnCssClass("actions").Icon(FA.Edit)
                .OnClick(x => x.Go<Customer.Settings.Contact.EnterPage>().Send("item", "item.ID").Send("company", "info.Company.ID").SendReturnUrl());

            this.ArchiveButtonColumn("Contact");

            Button("New Contact").IsDefault().Icon(FA.Plus)
                .OnClick(x => x.Go<Customer.Settings.Contact.EnterPage>().Send("company", "info.Company.ID").SendReturnUrl());

            ViewModelProperty<Domain.Company>("Company")
                .OnBound("info.Company = (await Database.FirstOrDefault<CompanyUser>(x => x.ID == Guid.Parse(User.GetId()))).Company;");
        }
    }
}
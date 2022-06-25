using MSharp;

namespace Modules
{
    class CustomerContactGroupList : BaseListModule<Domain.ContactGroup>
    {
        public CustomerContactGroupList()
        {
            HeaderText("Contact Groups")
                .DataSource("(await (await Database.FirstOrDefault<CompanyUser>(x => x.ID == Guid.Parse(User.GetId()))).Company.ContactGroups.GetList())");

            this.ArchiveSearch();
            SearchButton("Search").Icon(FA.Search).OnClick(x => x.ReturnView());

            Column(x => x.GroupName);
            LinkColumn("Contacts").HeaderText("Contacts").Text("@(await item.ContactsLinks.GetList().Where(x=> !((x.Person is Domain.Contact ? (x.Person as Domain.Contact).IsDeactivated : (x.Person as Domain.CompanyUser).IsDeactivated)) ).Count())").OnClick(x => x.Go<Customer.Settings.ContactGroup.ContactsPage>().Send("company", "info.Company.ID").Send("contact", "item.ID").SendReturnUrl());

            ButtonColumn("Edit").HeaderText("Edit").GridColumnCssClass("actions").Icon(FA.Edit)
                .OnClick(x => x.Go<Customer.Settings.ContactGroup.EnterPage>().Send("item", "item.ID").Send("company", "info.Company.ID").SendReturnUrl());

            this.ArchiveButtonColumn("Contact group");

            Button("New Contact Group").IsDefault().Icon(FA.Plus)
                .OnClick(x => x.Go<Customer.Settings.ContactGroup.EnterPage>().Send("company", "info.Company.ID").SendReturnUrl());

            ViewModelProperty<Domain.Company>("Company")
                .OnBound("info.Company = (await Database.FirstOrDefault<CompanyUser>(x => x.ID == Guid.Parse(User.GetId()))).Company;");
        }
    }
}
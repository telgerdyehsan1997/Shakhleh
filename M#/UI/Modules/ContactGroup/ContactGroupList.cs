using MSharp;

namespace Modules
{
    class ContactGroupList : BaseListModule<Domain.ContactGroup>
    {
        public ContactGroupList()
        {
            HeaderText("Contact Groups").DataSource("await info.Company.ContactGroups.GetList()");

            this.ArchiveSearch();
            SearchButton("Search").Icon(FA.Search).OnClick(x => x.ReturnView());

            Column(x => x.GroupName);
            LinkColumn("Contacts").HeaderText("Contacts").Text("@(await item.ContactsLinks.GetList().Where(x=> !((x.Person is Domain.Contact ? (x.Person as Domain.Contact).IsDeactivated : (x.Person as Domain.CompanyUser).IsDeactivated)) ).Count())").OnClick(x => x.Go<Admin.Company.ContactGroup.ContactsPage>().Send("company", "info.Company.ID").Send("contact", "item.ID").SendReturnUrl());

            ButtonColumn("Edit").HeaderText("Edit").GridColumnCssClass("actions").Icon(FA.Edit)
                .OnClick(x => x.Go<Admin.Company.ContactGroup.EnterPage>().Send("item", "item.ID").Send("company", "info.Company.ID").SendReturnUrl());

            this.ArchiveButtonColumn("Contact group");

            Button("New Contact Group").IsDefault().Icon(FA.Plus).VisibleIf("info.Company.Type != CompanyType.Other")
                .OnClick(x => x.Go<Admin.Company.ContactGroup.EnterPage>().Send("company", "info.Company.ID").SendReturnUrl());

            ViewModelProperty<Domain.Company>("Company").FromRequestParam("company");
        }
    }
}
using MSharp;

namespace Modules
{
    class ContactList : BaseListModule<Domain.Contact>
    {
        public ContactList()
        {
            HeaderText("Contacts").DataSource(" await info.Company.Contacts.OrderBy(x=>x.FirstName).GetList()");

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
                .OnClick(x => x.Go<Admin.Company.Contact.EnterPage>().Send("item", "item.ID").Send("company", "info.Company.ID").SendReturnUrl());

            this.ArchiveButtonColumn("Contact");

            Button("New Contact").IsDefault().Icon(FA.Plus).VisibleIf("info.Company.Type != CompanyType.Other")
                .OnClick(x => x.Go<Admin.Company.Contact.EnterPage>().Send("company", "info.Company.ID").SendReturnUrl());

            ViewModelProperty<Domain.Company>("Company").FromRequestParam("company");
        }
    }
}
using MSharp;

namespace Modules
{
    class ContactGroupContacts : ListModule<Domain.Person>
    {
        public ContactGroupContacts()
        {
            PageSize(50);
            Sortable();
            ShowHeaderRow();

            HeaderText("Contacts").DataSource("await info.Company.GetAvailableContacts()");
            SelectCheckbox();

            Search(GeneralSearch.AllFields).Label("Find");
            SearchButton("Search").Icon(FA.Search).OnClick(x => x.ReturnView());

            SelectCheckbox();

            Column(x => x.FirstName);
            Column(x => x.LastName);

            OnBound_GET("Set Selected Ids").Code(@"if(info.SelectedIds.None())
                                                        info.SelectedIds = await info.ContactGroup.Contacts.Select(x => x.ID).ToList();");

            Button("Save").Icon(FA.Check).OnClick(x =>
            {
                x.CSharp("await info.ContactGroup.UpdateContacts((await info.SelectedItems).ToList());");
                x.ReturnToPreviousPage();
            });

            ViewModelProperty<Domain.Company>("Company").FromRequestParam("company");
            ViewModelProperty<Domain.ContactGroup>("ContactGroup").FromRequestParam("contact");
        }
    }
}
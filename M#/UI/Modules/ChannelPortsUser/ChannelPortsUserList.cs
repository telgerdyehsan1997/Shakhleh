using Domain;
using MSharp;

namespace Modules
{
    public class ChannelPortsUserList : BaseListModule<Domain.ChannelPortsUser>
    {
        public ChannelPortsUserList()
        {
            ShowFooterRow()
                .UseDatabasePaging(false)
                .DataSource("(await Database.GetList<ChannelPortsUser>().OrderBy(x=>x.FirstName))")
                .HeaderText("Users");

            Search(GeneralSearch.AllFields).Label("Find");
            this.ArchiveSearch();
            SearchButton("Search").Icon(FA.Search).IsDefault()
                .OnClick(x => x.Reload());

            Column(x => x.FirstName);
            Column(x => x.LastName);
            Column(x => x.Email);
            Column(x => x.MobileNumber).HeaderTemplate("Phone Number");
            Column(x => x.IsAdmin).LabelText("Admin");

            ButtonColumn("Edit")
                .HeaderText("Edit")
                .GridColumnCssClass("actions")
                .Icon(FA.Edit)
                .OnClick(x => x.Go<Admin.Settings.Users.EnterPage>().Send("item", "item.ID").SendReturnUrl());

            this.ArchiveButtonColumn("user")
                    .VisibleIf("GetUser() != null && item.ID != GetUser().ID");

            Button("New User").Icon(FA.Plus)
                .OnClick(x => x.Go<Admin.Settings.Users.EnterPage>().SendReturnUrl());
        }
    }
}
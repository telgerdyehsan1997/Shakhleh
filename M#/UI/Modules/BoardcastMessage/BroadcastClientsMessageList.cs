using MSharp;

namespace Modules
{
    class BroadcastClientsMessageList : BaseListModule<Domain.BroadcastClientsMessage>
    {
        public BroadcastClientsMessageList()
        {
            HeaderText("Broadcasts")
                .SourceCriteria("item.User == User.GetId()");

            CustomSearch("Message")
               .Label("Messsage")
               .ViewModelName("Message")
               .ViewModelType("string?");

            Search(x => x.DateRecieved);

            Search(x => x.HasRead)
                .Control(ControlType.HorizontalRadioButtons)
                .DefaultValueExpression("false")
                .Label("Show");

            SearchButton("Search").Icon(FA.Search)
                           .IsDefault()
                           .CssClass("float-right")
                           .OnClick(x => x.ReturnView());

            Column(x => x.Message)
                .DisplayExpression("@item.Message.Subject");

            Column(x => x.Message)
               .LabelText("Download Attachments")
               .DisplayExpression(@"@if(item.Message.Attachment.FileName != ""NoFile.Empty""){<a href=""@item.Message.Attachment"" target=""_blank"">Download</a>}
                                  @if(item.Message.Attachment2.FileName != ""NoFile.Empty""){<a href=""@item.Message.Attachment2"" target=""_blank""> Download</a>}
                                  @if(item.Message.Attachment3.FileName != ""NoFile.Empty""){<a href=""@item.Message.Attachment3"" target=""_blank""> Download</a>}");

            Column(x => x.DateRecieved);

            ButtonColumn("Read")
               .HeaderText("Mark as Read")
               .OnClick(x =>
               {
                   x.CSharp("await Database.Update(item, x => x.HasRead = true);");
                   x.RefreshPage();

               }).VisibleIf("!item.HasRead");


        }
    }
}
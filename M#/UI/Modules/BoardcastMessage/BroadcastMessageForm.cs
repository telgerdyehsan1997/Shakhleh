using MSharp;

namespace Modules
{
    class BroadcastMessageForm : MFABaseForm<Domain.BroadcastMessage>
    {
        public BroadcastMessageForm()
        {
            HeaderText("Broadcast message details");

            Field(x => x.SendTo)
                .ReloadOnChange()
                .Label("Send To")
                .AsRadioButtons(Arrange.Horizontal)
                .Mandatory()
                .DataSource("await Database.GetList<MessageSendToType>()");


            Field(x => x.Subject).ControlCssClass("persist-casing");

            Field(x => x.Body)
                .ExtraControlAttributes("spellcheck='true'")
                .Control(ControlType.HtmlEditor);

            Field(x => x.Attachment);

            Field(x => x.Attachment2);

            Field(x => x.Attachment3);

            Field(x => x.CompanyTypesLinks)
                .SourceCriteria("item != CompanyType.Other")
                .AsCollapsibleCheckBoxList()
                .VisibleIf("info.SendTo != MessageSendToType.Channelports");

            Field(x => x.GVMSTypesLinks)
                .AsCollapsibleCheckBoxList()
                .VisibleIf("info.SendTo != MessageSendToType.Channelports");


            Field(x => x.InboundSafetyAndSecurityOptionsLinks)
                .AsCollapsibleCheckBoxList()
                .DisplayExpression("item.DisplayName")
                .VisibleIf("info.SendTo != MessageSendToType.Channelports");


            Button("Cancel").OnClick(x => x.ReturnToPreviousPage());

            Button("Save").IsDefault().Icon(FA.Check)
                .OnClick(x =>
                {
                    x.SaveInDatabase();
                    x.GentleMessage("Broadcast will be sent within the next 5 mins.");
                    x.ReturnToPreviousPage();
                });

        }
    }
}
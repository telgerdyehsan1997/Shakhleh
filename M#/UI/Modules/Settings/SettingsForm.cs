using MSharp;

namespace Modules
{
    class GlobalSettingsForm : MFABaseForm<Domain.Settings>
    {
        public GlobalSettingsForm()
        {
            HeaderText("Global Settings").DataSource("Domain.Settings.Current");

            //Field(x => x.DefaultDeclarant).Control(ControlType.AutoComplete).SourceCriteria("!item.IsDeactivated").Mandatory();
            Field(x => x.TimeUntilCleared).AfterControlAddon("Mins");

            Field(x => x.SendNCTSMessageViaASM)
                .Mandatory()
                .AsRadioButtons(Arrange.Horizontal)
                .Label("Send NCTS Message Via ASM");

            Field(x => x.ActivateUCN)
                .Mandatory()
                .AsRadioButtons(Arrange.Horizontal);

            Field(x => x.IntoUKDocumentCode);
            Field(x => x.IntoUKDocumentStatus);
            Field(x => x.IntoUKDocumentReference);

            Field(x => x.ChannelportsCFSPShipmentNumber)
                .ExtraControlAttributes("readonly");

            Field(x => x.CFSPMonthlyReportRecipients);

            Field(x => x.CPCFSPMonthEndEmailAddress);


            Field(x => x.NCTSHighValueThreshold).Label("NCTS High Value Threshold");

            Button("Save").IsDefault().Icon(FA.Check)
            .OnClick(x =>
            {
                x.SaveInDatabase();
                x.GentleMessage("Saved successfully.");
                x.RefreshPage();
            });

            OnBound("on first load").Code(@"var channelportsCFSPShipmentNumber = info.Item.ChannelportsCFSPShipmentNumber ?? info.ChannelportsCFSPShipmentNumber;
            if (channelportsCFSPShipmentNumber.IsEmpty())
                info.Item.ChannelportsCFSPShipmentNumber = await Settings.SetCFSPShipmentNumber();");

        }
    }
}
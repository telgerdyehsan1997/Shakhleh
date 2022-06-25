using MSharp;

namespace Modules
{
    class StatusEmailNotificationsShipmentList : ListModule<Domain.Progress>
    {
        public StatusEmailNotificationsShipmentList()
        {
            HeaderText("Channelport Status Email Notifications - Shipments")
                .PageSize(50)
                .Sortable()
                .UpdateWithPost();

            Column(x => x.SystemName);

            Column(x => x.RecieveEmailNotificationChannelport)
                .LabelText("Recieve Email Notification")
                .AsCheckBox();

            Column(x => x.DoNotRecieveEmailNotificationChannelport)
              .LabelText("Don't receive Email notification")
              .AsCheckBox();

           

            Button("Update").IsDefault().Icon(FA.Check)
             .OnClick(x =>
             {
                 x.CSharp(@"if(info.Items.Any(x => !x.RecieveEmailNotificationChannelport && !x.DoNotRecieveEmailNotificationChannelport))
                 {
                    Notify(""Either options for all Email Notifications must be selected before they can be Updated."");
                    return View(info);
                 }");

                 x.CSharp(@"foreach (var item in info.Items)
                 {
                   await Database.Update(item.Item, o =>
                      {
                        o.DoNotRecieveEmailNotificationChannelport = item.DoNotRecieveEmailNotificationChannelport;
                        o.RecieveEmailNotificationChannelport = item.RecieveEmailNotificationChannelport;

                     });
                 }");
                 x.GentleMessage("Saved successfully.");
                 x.ReturnView();
             });

        }
    }
}
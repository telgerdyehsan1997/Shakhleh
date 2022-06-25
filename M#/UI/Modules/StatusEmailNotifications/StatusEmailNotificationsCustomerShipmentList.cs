using MSharp;

namespace Modules
{
    class StatusEmailNotificationsCustomerShipmentList : ListModule<Domain.Progress>
    {
        public StatusEmailNotificationsCustomerShipmentList()
        {
            HeaderText("Customer Status Email Notifications - Shipments")
                .PageSize(50)
                .Sortable()
                .UpdateWithPost();

            Column(x => x.SystemName);

            Column(x => x.RecieveEmailNotificationCustomer)
                .LabelText("Recieve Email Notification")
                .AsCheckBox();

            Column(x => x.DoNotRecieveEmailNotificationCustomer)
               .LabelText("Don't receive Email notification")
               .AsCheckBox();

            Button("Update")
                .IsDefault()
                .Icon(FA.Check)
             .OnClick(x =>
             {
                 x.CSharp(@"if(info.Items.Any(x => !x.RecieveEmailNotificationCustomer && !x.DoNotRecieveEmailNotificationCustomer))
                 {
                    Notify(""Either options for all Email Notifications must be selected before they can be Updated."");
                    return View(info);
                 }");
                 x.CSharp(@"foreach (var item in info.Items)
                 {
                   await Database.Update(item.Item, o =>
                      {
                        o.DoNotRecieveEmailNotificationCustomer = item.DoNotRecieveEmailNotificationCustomer;
                        o.RecieveEmailNotificationCustomer = item.RecieveEmailNotificationCustomer;

                     });
                 }");
                 x.GentleMessage("Saved successfully.");
                 x.ReturnView();
             });

        }
    }
}
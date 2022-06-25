using MSharp;

namespace Modules
{
    class StatusEmailNotificationsUserCustomerShipmentList : ListModule<Domain.CustomerProgress>
    {
        public StatusEmailNotificationsUserCustomerShipmentList()
        {
            HeaderText("Status Email Notifications - Shipments")
                .SourceCriteria("item.UserId == info.Company.ID")
                .SortingStatement("item.Progress.AdminDisplay")
                .PageSize(50)
                .Sortable()
                .UpdateWithPost();

            ViewModelProperty<Domain.Company>("Company").FromRequestParam("company");

            Column(x => x.Progress)
                .DisplayExpression("@item.Progress.SystemName");

            Column(x => x.RecieveEmailNotificationUserCustomer)
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
                 x.CSharp(@"if(info.Items.Any(x => !x.RecieveEmailNotificationUserCustomer && !x.DoNotRecieveEmailNotificationCustomer))
                 {
                    Notify(""Either options for all Email Notifications must be selected before they can be Updated."");
                    return View(info);
                 }");

                 x.CSharp(@"foreach (var item in info.Items)
                 {
                    await Database.Update(item.Item, o =>
                      {
                        o.DoNotRecieveEmailNotificationCustomer = item.DoNotRecieveEmailNotificationCustomer;
                        o.RecieveEmailNotificationUserCustomer = item.RecieveEmailNotificationUserCustomer;

                     });
                 }");
                 x.GentleMessage("Saved successfully.");
                 x.ReturnView();
             });

            OnBound("set  companyId")
              .Code("info.Company = info.Company ?? User.ExtractUser<CompanyUser>().Company;");

            OnBound_GET("save progress first").Code(@"var progressItems = await Database.GetList<Progress>();
                var customerProgressItems = await Database.Of<CustomerProgress>().Where(x => x.User == info.Company.ID).GetList();

                if (customerProgressItems.Select(c => c.Progress.SystemName).LacksAny(progressItems.Select(p => p.SystemName)))
                {
                    foreach (var progressItem in progressItems)
                    {
                        if (customerProgressItems.Select(c => c.Progress).Lacks(progressItem))
                        {
                            await Database.Save(new CustomerProgress
                            {
                                Progress = progressItem,
                                User = info.Company,
                                DoNotRecieveEmailNotificationCustomer = progressItem.DoNotRecieveEmailNotificationCustomer,
                                RecieveEmailNotificationUserCustomer = progressItem.RecieveEmailNotificationCustomer
                            });
                        }
                    }
                    info.Items = await GetSource(info)
                       .Select(async item =>
                       {
                           var listItem = new vm.StatusEmailNotificationsUserCustomerShipmentList.ListItem(item);
                           await item.CopyDataTo(listItem);

                           if (Request.IsPost())
                               await TryUpdateModelAsync(listItem, prefix: ""Item - "" + item.ID);

                           return listItem;
                    }).ToList();
            }");
          
        }
    }
}
using MSharp;

namespace App
{
    public class Project : MSharp.Project
    {
        public Project()
        {
            Name("channel.ports").SolutionFile("channel.ports.sln");

            DefaultDateFormat("{0:d}");
            DefaultDateTimeFormat("{0:g}");

            Role("Local.Request");
            Role("Anonymous");
            Role("Admin").SkipQueryStringSecurity();
            Role("Super Admin").SkipQueryStringSecurity();
            Role("Customer");
            Role("Undischarged").SkipQueryStringSecurity();
            Role("Undischarged Admin").SkipQueryStringSecurity();

            Layout("Front end").AjaxRedirect().Default().VirtualPath("~/Views/Layouts/FrontEnd.cshtml");
            Layout("Blank").AjaxRedirect().VirtualPath("~/Views/Layouts/Blank.cshtml");
            Layout("Front end Modal").Modal().VirtualPath("~/Views/Layouts/FrontEnd.Modal.cshtml");
            Layout("Print").AjaxRedirect().VirtualPath("~/Views/Layouts/Print.cshtml");

            PageSetting("LeftMenu");
            PageSetting("SubMenu");
            PageSetting("TopMenu");
            PageSetting("HeaderModule");
            PageSetting("SubHeaderModule");

            AutoTask("Clean old temp uploads").Every(10, TimeUnit.Minute)
                .Run(@"await Olive.Context.Current.GetService<Olive.Mvc.IFileRequestService>()
                .DeleteTempFiles(olderThan: 1.Hours());");

            StyleRequiredFormElements(true);

            AutoTask("Send emails").Every(1, TimeUnit.Minute)
           .Run(@"var outbox = Context.Current.GetService<Olive.Email.IEmailOutbox>();
            await outbox.SendAll();")
           .RecordFailure();

            AutoTask("Reset shipment tracking number")
                .Every(1, TimeUnit.Minute)
                .Run(@"if (LocalTime.Now.Day == 1 &&
                    (!Settings.Current.DateSuffixesWereLastReset.HasValue || Settings.Current.DateSuffixesWereLastReset.Value < LocalTime.Now.GetBeginningOfMonth()))
                {
                    Context.Current.Database().Update(Settings.Current, x =>
                    {   x.CFSPShipmentNumber = 1;
                        x.IntoUKTrackingNumber = 1;
                        x.OutOfUKTrackingNumber = 1;
                        x.DateSuffixesWereLastReset = LocalTime.Now;
                    });
                }");

            AutoTask("Run Commodity Code Import Service")
                .Every(1, TimeUnit.Minute)
                .Run("await CommodityCodeImportService.ProcessNext();");

            AutoTask("Run UN Code Import Service")
                .Every(1, TimeUnit.Minute)
             .Run("await UNCodeImportService.ProcessNext();");

            AutoTask("Run Company Bulk Import Service")
                .Every(1, TimeUnit.Minute)
                .Run("await CompanyBulkUploadService.ProcessNext();");

            AutoTask("Run Product Bulk Import Service")
                .Every(1, TimeUnit.Minute)
               .Run("await ProductBulkUploadService.ProcessNext();");

            AutoTask("Transmit EAD Shipments In")
                .Every(5, TimeUnit.Minute)
               .Run(@"var service = Context.Current.GetService<IEADShipmentService>();
                      await service.Transmit();");

            AutoTask("Auto Cleared")
                .Every(5, TimeUnit.Minute)
            .Run(@"var service = Context.Current.GetService<IStatusManagementService>();
                      await service.AutoClearConsignments();");

            AutoTask("Process Authorised Locations Import Service")
                .Every(1, TimeUnit.Minute)
                .Run(@"await AuthorisedLocationsImportService.ProcessNext();");


            AutoTask("Create Invoice Job").Every(4, TimeUnit.Hour)
               .Run(@"var service = Context.Current.GetService<IInvoiceService>();  
                   await service.CreateInvoiceJobs(); ");

            AutoTask("Generate Invoice").Every(1, TimeUnit.Hour)
             .Run(@"var service = Context.Current.GetService<IInvoiceService>();  
                   await service.GenerateInvoices(); ");

            AutoTask("Generate Invoice Files").Every(1, TimeUnit.Hour)
             .Run(@"var service = Context.Current.GetService<IInvoiceService>();  
                   await service.GenerateInvoiceFiles(); ");

            AutoTask("Send Broadcast Message").Every(5, TimeUnit.Minute)
            .Run(@"var service = Context.Current.GetService<IBroadcastingMessage>();  
                   await service.SendBroadCastMessages(); ");

            AutoTask("Purge Email Item Queues Older Than 7 Days").Every(1, TimeUnit.Day)
                   .Run(@"await EmailMessage.PurgeEmailsOlderThan7Days(); ");

            //AutoTask("Run Create GVM Service").Every(15, TimeUnit.Minute)
            //    .Run(@"var service = Context.Current.GetService<IGVMS>();
            //     await service.CreateGVM(); ");

            AutoTask("Run Route Itinerary Import Service").Every(1, TimeUnit.Minute)
                  .Run("await RouteIterneriesImportService.ProcessNext();");

            //AutoTask("Send EAD Delivery Documents").Every(5, TimeUnit.Minute)
            //      .Run(@"var service = Context.Current.GetService<IDocumentService>();
            //     await service.SendEADDeliveryDocuments(); ");

            //AutoTask("Send NCTS Delivery Documents").Every(5, TimeUnit.Minute)
            //      .Run(@"var service = Context.Current.GetService<IDocumentService>();
            //     await service.SendNCTSDeliveryDocuments(); ");

            //AutoTask("Read HMRC emails from mailbox and save to database").Every(1, TimeUnit.Minute) //.Every(1, TimeUnit.Hour)
            //     .Run(@"var service = Context.Current.GetService<IHMRCEmailService>();
            //            await service.ReadHMRCEmails();");

            AutoTask("Run Row Quota Import Service")
                .Every(1, TimeUnit.Minute)
                .Run("await RowQuotaImportService.ProcessNext();");

        }
    }
}
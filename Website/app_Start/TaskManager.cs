namespace Website
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Olive;
    using Olive.Entities;
    using Olive.Entities.Data;
    using Domain;
    
    /// <summary>Executes the scheduled tasks in independent threads automatically.</summary>
    [EscapeGCop("Auto generated code.")]
    #pragma warning disable
    public partial class TaskManager : BackgroundJobsPlan
    {
        /// <summary>Registers the scheduled activities.</summary>
        public override void Initialize()
        {
            Register(new BackgroundJob("Auto Cleared", () => AutoCleared(), Hangfire.Cron.MinuteInterval(5)));
            Register(new BackgroundJob("Clean old temp uploads", () => CleanOldTempUploads(), Hangfire.Cron.MinuteInterval(10)));
            Register(new BackgroundJob("Create Invoice Job", () => CreateInvoiceJob(), Hangfire.Cron.HourInterval(4)));
            Register(new BackgroundJob("Generate Invoice", () => GenerateInvoice(), Hangfire.Cron.HourInterval(1)));
            Register(new BackgroundJob("Generate Invoice Files", () => GenerateInvoiceFiles(), Hangfire.Cron.HourInterval(1)));
            Register(new BackgroundJob("Process Authorised Locations Import Service", () => ProcessAuthorisedLocationsImportService(), Hangfire.Cron.MinuteInterval(1)));
            Register(new BackgroundJob("Purge Email Item Queues Older Than 7 Days", () => PurgeEmailItemQueuesOlderThan7Days(), Hangfire.Cron.DayInterval(1)));
            Register(new BackgroundJob("Reset shipment tracking number", () => ResetShipmentTrackingNumber(), Hangfire.Cron.MinuteInterval(1)));
            Register(new BackgroundJob("Run Commodity Code Import Service", () => RunCommodityCodeImportService(), Hangfire.Cron.MinuteInterval(1)));
            Register(new BackgroundJob("Run Company Bulk Import Service", () => RunCompanyBulkImportService(), Hangfire.Cron.MinuteInterval(1)));
            Register(new BackgroundJob("Run Product Bulk Import Service", () => RunProductBulkImportService(), Hangfire.Cron.MinuteInterval(1)));
            Register(new BackgroundJob("Run Route Itinerary Import Service", () => RunRouteItineraryImportService(), Hangfire.Cron.MinuteInterval(1)));
            Register(new BackgroundJob("Run Row Quota Import Service", () => RunRowQuotaImportService(), Hangfire.Cron.MinuteInterval(1)));
            Register(new BackgroundJob("Run UN Code Import Service", () => RunUNCodeImportService(), Hangfire.Cron.MinuteInterval(1)));
            Register(new BackgroundJob("Send Broadcast Message", () => SendBroadcastMessage(), Hangfire.Cron.MinuteInterval(5)));
            Register(new BackgroundJob("Send emails", () => SendEmails(), Hangfire.Cron.MinuteInterval(1)));
            Register(new BackgroundJob("Transmit EAD Shipments In", () => TransmitEADShipmentsIn(), Hangfire.Cron.MinuteInterval(5)));
        }
        
        /// <summary>Auto Cleared</summary>
        public static async Task AutoCleared()
        {
            var service = Context.Current.GetService<IStatusManagementService>();
            await service.AutoClearConsignments();
        }
        
        /// <summary>Clean old temp uploads</summary>
        public static async Task CleanOldTempUploads()
        {
            await Olive.Context.Current.GetService<Olive.Mvc.IFileRequestService>()
                .DeleteTempFiles(olderThan: 1.Hours());
        }
        
        /// <summary>Create Invoice Job</summary>
        public static async Task CreateInvoiceJob()
        {
            var service = Context.Current.GetService<IInvoiceService>();
            await service.CreateInvoiceJobs();
        }
        
        /// <summary>Generate Invoice</summary>
        public static async Task GenerateInvoice()
        {
            var service = Context.Current.GetService<IInvoiceService>();
            await service.GenerateInvoices();
        }
        
        /// <summary>Generate Invoice Files</summary>
        public static async Task GenerateInvoiceFiles()
        {
            var service = Context.Current.GetService<IInvoiceService>();
            await service.GenerateInvoiceFiles();
        }
        
        /// <summary>Process Authorised Locations Import Service</summary>
        public static async Task ProcessAuthorisedLocationsImportService()
        {
            await AuthorisedLocationsImportService.ProcessNext();
        }
        
        /// <summary>Purge Email Item Queues Older Than 7 Days</summary>
        public static async Task PurgeEmailItemQueuesOlderThan7Days()
        {
            await EmailMessage.PurgeEmailsOlderThan7Days();
        }
        
        /// <summary>Reset shipment tracking number</summary>
        public static Task ResetShipmentTrackingNumber()
        {
            if (LocalTime.Now.Day == 1 &&
            (!Settings.Current.DateSuffixesWereLastReset.HasValue || Settings.Current.DateSuffixesWereLastReset.Value < LocalTime.Now.GetBeginningOfMonth()))
            {
                Context.Current.Database().Update(Settings.Current, x =>
                {   x.CFSPShipmentNumber = 1;
                x.IntoUKTrackingNumber = 1;
                x.OutOfUKTrackingNumber = 1;
                x.DateSuffixesWereLastReset = LocalTime.Now;
            });
        }
        
        return Task.CompletedTask;
    }
    
    /// <summary>Run Commodity Code Import Service</summary>
    public static async Task RunCommodityCodeImportService()
    {
        await CommodityCodeImportService.ProcessNext();
    }
    
    /// <summary>Run Company Bulk Import Service</summary>
    public static async Task RunCompanyBulkImportService()
    {
        await CompanyBulkUploadService.ProcessNext();
    }
    
    /// <summary>Run Product Bulk Import Service</summary>
    public static async Task RunProductBulkImportService()
    {
        await ProductBulkUploadService.ProcessNext();
    }
    
    /// <summary>Run Route Itinerary Import Service</summary>
    public static async Task RunRouteItineraryImportService()
    {
        await RouteIterneriesImportService.ProcessNext();
    }
    
    /// <summary>Run Row Quota Import Service</summary>
    public static async Task RunRowQuotaImportService()
    {
        await RowQuotaImportService.ProcessNext();
    }
    
    /// <summary>Run UN Code Import Service</summary>
    public static async Task RunUNCodeImportService()
    {
        await UNCodeImportService.ProcessNext();
    }
    
    /// <summary>Send Broadcast Message</summary>
    public static async Task SendBroadcastMessage()
    {
        var service = Context.Current.GetService<IBroadcastingMessage>();
        await service.SendBroadCastMessages();
    }
    
    /// <summary>Send emails</summary>
    public static async Task SendEmails()
    {
        var outbox = Context.Current.GetService<Olive.Email.IEmailOutbox>();
        await outbox.SendAll();
    }
    
    /// <summary>Transmit EAD Shipments In</summary>
    public static async Task TransmitEADShipmentsIn()
    {
        var service = Context.Current.GetService<IEADShipmentService>();
        await service.Transmit();
    }
}
}
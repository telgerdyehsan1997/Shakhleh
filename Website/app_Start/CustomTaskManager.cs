namespace Website
{
    using Domain;
    using Olive;
    using System.Threading.Tasks;


#pragma warning disable
    public partial class CustomTaskManager : BackgroundJobsPlan
    {
        /// <summary>Registers the scheduled activities.</summary>
        public override void Initialize()
        {
            Register(new BackgroundJob("Get Exchange Rate Month", () => GetExchangeRateMonth(), "0 0 * * *"));
            Register(new BackgroundJob("Shipment File Deliveries", () => ShipmentFileDeliveries(), Hangfire.Cron.MinuteInterval(5)));
            Register(new BackgroundJob("Generate ISD for GVMS CFSP", () => TransmitGVMSShipmentsIn(), Hangfire.Cron.MinuteInterval(5)));
            Register(new BackgroundJob("Generate ISD for INV CFSP", () => TransmitINVShipmentsIn(), Hangfire.Cron.MinuteInterval(5)));

            Register(new BackgroundJob("Update Consigments In to Uk Port", () => UpdateConsigmentsInToUkPort(), Hangfire.Cron.MinuteInterval(5)));
            Register(new BackgroundJob("Archive With Importer", () => SendArchiveWithImporter(), Hangfire.Cron.MinuteInterval(5)));
            Register(new BackgroundJob("Removed UnConfirm Response", () => RemovedUnConfirmResponse(), Hangfire.Cron.MinuteInterval(5)));


        }
        public static async Task ShipmentFileDeliveries()
        {
            var service = Context.Current.GetService<IEADShipmentService>();
            await service.ShipmentFileDeliveries();
        }
        public static async Task GetExchangeRateMonth()
        {
            var service = Context.Current.GetService<IExChangeRateService>();
            await service.GetExChangeRate();
        }

        public static async Task TransmitGVMSShipmentsIn()
        {
            var service = Context.Current.GetService<IEADShipmentService>();
            await service.TransmitGVMS();
        }
        public static async Task TransmitINVShipmentsIn()
        {
            var service = Context.Current.GetService<IEADShipmentService>();
            await service.TransmitINV();
        }
        public static async Task UpdateConsigmentsInToUkPort()
        {
            var service = Context.Current.GetService<IEADShipmentService>();
            await service.UpdateInToUKType();
        }
        public static async Task SendArchiveWithImporter()
        {
            var service = Context.Current.GetService<IEADShipmentService>();
            await service.ArchiveWithImporter();
        }
        public static async Task RemovedUnConfirmResponse()
        {
            var service = Context.Current.GetService<IBroadcastingMessage>();
            await service.RemovedUnConfirmResponse();
        }
    }
}
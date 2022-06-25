namespace Domain
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Olive;
    using Olive.Entities;

    partial class Settings
    {

        static SemaphoreSlim SemaphoreSlim = new SemaphoreSlim(1, 1);
        private static readonly DateTime Date = LocalTime.Now;
        public override async Task Validate()
        {
            await base.Validate();

            if (IsNew && await Database.Any<Settings>())
                throw new Exception("Settings is Singleton!");

            if (DefaultDeclarantId.HasValue && DefaultDeclarant.IsDeactivated)
                throw new ValidationException("You cannot set an archived Company as Default declarant.");


            if (CFSPMonthlyReportRecipients.HasValue())
            {
                var emails = CFSPMonthlyReportRecipients.Split(",");
                foreach (var email in emails)
                {
                    if (!Helper.EmailIsValid(email))
                        throw new ValidationException(email + " is not a valid Email address");
                }
            }
            if (CPCFSPMonthEndEmailAddress.HasValue())
            {
                if (!Helper.EmailIsValid(CPCFSPMonthEndEmailAddress))
                    throw new ValidationException(CPCFSPMonthEndEmailAddress + " is not a valid Email address");
            }


        }

        public async static Task<string> SetCFSPShipmentNumber()
        {
            var firstLetter = "CP";
            var trackingNumberSuffix = await GetCFSPShipmentNumberSuffix();
            var cfspNumber = firstLetter + Date.Month.ToString("00") + Date.Year.ToString().Substring(2, 2) + trackingNumberSuffix;
            var setting = await Database.Of<Settings>().FirstOrDefault();
            await Database.Update(setting.Clone(), x => x.ChannelportsCFSPShipmentNumber = cfspNumber);
            return cfspNumber;
        }
        public async static Task<string> GetCFSPShipmentNumberSuffix()
        {
            await SemaphoreSlim.WaitAsync();
            try
            {
                var setting = await Database.Of<Settings>().FirstOrDefault();
                var suffix = "00000";
                suffix = setting.CFSPShipmentNumber.ToString("00000");
                await Database.Update(setting.Clone(), x => x.CFSPShipmentNumber += 1);

                return suffix;
            }
            finally
            {
                SemaphoreSlim.Release();
            }
        }

        public static string ResourceVersion => Config.Get("App.Resource.Version");


    }
}
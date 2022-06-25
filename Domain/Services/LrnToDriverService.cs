using Olive;
using Olive.Entities;
using Olive.Entities.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class LrnToDriverService : ILrnToDriverService
    {
        IDatabase Database;
        ISmsService SmsService;
        public LrnToDriverService(IDatabase database, ISmsService smsService)
        {
            Database = database;
            SmsService = smsService;
        }

        public async Task<LrnToDriverStatus> GenerateMessage(LrnToDriverMessage message)
        {
            if (message.MobileNumber.IsEmpty())
                return LrnToDriverStatus.NoNumber;

            if (message.CountryId == null)
                return LrnToDriverStatus.NoCountryCode;

            try
            {
                var savedMessage = await Database.Save(message);

                return LrnToDriverStatus.Faield;
            }
            catch (Exception ex)
            {
                Log.For<MFAMessage>().Error(ex);
                return LrnToDriverStatus.Faield;
            }
        }
    }

    public enum LrnToDriverStatus
    {
        Allowed,
        Denied,
        Sent,
        Faield,
        NoCountryCode,
        NoNumber,
    }
}

using Olive;
using Olive.Entities;
using Olive.Entities.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class MFAService : IMFAService
    {
        IDatabase Database;
        ISmsService SmsService;
        public MFAService(IDatabase database, ISmsService smsService)
        {
            Database = database;
            SmsService = smsService;
        }
      
        public async Task<MFAStatus> ValidateMFA(User user, string key)
        {
            if (!AppSetting.MFAEnabled)
                return MFAStatus.Allowed;

            var result = await Database.Any<MFAMessage>(t => t.MFAKey == key && t.UserId == user && !t.Expired && t.Sent);

            return result ? MFAStatus.Allowed : MFAStatus.Denied;
        }

        public async Task<(MFAStatus status , string key)> GenerateMFA(User user)
        {
            if (user.MobileNumber.IsEmpty())
                return (MFAStatus.NoNumber , null);

            var key = new Random().Next(11111, 99999).ToString();
            try
            {
                var mfa = await Database.Save(new MFAMessage
                {
                    MFAKey = key,
                    User = user,
                    To = user.MobileNumber,
                });

                var dispatch = await SmsService.Dispatch(mfa);

                if (dispatch == MFAStatus.Sent) 
                    return (MFAStatus.Sent, key);

                return (MFAStatus.Faield, null);
            }
            catch (Exception ex)
            {
                Log.For<MFAMessage>().Error(ex);
                return (MFAStatus.Faield, null);
            }
        }
    }

    public enum MFAStatus
    {
        Allowed,
        Denied,
        Sent,
        Faield,
        NoNumber,
    }
}

using Olive;
using Olive.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class BroadcastingMessage : IBroadcastingMessage
    {
        static IDatabase Database => Context.Current.Database();

        public async Task SendBroadCastMessages()
        {
            var messages = await Database.GetList<BroadcastMessage>(x => !x.HasSent).ToList();

            foreach (var message in messages)
            {
                if (message.SendToId == MessageSendToType.Channelports)
                {
                    await SendToChannelPortUser(message);
                }
                else if (message.SendToId == MessageSendToType.Customers)
                {
                    await SendToCompanyUsers(message);
                }
                else
                {
                    await SendToChannelPortUser(message);
                    await SendToCompanyUsers(message);
                }
            }
        }

        private async Task SendToCompanyUsers(BroadcastMessage message)
        {
            var companies = await Database.GetList<Company>(x => !x.IsDeactivated && !x.IsCreatedFromAPI);
            companies = await FilterCompaniesBySuppliedCriteria(message, companies);
            var attachments = GetUploadedAttachments(message);
            await companies.ForEachAsync(degreeOfParallelism: 3, async company =>
            {
                //company user notification
                foreach (var user in await company.CompanyUsers.Where(x => !x.IsDeactivated).GetList())
                {
                    await Database.Save(new BroadcastClientsMessage
                    {
                        Message = message,
                        User = user,
                        Company = company,
                    });
                }
                await Database.Update(await Database.Reload(message), x => x.HasSent = true);
            });
        }

        private static async Task SendToChannelPortUser(BroadcastMessage message)
        {
            var users = await Database.GetList<ChannelPortsUser>(x => !x.IsDeactivated).ToList();
            foreach (var user in users)
            {
                await Database.Save(new BroadcastClientsMessage
                {
                    Message = message,
                    User = user,
                });
            }
            await Database.Update(await Database.Reload(message), x => x.HasSent = true);
        }

        public async Task DeleteEmailMessagesMoreThen7Days()
        {
            var messages = await Database.GetList<EmailMessage>(x => x.SendableDate <= LocalTime.Now.AddDays(-7));
            await messages.DoAsync(x => Database.Delete(x, DeleteBehaviour.BypassAll));

        }

        private IEnumerable<Blob> GetUploadedAttachments(BroadcastMessage message)
        {
            var attachments = new List<Blob>();
            if (message.Attachment?.FileName != "NoFile.Empty")
            {
                attachments.Add(message.Attachment);
            }
            if (message.Attachment2?.FileName != "NoFile.Empty")
            {
                attachments.Add(message.Attachment2);
            }
            if (message.Attachment3?.FileName != "NoFile.Empty")
            {
                attachments.Add(message.Attachment3);
            }
            return attachments;
        }

        private async Task<IEnumerable<Company>> FilterCompaniesBySuppliedCriteria(BroadcastMessage message, IEnumerable<Company> companies)
        {
            var companyTypes = await message.CompanyTypes;
            var gvmsTypes = await message.GVMSTypes;
            var inboundSafetySecurityOptions = await message.InboundSafetyAndSecurityOptions;

            if (companyTypes.HasAny())
            {
                companies = await companies.Where(async (companies) => companies.TypeId.IsAnyOf(companyTypes));
            }

            if (gvmsTypes.HasAny())
            {
                companies = await companies.Where(async (companies) => companies.GVMSId.IsAnyOf(gvmsTypes));
            }

            if (inboundSafetySecurityOptions.HasAny())
            {
                companies = await companies.Where(async (companies) => companies.SafetyAndSecurityInboundId.IsAnyOf(inboundSafetySecurityOptions));
            }

            return companies;
        }


        public async Task RemovedUnConfirmResponse()
        {
            using (var scope = Database.CreateTransactionScope())
            {
                var response = await Database.Of<Response>().Where(x => x.IsConfirm == false).GetList().ToList();
                foreach (var item in response)
                {
                    await Database.Delete(await Database.Of<UserReponseNotification>().Where(x => x.ResponseId == item).GetList());
                    await Database.Delete(await Database.Of<ResponseAttachment>().Where(x => x.ResponseId == item).GetList());
                    await Database.Delete(item);
                }
                scope.Complete();
            }
        }
    }
}

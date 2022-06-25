using System;
using System.Collections.Generic;
using System.Text;
using IO.ClickSend.ClickSend.Api;
using IO.ClickSend.Client;
using IO.ClickSend.ClickSend.Model;
using Olive.Email;
using System.Threading.Tasks;
using System.Net.Mail;
using Olive;
using Newtonsoft.Json;

namespace Domain
{
    public class ClickSendDispatcher : IEmailDispatcher
    {
        public async Task Dispatch(MailMessage mail, IEmailMessage message)
        {
            var configuration = new Configuration
            {
                Username = AppSetting.EmailAPIUserName,
                Password = AppSetting.EmailAPIKey
            };

            var transactionalEmailApi = new TransactionalEmailApi(configuration);
            var listOfRecipients = new List<EmailRecipient>();
            var listOfCC = new List<EmailRecipient>();
            var listOfBCC = new List<EmailRecipient>();
            var listOfAttachments = new List<IO.ClickSend.ClickSend.Model.Attachment>();

            foreach (var item in mail.To)
            {
                listOfRecipients.Add(new EmailRecipient(
                     email: item.Address,
                     name: item.DisplayName
                 ));
            }

            foreach (var item in mail.CC)
            {
                listOfCC.Add(new EmailRecipient(
                    email: item.Address,
                    name: item.DisplayName
                ));
            }

            foreach (var item in mail.Bcc)
            {
                listOfBCC.Add(new EmailRecipient(
                    email: item.Address,
                    name: item.DisplayName
                ));
            }

            foreach (var attachment in mail.Attachments)
            {
                listOfAttachments.Add(new IO.ClickSend.ClickSend.Model.Attachment(
                   content: Convert.ToBase64String(attachment.ContentStream.ReadAllBytes()),
                   type: attachment.ContentType.ToString(),
                   filename: attachment.Name,
                   disposition: attachment.ContentDisposition.DispositionType,
                   contentId: attachment.ContentId
               ));
            }

            var emailFrom = new EmailFrom(
                emailAddressId: AppSetting.EmailAPIID,
                name: mail.From.DisplayName
            );
            try
            {
                var response = await transactionalEmailApi.EmailSendPostAsync(new Email(
                   to: listOfRecipients,
                   cc: listOfCC,
                   bcc: listOfBCC,
                   from: emailFrom,
                   subject: mail.Subject,
                   body: mail.Body,
                   attachments: listOfAttachments
               ));

                var responseJson = JsonConvert.DeserializeObject<dynamic>(response);

                if (responseJson.response_code != "SUCCESS")
                {
                    throw new Exception($"Unable to sent email {mail.To}");
                }
            }
            catch (Exception e)
            {
                Log.For(this).Error(e);
                throw;
            }

        }
    }
}

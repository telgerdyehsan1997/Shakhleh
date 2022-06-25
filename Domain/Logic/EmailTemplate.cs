namespace Domain
{
    using Olive;
    using Olive.Email;
    using Olive.Entities;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Provides the business logic for EmailTemplate class.
    /// </summary>
    partial class EmailTemplate : IEmailTemplate
    {
        public override async Task Validate()
        {
            await base.Validate();
            this.EnsurePlaceholders();
        }

        public async Task Send(User toUser, object mergeData, Action<EmailMessage> customise = null)
        {
            await Send(toUser.Email, mergeData, null, customise);
        }

        public async Task Send(string to, object mergeData, IEnumerable<string> bccs = null, Action<EmailMessage> customise = null, IEnumerable<Blob> attachments = null, string subject = null)
        {
            if (mergeData == null) throw new ArgumentNullException(nameof(mergeData));
            if (to.IsEmpty()) throw new ArgumentNullException(nameof(to));
            var subjectTo = subject.Or(this.MergeSubject(mergeData));
            var message = new EmailMessage
            {
                Html = true,
                Subject = subjectTo,
                Body = (this.MergeBody(mergeData) + GetEmailSignature()).Replace("Â", ""),
                To = to,
                Bcc = bccs?.ExceptNull()?.ToString(","),
                SendableDate = LocalTime.Now
            };

            customise?.Invoke(message);


            if (attachments != null)
                await attachments.ExceptNull().Do(async x =>
                {
                    await message.Attach(x);
                });

            await Database.Save(message);
        }

        /// <summary>
        /// Sends an email to the specified User using the Password Reset email template.
        /// </summary>
        public static async Task SendPasswordResetEmail(User user)
        {
            await PasswordHasBeenReset.Send(user, new
            {
                UserName = user.Name,
            });
        }

        public static async Task SendEADDeliveryDocument(Consignment consignment, Blob transitDocument, bool isX2 = false, bool isCustom = false)
        {
            var mergeData = new
            {
                REPORTNAME = transitDocument.FileNameWithoutExtension,
                CUSTOMERREFERENCE = consignment.Shipment.MyReferenceForCPInvoice,
                TRACKINGNUMBER = consignment.Shipment.TrackingNumber,
                ENTRYNUMBER = consignment.EntryReference,
                DUCRNUMBER = consignment.UCR.RemoveWhitespace(),
                ENTRYREFERENCENUMBER = consignment.EntryReference.RemoveWhitespace(),
                DATE = LocalTime.Now,
            };

            var bccs = await consignment.Shipment.GetExtraInformer();

            if (isX2)
            {
                var to = isCustom ? Config.Get("X2Email") : consignment.Shipment.PrimaryContact.Email;
                var toBccs = isCustom ? null : bccs;

                await X2EADDocumentDelivery.Send(to, mergeData, bccs: toBccs, customise: doc => doc.Attach(transitDocument));
            }
            else
                await EADDocumentDelivery.Send(consignment.Shipment.PrimaryContact.Email, mergeData, bccs: bccs, customise: doc => doc.Attach(transitDocument));
        }

        public static async Task SendEADDeliveryDocuments(Consignment consignment, IEnumerable<ConsignmentDocument> transitDocuments, bool isX2 = false, bool isCustom = false)
        {
            var mergeData = new
            {
                REPORTNAME = transitDocuments.Select(x => x.File.FileNameWithoutExtension).ToString(", "),
                CUSTOMERREFERENCE = consignment.Shipment.MyReferenceForCPInvoice,
                TRACKINGNUMBER = consignment.Shipment.TrackingNumber,
                ENTRYNUMBER = consignment.EntryReference,
                DATE = LocalTime.Now,
            };

            var bccs = await consignment.Shipment.GetExtraInformer();

            if (isX2)
            {
                var to = isCustom ? Config.Get("X2Email") : consignment.Shipment.PrimaryContact.Email;
                var toBccs = isCustom ? null : bccs;

                await X2EADDocumentDelivery.Send(to,
                    mergeData,
                    bccs: toBccs,
                    attachments: transitDocuments.Select(x => x.File));
            }
            else
                await EADDocumentDelivery.Send(consignment.Shipment.PrimaryContact.Email,
                    mergeData,
                    bccs: bccs,
                    attachments: transitDocuments.Select(x => x.File));
        }

        public static async Task SendRoute3DocumentEmail(string emailAddress, string consignmentReferenceNumber)
        {
            await RouteDocumentsEmail.Send(emailAddress, new
            {
                CONSIGNMENTREFERENCENUMBER = consignmentReferenceNumber,
                //CUSTOMERREFERENCE=
            });
        }


        public static async Task SendInventoryUsedWithNoUCN(string customeReference, string consigmentNumber, string dtibadge)
        {
            var to = Config.Get("X2Email");
            var mergeData = new
            {
                CONSIGNMENTNUMBER = consigmentNumber,
                CUSTOMERREFERENCE = customeReference,
                DTIBADGE = dtibadge
            };
            await InventoryUsed.Send(to, mergeData);
        }

        public static async Task SendControlEmail(string trackingNumber, string customeReference, string uktrader, string consigmentNumber)
        {
            var to = Config.Get("X2Email");
            var mergeData = new
            {
                CUSTOMERREFERENCE = customeReference,
                TRACKINGNUMBER = trackingNumber,
                UKTRADER = uktrader,
                CONSIGMENTNUMBER = consigmentNumber
            };
            await Control.Send(to, mergeData);
        }

        public static async Task ConsignmentInDraftForMoreThan7DaysEmail(string trackingNumber, string status, string customeReference)
        {
            var to = Config.Get("X2Email");
            var mergeData = new
            {
                TRACKINGNUMBER = trackingNumber,
                CONSIGNMENTSTATUS = status,
                CUSTOMERREFERENCE = customeReference
            };
            await ConsignmentInDraftForMoreThan7Days.Send(to, mergeData);
        }
        public static async Task SendStatusNotificationEmail(string trackingNumber, string myReferenceForCPInvoice, string status)
        {
            var to = Config.Get("X2Email");
            var mergeData = new
            {
                TRACKINGNUMBER = trackingNumber,
                CONSIGNMENTSTATUS = status,
                CUSTOMERREFERENCE = myReferenceForCPInvoice

            };
            await StatusNotificationEmail.Send(to, mergeData);
        }
        public static async Task SendCustomerStatusNotificationEmail(string to, string trackingNumber, string myReferenceForCPInvoice, string status)
        {
            var mergeData = new
            {
                TRACKINGNUMBER = trackingNumber,
                CONSIGNMENTSTATUS = status,
                CUSTOMERREFERENCE = myReferenceForCPInvoice
            };
            await StatusNotificationEmail.Send(to, mergeData);
        }

        public static async Task StatusArchiveNotification(string trackingNumber, string consigmentNumber, string customerReference, string toCustomer)
        {
            var to = Config.Get("X2Email");
            var mergeData = new
            {
                TRACKINGNUMBER = trackingNumber,
                CONSIGNMENTNUMBER = consigmentNumber,
                CUSTOMERREFERENCE = customerReference,
            };
            await DraftConsignmentArchived.Send(to, mergeData);
            if (toCustomer.HasValue())
                await DraftConsignmentArchived.Send(toCustomer, mergeData);
        }

        public static async Task SendArchiveNotification(string name, string message, string user, string trackingNumber = null, string customerReference = null)
        {
            object mergeData = null;
            var subject = "";
            if (trackingNumber.HasValue() && customerReference.HasValue())
            {
                mergeData = new
                {
                    ENTITYNAME = name,
                    DATETIME = LocalTime.Now.ToString(),
                    USERNAME = user,
                    MESSAGE = message,
                    TRACKINGNUMBER = trackingNumber,
                    CUSTOMERREFERENCE = customerReference,
                };
                subject = $"Archive Notification - {name},Your reference {customerReference},Tracking number {trackingNumber}";
            }
            else
            {
                mergeData = new
                {
                    ENTITYNAME = name,
                    DATETIME = LocalTime.Now.ToString(),
                    USERNAME = user,
                    MESSAGE = message,
                };
            }

            await ArchiveNotification.Send(AppSetting.CustomsProEmail, mergeData, null, null, null, subject);
        }

        public static async Task SendUnArchiveNotification(string name, string message, string user, string trackingNumber = null, string customerReference = null)
        {
            var to = Config.Get("X2Email");
            object mergeData = null;
            var subject = "";
            if (trackingNumber.HasValue() && customerReference.HasValue())
            {
                mergeData = new
                {
                    ENTITYNAME = name,
                    DATETIME = LocalTime.Now.ToString(),
                    USERNAME = user,
                    MESSAGE = message,
                    TRACKINGNUMBER = trackingNumber,
                    CUSTOMERREFERENCE = customerReference,
                };
                subject = $"Unarchive Notification - {name},Your reference {customerReference},Tracking number {trackingNumber}";
            }
            else
            {
                mergeData = new
                {
                    ENTITYNAME = name,
                    DATETIME = LocalTime.Now.ToString(),
                    USERNAME = user,
                    MESSAGE = message,
                    TRACKINGNUMBER = trackingNumber
                };
            }
            await UnarchiveNotification.Send(to, mergeData, null, null, null, subject);
        }

        public static async Task SendManualQuota(string trackingNumber, string customeReference, string uktrader, string consigmentNumber)
        {
            var to = Config.Get("X2Email");
            var mergeData = new
            {
                CUSTOMERREFERENCE = customeReference,
                TRACKINGNUMBER = trackingNumber,
                UKTRADER = uktrader,
                CONSIGMENTNUMBER = consigmentNumber
            };
            await ManualQuota.Send(to, mergeData);
        }

        string GetEmailSignature()
        {
            return Config.Get("Email:EmailSignature")?.FormatWith(AppSetting.ChannelPort);
        }

        public static async Task<bool> SendCopyShipmentDocument(SendMailEntryCopyViewModel sendMailEntryCopyViewModel)
        {
            var file = new List<Blob>();
            var consignments = await sendMailEntryCopyViewModel.Shipment.Consignments.GetList().ToList();

            foreach (var consignment in consignments)
            {
                var doc = (await consignment.Documents
                    .GetList()
                    .OrderByDescending(x => x.DateRecieved)
                    .Where(x => x.File.FileExtension == ".pdf"))
                    .GroupBy(x => x.File.FileName)
                    .Select(x => x.FirstOrDefault().File);

                file.AddRange(doc);
            }
            if (file.HasAny())
            {
                var mergeData = new
                {
                    TRACKINGNUMBER = sendMailEntryCopyViewModel.Shipment.TrackingNumber,
                    CUSTOMERREFERENCE = sendMailEntryCopyViewModel.Shipment.MyReferenceForCPInvoice
                };
                await SendCopyEntry.Send(sendMailEntryCopyViewModel.EmailAddress, mergeData, attachments: file);
                return true;
            }
            return false;
        }


        public static async Task ShipmentFileDeliveries(Consignment consignment)
        {
            try
            {
                var file = (await consignment.Documents
                    .GetList()
                    .OrderByDescending(x => x.DateRecieved)
                    .Where(x => x.File.FileExtension == ".pdf"))
                    .GroupBy(x => x.File.FileName)
                    .Select(x => x.FirstOrDefault().File);

                if (file.HasAny())
                {
                    var mergeData = new
                    {
                        TRACKINGNUMBER = consignment.Shipment.TrackingNumber,
                        CUSTOMERREFERENCE = consignment.Shipment.MyReferenceForCPInvoice,
                        ENTRYREFERENCENUMBER = consignment.EntryReference,
                        BARCODE = ImageTemplates(consignment.EntryReference),
                        ICSMRN = consignment.ICSMRNNumber,
                        ICSMRNBARCODE = ImageTemplates(consignment.ICSMRNNumber),
                    };
                    await ShipmentFileDelivery.Send(consignment.Shipment.PrimaryContact.Email, mergeData, attachments: file);
                    await Database.Update(consignment, x => x.HasShipmentFileDelivery = true);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private static string ImageTemplates(string mrn)
        {
            var imagePath = Helper.BarcodeGenerator(mrn);
            return @"<img src = " + imagePath + "  />";
        }
    }
}
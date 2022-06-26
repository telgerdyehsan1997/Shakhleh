using AsmApiService;
using Olive;
using Olive.Entities;
using Olive.Entities.Data;
using Olive.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class ReferenceData : IReferenceData
    {
        IDatabase Database;
        public ReferenceData(IDatabase database) => Database = database;

        async Task<T> Create<T>(T item) where T : IEntity
        {
            await Database.Save(item, SaveBehaviour.BypassAll);

            return item;
        }

        async Task<List<T>> Create<T>(IEnumerable<T> items) where T : IEntity
        {
            await Database.Save(items, SaveBehaviour.BypassAll);
            return items.ToList();
        }

        public async Task Create()
        {

            try
            {
                await CreateImportStatuses();
                await CreateImportTypes();
                await CreateEmailTemplates();
                await CreateUsers();
            }
            catch (Exception ex)
            {
                var details = ex.InnerException;
                throw;
            }
        }


        async Task CreateUsers()
        {
            var pass = SecurePassword.Create("test");
            await Create(new AdminUser
            {
#pragma warning disable GCop646 // Email addresses should not be hard-coded. Move this to Settings table or Config file.
                Email = "admin@uat.co",
#pragma warning restore GCop646 // Email addresses should not be hard-coded. Move this to Settings table or Config file.
                FirstName = "Ehsan",
                LastName = "Admin",
                Password = pass.Password,
                Salt = pass.Salt,
                IsAdmin = true,
                MobileNumber = "08004002343",
                ID = "90fa6886-d141-4a2e-8679-fc129958904c".To<Guid>()
            });

        }
        async Task CreateEmailTemplates()
        {
            await Create(new EmailTemplate
            {
                Key = nameof(EmailTemplate.RecoverPassword),
                Name = "Recover Password",
                Subject = "Channel Ports - Recover Password",
                Body = @"<p> Dear [#USERNAME#], </p>

                <p> Please click on the following link to reset your password. If you did not request this password reset then please contact us. </p>

                <div> [#LINK#] </div>

                <p>
                Best regards <br />
                Channel Ports
                </p>",
                MandatoryPlaceholders = "USERNAME, LINK",
                ID = "3c3f79e1-3aed-4a05-b1ef-8800de7344f1".To<Guid>()
            });

            await Create(new EmailTemplate
            {
                Key = nameof(EmailTemplate.SendCopyEntry),
                Name = "Send Copy Entry",
                Subject = "Copy Entry Documents for reference [#CUSTOMERREFERENCE#], Tracking number [#TRACKINGNUMBER#]",
                Body = @" <p> Please find attached the Entry Documents for : [#TRACKINGNUMBER#]. <p>
                 Best regards <br />
                Channel Ports
                </p>",
                MandatoryPlaceholders = "CUSTOMERREFERENCE, TRACKINGNUMBER",
                ID = "3c3f79e1-3aed-4a05-b1ef-8800de7344f2".To<Guid>()
            });

            await Create(new EmailTemplate
            {
                Key = nameof(EmailTemplate.ClosedTicket),
                Name = "Closed Support Tickets ",
                Subject = "Closed Support Tickets for Tracking number [#TRACKINGNUMBER#] , Ticket Number [#TICKETBNUMBER#]",
                Body = @" <p>Confirmation of closed support ticket, Ticket Number: [#TICKETBNUMBER#]</p>",
                MandatoryPlaceholders = "TRACKINGNUMBER,TICKETBNUMBER",
                ID = "3c3f79e1-3aed-4a05-b1ef-8800de7344f3".To<Guid>()
            });
            await Create(new EmailTemplate
            {
                Key = nameof(EmailTemplate.RaisedTicket),
                Name = "Raised Support Tickets ",
                Subject = "Raised Support Tickets for Tracking number [#TRACKINGNUMBER#] , Ticket Number [#TICKETBNUMBER#]",
                Body = @"<p>Confirmation of raised Support Ticket. Ticket Number: [#TICKETBNUMBER#] </p>",
                MandatoryPlaceholders = "TRACKINGNUMBER,TICKETBNUMBER",
                ID = "3c3f79e1-3aed-4a05-b1ef-8800de7344f9".To<Guid>()
            });
            await Create(new EmailTemplate
            {
                Key = nameof(EmailTemplate.ResponsesTicket),
                Name = "Support Ticket Response",
                Subject = "Response Added to  Ticket Number [#TICKETBNUMBER#], Tracking number [#TRACKINGNUMBER#]",
                Body = @" <p> A Response has been added to a Support Ticket that you have raised. Ticket Number: [#TICKETBNUMBER#] Response Message: [#MESSAGE#]<p> 
                <br /> <p> You can view and add a Response to the Ticket in CustomsPro here: <a href=""[#LINK#]""> Link </a></p>",
                MandatoryPlaceholders = "TRACKINGNUMBER,TICKETBNUMBER,MESSAGE,LINK",
                ID = "3c3f79e1-3aed-4a05-b1ef-8800de7344f4".To<Guid>()
            });
            await Create(new EmailTemplate
            {
                Key = nameof(EmailTemplate.ShipmentFileDelivery),
                Name = "Shipment File Delivery email.",
                Subject = "Shipment File Delivery for Your reference [#CUSTOMERREFERENCE#].Tracking number [#TRACKINGNUMBER#]",
                Body = @"<p> Please find enclosed your Shipment File Delivery for your consignment.</p><br/>
                       <p>You may need to send this document onto the company arranging the transport to complete a GVMS notification.</p><br/>
                       <p>If required the reference which is to be quoted on the GVMS application is the UK import entry number and for this consignment it is:-</p><br/>
                       <p>[#ENTRYREFERENCENUMBER#]</p><br/>
                       <p>[#BARCODE#]</p><br/>
                       <p>In addition as you have requested us to complete the in to UK Safety and Security declaration the S&S MRN number for this consignment is:-</p><br/>
                       <p>[#ICSMRN#]</p><br/>
                       <p>[#ICSMRNBARCODE#]</p><br/>

                Best regards <br />
                Channel Ports
                </p>",
                MandatoryPlaceholders = "CUSTOMERREFERENCE,TRACKINGNUMBER,ENTRYREFERENCENUMBER,BARCODE,ICSMRN,ICSMRNBARCODE",
                ID = "3c3f79e1-3aed-4a05-b1ef-8800de7344f0".To<Guid>()
            });
            await Create(new EmailTemplate
            {
                Key = nameof(EmailTemplate.CFSPMonthlyReport),
                Name = "CFSP Monthly Report",
                Subject = "CFSP Report for [#MONTH#]",
                Body = @"<p>Good afternoon,</p> 
                <br /><p>Please find attached your monthly report for: [#MONTH#]</p><br />
                Best regards <br />
                Channel Ports
                </p>",
                MandatoryPlaceholders = "MONTH",
                ID = "3c3f79e1-3aed-4a05-b1ef-8800de73445a".To<Guid>()
            });
            await Create(new EmailTemplate
            {
                Key = nameof(EmailTemplate.InventoryUsed),
                Name = "Inventory is used and the UCN field is blank",
                Subject = "Clients reference [#CUSTOMERREFERENCE#], Consignment number [#CONSIGNMENTNUMBER#]",
                Body = @"<p>This shipment will remain in draft until you update the details to include UCN number provided to you be either the shipping line or transport company.</p>
                       <p> You also need to ensure ChannelPorts have been nominated as the clearance agent using our badge code [#DTIBADGE#]</p>
                        <p>
                        Best regards <br />
                        Channel Ports
                        </p>",
                MandatoryPlaceholders = "CUSTOMERREFERENCE, CONSIGNMENTNUMBER,DTIBADGE",
                ID = "3c3f79e1-3aed-4a05-b1ef-8800de73455f".To<Guid>()
            });

            await Create(new EmailTemplate
            {
                Key = nameof(EmailTemplate.WelcomeEmail),
                Name = "Welcome Email",
                Subject = "Welcome to Channel Ports",
                Body = @"<p>Dear [#USERNAME#],</p>
                <p>Your account has been created successfully. Please click on the following link to set your password.</p>
                <div>  [#LINK#] </div>",
                MandatoryPlaceholders = "USERNAME, LINK",
                ID = "a9217dca-860a-40c9-982b-fb43e5047f5e".To<Guid>()
            });

            await Create(new EmailTemplate
            {
                Key = nameof(EmailTemplate.ShipmentSubmission),
                Name = "Shipment Submission",
                Subject = "Your Reference [#VERIFICATIONCODE#],Customer reference [#CUSTOMERREFERENCE#]",
                Body = @"We confirm that our tracking number [#TRACKINGNUMBER#] for [#VEHICLENUMBER#] has been accepted and the entry logged with customs.
                Should there be any issues with this shipment we will be in contact with you to resolve these.",
                MandatoryPlaceholders = "VERIFICATIONCODE, TRACKINGNUMBER, VEHICLENUMBER,CUSTOMERREFERENCE",
                ID = "23684b14-3775-4529-befc-563155cb0b23".To<Guid>()
            });

            await Create(new EmailTemplate
            {
                Key = nameof(EmailTemplate.TransitDocument),
                Name = "Shipment Submission",
                Subject = "Re: Vehicle number [#VEHICLENUMBER#]/[#TRAILERNUMBER#] Customer reference [#CUSTOMERREFERENCE#] Our reference [#TRACKINGNUMBER#]",
                Body = @"<p>Please find attached the NCTS document for the above shipment</p>",
                MandatoryPlaceholders = "VEHICLENUMBER, TRAILERNUMBER, CUSTOMERREFERENCE, TRACKINGNUMBER",
                ID = "472BC001-7AC2-4B2E-85B8-9B400F4B2734".To<Guid>()
            });

            await Create(new EmailTemplate
            {
                Key = nameof(EmailTemplate.EADDocumentDelivery),
                Name = "EAD Document Delivery",
                Subject = "[#REPORTNAME#], Your reference [#CUSTOMERREFERENCE#], Tracking number [#TRACKINGNUMBER#]",
                Body = @"<p>Please find enclosed your [#REPORTNAME#] for your consignment.</p>",
                MandatoryPlaceholders = "REPORTNAME, CUSTOMERREFERENCE, TRACKINGNUMBER, ENTRYREFERENCENUMBER, BARCODE",
                ID = "06273D5F-1742-404D-9934-CCB8FE889810".To<Guid>()
            });

            await Create(new EmailTemplate
            {
                Key = nameof(EmailTemplate.X2EADDocumentDelivery),
                Name = "Shipment File Delivery",
                Subject = "[#REPORTNAME#], Your reference [#CUSTOMERREFERENCE#], Tracking number [#TRACKINGNUMBER#]",
                Body = @"<p>Please find enclosed your [#REPORTNAME#] for your consignment.</p>
                         <p><b>PERMISSION TO PROGRESS</b></p>

                         <p>Entry number: [#ENTRYNUMBER#]</p>
                         <p>Permission to Progress (P2P) at:[#DATE#]</p>
                            <p>
                Best regards,<br/>
                Ro Ro clearances</p>",
                MandatoryPlaceholders = "REPORTNAME, CUSTOMERREFERENCE, TRACKINGNUMBER, ENTRYNUMBER, DATE, DUCRNUMBER, BARCODE",
                ID = "17273D5F-1742-404D-9934-CCB8FE889810".To<Guid>()
            });


            await Create(new EmailTemplate
            {
                Key = nameof(EmailTemplate.NCTSDocumentDelivery),
                Name = "NCTS Shipment File Delivery",
                Subject = "[#REPORTNAME#], Your reference [#CUSTOMERREFERENCE#], Tracking number [#TRACKINGNUMBER#]",
                Body = @"<p>Please find enclosed your [#REPORTNAME#] for your consignment.</p>


                Best regards,<br/>
                Ro Ro clearances",
                MandatoryPlaceholders = "REPORTNAME, CUSTOMERREFERENCE, TRACKINGNUMBER, NCTSMRNNUMBER, BARCODE",
                ID = "1EBFE9F4-9F43-4352-BA6B-AB5ACBBFDBD0".To<Guid>()
            });

            await Create(new EmailTemplate
            {
                Key = nameof(EmailTemplate.RouteDocumentsEmail),
                Name = "Route 3 documents",
                Subject = "Route 3 documents to be sent to HMRC for [#CONSIGNMENTREFERENCENUMBER#], Your reference [#CUSTOMERREFERENCE#]",
                Body = @"Please send all of the following documents to the HMRC address below

                C88
                E2
                Invoices
                Packing list (if available)
                Original EUR 1 / ATR / GSP (if applicable)
                T1 / T2 ( if applicable)
                CMR - copy (if available)
                Health Certificates (if applicable)

                Address documents are to be sent to is:-

                Freepost RTGR-LSCG-LTJS
                HM Revenue and Customs
                National Clearance Hub, Ralli Quays
                3 Stanley Street, Salford
                M60 9LA",
                MandatoryPlaceholders = "CONSIGNMENTREFERENCENUMBER,CUSTOMERREFERENCE",
                ID = "9edd61d7-b5e8-4473-be96-97cab58992cf".To<Guid>()
            });

            await Create(new EmailTemplate
            {
                Key = nameof(EmailTemplate.ASMAcceptedClientNotification),
                Name = "ASM Accepted Client Notification",
                Subject = "ASM Accepted Client Notification,Your reference [#CUSTOMERREFERENCE#]",
                Body = @"<p>Hi [#CUSTOMERNAME#]</p>    <p>Tracking Number&nbsp; [#TRACKINGNUMBER#], Your reference [#CUSTOMERREFERENCE#], has been accepted by NCTS and has been given a LRN Number of&nbsp;[#LRNNUMBER#].</p>    <p>Please ensure the driver is given this number when presenting himself to an Inland Border Facility (IBF) for release of the Transit Document. A full list of IBF&#39;s can be found here&nbsp;</p>    <p>https://www.gov.uk/government/publications/attending-an-inland-border-facility/attending-an-inland-border-facility</p>    <p>Regards</p>    <p>CustomsPro</p>  ",
                MandatoryPlaceholders = "CUSTOMERNAME, TRACKINGNUMBER, LRNNUMBER,CUSTOMERREFERENCE",
                ID = "0b4bf153-0ffc-42ff-9966-a4d3427b4c24".To<Guid>()
            });

            await Create(new EmailTemplate
            {
                Key = nameof(EmailTemplate.OnHoldDuetoValue),
                Name = "On Hold Due to Value",
                Subject = "Consignment is on hold due to exceed amount,Your reference [#CUSTOMERREFERENCE#]",
                Body = @"<p> The consignment [#CONSIGNMENTNUMBER#], Your reference [#CUSTOMERREFERENCE#] total value is more than £[#VALUE#]. Please review it to authorise.</p>
                <p>
                Best regards <br/>
                Channel Ports </p>",
                MandatoryPlaceholders = "CONSIGNMENTNUMBER, TRACKINGNUMBER, CUSTOMERREFERENCE, VALUE",
                DateEmailSent = LocalTime.Now,
                ID = "0b4bf153-0ffc-42ff-9966-a4d3427b4c26".To<Guid>()
            });

            await Create(new EmailTemplate
            {
                Key = nameof(EmailTemplate.ConsignmentInDraftForMoreThan7Days),
                Name = "Consignment in Draft for more than 7 days",
                Subject = "Consignment in Draft for more than 7 days,Your reference [#CUSTOMERREFERENCE#]",
                Body = @"<p>Your reference [#CUSTOMERREFERENCE#],[#TRACKINGNUMBER#] - Email alert for when a job stays in [#CONSIGNMENTSTATUS#] for 1 week</p>
                <p>
                Best regards <br/>
                Channel Ports </p>",
                MandatoryPlaceholders = "TRACKINGNUMBER, CONSIGNMENTSTATUS,CUSTOMERREFERENCE",
                DateEmailSent = LocalTime.Now,
                ID = "0b4bf153-0ffc-42ff-9966-a4d3427b4c20".To<Guid>()
            });

            await Create(new EmailTemplate
            {
                Key = nameof(EmailTemplate.StatusNotificationEmail),
                Name = "Status Notification Email",
                Subject = "Tracking number [#TRACKINGNUMBER#],Your reference [#CUSTOMERREFERENCE#]",
                Body = @"<p>The status of this movement has changed to [#CONSIGNMENTSTATUS#]. </p>
                <p>For statuses which will prevent clearance please pass to the Tech Team so they can action.</p>
                <p>
                Best regards <br/>
                Channel Ports </p>",
                MandatoryPlaceholders = "TRACKINGNUMBER,CUSTOMERREFERENCE",
                DateEmailSent = LocalTime.Now,
                ID = "6faa9640-68fc-44f1-8f5a-c29e60b8e339".To<Guid>()
            });

            await Create(new EmailTemplate
            {
                Key = nameof(EmailTemplate.DraftConsignmentArchived),
                Name = "Draft Consignment Archived",
                Subject = "Your Tracking number [#TRACKINGNUMBER#] , Reference [#CUSTOMERREFERENCE#], Consignment number [#CONSIGNMENTNUMBER#]",
                Body = @" <p>This is to confirm further to our previous email this consignment [#CONSIGNMENTNUMBER#] has now been archived.</p>

                        <p>If this consignment should not have been archived, then please email Customs Pro customer support team..</p>
                <p>
                Best regards <br/>
                Channel Ports </p>",
                MandatoryPlaceholders = "TRACKINGNUMBER,CUSTOMERREFERENCE,CONSIGNMENTNUMBER",
                DateEmailSent = LocalTime.Now,
                ID = "0b4bf153-0ffc-42ff-9966-a4d3427b4c21".To<Guid>()
            });

            await Create(new EmailTemplate
            {
                Key = nameof(EmailTemplate.CommodityCodeControlEmail),
                Name = "Commodity Code Control Email",
                Subject = "Re: [#CONSIGNMENTTRACKINGNUMBER#], [#UKTRADER#],Your reference [#CUSTOMERREFERENCE#]",
                Body = @"
                <p>The goods covered by this Tracking number are subject to additional Controls. 
                Please ensure a manager is informed for IMMEDIATE action.</p>

                <p>Best regards <br />
                Channel Ports
                </p>",
                MandatoryPlaceholders = "CONSIGNMENTTRACKINGNUMBER, UKTRADER,CUSTOMERREFERENCE",
                ID = "4db6153c-5cdc-4ef2-955a-ead70d26d783".To<Guid>()
            });

            await Create(new EmailTemplate
            {
                Key = nameof(EmailTemplate.CustomsIntervention),
                Name = "Customs Intervention",
                Subject = "Customs Intervention,Your reference [#CUSTOMERREFERENCE#]",
                Body = @"
                <p>Tracking number [#TRACKINGNUMBER#] has a route of [#ROUTENUMBER#].</p>
                <p>Please take the necessary action immediately. </p>

                <p>Best regards <br />
                Channel Ports
                </p>",
                MandatoryPlaceholders = "TRACKINGNUMBER, ROUTENUMBER,CUSTOMERREFERENCE",
                ID = "798cdd23-7ddb-4492-82de-977adea90f36".To<Guid>()
            });

            await Create(new EmailTemplate
            {
                Key = nameof(EmailTemplate.ErrorWhileTransitASM),
                Name = "Error While Transit ASM",
                Subject = "Error while transmitting to ASM (retries 10 times),Your reference [#CUSTOMERREFERENCE#]",
                Body = @"
                <p>This is to advise you Tracking number [#TRACKINGNUMBER#] has been drafted because of transmit issue.</p>
                <p>Please take the necessary action immediately. </p>

                <p>Best regards <br />
                Channel Ports
                </p>",
                MandatoryPlaceholders = "TRACKINGNUMBER,CUSTOMERREFERENCE",
                ID = "3c43cbf9-dd14-4281-b580-eb5f256c177d".To<Guid>()
            });

            await Create(new EmailTemplate
            {
                Key = nameof(EmailTemplate.ArchiveNotification),
                Name = "Archive Notification",
                Subject = "Archive Notification - [#ENTITYNAME#]",
                Body = @"
                <p>Archived - [#ENTITYNAME#]</p>
                <p>Date - [#DATETIME#]</p>
                <p>Message - [#MESSAGE#]</p>
                <p>By - [#USERNAME#]</p>

                <p>Best regards <br />
                Channel Ports
                </p>",
                MandatoryPlaceholders = "ENTITYNAME, DATETIME,MESSAGE,USERNAME",
                ID = "6143dd18-ec58-4387-9ace-7d6110f6317d".To<Guid>()
            });

            await Create(new EmailTemplate
            {
                Key = nameof(EmailTemplate.UnarchiveNotification),
                Name = "UnarchiveNotification",
                Subject = "UnArchive Notification - [#ENTITYNAME#]",
                Body = @"
                <p>UnArchived - [#ENTITYNAME#] with Tracking number of [#TRACKINGNUMBER#]</p>
                <p>Date - [#DATETIME#]</p>
                <p>By - [#USERNAME#]</p>

                <p>Best regards <br />
                Channel Ports
                </p>",
                MandatoryPlaceholders = "ENTITYNAME, DATETIME,USERNAME,TRACKINGNUMBER",
                ID = "1675946a-e9aa-482c-a0d6-14878cc2089f".To<Guid>()
            });

            await Create(new EmailTemplate
            {
                Key = nameof(EmailTemplate.Control),
                Name = "Control",
                Subject = "Consignment is controlled by HMRC: Your reference [#CUSTOMERREFERENCE#],TrackingNumber :[#TRACKINGNUMBER#] ,UK Trader:[#UKTRADER#]",
                Body = @"
                <p>[#CONSIGMENTNUMBER#] consignment is controlled by HMRC please investigate and transmit when able.</p>
                <p>Best regards <br />
                Channel Ports
                </p>",
                MandatoryPlaceholders = "CONSIGMENTNUMBER,TRACKINGNUMBER,UKTRADER,CUSTOMERREFERENCE",
                ID = "5b6eff6d-e17a-44da-bf49-576a389a24f2".To<Guid>()
            });

            await Create(new EmailTemplate
            {
                Key = nameof(EmailTemplate.ManualQuota),
                Name = "Manual Quota",
                Subject = "Manual Quota: TrackingNumber :[#TRACKINGNUMBER#] ,UK Trader:[#UKTRADER#]",
                Body = @"
                <p>[#CONSIGMENTNUMBER#] consignment is manual quota please investigate and transmit when able.</p>
                <p>This entry could be subject to quota and you must check each item manually using the HMRC Online tariff to see if any goods are subject to quota and then update ASM.</p>
                <p>If you are in anyway unsure how to check for quota or if a quota is applicable based on the origin of the goods you must refer the entry to a more senior colleague.</p>
                <p>Best regards <br />
                Channel Ports
                </p>",
                MandatoryPlaceholders = "CONSIGMENTNUMBER,TRACKINGNUMBER,CUSTOMERREFERENCE,UKTRADER",
                ID = "7d6bd73e-50e4-41b0-9e5b-aaf8c455761d".To<Guid>()
            });
        }
        async Task CreateImportTypes()
        {
            var types = new List<ImportType>
            {
                new ImportType { Name = nameof(ImportType.CommodityCode)},
                new ImportType { Name = nameof(ImportType.Company)},
                new ImportType { Name = nameof(ImportType.Product)},
                new ImportType { Name = nameof(ImportType.AuthorisedLocation) },
                new ImportType { Name = nameof(ImportType.Commodity)},
                new ImportType { Name = nameof(ImportType.Itinerary)},
                new ImportType { Name = nameof(ImportType.UnCodes)},
                new ImportType { Name = nameof(ImportType.RowQuota)}

            };

            await Create(types.AsEnumerable());
        }
        async Task CreateImportStatuses()
        {
            var statuses = new List<ImportStatus>
            {
                new ImportStatus { Name = "Failed", ID = "0aed88c1-d13a-4608-9649-10ceb9cbdd59".To<Guid>()},
                new ImportStatus { Name = "Pending", ID = "4a78265c-1d82-4aff-999f-3a179a166a0f".To<Guid>()},
                new ImportStatus { Name = "Processing", ID = "c33f86c1-6e34-4457-85f6-7ec258198475".To<Guid>()},
                new ImportStatus { Name = "Successful", ID = "6ea772d1-cb14-4d2f-acf8-3782d013ebbc".To<Guid>()},
                new ImportStatus { Name = "Partial success", ID = "de16e1be-e704-483c-b90c-fe2c0eb22afa".To<Guid>()}
            };

            await Create(statuses.AsEnumerable());
        }

    }
}
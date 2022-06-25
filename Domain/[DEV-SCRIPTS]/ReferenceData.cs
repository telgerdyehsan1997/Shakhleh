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
            await Create(new Settings { Name = "Current", PasswordResetTicketExpiryMinutes = 60, SendNCTSMessageViaASM = true });

            try
            {
                await CreateMessageSentType();
                await CreateInvoiceStatus();
                await CreateInvoiceJobStatus();
                await CreateInvoiceType();
                await CreateInvoiceFrequencyTypes();
                await CreateImportStatuses();
                await CreateImportTypes();
                await CreateGVMSTypes();
                await CreateGVMSStatus();
                await CreateShipmentTypes();
                await CreateProgress();
                await CreateLicenceTypes();
                await CreateLicences();
                await CreateTransitOfficeFilestatus();
                await CreateCPC();
                await CreateSecondQuantityDescriptions();
                await CreateCurrencies();
                await CreateContentBlocks();
                await CreateTermsOfSale();
                await CreateVAT();
                await CreateCommodityCodes();
                await CreateTransitOffice();
                await CreatePortTypes();
                await CreatePorts();
                await CreateGuarantorType();
                await CreateEmailTemplates();
                await CreateCompanyTypes();
                await CreateNotifyTypes();
                await CreatePreferenceTypes();
                await CreateCountries();
                await CreateCompaniesAndAssociatedProducts();
                await CreateUsers();
                await CreateShipments();
                await CreateConsignments();
                await CreateCommodities();
                await CreateAuthorizedLocation();
                await CreateAuthorizedLocationDefaultGuaranteeLength();
                await CreateShipmentbaseTypes();
                await CreateCompanyUKTraderPartnerLinkTypes();
                await CreateDDPPOptions();
                await CreateDefaultLicenseCurrencyOptions();
                await CreateDefaultExchequerCode();
                await CreateDefaultLicences();
                await CreateLicenseStartingMonthOptions();
                await CreateTransactionTypes();
                await CreateSafetyAndSecurity();
                await CreateCFSPCPCNumber();
                await CreateCFSPType();
                await CreateTicketsAction();
                await CreateTicketsStatus();
            }
            catch (Exception ex)
            {
                var details = ex.InnerException;
                throw;
            }
        }

        private async Task CreateLicenseStartingMonthOptions()
        {
            var months = new List<LicenseStartingMonthOption>
            {
                new LicenseStartingMonthOption { Name = "Jan", MonthNumber = 1},
                new LicenseStartingMonthOption { Name = "Feb", MonthNumber = 2},
                new LicenseStartingMonthOption { Name = "Mar", MonthNumber = 3},
                new LicenseStartingMonthOption { Name = "Apr", MonthNumber = 4},
                new LicenseStartingMonthOption { Name = "May", MonthNumber = 5},
                new LicenseStartingMonthOption { Name = "Jun", MonthNumber = 6},
                new LicenseStartingMonthOption { Name = "Jul", MonthNumber = 7},
                new LicenseStartingMonthOption { Name = "Aug", MonthNumber = 8},
                new LicenseStartingMonthOption { Name = "Sep", MonthNumber = 9},
                new LicenseStartingMonthOption { Name = "Oct", MonthNumber = 10},
                new LicenseStartingMonthOption { Name = "Nov", MonthNumber = 11},
                new LicenseStartingMonthOption { Name = "Dec", MonthNumber = 12},
            };
            await Create(months);
        }

        private async Task CreateDefaultLicences()
        {
            await Create(new Charge
            {
                Name = "Custom",
                LicenseFee = 0,
                Currency = ChargeCurrencyOption.Pounds,
                FreeConsignments = 0,
                PricePerAdditionalConsignment = 0,
                PricePerCommodity = 0,
                IsDefault = true

            });
        }

        private async Task CreateInvoiceFrequencyTypes()
        {
            await Create(new InvoiceFrequencyType
            {
                Name = "Monthly"
            });

            await Create(new InvoiceFrequencyType
            {
                Name = "Yearly"
            });
        }

        private async Task CreateDefaultExchequerCode()
        {
            await Create(new ExchequerCode
            {
                NominalCode = "510107",
                CostCentre = "CUS",
                Department = "AAA"
            });
        }

        private async Task CreateDefaultLicenseCurrencyOptions()
        {
            await Create(new ChargeCurrencyOption
            {
                Name = "Euro"
            });
            await Create(new ChargeCurrencyOption
            {
                Name = "Pounds"
            });
        }

        private async Task CreateShipmentbaseTypes()
        {
            await Create(new ShipmentBaseType
            {
                Name = "EAD",
                Order = 0
            });

            await Create(new ShipmentBaseType
            {
                Name = "NCTS",
                Order = 10
            });
        }

        private async Task CreateDDPPOptions()
        {
            await Create(new DDPType
            {
                Name = "Duty Inclusive"
            });
            await Create(new DDPType
            {
                Name = "Duty and VAT Inclusive"
            });
        }

        async Task CreateLicenceTypes()
        {
            await Create(new LicenceType
            {
                Name = "Electronic"
            });

            await Create(new LicenceType
            {
                Name = "Paper"
            });
        }

        async Task CreateSecondQuantityDescriptions()
        {
            await Create(new SecondQuantityDescription
            {
                QuantityCode = "099",
                Description = "Litres of Alcohol"
            });
        }
        async Task CreateTransitOffice()
        {
            await Create(new TransitOffice
            {
                ID = "7AB75199-2ACB-46DD-8B3E-093807EB1E20".To<Guid>(),
                NCTSCode = "GB000060",
                UsualName = "Dover/Folkestone Eurotunnel Freight",
                Departure = true,
                Destination = true,
                Transit = true,
                GeoLocation = "Atlantic coast",
                CountryCode = "GB",
                CountryName = "United Kingdom"
            });
        }

        async Task CreatePorts()
        {
            await Create(new Port
            {
                PortName = "Southampton",
                PortCode = "SOU",
                Non_UK = false,
                TransitOffice = await TransitOffice.FindByNCTSCode("GB000060"),
                OutOfUKType = PortType.GVMS,
                OutOfUKValue = "A",
                IntoUKValue = "D",
            });
            await Create(new Port
            {
                PortName = "Kent",
                PortCode = "KEN",
                Non_UK = true,
                TransitOffice = await TransitOffice.FindByNCTSCode("GB000060"),
                OutOfUKType = PortType.GVMS,
                OutOfUKValue = "A",
                IntoUKValue = "D",
            });
            await Create(new Port
            {
                PortName = "Blackpool",
                PortCode = "BLP",
                Non_UK = false,
                TransitOffice = await TransitOffice.FindByNCTSCode("GB000060"),
                OutOfUKType = PortType.GVMS,
                OutOfUKValue = "A",
                IntoUKValue = "D",
            });
            await Create(new Port
            {
                PortName = "Portsmouth",
                PortCode = "PTM",
                Non_UK = false,
                TransitOffice = await TransitOffice.FindByNCTSCode("GB000060"),
                UKBFEmail = "leit.portsmouth@homeoffice.gov.uk",
                OutOfUKType = PortType.GVMS,
                OutOfUKValue = "A",
                IntoUKValue = "D",
            });
        }

        async Task CreateAuthorizedLocation()
        {
            var adminUser = await Create(new AuthorisedLocation
            {
                ID = "7AB75199-2ACB-46DD-8B3E-093807EB1E21".To<Guid>(),
                LocationName = "Stop 24",
                CustomsIdentity = "24FOLK CT21 4BL",
                TransitOfficeId = "7AB75199-2ACB-46DD-8B3E-093807EB1E20".To<Guid>(),
                AuthorisationNumber = " 24FOLK CT21 4BL",
            });
        }

        async Task CreateAuthorizedLocationDefaultGuaranteeLength()
        {
            var adminUser = await Create(new GuaranteeLength
            {
                AuthorisedLocationId = "7AB75199-2ACB-46DD-8B3E-093807EB1E21".To<Guid>(),
                Length = 8
            });
        }

        async Task CreateCurrencies()
        {
            await Create(new Currency
            {
                Name = "EUR"
            });
            await Create(new Currency
            {
                Name = "USD"
            });
            await Create(new Currency
            {
                Name = "GBP"
            });
        }

        async Task CreateContentBlocks()
        {
            await Create(new ContentBlock
            {
                Key = nameof(ContentBlock.LoginIntro),
                Content = "<p>Welcome to our application.<br/>Please log in using the form below.</p>",
                ID = "f45f4a2d-9987-4a42-b91f-420327d13830".To<Guid>()
            });

            await Create(new ContentBlock
            {
                Key = nameof(ContentBlock.PasswordSuccessfullyReset),
                Content = "Your password has been successfully reset.",
                ID = "559571ab-dc4a-4a53-8f47-471785c1695d".To<Guid>()
            });

            await Create(new ContentBlock
            {
                Key = nameof(ContentBlock.PasswordSuccessfullySet),
                Content = "Your password has been successfully set.",
                ID = "a854f057-a601-4069-bbe1-646b67e8244a".To<Guid>()
            });
        }

        async Task CreateUsers()
        {
            var pass = SecurePassword.Create("test");
            await Create(new ChannelPortsUser
            {
#pragma warning disable GCop646 // Email addresses should not be hard-coded. Move this to Settings table or Config file.
                Email = "admin@uat.co",
#pragma warning restore GCop646 // Email addresses should not be hard-coded. Move this to Settings table or Config file.
                FirstName = "Geeks",
                LastName = "Admin",
                Password = pass.Password,
                Salt = pass.Salt,
                IsAdmin = true,
                MobileNumber = "08004002343",
                ID = "90fa6886-d141-4a2e-8679-fc129958904c".To<Guid>()
            });

            var pass2 = SecurePassword.Create("Test123");
            await Create(new ChannelPortsUser
            {
#pragma warning disable GCop646 // Email addresses should not be hard-coded. Move this to Settings table or Config file.
                Email = "JackSmith@uat.co",
#pragma warning restore GCop646 // Email addresses should not be hard-coded. Move this to Settings table or Config file.
                FirstName = "Jack",
                LastName = "Smith",
                Password = pass2.Password,
                Salt = pass2.Salt,
                IsAdmin = false,
                ID = "25784d8e-8253-4cdc-9ce4-8a889290283f".To<Guid>()
            });

            var company = await Database.FirstOrDefault<Company>(x => x.Name == "Imports Ltd");
            var pass3 = SecurePassword.Create("test");
            await Create(new CompanyUser
            {
                Email = "JimStevens@uat.co",
                Password = pass3.Password,
                Salt = pass3.Salt,
                FirstName = "Jim",
                LastName = "Stevens",
                TelephoneNumber = "1234",
                MobileNumber = "5678",
                Notes = "Regional Manager",
                Company = company,
                ID = "579345c1-8c4e-4a41-a0b7-374cc785e258".To<Guid>(),
                IsAdmin = false
            });

            var channelPorts = await Database.FirstOrDefault<Company>(x => x.Name == "Channel Ports");
            await Create(new CompanyUser
            {
                FirstName = "Paul",
                LastName = "Wells",
                Email = "paulwells@channelports.co.uk",
                Company = channelPorts,
                IsAdmin = false
            });



        }

        async Task CreateTermsOfSale()
        {
            await Create(new TermOfSale
            {
                Name = "EXW",
                Description = "Ex Works",
                Box45 = true,
                FreightCharge = true,
                ValueForVAT = 20
            });

            await Create(new TermOfSale
            {
                Name = "FCA",
                Description = "Free Carrier",
                Box45 = false,
                FreightCharge = true,
                ValueForVAT = 13.5
            });

            await Create(new TermOfSale
            {
                Name = "FAS",
                Description = "Free Alongside Ship",
                Box45 = true,
                FreightCharge = false,
                ValueForVAT = 5
            });
        }

        async Task CreateVAT()
        {
            await Create(new VATType { Name = "S" });
            await Create(new VATType { Name = "Z" });
            await Create(new VATType { Name = "E" });
            await Create(new VATType { Name = "SZ" });
            await Create(new VATType { Name = "ZS" });
            await Create(new VATType { Name = "ZAS" });
        }

        async Task CreateCommodityCodes()
        {
            var commodityCode1 = await Create(new CommodityCode
            {
                ExportCode = "12345678",
                ImportCode = "12",
                SecondQuantity = await Create(new SecondQuantityDescription
                {
                    QuantityCode = "025",
                    Description = "Litres"
                }),
                FullRateOfDuty = 12.45M,
                //VAT = await VATType.FindByName("S"),
            });
            await Create(new CommodityCodeMultipleVATLink
            {
                Commoditycode = commodityCode1,
                Vattype = await VATType.FindByName("S")
            });

            var commodityCode2 = await Create(new CommodityCode
            {
                ExportCode = "12345678",
                ImportCode = "14",
                //VAT = await VATType.FindByName("Z"),
                FullRateOfDuty = 10.56M
            });

            await Create(new CommodityCodeMultipleVATLink
            {
                Commoditycode = commodityCode2,
                Vattype = await VATType.FindByName("Z")
            });

            var commodityCode3 = await Create(new CommodityCode
            {
                ExportCode = "12121212",
                ImportCode = "14",
                //VAT = await VATType.FindByName("SZ"),
                FullRateOfDuty = 8.4M
            });

            await Create(new CommodityCodeMultipleVATLink
            {
                Commoditycode = commodityCode3,
                Vattype = await VATType.FindByName("Z")
            });

            var commodityCode4 = await Create(new CommodityCode
            {
                ExportCode = "34545343453",
                ImportCode = "14",
                SecondQuantity = await Create(new SecondQuantityDescription
                {
                    QuantityCode = "034",
                    Description = "Millilitres"
                }),
                ThirdQuantity = await Create(new SecondQuantityDescription
                {
                    QuantityCode = "023",
                    Description = "Millilitres"
                }),
                //VAT = await VATType.FindByName("ZAS"),
                FullRateOfDuty = 10.3M,
                Control = true
            });

            await Create(new CommodityCodeMultipleVATLink
            {
                Commoditycode = commodityCode4,
                Vattype = await VATType.FindByName("S")
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

        async Task CreateCompanyTypes()
        {
            await Create(new CompanyType { Name = "Customer", Order = 1, ID = "215e22ba-4194-4806-8f2d-3ca8e02cc393".To<Guid>() });
            await Create(new CompanyType { Name = "Flex", Order = 2, ID = "8B97B557-B9FC-47FA-9D86-926E36CFAF49".To<Guid>() });
            await Create(new CompanyType { Name = "Forwarder", Order = 3, ID = "7ee054e8-14f8-4c28-b30f-121b6e1d9b12".To<Guid>() });
            await Create(new CompanyType { Name = "Carrier", Order = 4, ID = "215e22ba-4194-4806-8f2d-3ca8e02cc394".To<Guid>() });
            await Create(new CompanyType { Name = "Other", Order = 5, ID = "4776e42b-2bea-42fc-a3c7-af2672343c1e".To<Guid>() });
        }

        async Task CreateCountries()
        {
            var firstCPC = await Database.FirstOrDefault<CPC>(x => x.Number == "1000001");
            var secondCPC = await Database.FirstOrDefault<CPC>(x => x.Number == "4000000");

            var uk = new Country
            {
                Name = "United Kingdom",
                Code = "GB",
                PreferenceAvailable = false,
                HasMFN = false,
                ImportCPCWithoutPreference = firstCPC,
                ImportCPCWithoutPreferenceDeclarationType = "IM",
                ImportCPCWithoutPreferencePreferenceCode = "123",
                ImportCPCWithPreferenceRateCode = "F",
                ImportCPCWithoutPreferenceRateCode = "F",
                ExportCPCWithoutPreference = secondCPC,
                ExportCPCWithoutPreferenceDeclarationType = "EX",
                InvoiceDeclarationDocumentType = "AB12",
                InvoiceDeclarationDocumentTypeDocumentStatus = "C1",
                PreferenceCertificateNumberDocumentType = "DE34",
                PreferenceCertificateNumberDocumentTypeDocumentStatus = "E3",
                CountryDiallingCode = "+44"
            };
            await Create(uk);
            await Create(new Ancillary
            {
                Country = uk
            });

            var italy = new Country
            {
                Name = "Italy",
                Code = "IT",
                PreferenceAvailable = false,
                ImportCPCWithoutPreference = firstCPC,
                ImportCPCWithoutPreferenceDeclarationType = "IM",
                ImportCPCWithoutPreferencePreferenceCode = "123",
                ImportCPCWithPreferenceRateCode = "F",
                ImportCPCWithoutPreferenceRateCode = "F",
                ExportCPCWithoutPreference = secondCPC,
                ExportCPCWithoutPreferenceDeclarationType = "EX",
                InvoiceDeclarationDocumentType = "AB12",
                InvoiceDeclarationDocumentTypeDocumentStatus = "C1",
                PreferenceCertificateNumberDocumentType = "DE34",
                PreferenceCertificateNumberDocumentTypeDocumentStatus = "E3",
                CountryDiallingCode = "+39"

            };
            await Create(italy);
            await Create(new Ancillary
            {
                Country = italy
            });

            var spain = new Country
            {
                Name = "Spain",
                Code = "ES",
                PreferenceAvailable = false,
                HasMFN = true,
                MFNCode1 = "A.B",
                ImportCPCWithoutPreference = firstCPC,
                ImportCPCWithoutPreferenceDeclarationType = "IM",
                ImportCPCWithoutPreferencePreferenceCode = "123",
                ImportCPCWithPreferenceRateCode = "F",
                ImportCPCWithoutPreferenceRateCode = "F",
                ExportCPCWithoutPreference = secondCPC,
                ExportCPCWithoutPreferenceDeclarationType = "EX",
                InvoiceDeclarationDocumentType = "AB12",
                InvoiceDeclarationDocumentTypeDocumentStatus = "C1",
                PreferenceCertificateNumberDocumentType = "DE34",
                PreferenceCertificateNumberDocumentTypeDocumentStatus = "E3",
                CountryDiallingCode = "+34"
            };
            await Create(spain);
            await Create(new Ancillary
            {
                Country = spain
            });

            var greece = new Country
            {
                Name = "Greece",
                Code = "GR",
                PreferenceAvailable = true,
                HasMFN = true,
                MFNCode1 = "C3S",
                ImportCPCWithPreference = firstCPC,
                ImportCPCWithPreferenceDeclarationType = "DE",
                ImportCPCWithPreferencePreferenceCode = "567",
                ImportCPCWithoutPreference = secondCPC,
                ImportCPCWithoutPreferenceDeclarationType = "IM",
                ImportCPCWithoutPreferencePreferenceCode = "123",
                ImportCPCWithPreferenceRateCode = "F",
                ImportCPCWithoutPreferenceRateCode = "F",
                ExportCPCWithPreference = secondCPC,
                ExportCPCWithPreferenceDeclarationType = "GH",
                ExportCPCWithoutPreference = firstCPC,
                ExportCPCWithoutPreferenceDeclarationType = "EX",
                InvoiceDeclarationDocumentType = "AB12",
                InvoiceDeclarationDocumentTypeDocumentStatus = "C1",
                PreferenceCertificateNumberDocumentType = "DE34",
                PreferenceCertificateNumberDocumentTypeDocumentStatus = "E3",
                CountryDiallingCode = "+30"
            };
            await Create(greece);
            await Create(new Ancillary
            {
                Country = greece
            });
        }

        async Task CreateCompaniesAndAssociatedProducts()
        {
            var channelPorts = new Company
            {
                ID = Constants.ChannelPortsID,
                Name = "Channel Ports",
                Type = CompanyType.Other,
                CustomerAccountNumber = "Z9999",
                Country = await Database.FirstOrDefault<Country>(x => x.Name == "United Kingdom"),
                AddressLine1 = "Folkestone Services, Junction 11",
                AddressLine2 = "M20",
                Town = "Hythe",
                Postcode = "CT21 4BL",
                EORINumber = "GB683470514001",
                RepresentationType = true,
                RefrenceSuffix = "ABC",
                InvoiceFrequency = InvoiceFrequencyType.Monthly,
                DefermentNumber = "1234657",
                GuarantorType = GuarantorType.Own
            };

            channelPorts.DefaultDeclarant = channelPorts;
            var company = await Create(channelPorts);
            await Create(new CompanyShipmentType { Company = company, ShipmentType = ShipmentType.IntoUk });


            var shippingCompanyLtd = new Company
            {
                Name = "Shipping Company Ltd",
                Type = CompanyType.Customer,
                CustomerAccountNumber = "A9958",
                Country = await Database.FirstOrDefault<Country>(x => x.Name == "Italy"),
                Postcode = "FG6 YFD",
                AddressLine1 = "56 Company Road",
                Town = "Rome",
                EORINumber = "SC859485859485",
                BranchIdentifier = "AG123",
                AEONumber = "12345678900987654321",
                //TSP = "RY47DJF7CJR9",
                // CFSP = true,
                //PaymentCode = "T",
                DefaultDeclarant = channelPorts,
                DefermentNumber = "1234567",
                RepresentationType = true,
                TIN = "SC248347554522",
                RefrenceSuffix = "DEF",
                InvoiceFrequency = InvoiceFrequencyType.Monthly,
                GuarantorType = GuarantorType.Own

            };
            company = await Create(shippingCompanyLtd);
            await Create(new CompanyShipmentType { Company = company, ShipmentType = ShipmentType.OutOfUk });
            await Create(new CompanyShipmentType { Company = company, ShipmentType = ShipmentType.IntoUk });

            await Create(new Product
            {
                Code = "ABS00001",
                Name = "Mac Pro",
                CommodityCode = await Database.FirstOrDefault<CommodityCode>(x => x.ExportCode == "12345678" && x.ImportCode == "12"),
                Quota = "666666",
                VAT = await VATType.FindByName("S"),
                Licenced = false,
                Company = shippingCompanyLtd
            });
            await Create(new Product
            {
                Code = "ABS00002",
                Name = "Macbook",
                CommodityCode = await Database.FirstOrDefault<CommodityCode>(x => x.ExportCode == "12345678" && x.ImportCode == "14"),
                Quota = "123456",
                VAT = await VATType.FindByName("Z"),
                Licenced = true,
                ExportLicence = "1234",
                Company = shippingCompanyLtd
            });

            var importsLtd = new Company
            {
                Name = "Imports Ltd",
                Type = CompanyType.Forwarder,
                CustomerAccountNumber = "A5443",
                Country = await Database.FirstOrDefault<Country>(x => x.Name == "Spain"),
                Postcode = "AG2 YGD",
                AddressLine1 = "99 Dead End Road",
                Town = "Rome",
                EORINumber = "IL859098859098",
                BranchIdentifier = "BR543",
                AEONumber = "03648392017584930213",
                //TSP = "7ABCABCABCABC",
                //CFSP = false,
                //PaymentCode = "T",
                DefaultDeclarant = channelPorts,
                DefermentNumber = "6234517",
                RepresentationType = false,
                TIN = "IL123428545754",
                RefrenceSuffix = "GHL",
                InvoiceFrequency = InvoiceFrequencyType.Monthly,
                GuarantorType = GuarantorType.Own

            };
            company = await Create(importsLtd);
            await Create(new CompanyShipmentType { Company = company, ShipmentType = ShipmentType.IntoUk });

            await Create(new Product
            {
                Code = "ABS00003",
                Name = "iPod 32GB",
                CommodityCode = await Database.FirstOrDefault<CommodityCode>(x => x.ExportCode == "34545343453" && x.ImportCode == "14"),
                Quota = "676767",
                VAT = await VATType.FindByName("SZ"),
                Licenced = false,
                Company = importsLtd
            });
            await Create(new Product
            {
                Code = "ABS00004",
                Name = "iPod 64GB",
                CommodityCode = await Database.FirstOrDefault<CommodityCode>(x => x.ExportCode == "12345678" && x.ImportCode == "12"),
                Quota = "123123",
                VAT = await VATType.FindByName("SZ"),
                Licenced = true,
                ExportLicence = "4422",
                Company = importsLtd
            });

            await Create(new Contact
            {
                FirstName = "Jack",
                LastName = "Smith",
                Company = importsLtd,
                Email = "JackSmith_1980@uat.co",
                TelephoneNumber = "0028747347343"
            });

        }

        async Task CreateShipments()
        {
            var company = await Database.FirstOrDefault<Company>(x => x.Name == "Imports Ltd");

            await Create(new Shipment
            {
                TrackingNumber = "R0219000001",
                Date = new DateTime(2019, 2, 1),
                MyReferenceForCPInvoice = "59444",
                Company = company,
                VehicleNumber = "588",
                TrailerNumber = "6513",
                Type = ShipmentType.IntoUk,
                PrimaryContact = await Database.FirstOrDefault<Contact>(x => x.CompanyId == company),
                NotifyAdditionalParty = NotifyType.NotRequired,
                ExpectedDate = new DateTime(2019, 3, 1),
                // Route = await Route.GetRoute()
                //PortOfArrival = await Database.FirstOrDefault<Port>(x => x.PortName == "Southampton")
            });

            await Create(new Shipment
            {
                TrackingNumber = "T0219000001",
                Date = new DateTime(2019, 2, 1),
                MyReferenceForCPInvoice = "59445",
                Company = company,
                VehicleNumber = "589",
                TrailerNumber = "6513",
                Type = ShipmentType.OutOfUk,
                PrimaryContact = await Database.FirstOrDefault<Contact>(x => x.CompanyId == company),
                NotifyAdditionalParty = NotifyType.NotRequired,
                ExpectedDate = new DateTime(2019, 3, 1),
            });
        }

        async Task CreateConsignments()
        {
            var shipmentIn = await Database.FirstOrDefault<Shipment>(x => x.TypeId == ShipmentType.IntoUk);
            var shipmentOut = await Database.FirstOrDefault<Shipment>(x => x.TypeId == ShipmentType.OutOfUk);
            var currency = await Database.FirstOrDefault<Currency>(x => x.Name == "GBP");
            var company = shipmentIn.Company;

            await Create(new Consignment
            {
                Shipment = shipmentIn,
                ConsignmentNumber = shipmentIn.TrackingNumber + "01",
                UKTrader = company,
                Partner = company,
                Declarant = company,
                Guarantor = await Database.FirstOrDefault<Company>(x => x.Name == "Channel Ports"),
                TotalPackages = 2,
                TotalGrossWeight = 5.55,
                TotalNetWeight = 4.44,
                InvoiceNumber = "TRUCKERS-CONSIGNMENT-I-01",
                InvoiceCurrency = currency,
                TotalValue = 105.99m,
                UCR = "9" + company.EORINumber + "-" + shipmentIn.TrackingNumber + "01",
                IdNumber = 1
            });

            // For testing "Search By EADMRN" for NCTS Consignments

            // Valid consignment for NCTS
            await Create(new Consignment
            {
                Shipment = shipmentOut,
                ConsignmentNumber = shipmentOut.TrackingNumber + "01",
                UKTrader = company,
                Partner = company,
                Declarant = company,
                Guarantor = await Database.FirstOrDefault<Company>(x => x.Name == "Channel Ports"),
                TotalPackages = 2,
                TotalGrossWeight = 5,
                TotalNetWeight = 3,
                InvoiceNumber = "TRUCKERS-CONSIGNMENT-O-01",
                InvoiceCurrency = currency,
                TotalValue = 500.50m,
                UCR = "9" + company.EORINumber + "-" + shipmentOut.TrackingNumber + "01",
                IdNumber = 1,
                Progress = Progress.Draft,
                EADMRN = "ABCDE12345"
            });

            // Invalid consignment for NCTS - total gross weight not matching
            await Create(new Consignment
            {
                Shipment = shipmentOut,
                ConsignmentNumber = shipmentOut.TrackingNumber + "02",
                UKTrader = company,
                Partner = company,
                Declarant = company,
                Guarantor = await Database.FirstOrDefault<Company>(x => x.Name == "Channel Ports"),
                TotalPackages = 2,
                TotalGrossWeight = 5.1,
                TotalNetWeight = 3,
                InvoiceNumber = "TRUCKERS-CONSIGNMENT-O-02",
                InvoiceCurrency = currency,
                TotalValue = 500.50m,
                UCR = "9" + company.EORINumber + "-" + shipmentOut.TrackingNumber + "02",
                IdNumber = 1,
                Progress = Progress.Draft,
                EADMRN = "TGRW123"
            });

            // Invalid consignment for NCTS - total net weight not matching
            await Create(new Consignment
            {
                Shipment = shipmentOut,
                ConsignmentNumber = shipmentOut.TrackingNumber + "03",
                UKTrader = company,
                Partner = company,
                Declarant = company,
                Guarantor = await Database.FirstOrDefault<Company>(x => x.Name == "Channel Ports"),
                TotalPackages = 2,
                TotalGrossWeight = 5,
                TotalNetWeight = 3.1,
                InvoiceNumber = "TRUCKERS-CONSIGNMENT-O-03",
                InvoiceCurrency = currency,
                TotalValue = 500.50m,
                UCR = "9" + company.EORINumber + "-" + shipmentOut.TrackingNumber + "03",
                IdNumber = 1,
                Progress = Progress.Draft,
                EADMRN = "TNEW123"
            });

            // Invalid consignment - first commodity must have at least one package
            await Create(new Consignment
            {
                Shipment = shipmentOut,
                ConsignmentNumber = shipmentOut.TrackingNumber + "04",
                UKTrader = company,
                Partner = company,
                Declarant = company,
                Guarantor = await Database.FirstOrDefault<Company>(x => x.Name == "Channel Ports"),
                TotalPackages = 2,
                TotalGrossWeight = 5,
                TotalNetWeight = 3,
                InvoiceNumber = "TRUCKERS-CONSIGNMENT-O-04",
                InvoiceCurrency = currency,
                TotalValue = 500.50m,
                UCR = "9" + company.EORINumber + "-" + shipmentOut.TrackingNumber + "04",
                IdNumber = 1,
                Progress = Progress.Draft,
                EADMRN = "TNUP123"
            });

            await Shipment.UpdateProgress(shipmentIn);
            await Shipment.UpdateProgress(shipmentOut);
        }

        async Task CreateCommodities()
        {
            var consignmentIn = await Database.FirstOrDefault<Consignment>(x => x.ConsignmentNumber == "R021900000101");
            var consignmentOut = await Database.FirstOrDefault<Consignment>(x => x.ConsignmentNumber == "T021900000101");
            var product = await Database.FirstOrDefault<Product>(x => x.Code == "ABS00003");
            var destination = await Database.FirstOrDefault<Country>(x => x.Name == "Greece");

            await Create(new Commodity
            {
                Product = product,
                Consignment = consignmentIn,
                GrossWeight = 5,
                NetWeight = 3,
                Value = 500.50m,
                CountryOfDestination = destination,
                NumberOfPackages = 2,
                HasPreference = true,
                VAT = await product?.CommodityCode?.MultipleVAT.FirstOrDefault()
            });

            await Create(new Commodity
            {
                Product = product,
                Consignment = consignmentOut,
                GrossWeight = 5,
                NetWeight = 3,
                Value = 500.50m,
                CountryOfDestination = destination,
                NumberOfPackages = 2,
                HasPreference = true,
                VAT = await product?.CommodityCode?.MultipleVAT.FirstOrDefault()
            });

            consignmentOut = await Database.FirstOrDefault<Consignment>(x => x.ConsignmentNumber == "T021900000102");

            await Create(new Commodity
            {
                Product = product,
                Consignment = consignmentOut,
                GrossWeight = 5,
                NetWeight = 3,
                Value = 500.50m,
                CountryOfDestination = destination,
                NumberOfPackages = 2,
                HasPreference = true,
                VAT = await product?.CommodityCode?.MultipleVAT.FirstOrDefault()
            });

            consignmentOut = await Database.FirstOrDefault<Consignment>(x => x.ConsignmentNumber == "T021900000103");

            await Create(new Commodity
            {
                Product = product,
                Consignment = consignmentOut,
                GrossWeight = 5,
                NetWeight = 2.9,
                Value = 500.50m,
                CountryOfDestination = destination,
                NumberOfPackages = 2,
                HasPreference = true,
                VAT = await product?.CommodityCode?.MultipleVAT.FirstOrDefault()
            });

            consignmentOut = await Database.FirstOrDefault<Consignment>(x => x.ConsignmentNumber == "T021900000104");

            await Create(new Commodity
            {
                Product = product,
                Consignment = consignmentOut,
                GrossWeight = 5,
                NetWeight = 3,
                Value = 500.50m,
                CountryOfDestination = destination,
                NumberOfPackages = null,
                HasPreference = true,
                VAT = await product.CommodityCode.MultipleVAT.FirstOrDefault()
            });
        }

        async Task CreateCPC()
        {
            await Create(new CPC
            {
                Type = ShipmentType.OutOfUk,
                Number = "1000001",
                CPCDescription = "details",
                Box44 = "N954 AG EUR",
                Box47A = "A00",
                Box47c1 = "UT",
                Box47C = "F"
            });

            await Create(new CPC
            {
                Type = ShipmentType.IntoUk,
                Number = "4000000",
                CPCDescription = "details 7654213",
                Box44 = "T853 AG EUR",
                Box47A = "B00",
                Box47c1 = "A",
                Box47C = "F"
            });
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

        async Task CreateLicences()
        {
            await Create(new Licence
            {
                LicenceName = "Local Import Licence",
                Type = ShipmentType.IntoUk,
                LicenceType = LicenceType.Electronic,
                RPTID = true,
                LicenceIdentifier = "LocImp"
            });
            await Create(new Licence
            {
                LicenceName = "Local Export Licence",
                Type = ShipmentType.OutOfUk,
                LicenceType = LicenceType.Electronic,
                RPTID = true,
                LicenceIdentifier = "LocExp"
            });
            await Create(new Licence
            {
                LicenceName = "Transport Express Licence",
                Type = ShipmentType.OutOfUk,
                LicenceType = LicenceType.Paper,
                RPTID = true,
                LicenceIdentifier = "TrExp"
            });
        }

        async Task CreateTransitOfficeFilestatus()
        {
            var types = new List<TransitOfficeFileStatus>
            {
                new TransitOfficeFileStatus { Name = nameof(TransitOfficeFileStatus.Successful)},
                new TransitOfficeFileStatus { Name = nameof(TransitOfficeFileStatus.PartialSuccess).ToLiteralFromPascalCase()},
                new TransitOfficeFileStatus { Name = nameof(TransitOfficeFileStatus.Failed)}
            };

            await Create(types.AsEnumerable());
        }

        async Task CreateNotifyTypes()
        {
            var types = new List<NotifyType>
            {
                new NotifyType { Name = nameof(NotifyType.NotRequired).ToLiteralFromPascalCase()},
                new NotifyType { Name = nameof(NotifyType.Group)},
                new NotifyType { Name = nameof(NotifyType.SpecificContacts).ToLiteralFromPascalCase()}
            };

            await Create(types.AsEnumerable());
        }

        async Task CreatePreferenceTypes()
        {
            var types = new List<PreferenceType>
            {
                new PreferenceType {
                    DisplayName ="Invoice Declaration",
                    Name= nameof(PreferenceType.InvoiceDeclaration).ToLiteralFromPascalCase()
                },
                new PreferenceType {
                    Name= nameof(PreferenceType.PreferenceCertificateNumber).ToLiteralFromPascalCase(),
                    DisplayName = "Preference Certificate Number"
                },
                new PreferenceType {
                    Name= nameof(PreferenceType.StatementOfOriginImportersKnowledge).ToLiteralFromPascalCase(),
                    DisplayName = "Statement Of Origin (Importers Knowledge)"
                }
            };
            await Create(types.AsEnumerable());
        }


        async Task CreateProgress()
        {

            var types = new List<Progress>
            {
                new Progress {
                    SystemName = nameof(Progress.Draft),
                    AdminDisplay = "Draft" ,
                    ClientDisplay = "Draft" ,
                    Weight = 10 ,
                    AdminEditable = true,
                    CustomerEditable = true},

                new Progress {
                    SystemName = nameof(Progress.EntryControlled),
                    AdminDisplay = "EntryControlled" ,
                    ClientDisplay = "Transmitted" ,
                    Weight = 10 ,
                    AdminEditable = true,
                    CustomerEditable = true},

                new Progress {
                    SystemName = nameof(Progress.ReadyToTransmit),
                    AdminDisplay = "Ready to Transmit" ,
                    ClientDisplay = "Ready to Transmit" ,
                    Weight = 30},

                 new Progress {
                    SystemName = nameof(Progress.ReadyToTransmitAPI),
                    AdminDisplay = "Ready to Transmit (API)" ,
                    ClientDisplay = "Ready to Transmit (API)" ,
                    Weight = 30},


                new Progress {
                    SystemName = nameof(Progress.ASMAccept),
                    AdminDisplay = "ASM Accepted" ,
                    ClientDisplay = "Transmitted" ,
                    Weight = 40,
                    AdminEditable = true},

                new Progress {
                    SystemName = nameof(Progress.ASMReject),
                    AdminDisplay = "ASM Rejected" ,
                    ClientDisplay = "Transmitted" ,
                    Weight = 40 ,
                    AdminEditable = true},

                new Progress {
                    SystemName = nameof(Progress.AwaitingArrival),
                    AdminDisplay = "Awaiting Arrival" ,
                    ClientDisplay = "Awaiting Arrival" ,
                    Weight = 50 ,
                    AdminEditable = true,
                    CustomerEditable = true},

                new Progress {
                    SystemName = nameof(Progress.AwaitingDeparture),
                    AdminDisplay = "Awaiting Departure" ,
                    ClientDisplay = "Awaiting Departure" ,
                    Weight = 50 ,
                    AdminEditable = true,
                    CustomerEditable = true},

                new Progress {
                    SystemName = nameof(Progress.ProcessingErrorArrival),
                    AdminDisplay = "Processing Error" ,
                    ClientDisplay = "Awaiting Arrival" ,
                    Weight = 60,
                    AdminEditable = true },

                new Progress {
                    SystemName = nameof(Progress.ProcessingErrorDeparture),
                    AdminDisplay = "Processing Error" ,
                    ClientDisplay = "Awaiting Departure" ,
                    Weight = 60 ,
                    AdminEditable = true },

                new Progress {
                    SystemName = nameof(Progress.Arrived),
                    AdminDisplay = "Arrived" ,
                    ClientDisplay = "Arrived" ,
                    Weight = 70},

                new Progress {
                    SystemName = nameof(Progress.WithCustoms),
                    AdminDisplay = "With Customs" ,
                    ClientDisplay = "With Customs" ,
                    Weight = 70},

                new Progress {
                    SystemName = nameof(Progress.QueriedArrived),
                    AdminDisplay = "Queried" ,
                    ClientDisplay = "Arrived" ,
                    Weight = 80,
                AdminEditable = true},

                 new Progress {
                    SystemName = nameof(Progress.QueriedWithCustoms),
                    AdminDisplay = "Queried" ,
                    ClientDisplay = "With Customs" ,
                    Weight = 80,
                 AdminEditable = true},

                new Progress {
                    SystemName = nameof(Progress.Cleared),
                    AdminDisplay = "Cleared" ,
                    ClientDisplay = "Cleared" ,
                    Weight = 90},

                new Progress {
                    SystemName = nameof(Progress.Cancelled),
                    AdminDisplay = "Cancelled" ,
                    ClientDisplay = "Cancelled" ,
                    Weight = 100},

                new Progress {
                    SystemName = nameof(Progress.ManualGenereal),
                    AdminDisplay = "Manual - General" ,
                    ClientDisplay = "Ready to Transmit" ,
                    Weight = 110,
                    AdminEditable = true,
                    IsManual = true},

                new Progress {
                    SystemName = nameof(Progress.ManualCPC),
                    AdminDisplay = "Manual - CPC" ,
                    ClientDisplay = "Ready to Transmit" ,
                    Weight = 110,
                    AdminEditable = true,
                    IsManual = true},

                new Progress {
                    SystemName = nameof(Progress.ManualLicense),
                    AdminDisplay = "Manual - License" ,
                    ClientDisplay = "Ready to Transmit" ,
                    Weight = 110,
                    AdminEditable = true,
                    IsManual = true},

                new Progress {
                    SystemName = nameof(Progress.ManualRoute),
                    AdminDisplay = "Manual - Route" ,
                    ClientDisplay = "Ready to Transmit" ,
                    Weight = 110,
                    AdminEditable = true,
                    IsManual = true},

                 new Progress {
                    SystemName = nameof(Progress.ManualGenerealASMAccepted),
                    AdminDisplay = "Manual - General - ASM Accepted" ,
                    ClientDisplay = "Transmitted" ,
                    Weight = 120,
                    IsManual = true},

                 new Progress {
                    SystemName = nameof(Progress.ManualCPCASMAccepted),
                    AdminDisplay = "Manual - CPC - ASM Accepted" ,
                    ClientDisplay = "Transmitted" ,
                    Weight = 120,
                    IsManual = true},

                  new Progress {
                    SystemName = nameof(Progress.ManualLicenseASMAccepted),
                    AdminDisplay = "Manual - License - ASM Accepted" ,
                    ClientDisplay = "Transmitted" ,
                    Weight = 120,
                    IsManual = true
                    },

                  new Progress {
                    SystemName = nameof(Progress.ManualRouteASMAccepted),
                    AdminDisplay = "Manual - Route - ASM Accepted" ,
                    ClientDisplay = "Transmitted" ,
                    Weight = 120,
                    IsManual = true
                    },

                   new Progress {
                    SystemName = nameof(Progress.ManualGenerealASMRejected),
                    AdminDisplay = "Manual - General - ASM Rejected" ,
                    ClientDisplay = "Transmitted" ,
                    Weight = 120,
                    AdminEditable = true,
                    IsManual = true},

                  new Progress {
                    SystemName = nameof(Progress.ManualCPCASMRejected),
                    AdminDisplay = "Manual - CPC - ASM Rejected" ,
                    ClientDisplay = "Transmitted" ,
                    Weight = 120,
                    AdminEditable = true,
                    IsManual = true},

                  new Progress {
                    SystemName = nameof(Progress.ManualLicenseASMRejected),
                    AdminDisplay = "Manual - License - ASM Rejected" ,
                    ClientDisplay = "Transmitted" ,
                    Weight = 120,
                    AdminEditable = true,
                    IsManual = true},

                  new Progress {
                    SystemName = nameof(Progress.ManualRouteASMRejected),
                    AdminDisplay = "Manual - Route - ASM Rejected" ,
                    ClientDisplay = "Transmitted" ,
                    Weight = 120,
                    AdminEditable = true,
                    IsManual = true},
                new Progress {
                    SystemName = nameof(Progress.InternalError),
                    AdminDisplay = "InternalError" ,
                    ClientDisplay = "Transmitted" ,
                    Weight = 180,
                    AdminEditable = true},
                new Progress {
                    SystemName = nameof(Progress.Partial),
                    AdminDisplay = "Partial" ,
                    ClientDisplay = "Partial" ,
                    Weight = 180,
                    },

                 new Progress {
                    SystemName = nameof(Progress.WithImporter),
                    AdminDisplay = "With Importer" ,
                    ClientDisplay = "With Importer" ,
                    Weight = 180,
                    },

                 new Progress {
                    SystemName = nameof(Progress.DutyPayment),
                    AdminDisplay = "Duty Payment" ,
                    ClientDisplay = "Duty Payment" ,
                    Weight = 190,
                    AdminEditable = true
                    },
                 new Progress {
                    SystemName = nameof(Progress.ManualQuota),
                    AdminDisplay = "Manual - Quota" ,
                    ClientDisplay = "Ready to Transmit" ,
                    Weight = 200,
                    IsManual = false
                    },
                   new Progress {
                    SystemName = nameof(Progress.LeftCountry),
                    AdminDisplay = "LeftCountry" ,
                    ClientDisplay = "LeftCountry" ,
                    Weight = 210 ,
                    AdminEditable = true,
                    CustomerEditable = true},
            };

            await Create(types.AsEnumerable());
        }

        async Task CreateTransactionTypes()
        {
            var types = new List<TransactionType>
            {
                new TransactionType
                {
                    ID = "1638927a-8919-46ee-9684-9f7a48dc9b3f".To<Guid>(),
                    Name = nameof(TransactionType.Deposit)
                },

                new TransactionType
                {
                    ID = "e88a88da-c718-4bbc-b442-ac1a08c741c4".To<Guid>(),
                    Name = nameof(TransactionType.Pending)
                },
                new TransactionType
                {
                    ID = "d0dbb944-b908-4ea2-a446-f777c2403b3f".To<Guid>(),
                    Name = nameof(TransactionType.Withdrawal)
                },
            };

            await Create(types.AsEnumerable());
        }

        //async Task CreateShipmentType()
        //{
        //    var types = new List<ShipmentType>
        //    {
        //        new ShipmentType { Name = nameof(ShipmentType.IntoUk).ToLiteralFromPascalCase()},
        //        new ShipmentType { Name = nameof(ShipmentType.OutOfUk).ToLiteralFromPascalCase()},
        //    };

        //    await Create(types.AsEnumerable());
        //}


        async Task CreateGVMSTypes()
        {
            var types = new List<GVMSType>
            {
                new GVMSType { ID="5B89A54C-2469-4388-B5F8-DE8989F16BB7".To<Guid>(), Name = nameof(GVMSType.Always).ToLiteralFromPascalCase()},
                new GVMSType { ID="5B89A54C-2469-4388-B5F8-DE8989F16BB8".To<Guid>(), Name = nameof(GVMSType.Sometimes).ToLiteralFromPascalCase()},
                new GVMSType { ID="5B89A54C-2469-4388-B5F8-DE8989F16BB9".To<Guid>(), Name = "Not GVMS"}
            };
            await Create(types.AsEnumerable());
        }
        async Task CreateGVMSStatus()
        {
            var types = new List<GVMSStatus>
            {
                new GVMSStatus {SystemName = nameof(GVMSStatus.Rejected).ToLiteralFromPascalCase()},
                new GVMSStatus {SystemName = nameof(GVMSStatus.Pending).ToLiteralFromPascalCase()},
                new GVMSStatus {SystemName = nameof(GVMSStatus.Successful).ToLiteralFromPascalCase()},
                new GVMSStatus {SystemName = nameof(GVMSStatus.Transmitted).ToLiteralFromPascalCase()}
            };
            await Create(types.AsEnumerable());
        }

        async Task CreatePortTypes()
        {
            var types = new List<PortType>
            {
                new PortType {Name = nameof(PortType.GVMS).ToLiteralFromPascalCase()},
                new PortType {Name = nameof(PortType.Inventory).ToLiteralFromPascalCase()},
            };
            await Create(types.AsEnumerable());
        }


        async Task CreateShipmentTypes()
        {
            var types = new List<ShipmentType>
            {
                new ShipmentType {Name = nameof(ShipmentType.IntoUk).ToLiteralFromPascalCase(), DisplayName = "Into UK"},
                new ShipmentType {Name = nameof(ShipmentType.OutOfUk).ToLiteralFromPascalCase(), DisplayName = "Out of UK"},
            };
            await Create(types.AsEnumerable());
        }

        async Task CreateInvoiceStatus()
        {
            await Create(new InvoiceStatus
            {
                Name = "InProgress",
                DisplayName = "In progress"
            });

            await Create(new InvoiceStatus
            {
                Name = "NotSentToExchequer",
                DisplayName = "Not sent to Exchequer"
            });

            await Create(new InvoiceStatus
            {
                Name = "SentToExchequer",
                DisplayName = "Sent to Exchequer"
            });
            await Create(new InvoiceStatus
            {
                Name = "NotSentToExchequerFailure",
                DisplayName = "Not sent to Exchequer (Failure)"
            });
        }
        async Task CreateMessageSentType()
        {
            await Create(new MessageSendToType
            {
                Name = "Channelports",
            });

            await Create(new MessageSendToType
            {
                Name = "Customers",
            });

            await Create(new MessageSendToType
            {
                Name = "All",
            });
        }
        async Task CreateInvoiceJobStatus()
        {
            await Create(new InvoiceJobStatus
            {
                Name = "NotStarted",
            });

            await Create(new InvoiceJobStatus
            {
                Name = "InProgress"
            });
            await Create(new InvoiceJobStatus
            {
                Name = "Done"
            });
            await Create(new InvoiceJobStatus
            {
                Name = "Error"
            });
        }

        async Task CreateInvoiceType()
        {
            await Create(new InvoiceType
            {
                Name = "Transaction",
                DisplayName = "Additional Consignments"
            });

            await Create(new InvoiceType
            {
                Name = "Charge",
                DisplayName = "License Fee"
            });

        }
        async Task CreateCompanyUKTraderPartnerLinkTypes()
        {
            await Create(new CompanyUKTraderPartnerLinkType
            {
                Name = nameof(CompanyUKTraderPartnerLinkType.Partner),
                ID = new Guid("e4eafcc3-b92d-42c2-a382-4371e1f739d5"),
                Display = "Partner"
            });
            await Create(new CompanyUKTraderPartnerLinkType
            {
                Name = nameof(CompanyUKTraderPartnerLinkType.UKTrader),
                ID = new Guid("59b85659-6715-408d-8a37-439a51f904ec"),
                Display = "UK Trader"
            });
        }

        async Task CreateSafetyAndSecurity()
        {
            await Create(new SafetyAndSecurity
            {
                Name = nameof(SafetyAndSecurity.Always),
                DisplayName = "Always",
                ID = new Guid("e4eafcc3-b92d-42c2-a382-4371e1f739d5"),
            });
            await Create(new SafetyAndSecurity
            {
                Name = nameof(SafetyAndSecurity.Sometimes),
                DisplayName = "Sometimes",
                ID = new Guid("59b85659-6715-408d-8a37-439a51f904ec"),
            });
            await Create(new SafetyAndSecurity
            {
                Name = nameof(SafetyAndSecurity.NoSafetyAndSecurity),
                DisplayName = "No Safety And Security",
                ID = new Guid("1638927a-8919-46ee-9684-9f7a48dc9b3f"),
            });
        }

        async Task CreateGuarantorType()
        {
            await Create(new GuarantorType
            {
                Name = nameof(GuarantorType.Own),
                DisplayName = "Own",
                ID = new Guid("1e8471f8-9105-43d2-95ed-851b15bb52bf"),
            });
            await Create(new GuarantorType
            {
                Name = nameof(GuarantorType.DifferentCompanyGuarantee),
                DisplayName = "Different Company's Guarantee",
                ID = new Guid("c4e41cb4-615d-402c-965a-7423d3fd48e2"),
            });
            await Create(new GuarantorType
            {
                Name = nameof(GuarantorType.None),
                DisplayName = "None",
                ID = new Guid("83f5299b-ab8b-4b08-bba2-d7f2fede89e8"),
            });
        }

        async Task CreateCFSPCPCNumber()
        {
            await Create(new CFSPCPCNumber
            {
                Name = nameof(CFSPCPCNumber._0610040),
                ID = new Guid("873e84c9-1d0a-4ba1-8f59-7b752003fa91"),
            });
            await Create(new CFSPCPCNumber
            {
                Name = nameof(CFSPCPCNumber._0612071),
                ID = new Guid("8fa2e8bf-8eed-45e9-bb81-1f036feb6997"),
            });
        }

        async Task CreateCFSPType()
        {
            await Create(new CFSPType
            {
                Name = nameof(CFSPType.Own),
                ID = new Guid("acec3dda-4f7a-4beb-baa9-2c30b2e348ae"),
            });
            await Create(new CFSPType
            {
                Name = nameof(CFSPType.Channelports),
                ID = new Guid("4cec47df-18cd-4cda-8ae7-0b97f538c8cc"),
            });
            await Create(new CFSPType
            {
                Name = nameof(CFSPType.None),
                ID = new Guid("55579965-9ff7-4465-aace-bb793d24a7bb"),
            });
        }

        async Task CreateTicketsAction()
        {

            var types = new List<SupportTicketAction>
            {
                new SupportTicketAction {
                    Name = nameof(SupportTicketAction.Claim),
                },
                new SupportTicketAction {
                    Name = nameof(SupportTicketAction.Unclaim),
                },
                new SupportTicketAction {
                    Name = nameof(SupportTicketAction.PlaceOnHold),
                },
                new SupportTicketAction {
                    Name = nameof(SupportTicketAction.HighlightToSupervisor),
                },
                new SupportTicketAction {
                    Name = nameof(SupportTicketAction.Respond),
                },
                new SupportTicketAction {
                    Name = nameof(SupportTicketAction.Close),
                },
            };
            await Create(types.AsEnumerable());
        }
        async Task CreateTicketsStatus()
        {

            var types = new List<SupportTicketStatus>
            {
                new SupportTicketStatus {
                    Name = nameof(SupportTicketStatus.Active),
                },
                new SupportTicketStatus {
                    Name = nameof(SupportTicketStatus.Closed),
                },
            };
            await Create(types.AsEnumerable());
        }

    }
}
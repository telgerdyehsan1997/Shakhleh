
ALTER TABLE [dbo].[Companies]
ADD ArchiveLogIds nvarchar(200);


ALTER TABLE [dbo].[Companies]
ADD IsDeactivated bit;


ALTER TABLE [dbo].[NCTSShipmentOuts]
ADD ArchiveLogIds nvarchar(200);


ALTER TABLE [dbo].[Shipments]
ADD ArchiveLogIds nvarchar(200);


ALTER TABLE [dbo].[AuthorisedLocations]
ADD ArchiveLogIds nvarchar(200);


ALTER TABLE [dbo].[Routes]
ADD ArchiveLogIds nvarchar(200);

ALTER TABLE [dbo].[Ports]
ADD ArchiveLogIds nvarchar(200);

ALTER TABLE [dbo].[Countries]
ADD ArchiveLogIds nvarchar(200);

ALTER TABLE [dbo].[Products]
ADD ArchiveLogIds nvarchar(200);

ALTER TABLE [dbo].[CPCs]
ADD ArchiveLogIds nvarchar(200);

ALTER TABLE [dbo].[TaxLines]
ADD ArchiveLogIds nvarchar(200);

ALTER TABLE [dbo].[PaymentTypes]
ADD ArchiveLogIds nvarchar(200);

ALTER TABLE [dbo].[NCTSShipmentOutConsignments]
ADD IsHighValue bit not null;


alter table [dbo].[NCTSConsignmentOutProgresses] add RecieveEmailNotification bit not null 


--alter table [dbo].[NCTSConsignmentOutProgresses] add RecieveEmailNotification bit 


ALTER TABLE [dbo].[CompanyUsers]
ADD ArchiveLogIds nvarchar(200);

ALTER TABLE [dbo].[CompanyUsers]
ADD IsDeactivated bit not null ;



update CompanyUsers set IsDeactivated = 0
update [dbo].[Contacts] set IsDeactivated = 0

update [dbo].[ChannelPortsUsers] set IsDeactivated = 0

update [dbo].[NCTSConsignmentOutProgresses] set RecieveEmailNotification = 0
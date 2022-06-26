// ********************************************************************
// WARNING: This file is auto-generated from M# Model.
// and may be overwritten at any time. Do not change it manually.
// ********************************************************************

namespace MSharp
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Olive.Entities;
    using Domain;
    using _F = System.Runtime.CompilerServices.CallerFilePathAttribute;
    using _L = System.Runtime.CompilerServices.CallerLineNumberAttribute;
    
    static partial class SchemaExtensions
    {
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.AdminUser> ArchiveLogIds(
            this ListModule<Domain.AdminUser>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.ArchiveLogIds, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.AdminUser> Email(
            this ListModule<Domain.AdminUser>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Email, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.AdminUser> FirstName(
            this ListModule<Domain.AdminUser>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.FirstName, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.AdminUser> ImpersonationToken(
            this ListModule<Domain.AdminUser>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.ImpersonationToken, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.AdminUser> IsAdmin(
            this ListModule<Domain.AdminUser>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.IsAdmin, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.AdminUser> IsDeactivated(
            this ListModule<Domain.AdminUser>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.IsDeactivated, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.AdminUser> LastName(
            this ListModule<Domain.AdminUser>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.LastName, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.AdminUser> MobileNumber(
            this ListModule<Domain.AdminUser>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.MobileNumber, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.AdminUser> Name(
            this ListModule<Domain.AdminUser>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Name, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.AdminUser> Password(
            this ListModule<Domain.AdminUser>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Password, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.AdminUser> Salt(
            this ListModule<Domain.AdminUser>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Salt, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.IArchivable> ArchiveLogIds(
            this ListModule<Domain.IArchivable>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.ArchiveLogIds, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.IArchivable> IsDeactivated(
            this ListModule<Domain.IArchivable>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.IsDeactivated, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.ArchiveLog> DateAndTime(
            this ListModule<Domain.ArchiveLog>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.DateAndTime, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.ArchiveLog> EntityId(
            this ListModule<Domain.ArchiveLog>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.EntityId, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.ArchiveLog> LoggedInUser(
            this ListModule<Domain.ArchiveLog>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.LoggedInUser, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.ArchiveLog> Reason(
            this ListModule<Domain.ArchiveLog>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Reason, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.ArchiveLog> TrackingNumber(
            this ListModule<Domain.ArchiveLog>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.TrackingNumber, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.ArchiveLog> UserIp(
            this ListModule<Domain.ArchiveLog>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.UserIp, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.ContentBlock> Content(
            this ListModule<Domain.ContentBlock>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Content, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.ContentBlock> Key(
            this ListModule<Domain.ContentBlock>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Key, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.EmailMessage> Attachments(
            this ListModule<Domain.EmailMessage>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Attachments, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.EmailMessage> Bcc(
            this ListModule<Domain.EmailMessage>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Bcc, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.EmailMessage> Body(
            this ListModule<Domain.EmailMessage>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Body, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.EmailMessage> Cc(
            this ListModule<Domain.EmailMessage>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Cc, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.EmailMessage> EnableSsl(
            this ListModule<Domain.EmailMessage>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.EnableSsl, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.EmailMessage> FromAddress(
            this ListModule<Domain.EmailMessage>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.FromAddress, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.EmailMessage> FromName(
            this ListModule<Domain.EmailMessage>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.FromName, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.EmailMessage> Html(
            this ListModule<Domain.EmailMessage>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Html, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.EmailMessage> Password(
            this ListModule<Domain.EmailMessage>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Password, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.EmailMessage> ReplyToAddress(
            this ListModule<Domain.EmailMessage>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.ReplyToAddress, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.EmailMessage> ReplyToName(
            this ListModule<Domain.EmailMessage>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.ReplyToName, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.EmailMessage> Retries(
            this ListModule<Domain.EmailMessage>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Retries, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.EmailMessage> SendableDate(
            this ListModule<Domain.EmailMessage>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.SendableDate, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.EmailMessage> SmtpHHost(
            this ListModule<Domain.EmailMessage>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.SmtpHHost, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.EmailMessage> SmtpPort(
            this ListModule<Domain.EmailMessage>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.SmtpPort, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.EmailMessage> Subject(
            this ListModule<Domain.EmailMessage>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Subject, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.EmailMessage> To(
            this ListModule<Domain.EmailMessage>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.To, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.EmailMessage> Username(
            this ListModule<Domain.EmailMessage>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Username, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.EmailMessage> VCalendarView(
            this ListModule<Domain.EmailMessage>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.VCalendarView, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.EmailTemplate> Body(
            this ListModule<Domain.EmailTemplate>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Body, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.EmailTemplate> DateEmailSent(
            this ListModule<Domain.EmailTemplate>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.DateEmailSent, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.EmailTemplate> Key(
            this ListModule<Domain.EmailTemplate>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Key, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.EmailTemplate> MandatoryPlaceholders(
            this ListModule<Domain.EmailTemplate>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.MandatoryPlaceholders, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.EmailTemplate> Name(
            this ListModule<Domain.EmailTemplate>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Name, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.EmailTemplate> Subject(
            this ListModule<Domain.EmailTemplate>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Subject, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.ImportError> ErrorReason(
            this ListModule<Domain.ImportError>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.ErrorReason, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.ImportError> ImportQueueItem(
            this ListModule<Domain.ImportError>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.ImportQueueItem, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.ImportError> LineNumber(
            this ListModule<Domain.ImportError>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.LineNumber, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.ImportQueueItem> Errors(
            this ListModule<Domain.ImportQueueItem>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Errors, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.ImportQueueItem> File(
            this ListModule<Domain.ImportQueueItem>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.File, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.ImportQueueItem> IsArchive(
            this ListModule<Domain.ImportQueueItem>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.IsArchive, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.ImportQueueItem> Status(
            this ListModule<Domain.ImportQueueItem>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Status, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.ImportQueueItem> Type(
            this ListModule<Domain.ImportQueueItem>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Type, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.ImportQueueItem> UploadDate(
            this ListModule<Domain.ImportQueueItem>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.UploadDate, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.ImportStatus> Name(
            this ListModule<Domain.ImportStatus>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Name, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.ImportType> Name(
            this ListModule<Domain.ImportType>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Name, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.LogonFailure> Attempts(
            this ListModule<Domain.LogonFailure>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Attempts, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.LogonFailure> Date(
            this ListModule<Domain.LogonFailure>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Date, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.LogonFailure> Email(
            this ListModule<Domain.LogonFailure>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Email, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.LogonFailure> IP(
            this ListModule<Domain.LogonFailure>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.IP, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.PasswordResetTicket> DateCreated(
            this ListModule<Domain.PasswordResetTicket>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.DateCreated, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.PasswordResetTicket> IsUsed(
            this ListModule<Domain.PasswordResetTicket>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.IsUsed, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.PasswordResetTicket> User(
            this ListModule<Domain.PasswordResetTicket>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.User, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Person> Email(
            this ListModule<Domain.Person>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Email, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Person> FirstName(
            this ListModule<Domain.Person>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.FirstName, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Person> LastName(
            this ListModule<Domain.Person>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.LastName, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Person> MobileNumber(
            this ListModule<Domain.Person>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.MobileNumber, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Person> Name(
            this ListModule<Domain.Person>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Name, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.ProgressASMResponseVM> ASMMessage(
            this ListModule<Domain.ProgressASMResponseVM>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.ASMMessage, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.ProgressASMResponseVM> Progress(
            this ListModule<Domain.ProgressASMResponseVM>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Progress, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.SendMailEntryCopyViewModel> EmailAddress(
            this ListModule<Domain.SendMailEntryCopyViewModel>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.EmailAddress, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.SendMailEntryCopyViewModel> EORINumber(
            this ListModule<Domain.SendMailEntryCopyViewModel>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.EORINumber, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Settings> AccountNo(
            this ListModule<Domain.Settings>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.AccountNo, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Settings> ActivateUCN(
            this ListModule<Domain.Settings>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.ActivateUCN, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Settings> Bankers(
            this ListModule<Domain.Settings>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Bankers, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Settings> BIC(
            this ListModule<Domain.Settings>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.BIC, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Settings> CacheVersion(
            this ListModule<Domain.Settings>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.CacheVersion, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Settings> CFSPMonthlyReportRecipients(
            this ListModule<Domain.Settings>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.CFSPMonthlyReportRecipients, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Settings> CFSPShipmentNumber(
            this ListModule<Domain.Settings>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.CFSPShipmentNumber, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Settings> ChannelportsCFSPShipmentNumber(
            this ListModule<Domain.Settings>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.ChannelportsCFSPShipmentNumber, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Settings> CPCFSPMonthEndEmailAddress(
            this ListModule<Domain.Settings>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.CPCFSPMonthEndEmailAddress, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Settings> DateSuffixesWereLastReset(
            this ListModule<Domain.Settings>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.DateSuffixesWereLastReset, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Settings> IBAN(
            this ListModule<Domain.Settings>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.IBAN, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Settings> IntoUKDocumentCode(
            this ListModule<Domain.Settings>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.IntoUKDocumentCode, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Settings> IntoUKDocumentReference(
            this ListModule<Domain.Settings>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.IntoUKDocumentReference, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Settings> IntoUKDocumentStatus(
            this ListModule<Domain.Settings>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.IntoUKDocumentStatus, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Settings> IntoUKTrackingNumber(
            this ListModule<Domain.Settings>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.IntoUKTrackingNumber, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Settings> Name(
            this ListModule<Domain.Settings>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Name, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Settings> NCTSHighValueThreshold(
            this ListModule<Domain.Settings>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.NCTSHighValueThreshold, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Settings> OutOfUKTrackingNumber(
            this ListModule<Domain.Settings>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.OutOfUKTrackingNumber, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Settings> PasswordResetTicketExpiryMinutes(
            this ListModule<Domain.Settings>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.PasswordResetTicketExpiryMinutes, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Settings> SendNCTSMessageViaASM(
            this ListModule<Domain.Settings>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.SendNCTSMessageViaASM, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Settings> SortCode(
            this ListModule<Domain.Settings>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.SortCode, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Settings> TimeUntilCleared(
            this ListModule<Domain.Settings>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.TimeUntilCleared, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.TransactionLog> Date(
            this ListModule<Domain.TransactionLog>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Date, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.TransactionLog> File(
            this ListModule<Domain.TransactionLog>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.File, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.TransactionLog> Type(
            this ListModule<Domain.TransactionLog>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Type, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.User> Email(
            this ListModule<Domain.User>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Email, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.User> FirstName(
            this ListModule<Domain.User>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.FirstName, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.User> LastName(
            this ListModule<Domain.User>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.LastName, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.User> MobileNumber(
            this ListModule<Domain.User>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.MobileNumber, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.User> Name(
            this ListModule<Domain.User>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Name, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.User> Password(
            this ListModule<Domain.User>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Password, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.User> Salt(
            this ListModule<Domain.User>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Salt, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.AdminUser> ArchiveLogIds(
            this ViewModule<Domain.AdminUser>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.ArchiveLogIds, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.AdminUser> Email(
            this ViewModule<Domain.AdminUser>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Email, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.AdminUser> FirstName(
            this ViewModule<Domain.AdminUser>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.FirstName, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.AdminUser> ImpersonationToken(
            this ViewModule<Domain.AdminUser>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.ImpersonationToken, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.AdminUser> IsAdmin(
            this ViewModule<Domain.AdminUser>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.IsAdmin, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.AdminUser> IsDeactivated(
            this ViewModule<Domain.AdminUser>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.IsDeactivated, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.AdminUser> LastName(
            this ViewModule<Domain.AdminUser>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.LastName, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.AdminUser> MobileNumber(
            this ViewModule<Domain.AdminUser>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.MobileNumber, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.AdminUser> Name(
            this ViewModule<Domain.AdminUser>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Name, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.AdminUser> Password(
            this ViewModule<Domain.AdminUser>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Password, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.AdminUser> Salt(
            this ViewModule<Domain.AdminUser>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Salt, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.IArchivable> ArchiveLogIds(
            this ViewModule<Domain.IArchivable>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.ArchiveLogIds, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.IArchivable> IsDeactivated(
            this ViewModule<Domain.IArchivable>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.IsDeactivated, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.ArchiveLog> DateAndTime(
            this ViewModule<Domain.ArchiveLog>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.DateAndTime, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.ArchiveLog> EntityId(
            this ViewModule<Domain.ArchiveLog>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.EntityId, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.ArchiveLog> LoggedInUser(
            this ViewModule<Domain.ArchiveLog>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.LoggedInUser, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.ArchiveLog> Reason(
            this ViewModule<Domain.ArchiveLog>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Reason, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.ArchiveLog> TrackingNumber(
            this ViewModule<Domain.ArchiveLog>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.TrackingNumber, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.ArchiveLog> UserIp(
            this ViewModule<Domain.ArchiveLog>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.UserIp, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.ContentBlock> Content(
            this ViewModule<Domain.ContentBlock>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Content, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.ContentBlock> Key(
            this ViewModule<Domain.ContentBlock>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Key, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.EmailMessage> Attachments(
            this ViewModule<Domain.EmailMessage>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Attachments, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.EmailMessage> Bcc(
            this ViewModule<Domain.EmailMessage>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Bcc, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.EmailMessage> Body(
            this ViewModule<Domain.EmailMessage>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Body, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.EmailMessage> Cc(
            this ViewModule<Domain.EmailMessage>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Cc, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.EmailMessage> EnableSsl(
            this ViewModule<Domain.EmailMessage>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.EnableSsl, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.EmailMessage> FromAddress(
            this ViewModule<Domain.EmailMessage>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.FromAddress, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.EmailMessage> FromName(
            this ViewModule<Domain.EmailMessage>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.FromName, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.EmailMessage> Html(
            this ViewModule<Domain.EmailMessage>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Html, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.EmailMessage> Password(
            this ViewModule<Domain.EmailMessage>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Password, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.EmailMessage> ReplyToAddress(
            this ViewModule<Domain.EmailMessage>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.ReplyToAddress, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.EmailMessage> ReplyToName(
            this ViewModule<Domain.EmailMessage>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.ReplyToName, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.EmailMessage> Retries(
            this ViewModule<Domain.EmailMessage>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Retries, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.EmailMessage> SendableDate(
            this ViewModule<Domain.EmailMessage>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.SendableDate, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.EmailMessage> SmtpHHost(
            this ViewModule<Domain.EmailMessage>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.SmtpHHost, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.EmailMessage> SmtpPort(
            this ViewModule<Domain.EmailMessage>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.SmtpPort, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.EmailMessage> Subject(
            this ViewModule<Domain.EmailMessage>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Subject, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.EmailMessage> To(
            this ViewModule<Domain.EmailMessage>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.To, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.EmailMessage> Username(
            this ViewModule<Domain.EmailMessage>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Username, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.EmailMessage> VCalendarView(
            this ViewModule<Domain.EmailMessage>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.VCalendarView, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.EmailTemplate> Body(
            this ViewModule<Domain.EmailTemplate>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Body, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.EmailTemplate> DateEmailSent(
            this ViewModule<Domain.EmailTemplate>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.DateEmailSent, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.EmailTemplate> Key(
            this ViewModule<Domain.EmailTemplate>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Key, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.EmailTemplate> MandatoryPlaceholders(
            this ViewModule<Domain.EmailTemplate>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.MandatoryPlaceholders, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.EmailTemplate> Name(
            this ViewModule<Domain.EmailTemplate>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Name, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.EmailTemplate> Subject(
            this ViewModule<Domain.EmailTemplate>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Subject, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.ImportError> ErrorReason(
            this ViewModule<Domain.ImportError>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.ErrorReason, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.ImportError> ImportQueueItem(
            this ViewModule<Domain.ImportError>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.ImportQueueItem, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.ImportError> LineNumber(
            this ViewModule<Domain.ImportError>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.LineNumber, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.ImportQueueItem> Errors(
            this ViewModule<Domain.ImportQueueItem>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Errors, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.ImportQueueItem> File(
            this ViewModule<Domain.ImportQueueItem>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.File, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.ImportQueueItem> IsArchive(
            this ViewModule<Domain.ImportQueueItem>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.IsArchive, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.ImportQueueItem> Status(
            this ViewModule<Domain.ImportQueueItem>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Status, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.ImportQueueItem> Type(
            this ViewModule<Domain.ImportQueueItem>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Type, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.ImportQueueItem> UploadDate(
            this ViewModule<Domain.ImportQueueItem>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.UploadDate, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.ImportStatus> Name(
            this ViewModule<Domain.ImportStatus>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Name, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.ImportType> Name(
            this ViewModule<Domain.ImportType>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Name, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.LogonFailure> Attempts(
            this ViewModule<Domain.LogonFailure>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Attempts, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.LogonFailure> Date(
            this ViewModule<Domain.LogonFailure>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Date, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.LogonFailure> Email(
            this ViewModule<Domain.LogonFailure>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Email, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.LogonFailure> IP(
            this ViewModule<Domain.LogonFailure>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.IP, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.PasswordResetTicket> DateCreated(
            this ViewModule<Domain.PasswordResetTicket>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.DateCreated, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.PasswordResetTicket> IsUsed(
            this ViewModule<Domain.PasswordResetTicket>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.IsUsed, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.PasswordResetTicket> User(
            this ViewModule<Domain.PasswordResetTicket>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.User, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Person> Email(
            this ViewModule<Domain.Person>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Email, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Person> FirstName(
            this ViewModule<Domain.Person>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.FirstName, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Person> LastName(
            this ViewModule<Domain.Person>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.LastName, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Person> MobileNumber(
            this ViewModule<Domain.Person>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.MobileNumber, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Person> Name(
            this ViewModule<Domain.Person>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Name, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.ProgressASMResponseVM> ASMMessage(
            this ViewModule<Domain.ProgressASMResponseVM>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.ASMMessage, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.ProgressASMResponseVM> Progress(
            this ViewModule<Domain.ProgressASMResponseVM>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Progress, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.SendMailEntryCopyViewModel> EmailAddress(
            this ViewModule<Domain.SendMailEntryCopyViewModel>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.EmailAddress, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.SendMailEntryCopyViewModel> EORINumber(
            this ViewModule<Domain.SendMailEntryCopyViewModel>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.EORINumber, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Settings> AccountNo(
            this ViewModule<Domain.Settings>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.AccountNo, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Settings> ActivateUCN(
            this ViewModule<Domain.Settings>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.ActivateUCN, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Settings> Bankers(
            this ViewModule<Domain.Settings>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Bankers, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Settings> BIC(
            this ViewModule<Domain.Settings>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.BIC, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Settings> CacheVersion(
            this ViewModule<Domain.Settings>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.CacheVersion, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Settings> CFSPMonthlyReportRecipients(
            this ViewModule<Domain.Settings>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.CFSPMonthlyReportRecipients, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Settings> CFSPShipmentNumber(
            this ViewModule<Domain.Settings>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.CFSPShipmentNumber, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Settings> ChannelportsCFSPShipmentNumber(
            this ViewModule<Domain.Settings>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.ChannelportsCFSPShipmentNumber, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Settings> CPCFSPMonthEndEmailAddress(
            this ViewModule<Domain.Settings>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.CPCFSPMonthEndEmailAddress, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Settings> DateSuffixesWereLastReset(
            this ViewModule<Domain.Settings>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.DateSuffixesWereLastReset, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Settings> IBAN(
            this ViewModule<Domain.Settings>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.IBAN, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Settings> IntoUKDocumentCode(
            this ViewModule<Domain.Settings>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.IntoUKDocumentCode, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Settings> IntoUKDocumentReference(
            this ViewModule<Domain.Settings>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.IntoUKDocumentReference, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Settings> IntoUKDocumentStatus(
            this ViewModule<Domain.Settings>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.IntoUKDocumentStatus, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Settings> IntoUKTrackingNumber(
            this ViewModule<Domain.Settings>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.IntoUKTrackingNumber, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Settings> Name(
            this ViewModule<Domain.Settings>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Name, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Settings> NCTSHighValueThreshold(
            this ViewModule<Domain.Settings>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.NCTSHighValueThreshold, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Settings> OutOfUKTrackingNumber(
            this ViewModule<Domain.Settings>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.OutOfUKTrackingNumber, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Settings> PasswordResetTicketExpiryMinutes(
            this ViewModule<Domain.Settings>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.PasswordResetTicketExpiryMinutes, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Settings> SendNCTSMessageViaASM(
            this ViewModule<Domain.Settings>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.SendNCTSMessageViaASM, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Settings> SortCode(
            this ViewModule<Domain.Settings>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.SortCode, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Settings> TimeUntilCleared(
            this ViewModule<Domain.Settings>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.TimeUntilCleared, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.TransactionLog> Date(
            this ViewModule<Domain.TransactionLog>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Date, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.TransactionLog> File(
            this ViewModule<Domain.TransactionLog>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.File, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.TransactionLog> Type(
            this ViewModule<Domain.TransactionLog>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Type, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.User> Email(
            this ViewModule<Domain.User>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Email, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.User> FirstName(
            this ViewModule<Domain.User>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.FirstName, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.User> LastName(
            this ViewModule<Domain.User>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.LastName, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.User> MobileNumber(
            this ViewModule<Domain.User>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.MobileNumber, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.User> Name(
            this ViewModule<Domain.User>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Name, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.User> Password(
            this ViewModule<Domain.User>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Password, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.User> Salt(
            this ViewModule<Domain.User>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Salt, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.AdminUser, string> ArchiveLogIds(
            this ListModule<Domain.AdminUser>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.ArchiveLogIds, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.AdminUser, string> Email(
            this ListModule<Domain.AdminUser>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Email, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.AdminUser, string> FirstName(
            this ListModule<Domain.AdminUser>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.FirstName, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.AdminUser, string> ImpersonationToken(
            this ListModule<Domain.AdminUser>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.ImpersonationToken, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.AdminUser, bool?> IsAdmin(
            this ListModule<Domain.AdminUser>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.IsAdmin, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.AdminUser, bool> IsDeactivated(
            this ListModule<Domain.AdminUser>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.IsDeactivated, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.AdminUser, string> LastName(
            this ListModule<Domain.AdminUser>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.LastName, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.AdminUser, string> MobileNumber(
            this ListModule<Domain.AdminUser>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.MobileNumber, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.AdminUser, string> Name(
            this ListModule<Domain.AdminUser>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Name, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.AdminUser, string> Password(
            this ListModule<Domain.AdminUser>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Password, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.AdminUser, string> Salt(
            this ListModule<Domain.AdminUser>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Salt, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.IArchivable, string> ArchiveLogIds(
            this ListModule<Domain.IArchivable>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.ArchiveLogIds, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.IArchivable, bool> IsDeactivated(
            this ListModule<Domain.IArchivable>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.IsDeactivated, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.ArchiveLog, DateTime> DateAndTime(
            this ListModule<Domain.ArchiveLog>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.DateAndTime, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.ArchiveLog, Guid?> EntityId(
            this ListModule<Domain.ArchiveLog>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.EntityId, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.ArchiveLog, Domain.User> LoggedInUser(
            this ListModule<Domain.ArchiveLog>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.LoggedInUser, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.ArchiveLog, string> Reason(
            this ListModule<Domain.ArchiveLog>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Reason, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.ArchiveLog, string> TrackingNumber(
            this ListModule<Domain.ArchiveLog>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.TrackingNumber, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.ArchiveLog, string> UserIp(
            this ListModule<Domain.ArchiveLog>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.UserIp, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.ContentBlock, string> Content(
            this ListModule<Domain.ContentBlock>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Content, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.ContentBlock, string> Key(
            this ListModule<Domain.ContentBlock>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Key, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.EmailMessage, string> Attachments(
            this ListModule<Domain.EmailMessage>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Attachments, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.EmailMessage, string> Bcc(
            this ListModule<Domain.EmailMessage>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Bcc, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.EmailMessage, string> Body(
            this ListModule<Domain.EmailMessage>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Body, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.EmailMessage, string> Cc(
            this ListModule<Domain.EmailMessage>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Cc, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.EmailMessage, bool?> EnableSsl(
            this ListModule<Domain.EmailMessage>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.EnableSsl, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.EmailMessage, string> FromAddress(
            this ListModule<Domain.EmailMessage>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.FromAddress, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.EmailMessage, string> FromName(
            this ListModule<Domain.EmailMessage>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.FromName, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.EmailMessage, bool> Html(
            this ListModule<Domain.EmailMessage>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Html, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.EmailMessage, string> Password(
            this ListModule<Domain.EmailMessage>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Password, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.EmailMessage, string> ReplyToAddress(
            this ListModule<Domain.EmailMessage>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.ReplyToAddress, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.EmailMessage, string> ReplyToName(
            this ListModule<Domain.EmailMessage>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.ReplyToName, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.EmailMessage, int> Retries(
            this ListModule<Domain.EmailMessage>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Retries, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.EmailMessage, DateTime> SendableDate(
            this ListModule<Domain.EmailMessage>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.SendableDate, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.EmailMessage, string> SmtpHHost(
            this ListModule<Domain.EmailMessage>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.SmtpHHost, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.EmailMessage, int?> SmtpPort(
            this ListModule<Domain.EmailMessage>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.SmtpPort, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.EmailMessage, string> Subject(
            this ListModule<Domain.EmailMessage>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Subject, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.EmailMessage, string> To(
            this ListModule<Domain.EmailMessage>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.To, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.EmailMessage, string> Username(
            this ListModule<Domain.EmailMessage>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Username, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.EmailMessage, string> VCalendarView(
            this ListModule<Domain.EmailMessage>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.VCalendarView, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.EmailTemplate, string> Body(
            this ListModule<Domain.EmailTemplate>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Body, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.EmailTemplate, DateTime?> DateEmailSent(
            this ListModule<Domain.EmailTemplate>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.DateEmailSent, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.EmailTemplate, string> Key(
            this ListModule<Domain.EmailTemplate>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Key, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.EmailTemplate, string> MandatoryPlaceholders(
            this ListModule<Domain.EmailTemplate>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.MandatoryPlaceholders, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.EmailTemplate, string> Name(
            this ListModule<Domain.EmailTemplate>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Name, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.EmailTemplate, string> Subject(
            this ListModule<Domain.EmailTemplate>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Subject, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.ImportError, string> ErrorReason(
            this ListModule<Domain.ImportError>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.ErrorReason, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.ImportError, Domain.ImportQueueItem> ImportQueueItem(
            this ListModule<Domain.ImportError>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.ImportQueueItem, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.ImportError, int?> LineNumber(
            this ListModule<Domain.ImportError>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.LineNumber, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.ImportQueueItem, IDatabaseQuery<Domain.ImportError>> Errors(
            this ListModule<Domain.ImportQueueItem>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Errors, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.ImportQueueItem, Blob> File(
            this ListModule<Domain.ImportQueueItem>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.File, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.ImportQueueItem, bool> IsArchive(
            this ListModule<Domain.ImportQueueItem>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.IsArchive, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.ImportQueueItem, Domain.ImportStatus> Status(
            this ListModule<Domain.ImportQueueItem>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Status, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.ImportQueueItem, Domain.ImportType> Type(
            this ListModule<Domain.ImportQueueItem>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Type, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.ImportQueueItem, DateTime> UploadDate(
            this ListModule<Domain.ImportQueueItem>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.UploadDate, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.ImportStatus, string> Name(
            this ListModule<Domain.ImportStatus>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Name, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.ImportType, string> Name(
            this ListModule<Domain.ImportType>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Name, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.LogonFailure, int> Attempts(
            this ListModule<Domain.LogonFailure>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Attempts, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.LogonFailure, DateTime> Date(
            this ListModule<Domain.LogonFailure>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Date, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.LogonFailure, string> Email(
            this ListModule<Domain.LogonFailure>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Email, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.LogonFailure, string> IP(
            this ListModule<Domain.LogonFailure>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.IP, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.PasswordResetTicket, DateTime> DateCreated(
            this ListModule<Domain.PasswordResetTicket>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.DateCreated, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.PasswordResetTicket, bool> IsUsed(
            this ListModule<Domain.PasswordResetTicket>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.IsUsed, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.PasswordResetTicket, Domain.User> User(
            this ListModule<Domain.PasswordResetTicket>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.User, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Person, string> Email(
            this ListModule<Domain.Person>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Email, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Person, string> FirstName(
            this ListModule<Domain.Person>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.FirstName, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Person, string> LastName(
            this ListModule<Domain.Person>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.LastName, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Person, string> MobileNumber(
            this ListModule<Domain.Person>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.MobileNumber, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Person, string> Name(
            this ListModule<Domain.Person>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Name, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.ProgressASMResponseVM, string> ASMMessage(
            this ListModule<Domain.ProgressASMResponseVM>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.ASMMessage, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.ProgressASMResponseVM, string> Progress(
            this ListModule<Domain.ProgressASMResponseVM>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Progress, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.SendMailEntryCopyViewModel, string> EmailAddress(
            this ListModule<Domain.SendMailEntryCopyViewModel>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.EmailAddress, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.SendMailEntryCopyViewModel, string> EORINumber(
            this ListModule<Domain.SendMailEntryCopyViewModel>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.EORINumber, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Settings, string> AccountNo(
            this ListModule<Domain.Settings>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.AccountNo, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Settings, bool?> ActivateUCN(
            this ListModule<Domain.Settings>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.ActivateUCN, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Settings, string> Bankers(
            this ListModule<Domain.Settings>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Bankers, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Settings, string> BIC(
            this ListModule<Domain.Settings>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.BIC, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Settings, int> CacheVersion(
            this ListModule<Domain.Settings>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.CacheVersion, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Settings, string> CFSPMonthlyReportRecipients(
            this ListModule<Domain.Settings>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.CFSPMonthlyReportRecipients, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Settings, int?> CFSPShipmentNumber(
            this ListModule<Domain.Settings>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.CFSPShipmentNumber, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Settings, string> ChannelportsCFSPShipmentNumber(
            this ListModule<Domain.Settings>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.ChannelportsCFSPShipmentNumber, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Settings, string> CPCFSPMonthEndEmailAddress(
            this ListModule<Domain.Settings>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.CPCFSPMonthEndEmailAddress, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Settings, DateTime?> DateSuffixesWereLastReset(
            this ListModule<Domain.Settings>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.DateSuffixesWereLastReset, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Settings, string> IBAN(
            this ListModule<Domain.Settings>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.IBAN, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Settings, string> IntoUKDocumentCode(
            this ListModule<Domain.Settings>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.IntoUKDocumentCode, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Settings, string> IntoUKDocumentReference(
            this ListModule<Domain.Settings>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.IntoUKDocumentReference, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Settings, string> IntoUKDocumentStatus(
            this ListModule<Domain.Settings>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.IntoUKDocumentStatus, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Settings, int?> IntoUKTrackingNumber(
            this ListModule<Domain.Settings>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.IntoUKTrackingNumber, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Settings, string> Name(
            this ListModule<Domain.Settings>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Name, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Settings, int?> NCTSHighValueThreshold(
            this ListModule<Domain.Settings>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.NCTSHighValueThreshold, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Settings, int?> OutOfUKTrackingNumber(
            this ListModule<Domain.Settings>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.OutOfUKTrackingNumber, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Settings, int> PasswordResetTicketExpiryMinutes(
            this ListModule<Domain.Settings>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.PasswordResetTicketExpiryMinutes, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Settings, bool?> SendNCTSMessageViaASM(
            this ListModule<Domain.Settings>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.SendNCTSMessageViaASM, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Settings, string> SortCode(
            this ListModule<Domain.Settings>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.SortCode, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Settings, int?> TimeUntilCleared(
            this ListModule<Domain.Settings>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.TimeUntilCleared, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.TransactionLog, DateTime> Date(
            this ListModule<Domain.TransactionLog>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Date, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.TransactionLog, Blob> File(
            this ListModule<Domain.TransactionLog>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.File, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.TransactionLog, LogType> Type(
            this ListModule<Domain.TransactionLog>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Type, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.User, string> Email(
            this ListModule<Domain.User>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Email, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.User, string> FirstName(
            this ListModule<Domain.User>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.FirstName, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.User, string> LastName(
            this ListModule<Domain.User>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.LastName, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.User, string> MobileNumber(
            this ListModule<Domain.User>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.MobileNumber, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.User, string> Name(
            this ListModule<Domain.User>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Name, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.User, string> Password(
            this ListModule<Domain.User>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Password, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.User, string> Salt(
            this ListModule<Domain.User>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Salt, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement ArchiveLogIds(
            this FormModule<Domain.AdminUser>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.ArchiveLogIds, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement Email(
            this FormModule<Domain.AdminUser>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Email, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement FirstName(
            this FormModule<Domain.AdminUser>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.FirstName, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement ImpersonationToken(
            this FormModule<Domain.AdminUser>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.ImpersonationToken, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static BooleanFormElement IsAdmin(
            this FormModule<Domain.AdminUser>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.IsAdmin, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static BooleanFormElement IsDeactivated(
            this FormModule<Domain.AdminUser>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.IsDeactivated, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement LastName(
            this FormModule<Domain.AdminUser>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.LastName, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement MobileNumber(
            this FormModule<Domain.AdminUser>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.MobileNumber, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement Password(
            this FormModule<Domain.AdminUser>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Password, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement Salt(
            this FormModule<Domain.AdminUser>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Salt, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement ArchiveLogIds(
            this FormModule<Domain.IArchivable>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.ArchiveLogIds, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static BooleanFormElement IsDeactivated(
            this FormModule<Domain.IArchivable>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.IsDeactivated, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static DateTimeFormElement DateAndTime(
            this FormModule<Domain.ArchiveLog>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.DateAndTime, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement EntityId(
            this FormModule<Domain.ArchiveLog>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.EntityId, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static AssociationFormElement LoggedInUser(
            this FormModule<Domain.ArchiveLog>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.LoggedInUser, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement Reason(
            this FormModule<Domain.ArchiveLog>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Reason, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement TrackingNumber(
            this FormModule<Domain.ArchiveLog>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.TrackingNumber, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement UserIp(
            this FormModule<Domain.ArchiveLog>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.UserIp, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement Content(
            this FormModule<Domain.ContentBlock>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Content, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement Key(
            this FormModule<Domain.ContentBlock>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Key, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement Attachments(
            this FormModule<Domain.EmailMessage>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Attachments, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement Bcc(
            this FormModule<Domain.EmailMessage>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Bcc, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement Body(
            this FormModule<Domain.EmailMessage>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Body, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement Cc(
            this FormModule<Domain.EmailMessage>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Cc, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static BooleanFormElement EnableSsl(
            this FormModule<Domain.EmailMessage>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.EnableSsl, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement FromAddress(
            this FormModule<Domain.EmailMessage>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.FromAddress, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement FromName(
            this FormModule<Domain.EmailMessage>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.FromName, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static BooleanFormElement Html(
            this FormModule<Domain.EmailMessage>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Html, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement Password(
            this FormModule<Domain.EmailMessage>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Password, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement ReplyToAddress(
            this FormModule<Domain.EmailMessage>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.ReplyToAddress, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement ReplyToName(
            this FormModule<Domain.EmailMessage>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.ReplyToName, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static NumberFormElement Retries(
            this FormModule<Domain.EmailMessage>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Retries, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static DateTimeFormElement SendableDate(
            this FormModule<Domain.EmailMessage>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.SendableDate, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement SmtpHHost(
            this FormModule<Domain.EmailMessage>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.SmtpHHost, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static NumberFormElement SmtpPort(
            this FormModule<Domain.EmailMessage>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.SmtpPort, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement Subject(
            this FormModule<Domain.EmailMessage>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Subject, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement To(
            this FormModule<Domain.EmailMessage>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.To, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement Username(
            this FormModule<Domain.EmailMessage>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Username, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement VCalendarView(
            this FormModule<Domain.EmailMessage>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.VCalendarView, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement Body(
            this FormModule<Domain.EmailTemplate>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Body, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static DateTimeFormElement DateEmailSent(
            this FormModule<Domain.EmailTemplate>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.DateEmailSent, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement Key(
            this FormModule<Domain.EmailTemplate>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Key, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement MandatoryPlaceholders(
            this FormModule<Domain.EmailTemplate>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.MandatoryPlaceholders, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement Name(
            this FormModule<Domain.EmailTemplate>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Name, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement Subject(
            this FormModule<Domain.EmailTemplate>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Subject, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement ErrorReason(
            this FormModule<Domain.ImportError>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.ErrorReason, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static AssociationFormElement ImportQueueItem(
            this FormModule<Domain.ImportError>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.ImportQueueItem, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static NumberFormElement LineNumber(
            this FormModule<Domain.ImportError>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.LineNumber, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static AssociationFormElement Errors(
            this FormModule<Domain.ImportQueueItem>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Errors, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static BinaryFormElement File(
            this FormModule<Domain.ImportQueueItem>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.File, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static BooleanFormElement IsArchive(
            this FormModule<Domain.ImportQueueItem>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.IsArchive, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static AssociationFormElement Status(
            this FormModule<Domain.ImportQueueItem>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Status, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static AssociationFormElement Type(
            this FormModule<Domain.ImportQueueItem>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Type, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static DateTimeFormElement UploadDate(
            this FormModule<Domain.ImportQueueItem>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.UploadDate, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement Name(
            this FormModule<Domain.ImportStatus>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Name, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement Name(
            this FormModule<Domain.ImportType>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Name, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static NumberFormElement Attempts(
            this FormModule<Domain.LogonFailure>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Attempts, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static DateTimeFormElement Date(
            this FormModule<Domain.LogonFailure>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Date, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement Email(
            this FormModule<Domain.LogonFailure>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Email, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement IP(
            this FormModule<Domain.LogonFailure>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.IP, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static DateTimeFormElement DateCreated(
            this FormModule<Domain.PasswordResetTicket>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.DateCreated, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static BooleanFormElement IsUsed(
            this FormModule<Domain.PasswordResetTicket>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.IsUsed, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static AssociationFormElement User(
            this FormModule<Domain.PasswordResetTicket>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.User, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement Email(
            this FormModule<Domain.Person>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Email, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement FirstName(
            this FormModule<Domain.Person>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.FirstName, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement LastName(
            this FormModule<Domain.Person>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.LastName, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement MobileNumber(
            this FormModule<Domain.Person>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.MobileNumber, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement ASMMessage(
            this FormModule<Domain.ProgressASMResponseVM>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.ASMMessage, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement Progress(
            this FormModule<Domain.ProgressASMResponseVM>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Progress, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement EmailAddress(
            this FormModule<Domain.SendMailEntryCopyViewModel>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.EmailAddress, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement EORINumber(
            this FormModule<Domain.SendMailEntryCopyViewModel>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.EORINumber, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement AccountNo(
            this FormModule<Domain.Settings>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.AccountNo, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static BooleanFormElement ActivateUCN(
            this FormModule<Domain.Settings>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.ActivateUCN, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement Bankers(
            this FormModule<Domain.Settings>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Bankers, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement BIC(
            this FormModule<Domain.Settings>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.BIC, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static NumberFormElement CacheVersion(
            this FormModule<Domain.Settings>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.CacheVersion, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement CFSPMonthlyReportRecipients(
            this FormModule<Domain.Settings>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.CFSPMonthlyReportRecipients, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static NumberFormElement CFSPShipmentNumber(
            this FormModule<Domain.Settings>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.CFSPShipmentNumber, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement ChannelportsCFSPShipmentNumber(
            this FormModule<Domain.Settings>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.ChannelportsCFSPShipmentNumber, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement CPCFSPMonthEndEmailAddress(
            this FormModule<Domain.Settings>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.CPCFSPMonthEndEmailAddress, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static DateTimeFormElement DateSuffixesWereLastReset(
            this FormModule<Domain.Settings>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.DateSuffixesWereLastReset, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement IBAN(
            this FormModule<Domain.Settings>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.IBAN, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement IntoUKDocumentCode(
            this FormModule<Domain.Settings>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.IntoUKDocumentCode, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement IntoUKDocumentReference(
            this FormModule<Domain.Settings>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.IntoUKDocumentReference, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement IntoUKDocumentStatus(
            this FormModule<Domain.Settings>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.IntoUKDocumentStatus, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static NumberFormElement IntoUKTrackingNumber(
            this FormModule<Domain.Settings>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.IntoUKTrackingNumber, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement Name(
            this FormModule<Domain.Settings>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Name, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static NumberFormElement NCTSHighValueThreshold(
            this FormModule<Domain.Settings>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.NCTSHighValueThreshold, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static NumberFormElement OutOfUKTrackingNumber(
            this FormModule<Domain.Settings>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.OutOfUKTrackingNumber, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static NumberFormElement PasswordResetTicketExpiryMinutes(
            this FormModule<Domain.Settings>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.PasswordResetTicketExpiryMinutes, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static BooleanFormElement SendNCTSMessageViaASM(
            this FormModule<Domain.Settings>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.SendNCTSMessageViaASM, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement SortCode(
            this FormModule<Domain.Settings>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.SortCode, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static NumberFormElement TimeUntilCleared(
            this FormModule<Domain.Settings>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.TimeUntilCleared, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static DateTimeFormElement Date(
            this FormModule<Domain.TransactionLog>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Date, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static BinaryFormElement File(
            this FormModule<Domain.TransactionLog>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.File, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static NumberFormElement Type(
            this FormModule<Domain.TransactionLog>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Type, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement Email(
            this FormModule<Domain.User>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Email, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement FirstName(
            this FormModule<Domain.User>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.FirstName, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement LastName(
            this FormModule<Domain.User>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.LastName, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement MobileNumber(
            this FormModule<Domain.User>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.MobileNumber, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement Password(
            this FormModule<Domain.User>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Password, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement Salt(
            this FormModule<Domain.User>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Salt, fl, ln);
    }
}
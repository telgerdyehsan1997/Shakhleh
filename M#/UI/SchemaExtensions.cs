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
        public static PropertyFilterElement<Domain.Administrator> Email(
            this ListModule<Domain.Administrator>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Email, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Administrator> FirstName(
            this ListModule<Domain.Administrator>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.FirstName, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Administrator> ImpersonationToken(
            this ListModule<Domain.Administrator>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.ImpersonationToken, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Administrator> IsDeactivated(
            this ListModule<Domain.Administrator>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.IsDeactivated, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Administrator> LastName(
            this ListModule<Domain.Administrator>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.LastName, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Administrator> Name(
            this ListModule<Domain.Administrator>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Name, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Administrator> Password(
            this ListModule<Domain.Administrator>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Password, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Administrator> Salt(
            this ListModule<Domain.Administrator>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Salt, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.AuditEvent> Date(
            this ListModule<Domain.AuditEvent>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Date, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.AuditEvent> Event(
            this ListModule<Domain.AuditEvent>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Event, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.AuditEvent> ItemData(
            this ListModule<Domain.AuditEvent>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.ItemData, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.AuditEvent> ItemGroup(
            this ListModule<Domain.AuditEvent>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.ItemGroup, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.AuditEvent> ItemId(
            this ListModule<Domain.AuditEvent>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.ItemId, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.AuditEvent> ItemType(
            this ListModule<Domain.AuditEvent>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.ItemType, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.AuditEvent> UserId(
            this ListModule<Domain.AuditEvent>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.UserId, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.AuditEvent> UserIp(
            this ListModule<Domain.AuditEvent>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.UserIp, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Contact> FirstName(
            this ListModule<Domain.Contact>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.FirstName, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Contact> LastName(
            this ListModule<Domain.Contact>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.LastName, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Contact> Name(
            this ListModule<Domain.Contact>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Name, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Contact> PhoneNumber(
            this ListModule<Domain.Contact>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.PhoneNumber, fl, ln);
        
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
        public static PropertyFilterElement<Domain.PasswordResetTicket> IsExpired(
            this ListModule<Domain.PasswordResetTicket>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.IsExpired, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.PasswordResetTicket> IsUsed(
            this ListModule<Domain.PasswordResetTicket>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.IsUsed, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.PasswordResetTicket> User(
            this ListModule<Domain.PasswordResetTicket>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.User, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Settings> CacheVersion(
            this ListModule<Domain.Settings>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.CacheVersion, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Settings> Name(
            this ListModule<Domain.Settings>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Name, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Settings> PasswordResetTicketExpiryMinutes(
            this ListModule<Domain.Settings>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.PasswordResetTicketExpiryMinutes, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.User> Email(
            this ListModule<Domain.User>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Email, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.User> FirstName(
            this ListModule<Domain.User>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.FirstName, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.User> IsDeactivated(
            this ListModule<Domain.User>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.IsDeactivated, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.User> LastName(
            this ListModule<Domain.User>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.LastName, fl, ln);
        
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
        public static ViewElement<Domain.Administrator> Email(
            this ViewModule<Domain.Administrator>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Email, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Administrator> FirstName(
            this ViewModule<Domain.Administrator>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.FirstName, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Administrator> ImpersonationToken(
            this ViewModule<Domain.Administrator>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.ImpersonationToken, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Administrator> IsDeactivated(
            this ViewModule<Domain.Administrator>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.IsDeactivated, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Administrator> LastName(
            this ViewModule<Domain.Administrator>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.LastName, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Administrator> Name(
            this ViewModule<Domain.Administrator>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Name, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Administrator> Password(
            this ViewModule<Domain.Administrator>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Password, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Administrator> Salt(
            this ViewModule<Domain.Administrator>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Salt, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.AuditEvent> Date(
            this ViewModule<Domain.AuditEvent>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Date, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.AuditEvent> Event(
            this ViewModule<Domain.AuditEvent>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Event, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.AuditEvent> ItemData(
            this ViewModule<Domain.AuditEvent>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.ItemData, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.AuditEvent> ItemGroup(
            this ViewModule<Domain.AuditEvent>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.ItemGroup, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.AuditEvent> ItemId(
            this ViewModule<Domain.AuditEvent>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.ItemId, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.AuditEvent> ItemType(
            this ViewModule<Domain.AuditEvent>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.ItemType, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.AuditEvent> UserId(
            this ViewModule<Domain.AuditEvent>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.UserId, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.AuditEvent> UserIp(
            this ViewModule<Domain.AuditEvent>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.UserIp, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Contact> FirstName(
            this ViewModule<Domain.Contact>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.FirstName, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Contact> LastName(
            this ViewModule<Domain.Contact>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.LastName, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Contact> Name(
            this ViewModule<Domain.Contact>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Name, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Contact> PhoneNumber(
            this ViewModule<Domain.Contact>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.PhoneNumber, fl, ln);
        
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
        public static ViewElement<Domain.PasswordResetTicket> IsExpired(
            this ViewModule<Domain.PasswordResetTicket>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.IsExpired, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.PasswordResetTicket> IsUsed(
            this ViewModule<Domain.PasswordResetTicket>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.IsUsed, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.PasswordResetTicket> User(
            this ViewModule<Domain.PasswordResetTicket>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.User, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Settings> CacheVersion(
            this ViewModule<Domain.Settings>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.CacheVersion, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Settings> Name(
            this ViewModule<Domain.Settings>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Name, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Settings> PasswordResetTicketExpiryMinutes(
            this ViewModule<Domain.Settings>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.PasswordResetTicketExpiryMinutes, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.User> Email(
            this ViewModule<Domain.User>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Email, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.User> FirstName(
            this ViewModule<Domain.User>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.FirstName, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.User> IsDeactivated(
            this ViewModule<Domain.User>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.IsDeactivated, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.User> LastName(
            this ViewModule<Domain.User>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.LastName, fl, ln);
        
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
        public static ViewElement<Domain.Administrator, string> Email(
            this ListModule<Domain.Administrator>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Email, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Administrator, string> FirstName(
            this ListModule<Domain.Administrator>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.FirstName, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Administrator, string> ImpersonationToken(
            this ListModule<Domain.Administrator>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.ImpersonationToken, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Administrator, bool> IsDeactivated(
            this ListModule<Domain.Administrator>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.IsDeactivated, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Administrator, string> LastName(
            this ListModule<Domain.Administrator>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.LastName, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Administrator, string> Name(
            this ListModule<Domain.Administrator>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Name, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Administrator, string> Password(
            this ListModule<Domain.Administrator>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Password, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Administrator, string> Salt(
            this ListModule<Domain.Administrator>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Salt, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.AuditEvent, DateTime> Date(
            this ListModule<Domain.AuditEvent>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Date, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.AuditEvent, string> Event(
            this ListModule<Domain.AuditEvent>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Event, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.AuditEvent, string> ItemData(
            this ListModule<Domain.AuditEvent>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.ItemData, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.AuditEvent, string> ItemGroup(
            this ListModule<Domain.AuditEvent>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.ItemGroup, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.AuditEvent, string> ItemId(
            this ListModule<Domain.AuditEvent>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.ItemId, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.AuditEvent, string> ItemType(
            this ListModule<Domain.AuditEvent>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.ItemType, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.AuditEvent, string> UserId(
            this ListModule<Domain.AuditEvent>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.UserId, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.AuditEvent, string> UserIp(
            this ListModule<Domain.AuditEvent>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.UserIp, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Contact, string> FirstName(
            this ListModule<Domain.Contact>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.FirstName, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Contact, string> LastName(
            this ListModule<Domain.Contact>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.LastName, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Contact, string> Name(
            this ListModule<Domain.Contact>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Name, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Contact, string> PhoneNumber(
            this ListModule<Domain.Contact>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.PhoneNumber, fl, ln);
        
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
        public static ViewElement<Domain.PasswordResetTicket, bool> IsExpired(
            this ListModule<Domain.PasswordResetTicket>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.IsExpired, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.PasswordResetTicket, bool> IsUsed(
            this ListModule<Domain.PasswordResetTicket>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.IsUsed, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.PasswordResetTicket, Domain.User> User(
            this ListModule<Domain.PasswordResetTicket>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.User, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Settings, int> CacheVersion(
            this ListModule<Domain.Settings>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.CacheVersion, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Settings, string> Name(
            this ListModule<Domain.Settings>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Name, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Settings, int> PasswordResetTicketExpiryMinutes(
            this ListModule<Domain.Settings>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.PasswordResetTicketExpiryMinutes, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.User, string> Email(
            this ListModule<Domain.User>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Email, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.User, string> FirstName(
            this ListModule<Domain.User>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.FirstName, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.User, bool> IsDeactivated(
            this ListModule<Domain.User>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.IsDeactivated, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.User, string> LastName(
            this ListModule<Domain.User>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.LastName, fl, ln);
        
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
        public static StringFormElement Email(
            this FormModule<Domain.Administrator>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Email, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement FirstName(
            this FormModule<Domain.Administrator>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.FirstName, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement ImpersonationToken(
            this FormModule<Domain.Administrator>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.ImpersonationToken, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static BooleanFormElement IsDeactivated(
            this FormModule<Domain.Administrator>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.IsDeactivated, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement LastName(
            this FormModule<Domain.Administrator>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.LastName, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement Password(
            this FormModule<Domain.Administrator>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Password, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement Salt(
            this FormModule<Domain.Administrator>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Salt, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static DateTimeFormElement Date(
            this FormModule<Domain.AuditEvent>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Date, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement Event(
            this FormModule<Domain.AuditEvent>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Event, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement ItemData(
            this FormModule<Domain.AuditEvent>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.ItemData, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement ItemGroup(
            this FormModule<Domain.AuditEvent>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.ItemGroup, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement ItemId(
            this FormModule<Domain.AuditEvent>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.ItemId, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement ItemType(
            this FormModule<Domain.AuditEvent>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.ItemType, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement UserId(
            this FormModule<Domain.AuditEvent>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.UserId, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement UserIp(
            this FormModule<Domain.AuditEvent>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.UserIp, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement FirstName(
            this FormModule<Domain.Contact>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.FirstName, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement LastName(
            this FormModule<Domain.Contact>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.LastName, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement PhoneNumber(
            this FormModule<Domain.Contact>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.PhoneNumber, fl, ln);
        
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
        public static NumberFormElement CacheVersion(
            this FormModule<Domain.Settings>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.CacheVersion, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement Name(
            this FormModule<Domain.Settings>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Name, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static NumberFormElement PasswordResetTicketExpiryMinutes(
            this FormModule<Domain.Settings>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.PasswordResetTicketExpiryMinutes, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement Email(
            this FormModule<Domain.User>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Email, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement FirstName(
            this FormModule<Domain.User>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.FirstName, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static BooleanFormElement IsDeactivated(
            this FormModule<Domain.User>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.IsDeactivated, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement LastName(
            this FormModule<Domain.User>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.LastName, fl, ln);
        
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
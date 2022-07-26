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
        public static PropertyFilterElement<Domain.Administrator> ArchiveLogIds(
            this ListModule<Domain.Administrator>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.ArchiveLogIds, fl, ln);
        
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
        public static PropertyFilterElement<Domain.Administrator> Phone(
            this ListModule<Domain.Administrator>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Phone, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Administrator> Salt(
            this ListModule<Domain.Administrator>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Salt, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.IArchivable> IsDeactivated(
            this ListModule<Domain.IArchivable>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.IsDeactivated, fl, ln);
        
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
        public static PropertyFilterElement<Domain.Discount> Amount(
            this ListModule<Domain.Discount>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Amount, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Discount> ArchiveLogIds(
            this ListModule<Domain.Discount>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.ArchiveLogIds, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Discount> CalculationType(
            this ListModule<Domain.Discount>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.CalculationType, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Discount> Description(
            this ListModule<Domain.Discount>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Description, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Discount> DiscountedFoods(
            this ListModule<Domain.Discount>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.DiscountedFoods, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Discount> DiscountedFoodsLinks(
            this ListModule<Domain.Discount>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.DiscountedFoodsLinks, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Discount> DiscountReceivers(
            this ListModule<Domain.Discount>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.DiscountReceivers, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Discount> DiscountReceiversLinks(
            this ListModule<Domain.Discount>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.DiscountReceiversLinks, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Discount> End(
            this ListModule<Domain.Discount>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.End, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Discount> ExcludedFoods(
            this ListModule<Domain.Discount>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.ExcludedFoods, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Discount> ExcludedFoodsLinks(
            this ListModule<Domain.Discount>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.ExcludedFoodsLinks, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Discount> FoodType(
            this ListModule<Domain.Discount>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.FoodType, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Discount> IsDeactivated(
            this ListModule<Domain.Discount>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.IsDeactivated, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Discount> IsUserSpecific(
            this ListModule<Domain.Discount>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.IsUserSpecific, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Discount> MaximumAmountOfPriceToUse(
            this ListModule<Domain.Discount>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.MaximumAmountOfPriceToUse, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Discount> MinimumAmountOfPriceToUse(
            this ListModule<Domain.Discount>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.MinimumAmountOfPriceToUse, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Discount> Name(
            this ListModule<Domain.Discount>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Name, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Discount> Percent(
            this ListModule<Domain.Discount>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Percent, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Discount> Shop(
            this ListModule<Domain.Discount>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Shop, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Discount> Start(
            this ListModule<Domain.Discount>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Start, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.DiscountCalculationType> DisplayName(
            this ListModule<Domain.DiscountCalculationType>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.DisplayName, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.DiscountCalculationType> Name(
            this ListModule<Domain.DiscountCalculationType>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Name, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.DiscountDiscountReceiversLink> Discount(
            this ListModule<Domain.DiscountDiscountReceiversLink>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Discount, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.DiscountDiscountReceiversLink> Shopcustomer(
            this ListModule<Domain.DiscountDiscountReceiversLink>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Shopcustomer, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.DiscountDiscountedFoodsLink> Discount(
            this ListModule<Domain.DiscountDiscountedFoodsLink>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Discount, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.DiscountDiscountedFoodsLink> Food(
            this ListModule<Domain.DiscountDiscountedFoodsLink>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Food, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.DiscountExcludedFoodsLink> Discount(
            this ListModule<Domain.DiscountExcludedFoodsLink>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Discount, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.DiscountExcludedFoodsLink> Food(
            this ListModule<Domain.DiscountExcludedFoodsLink>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Food, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.DiscountFoodType> DisplayName(
            this ListModule<Domain.DiscountFoodType>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.DisplayName, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.DiscountFoodType> Name(
            this ListModule<Domain.DiscountFoodType>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Name, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Food> ArchiveLogIds(
            this ListModule<Domain.Food>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.ArchiveLogIds, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Food> Description(
            this ListModule<Domain.Food>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Description, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Food> Image(
            this ListModule<Domain.Food>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Image, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Food> IsDeactivated(
            this ListModule<Domain.Food>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.IsDeactivated, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Food> Name(
            this ListModule<Domain.Food>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Name, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Food> Price(
            this ListModule<Domain.Food>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Price, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Food> Shop(
            this ListModule<Domain.Food>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Shop, fl, ln);
        
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
        public static PropertyFilterElement<Domain.Order> Customer(
            this ListModule<Domain.Order>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Customer, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Order> DateReceived(
            this ListModule<Domain.Order>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.DateReceived, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Order> Details(
            this ListModule<Domain.Order>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Details, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Order> FoodItems(
            this ListModule<Domain.Order>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.FoodItems, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Order> Shop(
            this ListModule<Domain.Order>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Shop, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Order> TotalPrice(
            this ListModule<Domain.Order>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.TotalPrice, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Order> TotalPriceWithDiscount(
            this ListModule<Domain.Order>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.TotalPriceWithDiscount, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Order> UsedDiscount(
            this ListModule<Domain.Order>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.UsedDiscount, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.OrderItem> Count(
            this ListModule<Domain.OrderItem>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Count, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.OrderItem> Food(
            this ListModule<Domain.OrderItem>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Food, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.OrderItem> Order(
            this ListModule<Domain.OrderItem>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Order, fl, ln);
        
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
        public static PropertyFilterElement<Domain.Shop> Address(
            this ListModule<Domain.Shop>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Address, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Shop> ArchiveLogIds(
            this ListModule<Domain.Shop>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.ArchiveLogIds, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Shop> Customers(
            this ListModule<Domain.Shop>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Customers, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Shop> Description(
            this ListModule<Domain.Shop>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Description, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Shop> Discounts(
            this ListModule<Domain.Shop>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Discounts, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Shop> Email(
            this ListModule<Domain.Shop>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Email, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Shop> Foods(
            this ListModule<Domain.Shop>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Foods, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Shop> IsDeactivated(
            this ListModule<Domain.Shop>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.IsDeactivated, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Shop> Name(
            this ListModule<Domain.Shop>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Name, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Shop> Orders(
            this ListModule<Domain.Shop>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Orders, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Shop> Phone(
            this ListModule<Domain.Shop>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Phone, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.Shop> Users(
            this ListModule<Domain.Shop>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Users, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.ShopCustomer> ArchiveLogIds(
            this ListModule<Domain.ShopCustomer>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.ArchiveLogIds, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.ShopCustomer> Email(
            this ListModule<Domain.ShopCustomer>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Email, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.ShopCustomer> FirstName(
            this ListModule<Domain.ShopCustomer>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.FirstName, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.ShopCustomer> IsDeactivated(
            this ListModule<Domain.ShopCustomer>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.IsDeactivated, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.ShopCustomer> LastName(
            this ListModule<Domain.ShopCustomer>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.LastName, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.ShopCustomer> Name(
            this ListModule<Domain.ShopCustomer>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Name, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.ShopCustomer> Password(
            this ListModule<Domain.ShopCustomer>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Password, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.ShopCustomer> Phone(
            this ListModule<Domain.ShopCustomer>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Phone, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.ShopCustomer> Salt(
            this ListModule<Domain.ShopCustomer>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Salt, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.ShopCustomer> Shop(
            this ListModule<Domain.ShopCustomer>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Shop, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.ShopUser> ArchiveLogIds(
            this ListModule<Domain.ShopUser>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.ArchiveLogIds, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.ShopUser> Email(
            this ListModule<Domain.ShopUser>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Email, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.ShopUser> FirstName(
            this ListModule<Domain.ShopUser>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.FirstName, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.ShopUser> IsAdmin(
            this ListModule<Domain.ShopUser>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.IsAdmin, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.ShopUser> IsDeactivated(
            this ListModule<Domain.ShopUser>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.IsDeactivated, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.ShopUser> LastName(
            this ListModule<Domain.ShopUser>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.LastName, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.ShopUser> Name(
            this ListModule<Domain.ShopUser>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Name, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.ShopUser> Password(
            this ListModule<Domain.ShopUser>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Password, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.ShopUser> Phone(
            this ListModule<Domain.ShopUser>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Phone, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.ShopUser> Salt(
            this ListModule<Domain.ShopUser>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Salt, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.ShopUser> Shop(
            this ListModule<Domain.ShopUser>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Shop, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.User> ArchiveLogIds(
            this ListModule<Domain.User>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.ArchiveLogIds, fl, ln);
        
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
        public static PropertyFilterElement<Domain.User> Phone(
            this ListModule<Domain.User>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Phone, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static PropertyFilterElement<Domain.User> Salt(
            this ListModule<Domain.User>.SearchComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Search(x => x.Salt, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Administrator> ArchiveLogIds(
            this ViewModule<Domain.Administrator>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.ArchiveLogIds, fl, ln);
        
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
        public static ViewElement<Domain.Administrator> Phone(
            this ViewModule<Domain.Administrator>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Phone, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Administrator> Salt(
            this ViewModule<Domain.Administrator>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Salt, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.IArchivable> IsDeactivated(
            this ViewModule<Domain.IArchivable>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.IsDeactivated, fl, ln);
        
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
        public static ViewElement<Domain.Discount> Amount(
            this ViewModule<Domain.Discount>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Amount, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Discount> ArchiveLogIds(
            this ViewModule<Domain.Discount>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.ArchiveLogIds, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Discount> CalculationType(
            this ViewModule<Domain.Discount>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.CalculationType, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Discount> Description(
            this ViewModule<Domain.Discount>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Description, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Discount> DiscountedFoods(
            this ViewModule<Domain.Discount>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.DiscountedFoods, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Discount> DiscountedFoodsLinks(
            this ViewModule<Domain.Discount>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.DiscountedFoodsLinks, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Discount> DiscountReceivers(
            this ViewModule<Domain.Discount>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.DiscountReceivers, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Discount> DiscountReceiversLinks(
            this ViewModule<Domain.Discount>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.DiscountReceiversLinks, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Discount> End(
            this ViewModule<Domain.Discount>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.End, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Discount> ExcludedFoods(
            this ViewModule<Domain.Discount>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.ExcludedFoods, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Discount> ExcludedFoodsLinks(
            this ViewModule<Domain.Discount>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.ExcludedFoodsLinks, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Discount> FoodType(
            this ViewModule<Domain.Discount>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.FoodType, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Discount> IsDeactivated(
            this ViewModule<Domain.Discount>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.IsDeactivated, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Discount> IsUserSpecific(
            this ViewModule<Domain.Discount>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.IsUserSpecific, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Discount> MaximumAmountOfPriceToUse(
            this ViewModule<Domain.Discount>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.MaximumAmountOfPriceToUse, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Discount> MinimumAmountOfPriceToUse(
            this ViewModule<Domain.Discount>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.MinimumAmountOfPriceToUse, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Discount> Name(
            this ViewModule<Domain.Discount>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Name, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Discount> Percent(
            this ViewModule<Domain.Discount>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Percent, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Discount> Shop(
            this ViewModule<Domain.Discount>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Shop, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Discount> Start(
            this ViewModule<Domain.Discount>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Start, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.DiscountCalculationType> DisplayName(
            this ViewModule<Domain.DiscountCalculationType>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.DisplayName, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.DiscountCalculationType> Name(
            this ViewModule<Domain.DiscountCalculationType>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Name, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.DiscountDiscountReceiversLink> Discount(
            this ViewModule<Domain.DiscountDiscountReceiversLink>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Discount, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.DiscountDiscountReceiversLink> Shopcustomer(
            this ViewModule<Domain.DiscountDiscountReceiversLink>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Shopcustomer, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.DiscountDiscountedFoodsLink> Discount(
            this ViewModule<Domain.DiscountDiscountedFoodsLink>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Discount, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.DiscountDiscountedFoodsLink> Food(
            this ViewModule<Domain.DiscountDiscountedFoodsLink>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Food, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.DiscountExcludedFoodsLink> Discount(
            this ViewModule<Domain.DiscountExcludedFoodsLink>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Discount, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.DiscountExcludedFoodsLink> Food(
            this ViewModule<Domain.DiscountExcludedFoodsLink>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Food, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.DiscountFoodType> DisplayName(
            this ViewModule<Domain.DiscountFoodType>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.DisplayName, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.DiscountFoodType> Name(
            this ViewModule<Domain.DiscountFoodType>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Name, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Food> ArchiveLogIds(
            this ViewModule<Domain.Food>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.ArchiveLogIds, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Food> Description(
            this ViewModule<Domain.Food>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Description, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Food> Image(
            this ViewModule<Domain.Food>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Image, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Food> IsDeactivated(
            this ViewModule<Domain.Food>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.IsDeactivated, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Food> Name(
            this ViewModule<Domain.Food>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Name, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Food> Price(
            this ViewModule<Domain.Food>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Price, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Food> Shop(
            this ViewModule<Domain.Food>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Shop, fl, ln);
        
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
        public static ViewElement<Domain.Order> Customer(
            this ViewModule<Domain.Order>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Customer, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Order> DateReceived(
            this ViewModule<Domain.Order>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.DateReceived, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Order> Details(
            this ViewModule<Domain.Order>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Details, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Order> FoodItems(
            this ViewModule<Domain.Order>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.FoodItems, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Order> Shop(
            this ViewModule<Domain.Order>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Shop, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Order> TotalPrice(
            this ViewModule<Domain.Order>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.TotalPrice, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Order> TotalPriceWithDiscount(
            this ViewModule<Domain.Order>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.TotalPriceWithDiscount, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Order> UsedDiscount(
            this ViewModule<Domain.Order>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.UsedDiscount, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.OrderItem> Count(
            this ViewModule<Domain.OrderItem>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Count, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.OrderItem> Food(
            this ViewModule<Domain.OrderItem>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Food, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.OrderItem> Order(
            this ViewModule<Domain.OrderItem>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Order, fl, ln);
        
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
        public static ViewElement<Domain.Shop> Address(
            this ViewModule<Domain.Shop>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Address, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Shop> ArchiveLogIds(
            this ViewModule<Domain.Shop>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.ArchiveLogIds, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Shop> Customers(
            this ViewModule<Domain.Shop>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Customers, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Shop> Description(
            this ViewModule<Domain.Shop>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Description, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Shop> Discounts(
            this ViewModule<Domain.Shop>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Discounts, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Shop> Email(
            this ViewModule<Domain.Shop>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Email, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Shop> Foods(
            this ViewModule<Domain.Shop>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Foods, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Shop> IsDeactivated(
            this ViewModule<Domain.Shop>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.IsDeactivated, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Shop> Name(
            this ViewModule<Domain.Shop>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Name, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Shop> Orders(
            this ViewModule<Domain.Shop>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Orders, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Shop> Phone(
            this ViewModule<Domain.Shop>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Phone, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.Shop> Users(
            this ViewModule<Domain.Shop>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Users, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.ShopCustomer> ArchiveLogIds(
            this ViewModule<Domain.ShopCustomer>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.ArchiveLogIds, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.ShopCustomer> Email(
            this ViewModule<Domain.ShopCustomer>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Email, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.ShopCustomer> FirstName(
            this ViewModule<Domain.ShopCustomer>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.FirstName, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.ShopCustomer> IsDeactivated(
            this ViewModule<Domain.ShopCustomer>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.IsDeactivated, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.ShopCustomer> LastName(
            this ViewModule<Domain.ShopCustomer>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.LastName, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.ShopCustomer> Name(
            this ViewModule<Domain.ShopCustomer>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Name, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.ShopCustomer> Password(
            this ViewModule<Domain.ShopCustomer>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Password, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.ShopCustomer> Phone(
            this ViewModule<Domain.ShopCustomer>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Phone, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.ShopCustomer> Salt(
            this ViewModule<Domain.ShopCustomer>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Salt, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.ShopCustomer> Shop(
            this ViewModule<Domain.ShopCustomer>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Shop, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.ShopUser> ArchiveLogIds(
            this ViewModule<Domain.ShopUser>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.ArchiveLogIds, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.ShopUser> Email(
            this ViewModule<Domain.ShopUser>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Email, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.ShopUser> FirstName(
            this ViewModule<Domain.ShopUser>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.FirstName, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.ShopUser> IsAdmin(
            this ViewModule<Domain.ShopUser>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.IsAdmin, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.ShopUser> IsDeactivated(
            this ViewModule<Domain.ShopUser>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.IsDeactivated, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.ShopUser> LastName(
            this ViewModule<Domain.ShopUser>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.LastName, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.ShopUser> Name(
            this ViewModule<Domain.ShopUser>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Name, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.ShopUser> Password(
            this ViewModule<Domain.ShopUser>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Password, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.ShopUser> Phone(
            this ViewModule<Domain.ShopUser>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Phone, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.ShopUser> Salt(
            this ViewModule<Domain.ShopUser>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Salt, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.ShopUser> Shop(
            this ViewModule<Domain.ShopUser>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Shop, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.User> ArchiveLogIds(
            this ViewModule<Domain.User>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.ArchiveLogIds, fl, ln);
        
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
        public static ViewElement<Domain.User> Phone(
            this ViewModule<Domain.User>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Phone, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static ViewElement<Domain.User> Salt(
            this ViewModule<Domain.User>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Salt, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Administrator, string> ArchiveLogIds(
            this ListModule<Domain.Administrator>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.ArchiveLogIds, fl, ln);
        
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
        public static ViewElement<Domain.Administrator, string> Phone(
            this ListModule<Domain.Administrator>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Phone, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Administrator, string> Salt(
            this ListModule<Domain.Administrator>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Salt, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.IArchivable, bool> IsDeactivated(
            this ListModule<Domain.IArchivable>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.IsDeactivated, fl, ln);
        
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
        public static ViewElement<Domain.Discount, int?> Amount(
            this ListModule<Domain.Discount>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Amount, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Discount, string> ArchiveLogIds(
            this ListModule<Domain.Discount>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.ArchiveLogIds, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Discount, Domain.DiscountCalculationType> CalculationType(
            this ListModule<Domain.Discount>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.CalculationType, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Discount, string> Description(
            this ListModule<Domain.Discount>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Description, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Discount, Task<IEnumerable<Food>>> DiscountedFoods(
            this ListModule<Domain.Discount>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.DiscountedFoods, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Discount, IDatabaseQuery<Domain.DiscountDiscountedFoodsLink>> DiscountedFoodsLinks(
            this ListModule<Domain.Discount>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.DiscountedFoodsLinks, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Discount, Task<IEnumerable<ShopCustomer>>> DiscountReceivers(
            this ListModule<Domain.Discount>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.DiscountReceivers, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Discount, IDatabaseQuery<Domain.DiscountDiscountReceiversLink>> DiscountReceiversLinks(
            this ListModule<Domain.Discount>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.DiscountReceiversLinks, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Discount, DateTime?> End(
            this ListModule<Domain.Discount>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.End, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Discount, Task<IEnumerable<Food>>> ExcludedFoods(
            this ListModule<Domain.Discount>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.ExcludedFoods, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Discount, IDatabaseQuery<Domain.DiscountExcludedFoodsLink>> ExcludedFoodsLinks(
            this ListModule<Domain.Discount>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.ExcludedFoodsLinks, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Discount, Domain.DiscountFoodType> FoodType(
            this ListModule<Domain.Discount>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.FoodType, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Discount, bool> IsDeactivated(
            this ListModule<Domain.Discount>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.IsDeactivated, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Discount, bool> IsUserSpecific(
            this ListModule<Domain.Discount>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.IsUserSpecific, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Discount, decimal?> MaximumAmountOfPriceToUse(
            this ListModule<Domain.Discount>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.MaximumAmountOfPriceToUse, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Discount, decimal?> MinimumAmountOfPriceToUse(
            this ListModule<Domain.Discount>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.MinimumAmountOfPriceToUse, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Discount, string> Name(
            this ListModule<Domain.Discount>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Name, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Discount, decimal?> Percent(
            this ListModule<Domain.Discount>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Percent, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Discount, Domain.Shop> Shop(
            this ListModule<Domain.Discount>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Shop, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Discount, DateTime?> Start(
            this ListModule<Domain.Discount>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Start, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.DiscountCalculationType, string> DisplayName(
            this ListModule<Domain.DiscountCalculationType>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.DisplayName, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.DiscountCalculationType, string> Name(
            this ListModule<Domain.DiscountCalculationType>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Name, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.DiscountDiscountReceiversLink, Domain.Discount> Discount(
            this ListModule<Domain.DiscountDiscountReceiversLink>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Discount, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.DiscountDiscountReceiversLink, Domain.ShopCustomer> Shopcustomer(
            this ListModule<Domain.DiscountDiscountReceiversLink>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Shopcustomer, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.DiscountDiscountedFoodsLink, Domain.Discount> Discount(
            this ListModule<Domain.DiscountDiscountedFoodsLink>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Discount, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.DiscountDiscountedFoodsLink, Domain.Food> Food(
            this ListModule<Domain.DiscountDiscountedFoodsLink>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Food, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.DiscountExcludedFoodsLink, Domain.Discount> Discount(
            this ListModule<Domain.DiscountExcludedFoodsLink>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Discount, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.DiscountExcludedFoodsLink, Domain.Food> Food(
            this ListModule<Domain.DiscountExcludedFoodsLink>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Food, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.DiscountFoodType, string> DisplayName(
            this ListModule<Domain.DiscountFoodType>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.DisplayName, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.DiscountFoodType, string> Name(
            this ListModule<Domain.DiscountFoodType>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Name, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Food, string> ArchiveLogIds(
            this ListModule<Domain.Food>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.ArchiveLogIds, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Food, string> Description(
            this ListModule<Domain.Food>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Description, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Food, Blob> Image(
            this ListModule<Domain.Food>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Image, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Food, bool> IsDeactivated(
            this ListModule<Domain.Food>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.IsDeactivated, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Food, string> Name(
            this ListModule<Domain.Food>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Name, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Food, int?> Price(
            this ListModule<Domain.Food>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Price, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Food, Domain.Shop> Shop(
            this ListModule<Domain.Food>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Shop, fl, ln);
        
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
        public static ViewElement<Domain.Order, Domain.ShopCustomer> Customer(
            this ListModule<Domain.Order>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Customer, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Order, DateTime> DateReceived(
            this ListModule<Domain.Order>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.DateReceived, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Order, string> Details(
            this ListModule<Domain.Order>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Details, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Order, IDatabaseQuery<Domain.OrderItem>> FoodItems(
            this ListModule<Domain.Order>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.FoodItems, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Order, Domain.Shop> Shop(
            this ListModule<Domain.Order>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Shop, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Order, int?> TotalPrice(
            this ListModule<Domain.Order>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.TotalPrice, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Order, int?> TotalPriceWithDiscount(
            this ListModule<Domain.Order>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.TotalPriceWithDiscount, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Order, Domain.Discount> UsedDiscount(
            this ListModule<Domain.Order>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.UsedDiscount, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.OrderItem, int> Count(
            this ListModule<Domain.OrderItem>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Count, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.OrderItem, Domain.Food> Food(
            this ListModule<Domain.OrderItem>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Food, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.OrderItem, Domain.Order> Order(
            this ListModule<Domain.OrderItem>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Order, fl, ln);
        
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
        public static ViewElement<Domain.Shop, string> Address(
            this ListModule<Domain.Shop>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Address, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Shop, string> ArchiveLogIds(
            this ListModule<Domain.Shop>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.ArchiveLogIds, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Shop, IDatabaseQuery<Domain.ShopCustomer>> Customers(
            this ListModule<Domain.Shop>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Customers, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Shop, string> Description(
            this ListModule<Domain.Shop>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Description, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Shop, IDatabaseQuery<Domain.Discount>> Discounts(
            this ListModule<Domain.Shop>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Discounts, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Shop, string> Email(
            this ListModule<Domain.Shop>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Email, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Shop, IDatabaseQuery<Domain.Food>> Foods(
            this ListModule<Domain.Shop>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Foods, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Shop, bool> IsDeactivated(
            this ListModule<Domain.Shop>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.IsDeactivated, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Shop, string> Name(
            this ListModule<Domain.Shop>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Name, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Shop, IDatabaseQuery<Domain.Order>> Orders(
            this ListModule<Domain.Shop>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Orders, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Shop, string> Phone(
            this ListModule<Domain.Shop>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Phone, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.Shop, IDatabaseQuery<Domain.ShopUser>> Users(
            this ListModule<Domain.Shop>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Users, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.ShopCustomer, string> ArchiveLogIds(
            this ListModule<Domain.ShopCustomer>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.ArchiveLogIds, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.ShopCustomer, string> Email(
            this ListModule<Domain.ShopCustomer>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Email, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.ShopCustomer, string> FirstName(
            this ListModule<Domain.ShopCustomer>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.FirstName, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.ShopCustomer, bool> IsDeactivated(
            this ListModule<Domain.ShopCustomer>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.IsDeactivated, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.ShopCustomer, string> LastName(
            this ListModule<Domain.ShopCustomer>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.LastName, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.ShopCustomer, string> Name(
            this ListModule<Domain.ShopCustomer>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Name, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.ShopCustomer, string> Password(
            this ListModule<Domain.ShopCustomer>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Password, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.ShopCustomer, string> Phone(
            this ListModule<Domain.ShopCustomer>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Phone, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.ShopCustomer, string> Salt(
            this ListModule<Domain.ShopCustomer>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Salt, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.ShopCustomer, Domain.Shop> Shop(
            this ListModule<Domain.ShopCustomer>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Shop, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.ShopUser, string> ArchiveLogIds(
            this ListModule<Domain.ShopUser>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.ArchiveLogIds, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.ShopUser, string> Email(
            this ListModule<Domain.ShopUser>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Email, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.ShopUser, string> FirstName(
            this ListModule<Domain.ShopUser>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.FirstName, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.ShopUser, bool> IsAdmin(
            this ListModule<Domain.ShopUser>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.IsAdmin, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.ShopUser, bool> IsDeactivated(
            this ListModule<Domain.ShopUser>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.IsDeactivated, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.ShopUser, string> LastName(
            this ListModule<Domain.ShopUser>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.LastName, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.ShopUser, string> Name(
            this ListModule<Domain.ShopUser>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Name, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.ShopUser, string> Password(
            this ListModule<Domain.ShopUser>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Password, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.ShopUser, string> Phone(
            this ListModule<Domain.ShopUser>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Phone, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.ShopUser, string> Salt(
            this ListModule<Domain.ShopUser>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Salt, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.ShopUser, Domain.Shop> Shop(
            this ListModule<Domain.ShopUser>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Shop, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.User, string> ArchiveLogIds(
            this ListModule<Domain.User>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.ArchiveLogIds, fl, ln);
        
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
        public static ViewElement<Domain.User, string> Phone(
            this ListModule<Domain.User>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Phone, fl, ln);
        
        [MethodColor("#0CCC68")]
        public static ViewElement<Domain.User, string> Salt(
            this ListModule<Domain.User>.ColumnComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Column(x => x.Salt, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement ArchiveLogIds(
            this FormModule<Domain.Administrator>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.ArchiveLogIds, fl, ln);
        
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
        public static StringFormElement Phone(
            this FormModule<Domain.Administrator>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Phone, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement Salt(
            this FormModule<Domain.Administrator>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Salt, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static BooleanFormElement IsDeactivated(
            this FormModule<Domain.IArchivable>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.IsDeactivated, fl, ln);
        
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
        public static NumberFormElement Amount(
            this FormModule<Domain.Discount>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Amount, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement ArchiveLogIds(
            this FormModule<Domain.Discount>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.ArchiveLogIds, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static AssociationFormElement CalculationType(
            this FormModule<Domain.Discount>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.CalculationType, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement Description(
            this FormModule<Domain.Discount>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Description, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static AssociationFormElement DiscountedFoodsLinks(
            this FormModule<Domain.Discount>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.DiscountedFoodsLinks, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static AssociationFormElement DiscountReceiversLinks(
            this FormModule<Domain.Discount>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.DiscountReceiversLinks, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static DateTimeFormElement End(
            this FormModule<Domain.Discount>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.End, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static AssociationFormElement ExcludedFoodsLinks(
            this FormModule<Domain.Discount>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.ExcludedFoodsLinks, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static AssociationFormElement FoodType(
            this FormModule<Domain.Discount>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.FoodType, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static BooleanFormElement IsDeactivated(
            this FormModule<Domain.Discount>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.IsDeactivated, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static BooleanFormElement IsUserSpecific(
            this FormModule<Domain.Discount>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.IsUserSpecific, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static NumberFormElement MaximumAmountOfPriceToUse(
            this FormModule<Domain.Discount>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.MaximumAmountOfPriceToUse, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static NumberFormElement MinimumAmountOfPriceToUse(
            this FormModule<Domain.Discount>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.MinimumAmountOfPriceToUse, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement Name(
            this FormModule<Domain.Discount>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Name, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static NumberFormElement Percent(
            this FormModule<Domain.Discount>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Percent, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static AssociationFormElement Shop(
            this FormModule<Domain.Discount>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Shop, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static DateTimeFormElement Start(
            this FormModule<Domain.Discount>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Start, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement DisplayName(
            this FormModule<Domain.DiscountCalculationType>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.DisplayName, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement Name(
            this FormModule<Domain.DiscountCalculationType>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Name, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static AssociationFormElement Discount(
            this FormModule<Domain.DiscountDiscountReceiversLink>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Discount, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static AssociationFormElement Shopcustomer(
            this FormModule<Domain.DiscountDiscountReceiversLink>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Shopcustomer, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static AssociationFormElement Discount(
            this FormModule<Domain.DiscountDiscountedFoodsLink>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Discount, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static AssociationFormElement Food(
            this FormModule<Domain.DiscountDiscountedFoodsLink>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Food, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static AssociationFormElement Discount(
            this FormModule<Domain.DiscountExcludedFoodsLink>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Discount, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static AssociationFormElement Food(
            this FormModule<Domain.DiscountExcludedFoodsLink>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Food, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement DisplayName(
            this FormModule<Domain.DiscountFoodType>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.DisplayName, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement Name(
            this FormModule<Domain.DiscountFoodType>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Name, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement ArchiveLogIds(
            this FormModule<Domain.Food>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.ArchiveLogIds, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement Description(
            this FormModule<Domain.Food>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Description, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static BinaryFormElement Image(
            this FormModule<Domain.Food>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Image, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static BooleanFormElement IsDeactivated(
            this FormModule<Domain.Food>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.IsDeactivated, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement Name(
            this FormModule<Domain.Food>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Name, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static NumberFormElement Price(
            this FormModule<Domain.Food>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Price, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static AssociationFormElement Shop(
            this FormModule<Domain.Food>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Shop, fl, ln);
        
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
        public static AssociationFormElement Customer(
            this FormModule<Domain.Order>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Customer, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static DateTimeFormElement DateReceived(
            this FormModule<Domain.Order>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.DateReceived, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement Details(
            this FormModule<Domain.Order>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Details, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static AssociationFormElement FoodItems(
            this FormModule<Domain.Order>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.FoodItems, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static AssociationFormElement Shop(
            this FormModule<Domain.Order>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Shop, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static NumberFormElement TotalPriceWithDiscount(
            this FormModule<Domain.Order>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.TotalPriceWithDiscount, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static AssociationFormElement UsedDiscount(
            this FormModule<Domain.Order>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.UsedDiscount, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static NumberFormElement Count(
            this FormModule<Domain.OrderItem>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Count, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static AssociationFormElement Food(
            this FormModule<Domain.OrderItem>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Food, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static AssociationFormElement Order(
            this FormModule<Domain.OrderItem>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Order, fl, ln);
        
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
        public static StringFormElement Address(
            this FormModule<Domain.Shop>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Address, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement ArchiveLogIds(
            this FormModule<Domain.Shop>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.ArchiveLogIds, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static AssociationFormElement Customers(
            this FormModule<Domain.Shop>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Customers, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement Description(
            this FormModule<Domain.Shop>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Description, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static AssociationFormElement Discounts(
            this FormModule<Domain.Shop>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Discounts, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement Email(
            this FormModule<Domain.Shop>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Email, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static AssociationFormElement Foods(
            this FormModule<Domain.Shop>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Foods, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static BooleanFormElement IsDeactivated(
            this FormModule<Domain.Shop>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.IsDeactivated, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement Name(
            this FormModule<Domain.Shop>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Name, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static AssociationFormElement Orders(
            this FormModule<Domain.Shop>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Orders, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement Phone(
            this FormModule<Domain.Shop>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Phone, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static AssociationFormElement Users(
            this FormModule<Domain.Shop>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Users, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement ArchiveLogIds(
            this FormModule<Domain.ShopCustomer>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.ArchiveLogIds, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement Email(
            this FormModule<Domain.ShopCustomer>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Email, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement FirstName(
            this FormModule<Domain.ShopCustomer>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.FirstName, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static BooleanFormElement IsDeactivated(
            this FormModule<Domain.ShopCustomer>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.IsDeactivated, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement LastName(
            this FormModule<Domain.ShopCustomer>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.LastName, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement Password(
            this FormModule<Domain.ShopCustomer>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Password, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement Phone(
            this FormModule<Domain.ShopCustomer>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Phone, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement Salt(
            this FormModule<Domain.ShopCustomer>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Salt, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static AssociationFormElement Shop(
            this FormModule<Domain.ShopCustomer>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Shop, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement ArchiveLogIds(
            this FormModule<Domain.ShopUser>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.ArchiveLogIds, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement Email(
            this FormModule<Domain.ShopUser>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Email, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement FirstName(
            this FormModule<Domain.ShopUser>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.FirstName, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static BooleanFormElement IsAdmin(
            this FormModule<Domain.ShopUser>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.IsAdmin, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static BooleanFormElement IsDeactivated(
            this FormModule<Domain.ShopUser>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.IsDeactivated, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement LastName(
            this FormModule<Domain.ShopUser>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.LastName, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement Password(
            this FormModule<Domain.ShopUser>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Password, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement Phone(
            this FormModule<Domain.ShopUser>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Phone, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement Salt(
            this FormModule<Domain.ShopUser>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Salt, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static AssociationFormElement Shop(
            this FormModule<Domain.ShopUser>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Shop, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement ArchiveLogIds(
            this FormModule<Domain.User>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.ArchiveLogIds, fl, ln);
        
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
        public static StringFormElement Phone(
            this FormModule<Domain.User>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Phone, fl, ln);
        
        [MethodColor("#AFCD14")]
        public static StringFormElement Salt(
            this FormModule<Domain.User>.FieldComponents @this, [_F] string fl = null, [_L] int ln = 0)
        => @this.container.Field(x => x.Salt, fl, ln);
    }
}
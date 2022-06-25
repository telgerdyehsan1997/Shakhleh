using Olive;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public static class AppSetting
    {
        public static string ChannelPort => Config.Get<string>("Integration:ChannelPort:Root");
        public static string ChannelPortNotifyShipmentUrl => ChannelPort + Config.Get<string>("Integration:ChannelPort:Shipment");
        public static string BadgeCode => Config.Get<string>("Badge:Code");
        public static string BadgePort => Config.Get<string>("Badge:PortCode");
        public static string ExchangeUrl => Config.Get<string>("Integration:Gov:ExchangeUrl").Replace("{Year}", LocalTime.Now.Year.ToString());
        public static string ExchangeRateUrl => Config.Get<string>("Integration:Gov:ExchangeRateUrl")
            .Replace("{Month}", LocalTime.Now.ToString("MM"))
            .Replace("{Year}", LocalTime.Now.ToString("yy"));

        public static string StatementOfOriginUrl => Config.Get<string>("Integration:Gov:Origin");
        public static string GovBaseUrl => Config.Get<string>("Integration:Gov:Base");
        public static bool MFAEnabled => Config.Get<bool>("MFA.Enable");
        public static string HMRCAPIUrl => Config.Get<string>("Integration:HMRC:URL");
        public static string HMRCAPIKey => Config.Get<string>("Integration:HMRC:Key");
        public static string CustomsProEmail => Config.Get<string>("CustomsProEmail");
        public static string EmailAPIUserName => Config.Get<string>("Email:Api:Username");
        public static string EmailAPIKey => Config.Get<string>("Email:Api:Key");
        public static string EmailAPIID => Config.Get<string>("Email:Api:EmailAddressID");
        public static bool EnableEmailAPI => Config.Get<bool>("Email:Api:Enable");

        public static string ICSSystemBaseUrl = Config.Get<string>("Integration:ICS:URL");
        public static string ICSSystemTokenBaseUrl = Config.Get<string>("Integration:ICS:TOKEN_URL");
        public static string ICSSystemClientId = Config.Get<string>("Integration:ICS:ClientId");
        public static string ICSSystemClientSecret = Config.Get<string>("Integration:ICS:ClientSecret");
        public static string ICSSystemReturnUrl = Config.Get<string>("Integration:ICS:ReturnUrl");
        public static bool ICSSystemTestMode = Config.Get<bool>("Integration:ICS:TestMode");


        public static string GVMSPushUrl = Config.Get<string>("Integration:GVMS:PushURL");


        public static string CFSPChannelportsAuthorisationNumber = Config.Get<string>("CFSP:AuthorisationNumber");

    }
}

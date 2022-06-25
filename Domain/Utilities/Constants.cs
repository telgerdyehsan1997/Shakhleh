using Olive;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public static class Constants
    {

        public const string NCTS_Error = "The 8 digit NCTS code must have first 2 characters alpha only, and the last 6 characters alphanumeric.";

        public const int EADShipmentCommodityMax = 99;

        public const int NCTSShipmentCommodityMax = 99;

        public static readonly string GBDoubleO = "GB000060";

        public static readonly Guid ChannelPortsID = "AEEB1ED8-C2A7-450D-93BA-2130E7727E80".To<Guid>();

        public static readonly string ChannelPortEORI = "GB683470514000";

        public static readonly string GBCountryCode = "GB";

        public static readonly int NameAndAddressLenght = 35;

        public static readonly int TradeLenght = 17;

        public static readonly string MFAKey = "MFA";
    }
}

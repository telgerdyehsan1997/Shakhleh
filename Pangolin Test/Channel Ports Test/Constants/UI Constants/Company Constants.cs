using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChannelPorts.Pangolin.UI_Constants
{
    public static class CompanyTypeConstants
    {
        public const string Customer = "Customer";
        public const string Flex = "Flex";
        public const string Forwarder = "Fowarder";
        public const string Other = "Other";
    }

    public static class TransactionTypeConstants
    {
        public const string IntoUK = "Into uk";
        public const string OutOfUK = "Out of UK";
    }

    public static class NCTS
    {
        public const string Always = "Always";
        public const string Sometimes = "Sometimes";
        public const string None = "None";
    }

    public static class GVMSConstants
    {
        public const string Always = "Always";
        public const string Sometimes = "Sometimes";
        public const string None = "Not GVMS";
    }

    public static class SecurityInboundConstants
    {
        public const string Always = "Always";
        public const string Sometimes = "Sometimes";
        public const string None = "No Safety And Security";
    }

    public static class SecurityOutboundConstants
    {
        public const string Always = "Always";
        public const string Sometimes = "Sometimes";
        public const string None = "No Safety And Security";
    }

    public static class CFSPConstants
    {
        public const string ChannelPorts = "Channelports";
        public const string None = "None";
        public const string Own = "Own";
    }

    public static class RepresentationTypeConstants
    {
        public const string Direct = "Direct";
        public const string Indirect = "Indirect";
    }

    public static class GuarantorType
    {
        public const string Own = "Own";
        public const string DifferentCompany = "Different Company's Guarantee";
        public const string None = "None";
    }
}
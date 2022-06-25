using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Domain
{
    public enum LogType
    {
        [FileExtension(".json")]
        ChannelPort2Request = 1,
        [FileExtension(".json")]
        ChannelPort2Response = 2,
        [FileExtension(".json")]
        ChannelPortRecievedRequest = 3,
        [Description("ChannelPort Recieved Response")]
        [FileExtension(".json")]
        ChannelPortRecievedResponse = 4,
        [FileExtension(".xml")]
        CreateASMDeclarationRequest = 5,
        [FileExtension(".xml")]
        CreateASMDeclarationResponse = 6,
        [FileExtension(".xml")]
        SendASMDeclarationRequest = 7,
        [FileExtension(".xml")]
        SendASMDeclarationResponse = 8,
        Request = 9,
        Response = 10,
        [FileExtension(".txt")]
        Error = 11,
        EadShipmentAPIRequest = 12,
        EadShipmentAPIResponse = 13,
        NCTSShipmentAPIRequest = 14,
        NCTSShipmentAPIResponse = 15,

        [FileExtension(".xml")]
        AmendASMDeclarationRequest = 16,

        [FileExtension(".xml")]
        AmendASMDeclarationResponse = 17,


        [FileExtension(".xml")]
        GetCustomsReportDeclarationRequest = 18,

        [FileExtension(".xml")]
        GetCustomsReportDeclarationResponse = 19,

        [FileExtension(".json")]
        CreateJamesDeclarationRequest = 20,

        [FileExtension(".json")]
        CreateJamesDeclarationResponse = 21,


        [FileExtension(".xml")]
        GetICSDeclarationResponse = 22,

        [FileExtension(".xml")]
        GetICSDeclarationRequest = 23,

        [FileExtension(".xml")]
        GetICSDeclarationOutcomesResponse = 24,

        [FileExtension(".xml")]
        GetICSDeclarationOutcomesRequest = 25,

        [FileExtension(".xml")]
        GetGVMSCustomResponse = 26,

        [FileExtension(".xml")]
        GetGVMSCustomRequest = 27,

        [FileExtension(".xml")]
        GetGVMSNotificationResponse = 28,

        [FileExtension(".xml")]
        CancelASMDeclarationRequest = 29,

        [FileExtension(".xml")]
        CancelASMDeclarationResponse = 30,

    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace APIHandler
{
    public class GVMSNotifications
    {
        [JsonProperty("notificationId")]
        public string NotificationId { get; set; }

        [JsonProperty("boxId")]
        public string BoxId { get; set; }

    }
}

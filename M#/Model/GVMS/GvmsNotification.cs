using MSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class GvmsNotification : EntityType
    {
        public GvmsNotification()
        {
            Associate<Shipment>("Shipment");
            String("NotificationId");
            String("BoxId");
            BigString("Message");

        }
    }
}

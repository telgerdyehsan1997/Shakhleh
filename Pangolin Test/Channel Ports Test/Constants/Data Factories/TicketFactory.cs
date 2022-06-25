using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Channel_Ports_Test.Constants
{
    public class TicketFactory
    {
        private Ticket _TicketForGoroMajima { get; set; }
        public Ticket AddTicketForMajimaShipment()
        {
            _TicketForGoroMajima = _TicketForGoroMajima ?? new Ticket
            {
                FullName = "Goro Majima",
                Email = "goro.majima@uat.co",
                Phone = "07804659222",
                TrackingNumber = "T0721000001",
                Details = "Raised Ticket",
                CarbonCopy = "TEST1@UAT.CO"
            };
            return _TicketForGoroMajima;
        }
    }
}

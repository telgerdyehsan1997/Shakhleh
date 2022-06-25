using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Channel_Ports_Test.Constants
{
    public class NCTSConsignment
    {
        public string EADMRN { get; set; }
        public string ImportEADCommodities { get; set; }
        public string UKTrader { get; set; }
        public string PartnerName { get; set; }
        public string CountryOfDestination { get; set; }
        public string TotalPackages { get; set; }
        public string TotalGrossWeight { get; set; }
        public string TotalNetWeight { get; set; }
        public string InvoiceCurrency { get; set; }
        public string TotalValue { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Channel_Ports_Test.Constants
{
    public class Consignment
    {
        public string TrackingNumber { get; set; }
        public string FullDeclarationDetails { get; set; }
        public string UKTrader { get; set; }
        public string PartnerName { get; set; }
        public string Declarant { get; set; }
        public string UsingEIDR { get; set; }
        public string SequenceNumber { get; set; }
        public string CFSPShipmentNumber { get; set; }
        public string TotalPackages { get; set; }
        public string TotalGrossWeight { get; set; }
        public string TotalNetWeight { get; set; }
        public string InvoiceNumber { get; set; }
        public string InvoiceCurrency { get; set; }
        public string TotalValue { get; set; }
        public string TermsOfSale { get; set; }
        public string FreightCurrency { get; set; }
        public string FreightAmount { get; set; }
        public string IsImporterPayingInsurance { get; set; }
        public string IsOnlyOneCommodity { get; set; }
        public string ConsignmentNumber { get; set; }
    }
}

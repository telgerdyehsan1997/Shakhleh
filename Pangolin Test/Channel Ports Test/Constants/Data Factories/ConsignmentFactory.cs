using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Channel_Ports_Test.Constants
{
    public class ConsignmentFactory
    {
        private Consignment _MajimaConsignmentOutOfUK { get; set; }
        public Consignment AddMajimaConstructionConsignmentOutOfUK()
        {
            _MajimaConsignmentOutOfUK = _MajimaConsignmentOutOfUK ?? new Consignment()
            {
                ConsignmentNumber = "T072100000101",
                UKTrader = "Channel Ports",
                PartnerName = "MAJIMA CONSTRUCTION - SOTENBORI - OSA OB1 - JAPAN",
                Declarant = "Channel Ports - Hythe - CT21 4BL - GB683470514001 - 1234657",
                TotalPackages = "1",
                TotalGrossWeight = "1",
                TotalNetWeight = "1",
                InvoiceNumber = "INV-01",
                InvoiceCurrency = "GBP",
                TotalValue = "1",
                TermsOfSale = "FAS - Free Alongside Ship",
            };
            return _MajimaConsignmentOutOfUK;
        }

        private Consignment _CFSPConsignment { get; set; }
        public Consignment AddCFSPConsignment()
        {
            _CFSPConsignment = _CFSPConsignment ?? new Consignment()
            {
                ConsignmentNumber = "",
                UKTrader = "Channel Ports - Hythe - EORI GB683470514001 - Deferment  1234657",
                FullDeclarationDetails = "Yes",
                PartnerName = "Imports Ltd - Rome - AG2 YGD - Spain - IL859098859098",
                Declarant = "CFSP OWN TEST - LONDON - LND OA1 - GB683470514001",
                UsingEIDR = "Yes",
                CFSPShipmentNumber = "CP072100001",
                TotalPackages = "1",
                TotalGrossWeight = "1",
                TotalNetWeight = "1",
                InvoiceNumber = "CFSP-01",
                InvoiceCurrency = "GBP",
                TotalValue = "1",
                TermsOfSale = "FAS - Free Alongside Ship",
            };
            return _CFSPConsignment;
        }

        private Consignment _ChannelPortsCFSPConsignment { get; set; }
        public Consignment AddChannelPortsCFSPConsignment()
        {
            _ChannelPortsCFSPConsignment = _ChannelPortsCFSPConsignment ?? new Consignment()
            {
                PartnerName = "Imports Ltd - Rome - AG2 YGD - Spain - IL859098859098",
                TotalPackages = "1",
                TotalGrossWeight = "1",
                TotalNetWeight = "1",
                InvoiceNumber = "ChannelPorts-CFSP-01",
                InvoiceCurrency = "GBP",
                TotalValue = "1",
                TermsOfSale = "FAS - Free Alongside Ship"
            };
            return _ChannelPortsCFSPConsignment;
        }

        private Consignment _SFDCFSPConsignment { get; set; }
        public Consignment AddSFDCFSPConsignment()
        {
            _SFDCFSPConsignment = _SFDCFSPConsignment ?? new Consignment()
            {
                PartnerName = "Imports Ltd - Rome - AG2 YGD - Spain - IL859098859098",
                TotalPackages = "1",
                TotalGrossWeight = "1",
                TotalNetWeight = "1",
                InvoiceNumber = "ChannelPorts-CFSP-01",
                InvoiceCurrency = "GBP",
                TotalValue = "1",
                TermsOfSale = "FAS - Free Alongside Ship",
                SequenceNumber = "CP-10001"
            };
            return _SFDCFSPConsignment;
        }

        private Consignment _SAndSConsignmentIntoUK { get; set; }
        public Consignment AddSandSConsignmentIntoUK()
        {
            _SAndSConsignmentIntoUK = _SAndSConsignmentIntoUK ?? new Consignment()
            {
                ConsignmentNumber = "R072100000101",
                UKTrader = "Channel Ports",
                PartnerName = "Imports Ltd - Rome - AG2 YGD - Spain - IL859098859098",
                Declarant = "Channel Ports - Hythe - CT21 4BL - GB683470514001 - 1234657",
                TotalPackages = "1",
                TotalGrossWeight = "1",
                TotalNetWeight = "1",
                InvoiceNumber = "SAndS-01",
                InvoiceCurrency = "GBP",
                TotalValue = "1",
                TermsOfSale = "FAS - Free Alongside Ship",
            };
            return _SAndSConsignmentIntoUK;
        }

        private Consignment _MajimaConsignmentIntoUK { get; set; }
        public Consignment AddMajimaConstructionConsignmentIntoUK()
        {
            _MajimaConsignmentIntoUK = _MajimaConsignmentIntoUK ?? new Consignment()
            {
                ConsignmentNumber = "R072100000101",
                UKTrader = "Channel Ports",
                PartnerName = "Imports Ltd - Rome - AG2 YGD - Spain - IL859098859098",
                Declarant = "Channel Ports - Hythe - CT21 4BL - GB683470514001 - 1234657",
                TotalPackages = "1",
                TotalGrossWeight = "1",
                TotalNetWeight = "1",
                InvoiceNumber = "Into-UK01",
                InvoiceCurrency = "GBP",
                TotalValue = "1",
                TermsOfSale = "FAS - Free Alongside Ship",
            };
            return _MajimaConsignmentIntoUK;
        }

        private Consignment _UKCompanyConsignmentIntoUK { get; set; }
        public Consignment AddUKCompanyConsignmentIntoUK()
        {
            _UKCompanyConsignmentIntoUK = _UKCompanyConsignmentIntoUK ?? new Consignment()
            {

                ConsignmentNumber = "R072100000101",
                UKTrader = "UK COMPANY - TWICKENHAM - EORI GB683470514002 - Deferment by deposit",
                PartnerName = "Imports Ltd - Rome - AG2 YGD - Spain - IL859098859098",
                Declarant = "Channel Ports - Hythe - CT21 4BL - GB683470514001 - 1234657",
                TotalPackages = "1",
                TotalGrossWeight = "1",
                TotalNetWeight = "1",
                InvoiceNumber = "Into-UK01",
                InvoiceCurrency = "GBP",
                TotalValue = "1",
                TermsOfSale = "FAS - Free Alongside Ship",
            };
            return _UKCompanyConsignmentIntoUK;
        }

        private Consignment _RowConsignment { get; set; }
        public Consignment AddRowConsignment()
        {
            {
                _RowConsignment = _RowConsignment ?? new Consignment()
                {
                    ConsignmentNumber = "R072100000101",
                    UKTrader = "Channel Ports",
                    PartnerName = "Imports Ltd - Rome - AG2 YGD - Spain - IL859098859098",
                    Declarant = "Channel Ports - Hythe - CT21 4BL - GB683470514001 - 1234657",
                    TotalPackages = "1",
                    TotalGrossWeight = "1",
                    TotalNetWeight = "1",
                    InvoiceNumber = "RoW-01",
                    InvoiceCurrency = "GBP",
                    TotalValue = "1",
                    TermsOfSale = "FAS - Free Alongside Ship",
                };
                return _RowConsignment;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace APIHandler
{
    [System.Serializable]

    [XmlRoot(ElementName = "exportDeclaration", Namespace = Constant.ASM_XMLNamespace_Root + "/DeclarationGbExport")]
    public class AsmExportGbDeclaration : IXmlContract
    {
        [XmlElement("declarantBadge", Order = 1)]
        public DeclarantBadge DeclarantBadge { get; set; }

        [XmlElement("declarationCurrency", Order = 2)]
        public DeclarationCurrency DeclarationCurrency { get; set; }

        [XmlElement("declarationType", Order = 3)]
        public DeclarationType DeclarationType { get; set; }

        [XmlElement("subDivision", Order = 4)]
        public string SubDivision { get; set; }

        [XmlElement("consignorParty", Order = 5)]
        public AsmAddress ConsignorParty { get; set; }

        [XmlElement("consigneeParty", Order = 6)]
        public AsmAddress ConsigneeParty { get; set; }

        [XmlElement("declarantParty", Order = 7)]
        public AsmAddress DeclarantParty { get; set; }

        [XmlElement("warehousePremises", Order = 8)]
        public string WarehousePremises { get; set; }

        [XmlElement("totalPackages", Order = 9)]
        public int TotalPackages { get; set; }

        [XmlElement("declarationStatus", Order = 10)]
        public string DeclarationStatus { get; set; }

        [XmlElement("traderReference", Order = 11)]
        public string TraderReference { get; set; }

        [XmlElement("representation", Order = 12)]
        public int Representation { get; set; }

        [XmlElement("declarationUcr", Order = 13)]
        public string DeclarationUcr { get; set; }

        [XmlElement("dispatchCountry", Order = 14)]
        public ASMCountry DispatchCountry { get; set; }

        [XmlElement("transportCountry", Order = 15)]
        public ASMCountry TransportCountry { get; set; }

        //[XmlElement("transportCountryFecIndicator", Order = 16)]
        //public bool TransportCountryFecIndicator { get; set; }

        [XmlElement("transportIdentity", Order = 17)]
        public string TransportIdentity { get; set; }

        [XmlElement("invoiceCurrency", Order = 18)]
        public Currency InvoiceCurrency { get; set; }

        [XmlElement("borderTransportMode", Order = 19)]
        public EADBorderTransportMode BorderTransportMode { get; set; }

        [XmlElement("goodsLocationCountry", Order = 20)]
        public ASMCountry GoodsLocationCountry { get; set; }

        [XmlElement("goodsLocationPort", Order = 21)]
        public Location GoodsLocationPort { get; set; }

        [XmlElement("destinationCountry", Order = 22)]
        public ASMCountry DestinationCountry { get; set; }

        //[XmlElement("destinationCountryFecIndicator", Order = 23)]
        //public bool DestinationCountryFecIndicator { get; set; }

        [XmlElement("transportChargesPaymentMethod", Order = 24)]
        public string TransportChargesPaymentMethod { get; set; }

        [XmlArray("routingCountries", Order = 25)]
        [XmlArrayItem("routingCountry")]
        public List<ASMCountry> RoutingCountries { get; set; } = new List<ASMCountry>();

        //[XmlArray("seals")]
        //[XmlArrayItem("seal")]
        //public List<EADSeal> Seals = new List<EADSeal>();

        [XmlArray("aiStatements", Order = 26)]
        [XmlArrayItem("aiStatement")]
        public List<AIStatement> AiStatements { get; set; } = new List<AIStatement>();

        [XmlArray("producedDocuments", Order = 27)]
        [XmlArrayItem("producedDocument")]
        public List<ProducedDocument> ProducedDocuments { get; set; } = new List<ProducedDocument>();

        [XmlElement("reasonForAmendment", Order = 28)]
        public string ReasonForAmendment { get; set; }

        [XmlArray("items", Order = 29)]
        [XmlArrayItem("item")]
        public List<EadOutItem> Items { get; set; } = new List<EadOutItem>();

    }
}

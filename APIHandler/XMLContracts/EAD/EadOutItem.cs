using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace APIHandler
{
    [Serializable]
    [XmlRoot(ElementName = "item")]
    public class EadOutItem : IXmlContract
    {
        public const string _Namespace = Constant.ASM_XMLNamespace_Root + "/DeclarationGbExportItem";

        [XmlElement(ElementName = "itemNumber", Namespace = _Namespace, Order = 1)]
        public int ItemNumber { get; set; }

        [XmlElement(ElementName = "commodityCode", Namespace = _Namespace, Order = 2)]
        public string CommodityCode { get; set; }

        [XmlElement(ElementName = "goodsDescription", Namespace = _Namespace, Order = 3)]
        public string GoodsDescription { get; set; }

        [XmlElement(ElementName = "originCountry", Namespace = _Namespace, Order = 4)]
        public ASMCountry OriginCountry { get; set; }

        //[XmlElement(ElementName = "originCountryFecIndicator", Namespace = _Namespace)]
        //public bool OriginCountryFecIndicator{get;set;}

        [XmlElement(ElementName = "cpc", Namespace = _Namespace, Order = 5)]
        public string Cpc { get; set; }

        [XmlElement(ElementName = "unDangerousGoods", Namespace = _Namespace, Order = 6)]
        public string UnDangerousGoods { get; set; }

        [XmlElement(ElementName = "netMass", Namespace = _Namespace, Order = 7)]
        public double NetMass { get; set; }

        //[XmlElement(ElementName = "netMassFecIndicator", Namespace = _Namespace, Order = 7)]
        //public bool NetMassFecIndicator { get; set; }

        [XmlElement(ElementName = "statisticalValue", Namespace = _Namespace, Order = 8)]
        public decimal StatisticalValue { get; set; }

        [XmlElement(ElementName = "supplementaryUnits", Namespace = _Namespace, Order = 9)]
        public string SupplementaryUnits { get; set; }

        //[XmlElement(ElementName = "supplementaryUnitsFecIndicator", Namespace = _Namespace, Order = 10)]
        //public bool SupplementaryUnitsFecIndicator { get; set; }

        [XmlElement(ElementName = "dispatchCountry", Namespace = _Namespace, Order = 11)]
        public ASMCountry DispatchCountry { get; set; }

        //[XmlElement(ElementName = "dispatchCountryFecIndicator", Namespace = _Namespace)]
        //public bool DispatchCountryFecIndicator{get;set;}

        [XmlElement(ElementName = "destinationCountry", Namespace = _Namespace, Order = 12)]
        public ASMCountry DestinationCountry { get; set; }

        [XmlElement(ElementName = "grossMass", Namespace = _Namespace, Order = 13)]
        public double GrossMass { get; set; }

        [XmlElement(ElementName = "thirdQuantity", Namespace = _Namespace, Order = 14)]
        public string ThirdQuantity { get; set; }

        //[XmlArray("taxLines", Namespace = _Namespace)]
        //[XmlArrayItem("taxLine")]
        //public List<TaxLine> TaxLines = new List<TaxLine>();

        [XmlArray("previousDocuments", Namespace = _Namespace, Order = 15)]
        [XmlArrayItem("previousDocument")]
        public List<PreviousDocument> PreviousDocuments { get; set; } = new List<PreviousDocument>();

        [XmlArray("packages", Namespace = _Namespace, Order = 16)]
        [XmlArrayItem("package")]
        public List<Package> Packages { get; set; } = new List<Package>();


        [XmlArray("containers", Namespace = _Namespace, Order = 17)]
        [XmlArrayItem("container")]
        public List<Container> Containers { get; set; } = new List<Container>();
        
        [XmlArray("aiStatements", Namespace = _Namespace, Order = 18)]
        [XmlArrayItem("aiStatement")]
        public List<AIStatement> AiStatements { get; set; }

        [XmlArray("producedDocuments", Namespace = _Namespace, Order = 19)]
        [XmlArrayItem("producedDocument")]
        public List<ProducedDocument> ProducedDocuments { get; set; }


        [XmlElement(ElementName = "euQuota", Namespace = _Namespace, Order = 20)]
        public string EuQuota { get; set; }


        //[XmlArray("producedDocuments", Namespace = _Namespace, Order = 21)]
        //[XmlArrayItem("producedDocument")]
        //public List<ProducedLicencesDocument> ProducedLicencesDocument { get; set; } = new List<ProducedLicencesDocument>();

    }
}

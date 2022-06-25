using Olive;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace APIHandler
{
    [Serializable]
    [XmlRoot(ElementName = "item")]
    public class EadInItem : IXmlContract
    {
        public const string _Namespace = Constant.ASM_XMLNamespace_Root + "/DeclarationGbImportItem";

        [XmlElement(ElementName = "itemNumber", Namespace = _Namespace, Order = 1)]
        public int ItemNumber { get; set; }

        [XmlElement(ElementName = "commodityCode", Namespace = _Namespace, Order = 2)]
        public string CommodityCode { get; set; }

        [XmlElement(ElementName = "commodityCodeSupplemental", Namespace = _Namespace, Order = 3)]
        public string CommodityCodeSupplemental { get; set; }

        [XmlElement(ElementName = "goodsDescription", Namespace = _Namespace, Order = 4)]
        public string GoodsDescription { get; set; }

        [XmlElement(ElementName = "originCountry", Namespace = _Namespace, Order = 5)]
        public ASMCountry OriginCountry { get; set; }

        //[XmlElement(ElementName = "originCountryFecIndicator", Namespace = _Namespace, Order = 5)]
        //public bool OriginCountryFecIndicator;

        [XmlElement(ElementName = "cpc", Namespace = _Namespace, Order = 6)]
        public string Cpc { get; set; }

        [XmlElement(ElementName = "unDangerousGoods", Namespace = _Namespace, Order = 7)]
        public string UnDangerousGoods { get; set; }

        [XmlElement(ElementName = "preferenceCode", Namespace = _Namespace, Order = 8)]
        public string PreferenceCode { get; set; }

        [XmlElement(ElementName = "quota", Namespace = _Namespace, Order = 9)]
        public string EuQuota { get; set; }


        [XmlIgnore]
        public decimal? Price { get; set; }

        [XmlElement(ElementName = "price", Namespace = _Namespace, Order = 10)]
        public string PriceAsString
        {
            get
            {
                return Price.HasValue ? Price.ToString() : null;
            }

            set
            {
                Price = value.HasValue() ? Convert.ToDecimal(value) : default(decimal?);
            }
        }

        [XmlIgnore]
        public double? NetMass { get; set; }

        [XmlElement(ElementName = "netMass", Namespace = _Namespace, Order = 11)]
        public string NetMassAsString
        {
            get
            {
                return NetMass.HasValue ? NetMass.ToString() : null;
            }

            set
            {
                NetMass = value.HasValue() ? Convert.ToDouble(value) : default(double?);
            }
        }

        //[XmlElement(ElementName = "netMassFecIndicator", Namespace = _Namespace, Order = 10)]
        //public bool NetMassFecIndicator;

        [XmlElement(ElementName = "supplementaryUnits", Namespace = _Namespace, Order = 12)]
        public string SupplementaryUnits { get; set; }

        [XmlElement(ElementName = "valuationMethod", Namespace = _Namespace, Order = 13)]
        public string ValuationMethod { get; set; }

        //[XmlElement(ElementName = "supplementaryUnitsFecIndicator", Namespace = _Namespace, Order = 11)]
        //public bool SupplementaryUnitsFecIndicator { get; set; }

        [XmlElement(ElementName = "valuationAdjustment", Namespace = _Namespace, Order = 14)]
        public string ValuationAdjustment { get; set; }

        [XmlElement(ElementName = "valuationAdjustmentPercentage", Namespace = _Namespace, Order = 15)]
        public decimal ValuationAdjustmentPercentage { get; set; }

        [XmlElement(ElementName = "thirdQuantity", Namespace = _Namespace, Order = 16)]
        public string ThirdQuantity { get; set; }

        [XmlArray("taxLines", Namespace = _Namespace, Order = 17)]
        [XmlArrayItem("taxLine")]
        public List<TaxLine> TaxLines { get; set; } = new List<TaxLine>();

        [XmlArray("previousDocuments", Namespace = _Namespace, Order = 18)]
        [XmlArrayItem("previousDocument")]
        public List<PreviousDocument> PreviousDocuments { get; set; } = new List<PreviousDocument>();

        [XmlArray("packages", Namespace = _Namespace, Order = 19)]
        [XmlArrayItem("package")]
        public List<Package> Packages { get; set; } = new List<Package>();

        [XmlArray("aiStatements", Namespace = _Namespace, Order = 20)]
        [XmlArrayItem("aiStatement")]
        public List<AIStatement> AiStatements { get; set; }

        [XmlArray("producedDocuments", Namespace = _Namespace, Order = 21)]
        [XmlArrayItem("producedDocument")]
        public List<ProducedDocument> ProducedDocuments { get; set; } = new List<ProducedDocument>();
    }
}



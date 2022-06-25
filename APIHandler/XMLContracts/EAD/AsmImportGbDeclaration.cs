using Olive;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace APIHandler
{
    [System.Serializable]

    [XmlRoot(ElementName = "importDeclaration", Namespace = Constant.ASM_XMLNamespace_Root + "/DeclarationGbImport")]
    public class AsmImportGbDeclaration : IXmlContract
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

        [XmlElement("totalPackages", Order = 8)]
        public int TotalPackages { get; set; }

        [XmlElement("traderReference", Order = 9)]
        public string TraderReference { get; set; }

        [XmlElement("representation", Order = 10)]
        public string Representation { get; set; }

        [XmlElement("declarationUcr", Order = 11)]
        public string DeclarationUcr { get; set; }

        [XmlElement("declarationUcrPart", Order = 12)]
        public string DeclarationUcrPart { get; set; }

        [XmlElement("masterUcr", Order = 13)]
        public string MasterUcr { get; set; }

        [XmlElement("dispatchCountry", Order = 14)]
        public ASMCountry DispatchCountry { get; set; }

        //[XmlElement("dispatchCountryFecIndicator", Order = 15)]
        //public bool DispatchCountryFecIndicator { get; set; }

        [XmlElement("transportCountry", Order = 16)]
        public ASMCountry TransportCountry { get; set; }

        //[XmlElement("transportCountryFecIndicator", Order = 17)]
        //public bool TransportCountryFecIndicator { get; set; }

        [XmlElement("transportIdentity", Order = 18)]
        public string TransportIdentity { get; set; }

        [XmlElement("invoiceCurrency", Order = 19)]
        public Currency InvoiceCurrency { get; set; }

        [XmlIgnore]
        public decimal? InvoiceAmount { get; set; }

        [XmlElement("invoiceAmount", Order = 20)]
        public string InvoiceAmountAsString
        {
            get
            {
                return InvoiceAmount.HasValue ? InvoiceAmount.ToString() : null;
            }

            set
            {
                InvoiceAmount = value.HasValue() ?  Convert.ToDecimal(value) : default(decimal?);
            }
        }

        [XmlElement("borderTransportMode", Order = 21)]
        public EADBorderTransportMode BorderTransportMode { get; set; }

        [XmlElement("goodsLocationCountry", Order = 22)]
        public ASMCountry GoodsLocationCountry { get; set; }

        [XmlElement("goodsLocationPort", Order = 23)]
        public Location GoodsLocationPort { get; set; }

        [XmlElement("goodsLocationShed", Order = 24)]
        public string GoodsLocationShed { get; set; }

        [XmlElement("firstDefermentPrefix", Order = 25)]
        public string FirstDefermentPrefix { get; set; }

        [XmlElement("firstDefermentNumber", Order = 26)]
        public string FirstDefermentNumber { get; set; }

        [XmlElement("warehousePremises", Order = 27)]
        public WarehousePremise WarehousePremise { get; set; }

        [XmlElement("supervisingOffice", Order = 28)]
        public SupervisingOffice SupervisingOffice { get; set; }

        [XmlElement("valueBuildUp", Order = 29)]
        public ValueBuildUp ValueBuildUp { get; set; }

        [XmlArray("aiStatements", Order = 30)]
        [XmlArrayItem("aiStatement")]
        public List<AIStatement> AiStatements { get; set; }

        [XmlArray("producedDocuments", Order = 31)]
        [XmlArrayItem("producedDocument")]
        public List<ProducedDocument> ProducedDocuments { get; set; } = new List<ProducedDocument>();

        [XmlElement("reasonForAmendment", Order = 32)]
        public string ReasonForAmendment { get; set; }

        [XmlArray("items", Order = 33)]
        [XmlArrayItem("item")]
        public List<EadInItem> Items { get; set; } = new List<EadInItem>();
    }
}

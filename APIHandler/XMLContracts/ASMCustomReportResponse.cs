using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace APIHandler.XMLContracts
{
    [XmlRoot(ElementName = "declarationUcr", Namespace = "asm.org.uk/Sequoia/DeclarationGbIdentityType")]
    public class DeclarationUcr
    {
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "declarationIdentity", Namespace = "asm.org.uk/Sequoia/DeclarationGbResponse")]
    public class DeclarationIdentity
    {
        [XmlElement(ElementName = "declarationUcr", Namespace = "asm.org.uk/Sequoia/DeclarationGbIdentityType")]
        public DeclarationUcr DeclarationUcr { get; set; }
    }

    [XmlRoot(ElementName = "reportType", Namespace = "asm.org.uk/Sequoia/DeclarationGbAcceptanceResponse")]
    public class ReportType
    {
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "ICS", Namespace = "asm.org.uk/Sequoia/DeclarationGbAcceptanceResponse")]
    public class ICS
    {
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "entryRoute", Namespace = "asm.org.uk/Sequoia/DeclarationGbAcceptanceResponse")]
    public class EntryRoute
    {
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "entryTime", Namespace = "asm.org.uk/Sequoia/DeclarationGbAcceptanceResponse")]
    public class EntryTime
    {
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "acceptanceTime", Namespace = "asm.org.uk/Sequoia/DeclarationGbAcceptanceResponse")]
    public class AcceptanceTime
    {
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "goodsLocationCode", Namespace = "asm.org.uk/Sequoia/DeclarationGbAcceptanceResponse")]
    public class GoodsLocationCode
    {
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "acceptanceEpuNumber", Namespace = "asm.org.uk/Sequoia/DeclarationGbAcceptanceResponse")]
    public class AcceptanceEpuNumber
    {
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "entryEpuNumber", Namespace = "asm.org.uk/Sequoia/DeclarationGbAcceptanceResponse")]
    public class EntryEpuNumber
    {
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "entryNumber", Namespace = "asm.org.uk/Sequoia/DeclarationGbAcceptanceResponse")]
    public class EntryNumber
    {
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "entryVersionNumber", Namespace = "asm.org.uk/Sequoia/DeclarationGbAcceptanceResponse")]
    public class EntryVersionNumber
    {
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "currencyCode", Namespace = "asm.org.uk/Sequoia/Currency")]
    public class CurrencyCode
    {
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "invoiceCurrency", Namespace = "asm.org.uk/Sequoia/DeclarationGbAcceptanceResponse")]
    public class InvoiceCurrency
    {
        [XmlElement(ElementName = "currencyCode", Namespace = "asm.org.uk/Sequoia/Currency")]
        public CurrencyCode CurrencyCode { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

    [XmlRoot(ElementName = "value", Namespace = "asm.org.uk/Sequoia/MonetaryType")]
    public class Value
    {
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "invoiceTotal", Namespace = "asm.org.uk/Sequoia/DeclarationGbAcceptanceResponse")]
    public class InvoiceTotal
    {
        [XmlElement(ElementName = "value", Namespace = "asm.org.uk/Sequoia/MonetaryType")]
        public Value Value { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

    [XmlRoot(ElementName = "invoiceExchangeRate", Namespace = "asm.org.uk/Sequoia/DeclarationGbAcceptanceResponse")]
    public class InvoiceExchangeRate
    {
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "customsDutyPayable", Namespace = "asm.org.uk/Sequoia/DeclarationGbAcceptanceResponse")]
    public class CustomsDutyPayable
    {
        [XmlElement(ElementName = "value", Namespace = "asm.org.uk/Sequoia/MonetaryType")]
        public Value Value { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

    [XmlRoot(ElementName = "vatPayable", Namespace = "asm.org.uk/Sequoia/DeclarationGbAcceptanceResponse")]
    public class VatPayable
    {
        [XmlElement(ElementName = "value", Namespace = "asm.org.uk/Sequoia/MonetaryType")]
        public Value Value { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

    [XmlRoot(ElementName = "deferedRevenue", Namespace = "asm.org.uk/Sequoia/DeclarationGbAcceptanceResponse")]
    public class DeferedRevenue
    {
        [XmlElement(ElementName = "value", Namespace = "asm.org.uk/Sequoia/MonetaryType")]
        public Value Value { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

    [XmlRoot(ElementName = "immediateRevenue", Namespace = "asm.org.uk/Sequoia/DeclarationGbAcceptanceResponse")]
    public class ImmediateRevenue
    {
        [XmlElement(ElementName = "value", Namespace = "asm.org.uk/Sequoia/MonetaryType")]
        public Value Value { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

    [XmlRoot(ElementName = "revenuePayable", Namespace = "asm.org.uk/Sequoia/DeclarationGbAcceptanceResponse")]
    public class RevenuePayable
    {
        [XmlElement(ElementName = "value", Namespace = "asm.org.uk/Sequoia/MonetaryType")]
        public Value Value { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

    [XmlRoot(ElementName = "inventorySystemIdentity", Namespace = "asm.org.uk/Sequoia/DeclarationGbAcceptanceResponse")]
    public class InventorySystemIdentity
    {
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "submittingTurn", Namespace = "asm.org.uk/Sequoia/DeclarationGbAcceptanceResponse")]
    public class SubmittingTurn
    {
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "declarantTraderIdentity", Namespace = "asm.org.uk/Sequoia/DeclarationGbAcceptanceResponse")]
    public class DeclarantTraderIdentity
    {
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "goodsLocationEpuNumber", Namespace = "asm.org.uk/Sequoia/DeclarationGbAcceptanceResponse")]
    public class GoodsLocationEpuNumber
    {
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "entryType", Namespace = "asm.org.uk/Sequoia/DeclarationGbAcceptanceResponse")]
    public class EntryType
    {
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "userReference", Namespace = "asm.org.uk/Sequoia/DeclarationGbAcceptanceResponse")]
    public class UserReference
    {
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "taxType", Namespace = "asm.org.uk/Sequoia/DeclarationGbTaxlineResponse")]
    public class TaxType
    {
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "methodOfPayment", Namespace = "asm.org.uk/Sequoia/DeclarationGbTaxlineResponse")]
    public class MethodOfPayment
    {
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "taxRevenue", Namespace = "asm.org.uk/Sequoia/DeclarationGbTaxlineResponse")]
    public class TaxRevenue
    {
        [XmlElement(ElementName = "value", Namespace = "asm.org.uk/Sequoia/MonetaryType")]
        public Value Value { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

    [XmlRoot(ElementName = "taxline", Namespace = "asm.org.uk/Sequoia/DeclarationGbAcceptanceResponse")]
    public class Taxline
    {
        [XmlElement(ElementName = "taxType", Namespace = "asm.org.uk/Sequoia/DeclarationGbTaxlineResponse")]
        public TaxType TaxType { get; set; }
        [XmlElement(ElementName = "methodOfPayment", Namespace = "asm.org.uk/Sequoia/DeclarationGbTaxlineResponse")]
        public MethodOfPayment MethodOfPayment { get; set; }
        [XmlElement(ElementName = "taxRevenue", Namespace = "asm.org.uk/Sequoia/DeclarationGbTaxlineResponse")]
        public TaxRevenue TaxRevenue { get; set; }
    }

    [XmlRoot(ElementName = "summaryTaxDetails", Namespace = "asm.org.uk/Sequoia/DeclarationGbAcceptanceResponse")]
    public class SummaryTaxDetails
    {
        [XmlElement(ElementName = "taxline", Namespace = "asm.org.uk/Sequoia/DeclarationGbAcceptanceResponse")]
        public Taxline Taxline { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

    [XmlRoot(ElementName = "itemNumber", Namespace = "asm.org.uk/Sequoia/DeclarationGbItemResponse")]
    public class ItemNumber
    {
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "licenseValue", Namespace = "asm.org.uk/Sequoia/DeclarationGbItemResponse")]
    public class LicenseValue
    {
        [XmlElement(ElementName = "value", Namespace = "asm.org.uk/Sequoia/MonetaryType")]
        public Value Value { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

    [XmlRoot(ElementName = "customsValue", Namespace = "asm.org.uk/Sequoia/DeclarationGbItemResponse")]
    public class CustomsValue
    {
        [XmlElement(ElementName = "value", Namespace = "asm.org.uk/Sequoia/MonetaryType")]
        public Value Value { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

    [XmlRoot(ElementName = "valueForVat", Namespace = "asm.org.uk/Sequoia/DeclarationGbItemResponse")]
    public class ValueForVat
    {
        [XmlElement(ElementName = "value", Namespace = "asm.org.uk/Sequoia/MonetaryType")]
        public Value Value { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

    [XmlRoot(ElementName = "itemPrice", Namespace = "asm.org.uk/Sequoia/DeclarationGbItemResponse")]
    public class ItemPrice
    {
        [XmlElement(ElementName = "value", Namespace = "asm.org.uk/Sequoia/MonetaryType")]
        public Value Value { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

    [XmlRoot(ElementName = "statisticalValue", Namespace = "asm.org.uk/Sequoia/DeclarationGbItemResponse")]
    public class StatisticalValue
    {
        [XmlElement(ElementName = "value", Namespace = "asm.org.uk/Sequoia/MonetaryType")]
        public Value Value { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

    [XmlRoot(ElementName = "taxline", Namespace = "asm.org.uk/Sequoia/DeclarationGbItemResponse")]
    public class Taxline2
    {
        [XmlElement(ElementName = "taxType", Namespace = "asm.org.uk/Sequoia/DeclarationGbTaxlineResponse")]
        public TaxType TaxType { get; set; }
        [XmlElement(ElementName = "methodOfPayment", Namespace = "asm.org.uk/Sequoia/DeclarationGbTaxlineResponse")]
        public MethodOfPayment MethodOfPayment { get; set; }
        [XmlElement(ElementName = "taxRevenue", Namespace = "asm.org.uk/Sequoia/DeclarationGbTaxlineResponse")]
        public TaxRevenue TaxRevenue { get; set; }
    }

    [XmlRoot(ElementName = "taxDetails", Namespace = "asm.org.uk/Sequoia/DeclarationGbItemResponse")]
    public class TaxDetails
    {
        [XmlElement(ElementName = "taxline", Namespace = "asm.org.uk/Sequoia/DeclarationGbItemResponse")]
        public Taxline2 Taxline2 { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

    [XmlRoot(ElementName = "itemResponse", Namespace = "asm.org.uk/Sequoia/DeclarationGbAcceptanceResponse")]
    public class ItemResponse
    {
        [XmlElement(ElementName = "itemNumber", Namespace = "asm.org.uk/Sequoia/DeclarationGbItemResponse")]
        public ItemNumber ItemNumber { get; set; }
        [XmlElement(ElementName = "licenseValue", Namespace = "asm.org.uk/Sequoia/DeclarationGbItemResponse")]
        public LicenseValue LicenseValue { get; set; }
        [XmlElement(ElementName = "customsValue", Namespace = "asm.org.uk/Sequoia/DeclarationGbItemResponse")]
        public CustomsValue CustomsValue { get; set; }
        [XmlElement(ElementName = "valueForVat", Namespace = "asm.org.uk/Sequoia/DeclarationGbItemResponse")]
        public ValueForVat ValueForVat { get; set; }
        [XmlElement(ElementName = "itemPrice", Namespace = "asm.org.uk/Sequoia/DeclarationGbItemResponse")]
        public ItemPrice ItemPrice { get; set; }
        [XmlElement(ElementName = "statisticalValue", Namespace = "asm.org.uk/Sequoia/DeclarationGbItemResponse")]
        public StatisticalValue StatisticalValue { get; set; }
        [XmlElement(ElementName = "taxDetails", Namespace = "asm.org.uk/Sequoia/DeclarationGbItemResponse")]
        public List<TaxDetails> TaxDetails { get; set; }
    }

    [XmlRoot(ElementName = "itemResponses", Namespace = "asm.org.uk/Sequoia/DeclarationGbAcceptanceResponse")]
    public class ItemResponses
    {
        [XmlElement(ElementName = "itemResponse", Namespace = "asm.org.uk/Sequoia/DeclarationGbAcceptanceResponse")]
        public ItemResponse ItemResponse { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

    [XmlRoot(ElementName = "acceptanceResponse", Namespace = "asm.org.uk/Sequoia/DeclarationGbResponse")]
    public class AcceptanceResponse
    {
        [XmlElement(ElementName = "reportType", Namespace = "asm.org.uk/Sequoia/DeclarationGbAcceptanceResponse")]
        public ReportType ReportType { get; set; }
        [XmlElement(ElementName = "ICS", Namespace = "asm.org.uk/Sequoia/DeclarationGbAcceptanceResponse")]
        public ICS ICS { get; set; }
        [XmlElement(ElementName = "entryRoute", Namespace = "asm.org.uk/Sequoia/DeclarationGbAcceptanceResponse")]
        public EntryRoute EntryRoute { get; set; }
        [XmlElement(ElementName = "entryTime", Namespace = "asm.org.uk/Sequoia/DeclarationGbAcceptanceResponse")]
        public EntryTime EntryTime { get; set; }
        [XmlElement(ElementName = "acceptanceTime", Namespace = "asm.org.uk/Sequoia/DeclarationGbAcceptanceResponse")]
        public AcceptanceTime AcceptanceTime { get; set; }
        [XmlElement(ElementName = "goodsLocationCode", Namespace = "asm.org.uk/Sequoia/DeclarationGbAcceptanceResponse")]
        public GoodsLocationCode GoodsLocationCode { get; set; }
        [XmlElement(ElementName = "acceptanceEpuNumber", Namespace = "asm.org.uk/Sequoia/DeclarationGbAcceptanceResponse")]
        public AcceptanceEpuNumber AcceptanceEpuNumber { get; set; }
        [XmlElement(ElementName = "entryEpuNumber", Namespace = "asm.org.uk/Sequoia/DeclarationGbAcceptanceResponse")]
        public EntryEpuNumber EntryEpuNumber { get; set; }
        [XmlElement(ElementName = "entryNumber", Namespace = "asm.org.uk/Sequoia/DeclarationGbAcceptanceResponse")]
        public EntryNumber EntryNumber { get; set; }
        [XmlElement(ElementName = "entryVersionNumber", Namespace = "asm.org.uk/Sequoia/DeclarationGbAcceptanceResponse")]
        public EntryVersionNumber EntryVersionNumber { get; set; }
        [XmlElement(ElementName = "invoiceCurrency", Namespace = "asm.org.uk/Sequoia/DeclarationGbAcceptanceResponse")]
        public InvoiceCurrency InvoiceCurrency { get; set; }
        [XmlElement(ElementName = "invoiceTotal", Namespace = "asm.org.uk/Sequoia/DeclarationGbAcceptanceResponse")]
        public InvoiceTotal InvoiceTotal { get; set; }
        [XmlElement(ElementName = "invoiceExchangeRate", Namespace = "asm.org.uk/Sequoia/DeclarationGbAcceptanceResponse")]
        public InvoiceExchangeRate InvoiceExchangeRate { get; set; }
        [XmlElement(ElementName = "customsDutyPayable", Namespace = "asm.org.uk/Sequoia/DeclarationGbAcceptanceResponse")]
        public CustomsDutyPayable CustomsDutyPayable { get; set; }
        [XmlElement(ElementName = "vatPayable", Namespace = "asm.org.uk/Sequoia/DeclarationGbAcceptanceResponse")]
        public VatPayable VatPayable { get; set; }
        [XmlElement(ElementName = "deferedRevenue", Namespace = "asm.org.uk/Sequoia/DeclarationGbAcceptanceResponse")]
        public DeferedRevenue DeferedRevenue { get; set; }
        [XmlElement(ElementName = "immediateRevenue", Namespace = "asm.org.uk/Sequoia/DeclarationGbAcceptanceResponse")]
        public ImmediateRevenue ImmediateRevenue { get; set; }
        [XmlElement(ElementName = "revenuePayable", Namespace = "asm.org.uk/Sequoia/DeclarationGbAcceptanceResponse")]
        public RevenuePayable RevenuePayable { get; set; }
        [XmlElement(ElementName = "inventorySystemIdentity", Namespace = "asm.org.uk/Sequoia/DeclarationGbAcceptanceResponse")]
        public InventorySystemIdentity InventorySystemIdentity { get; set; }
        [XmlElement(ElementName = "submittingTurn", Namespace = "asm.org.uk/Sequoia/DeclarationGbAcceptanceResponse")]
        public SubmittingTurn SubmittingTurn { get; set; }
        [XmlElement(ElementName = "declarantTraderIdentity", Namespace = "asm.org.uk/Sequoia/DeclarationGbAcceptanceResponse")]
        public DeclarantTraderIdentity DeclarantTraderIdentity { get; set; }
        [XmlElement(ElementName = "goodsLocationEpuNumber", Namespace = "asm.org.uk/Sequoia/DeclarationGbAcceptanceResponse")]
        public GoodsLocationEpuNumber GoodsLocationEpuNumber { get; set; }
        [XmlElement(ElementName = "entryType", Namespace = "asm.org.uk/Sequoia/DeclarationGbAcceptanceResponse")]
        public EntryType EntryType { get; set; }
        [XmlElement(ElementName = "userReference", Namespace = "asm.org.uk/Sequoia/DeclarationGbAcceptanceResponse")]
        public UserReference UserReference { get; set; }
        [XmlElement(ElementName = "summaryTaxDetails", Namespace = "asm.org.uk/Sequoia/DeclarationGbAcceptanceResponse")]
        public List<SummaryTaxDetails> SummaryTaxDetails { get; set; }
        [XmlElement(ElementName = "itemResponses", Namespace = "asm.org.uk/Sequoia/DeclarationGbAcceptanceResponse")]
        public ItemResponses ItemResponses { get; set; }
    }

    [XmlRoot(ElementName = "declarationGbResponse", Namespace = "asm.org.uk/Sequoia/DeclarationGbResponse")]
    public class DeclarationGbResponse
    {
        [XmlElement(ElementName = "declarationIdentity", Namespace = "asm.org.uk/Sequoia/DeclarationGbResponse")]
        public DeclarationIdentity DeclarationIdentity { get; set; }
        [XmlElement(ElementName = "responseType", Namespace = "asm.org.uk/Sequoia/DeclarationGbResponse")]
        public string ResponseType { get; set; }
        [XmlElement(ElementName = "responseTime", Namespace = "asm.org.uk/Sequoia/DeclarationGbResponse")]
        public string ResponseTime { get; set; }
        [XmlElement(ElementName = "isFirstAcceptanceResponse", Namespace = "asm.org.uk/Sequoia/DeclarationGbResponse")]
        public string IsFirstAcceptanceResponse { get; set; }
        [XmlElement(ElementName = "acceptanceResponse", Namespace = "asm.org.uk/Sequoia/DeclarationGbResponse")]
        public AcceptanceResponse AcceptanceResponse { get; set; }
        [XmlAttribute(AttributeName = "xsd", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Xsd { get; set; }
        [XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Xsi { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

}

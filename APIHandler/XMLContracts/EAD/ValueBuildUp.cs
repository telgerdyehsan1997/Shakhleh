using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace APIHandler
{
    [System.Serializable]
    public class ValueBuildUp : IXmlContract
    {
        public const string _Namespace = Constant.ASM_XMLNamespace_Root + "/DeclarationGbValueBuildUp";

        //[XmlElement(ElementName = "airportOfLoading", Namespace = _Namespace)]
        //public Location AirportOfLoading { get; set; }

        //[XmlElement(ElementName = "airTransportCosts", Namespace = _Namespace)]
        //public decimal AirTransportCosts { get; set; }

        [XmlElement(ElementName = "freightChargeCurrency", Namespace = _Namespace, IsNullable = false)]
        public Currency FreightChargeCurrency { get; set; }

        [XmlElement(ElementName = "freightChargeAmount", Namespace = _Namespace, IsNullable = false)]
        public string FreightChargeAmount { get; set; }

        [XmlElement(ElementName = "insuranceCurrency", Namespace = _Namespace, IsNullable = false)]
        public Currency InsuranceCurrency { get; set; }

        [XmlElement(ElementName = "insuranceAmount", Namespace = _Namespace, IsNullable = false)]
        public string InsuranceAmount { get; set; }

        [XmlElement(ElementName = "vatAdjustmentCurrency", Namespace = _Namespace, IsNullable = false)]
        public Currency VatAdjustmentCurrency { get; set; }

        [XmlElement(ElementName = "vatAdjustmentAmount", Namespace = _Namespace, IsNullable = false)]
        public string VatAdjustmentAmount { get; set; }
    }
}


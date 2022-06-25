using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace APIHandler
{
    [System.Serializable]
    [System.Xml.Serialization.XmlInclude(typeof(TaxLine))]
    public class TaxLine : IXmlContract
    {
        [XmlElement(ElementName = "type", Namespace = Constant.ASM_XMLNamespace_Root + "/DeclarationGbTaxLine")]
        public string Type { get; set; }


        [XmlElement(ElementName = "baseAmount", Namespace = Constant.ASM_XMLNamespace_Root + "/DeclarationGbTaxLine", IsNullable = false)]
        public string BaseAmount { get; set; }

        [XmlElement(ElementName = "baseQuantity", Namespace = Constant.ASM_XMLNamespace_Root + "/DeclarationGbTaxLine", IsNullable = false)]
        public string BaseQuantity { get; set; }

        [XmlElement(ElementName = "amount", Namespace = Constant.ASM_XMLNamespace_Root + "/DeclarationGbTaxLine", IsNullable = false)]
        public string Amount { get; set; }

        [XmlElement(ElementName = "rate", Namespace = Constant.ASM_XMLNamespace_Root + "/DeclarationGbTaxLine")]
        public string Rate { get; set; }

        [XmlElement(ElementName = "overrideCode", Namespace = Constant.ASM_XMLNamespace_Root + "/DeclarationGbTaxLine", IsNullable = false)]
        public string OverrideCode { get; set; }

        [XmlElement(ElementName = "methodOfPayment", Namespace = Constant.ASM_XMLNamespace_Root + "/DeclarationGbTaxLine", IsNullable = false)]
        public string MethodOfPayment { get; set; }


    }
}

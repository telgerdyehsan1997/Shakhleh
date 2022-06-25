using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace APIHandler
{
    [System.Serializable]
    public class DeclarationCurrency : IXmlContract
    {
        public const string _Namespace = Constant.ASM_XMLNamespace_Root + "/Currency";

        [XmlElement(ElementName = "currencyCode", Namespace = _Namespace)]
        public string CurrencyCode { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace APIHandler
{
    [System.Serializable]
    public class Currency : IXmlContract
    {
        [XmlElement(ElementName = "currencyCode", Namespace = "asm.org.uk/Sequoia/Currency")]
        public string CurrencyCode { get; set; }
    }
}

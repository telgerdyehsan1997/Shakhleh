using Olive;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace APIHandler
{
    public class ProducedDocument : IXmlContract
    {
        public const string _Namespace = Constant.ASM_XMLNamespace_Root + "/DeclarationGbProducedDocument";

        [XmlElement(ElementName = "code", Namespace = _Namespace)]
        public string Code { get; set; }

        [XmlElement(ElementName = "status", Namespace = _Namespace)]
        public string Status { get; set; }

        [XmlElement(ElementName = "reference", Namespace = _Namespace)]
        public string Reference { get; set; }

        [XmlElement(ElementName = "reason", Namespace = _Namespace)]
        public string Reason { get; set; }

        [XmlIgnore]
        public int? Quantity { get; set; }

        [XmlElement(ElementName = "quantity", Namespace = _Namespace)]
        public string QuantityAsString
        {
            get
            {
                return Quantity.HasValue ? Quantity.ToString() : null;
            }

            set
            {
                Quantity = value.HasValue() ? value.To<int>() : default(int?);
            }
        }
    }
}

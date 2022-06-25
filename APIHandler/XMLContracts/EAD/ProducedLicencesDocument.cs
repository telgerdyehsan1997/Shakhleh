using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace APIHandler
{
    public class ProducedLicencesDocument : IXmlContract    
    {
        public const string _Namespace = Constant.ASM_XMLNamespace_Root + "/DeclarationGbProducedDocument";

        [XmlElement(ElementName = "code", Namespace = _Namespace)]
        public string Code { get; set; }

        [XmlElement(ElementName = "status", Namespace = _Namespace)]
        public string Status { get; set; }

        [XmlElement(ElementName = "reference", Namespace = _Namespace)]
        public string Reference { get; set; }

        [XmlElement(ElementName = "quantity", Namespace = _Namespace)]
        public int? Quantity { get; set; }
    }
}

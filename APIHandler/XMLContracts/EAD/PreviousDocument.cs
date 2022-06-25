using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace APIHandler
{
    [System.Serializable]
    [System.Xml.Serialization.XmlInclude(typeof(PreviousDocument))]
    public class PreviousDocument : IXmlContract
    {
        public const string _Namespace = Constant.ASM_XMLNamespace_Root + "/DeclarationGbPreviousDocument";

        [XmlElement(ElementName = "documentClass", Namespace = _Namespace)]
        public string DocumentClass { get; set; }

        [XmlElement(ElementName = "type", Namespace = _Namespace)]
        public string Type { get; set; }

        [XmlElement(ElementName = "reference", Namespace = _Namespace)]
        public string Reference { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace APIHandler
{
    [System.Serializable]
    [XmlRoot(ElementName = "seal")]
    public class EADSeal : IXmlContract
    {
        public const string _Namespace = Constant.ASM_XMLNamespace_Root + "/DeclarationGbSeal";

        [XmlElement(ElementName = "number", Namespace = _Namespace)]
        public string Number { get; set; }
    }
}

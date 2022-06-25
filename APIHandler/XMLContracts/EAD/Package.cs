using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace APIHandler
{
    [System.Serializable]
    public class Package : IXmlContract
    {
        public const string _Namespace = Constant.ASM_XMLNamespace_Root + "/DeclarationGbPackage";

        [XmlElement(ElementName = "kind", Namespace = _Namespace)]
        public string Kind { get; set; }

        [XmlElement(ElementName = "numberOfPackages", Namespace = _Namespace)]
        public int NumberOfPackages { get; set; }

        [XmlElement(ElementName = "marks", Namespace = _Namespace)]
        public string Marks { get; set; }

        [XmlElement(ElementName = "transportidentity", Namespace = _Namespace)]
        public string TransportIdentity { get; set; }

    }
}

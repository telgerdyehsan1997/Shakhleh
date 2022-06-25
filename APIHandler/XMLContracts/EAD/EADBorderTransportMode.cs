using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace APIHandler
{
    [Serializable]
    public class EADBorderTransportMode : IXmlContract
    {
        public const string _Namespace = Constant.ASM_XMLNamespace_Root + "/TransportMode";

        [XmlElement(ElementName = "modeOfTransportCode", Namespace = _Namespace)]
        public int ModeOfTransportCode { get; set; }
    }
}

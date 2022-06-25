using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace APIHandler
{
    [System.Serializable]
    public class ASMCountry : IXmlContract
    {
        public const string _Namespace = Constant.ASM_XMLNamespace_Root + "/Country";

        [XmlElement(ElementName = "code", Namespace = _Namespace)]
        public string Code { get; set; }
    }
}

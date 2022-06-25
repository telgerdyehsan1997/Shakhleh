using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace APIHandler
{
    [System.Serializable]
    public class Location : IXmlContract
    {
        public const string _Namespace = Constant.ASM_XMLNamespace_Root + "/UnLocation";

        [XmlElement(ElementName = "iataPortCode", Namespace = _Namespace)]
        public string IATAPortCode { get; set; }

        //[XmlElement(ElementName = "oceanPortCode", Namespace = _Namespace)]
        //public string OceanPortCode { get; set; }
    }
}

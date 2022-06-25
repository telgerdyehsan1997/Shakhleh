using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace APIHandler
{

    [System.Serializable]
    [System.Xml.Serialization.XmlInclude(typeof(Location))]
    public class DeclarantBadge : IXmlContract
    {
        public const string _Namespace = Constant.ASM_XMLNamespace_Root + "/Badge";

        [XmlElement(ElementName = "code", Namespace = _Namespace)]
        public string Code { get; set; }

        //[XmlElement("location", Namespace = _Namespace)]
        //public Location Location;
    }
}

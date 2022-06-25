using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace APIHandler
{
    [System.Serializable]
    public class AsmAddress : IXmlContract
    {
        public const string _Namespace = Constant.ASM_XMLNamespace_Root + "/DeclarationGbParty";

        [XmlElement(ElementName = "traderIdentityNumber", Namespace = _Namespace, IsNullable = false)]
        public string TraderIdentityNumber { get; set; }

        [XmlElement(ElementName = "name", Namespace = _Namespace)]
        public string Name { get; set; }

        [XmlElement(ElementName = "street", Namespace = _Namespace)]
        public string Street { get; set; }

        [XmlElement(ElementName = "city", Namespace = _Namespace)]
        public string City { get; set; }

        [XmlElement(ElementName = "postCode", Namespace = _Namespace)]
        public string PostCode { get; set; }

        [XmlElement("countryCode", Namespace = _Namespace)]
        public ASMCountry Country { get; set; }

        [XmlElement(ElementName = "shortCode", Namespace = _Namespace)]
        public string ShortCode { get; set; }
    }
}

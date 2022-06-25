using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace APIHandler
{
    [System.Serializable]
    public class DeclarationIdentity : IXmlContract
    {
        public const string _Namespace = Constant.ASM_XMLNamespace_Root + "/DeclarationGbIdentityType";

        [XmlElement(ElementName = "declarationUcr", Namespace = _Namespace)]
        public string DeclarationUcr { get; set; }


    }
}
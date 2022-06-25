using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace APIHandler
{
    [Serializable]
    public class AsmDeclarationIdentity : IXmlContract
    {
        public const string _Namespace = Constant.ASM_XMLNamespace_Root + "/DeclarationGbIdentityType";

        [XmlElement(ElementName = "declarationUcr", Namespace = _Namespace)]
        public string DeclarationUcr { get; set; }

        [XmlElement(ElementName = "declarationUcrPartNumber", Namespace = _Namespace)]
        public string DeclarationUcrPartNumber { get; set; }

    }
}

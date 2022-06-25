using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace APIHandler
{
    [System.Serializable]

    [XmlRoot(ElementName = "declarationIdentifier", Namespace = Constant.ASM_XMLNamespace_Root + "/DeclarationGbIdentifier")]
    public class AsmSendGbDeclaration : IXmlContract
    {
        [XmlElement(ElementName = "declarationIdentity")]
        public AsmDeclarationIdentity DeclarationIdentity { get; set; }
    }
}

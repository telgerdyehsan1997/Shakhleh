using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace APIHandler
{
    [System.Serializable]
    public class AIStatement : IXmlContract
    {
        public const string _Namespace = Constant.ASM_XMLNamespace_Root + "/DeclarationGbAiStatement";

        [XmlElement(ElementName = "code", Namespace = _Namespace)]
        public string Code { get; set; }
        [XmlElement(ElementName = "text", Namespace = _Namespace, IsNullable = false)]
        public string Text { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace APIHandler
{
    [System.Serializable]
    [XmlRoot(ElementName = "declarationReference", Namespace = Constant.ASM_XMLNamespace_Root + "/DeclarationResponseRequest")]
    public class ASMFrontierCustomReportRequest
    {

        [XmlElement("ducr", Order = 1)]
        public DUCR DUCR { get; set; }
    }
}

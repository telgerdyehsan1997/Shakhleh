using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace APIHandler
{
    [System.Serializable]
    [XmlRoot(ElementName = "declarationGBReportRequest", Namespace = Constant.ASM_XMLNamespace_Root + "/DeclarationGbReportRequest")]
    public class ASMCustomReportRequest
    {

        [XmlElement("declarationIdentity", Order = 1)]
        public DeclarationIdentity DeclarationIdentity { get; set; }

        [XmlElement("reportName", Order = 2)]
        public string ReportName { get; set; }
    }
}
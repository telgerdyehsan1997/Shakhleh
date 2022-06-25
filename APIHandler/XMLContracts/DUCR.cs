using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace APIHandler
{
    public class DUCR : IXmlContract
    {
        [XmlElement(ElementName = "declarationUcr")]
        public string DeclarationUcr { get; set; }
    }
}

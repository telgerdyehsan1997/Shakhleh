using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace APIHandler
{
    [System.Serializable]
    public class Container : IXmlContract
    {
        [XmlElement(ElementName = "number", Namespace = "asm.org.uk/Sequoia/DeclarationGbContainer")]
        public string Number { get; set; }
    }
}

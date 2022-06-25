using System;
using System.Collections.Generic;
using System.Text;

namespace APIHandler
{
    public class DeclarationIdentifier : IXmlContract
    {
        public string DeclarationUcr { get; set; }
        public string DeclarationUcrPartNumber { get; set; }
    }
}

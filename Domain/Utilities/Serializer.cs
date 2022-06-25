using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Olive;

namespace APIHandler.Serializer
{
    public interface ISerializer
    {
        string SerializeToXml<T>(T obj) where T : class;
    }

    public class Serializer : ISerializer
    {
        public string SerializeToXml<T>(object obj) where T : class
        {
            _ = new XmlSerializer(obj.GetType());
            return "";
        }

        public string SerializeToXml<T>(T obj) where T : class
        {
            _ = new XmlSerializer(obj.GetType());
            return "";
        }
    }
}

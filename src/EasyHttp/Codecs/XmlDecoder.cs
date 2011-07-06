using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace EasyHttp.Codecs
{
    public class XmlDecoder : IDecoder
    {
        private XmlSerializerNamespaces _namespace;
        private XmlWriterSettings _settings;

        public XmlDecoder()
        {
            CreateNamespace();
            CreateSettings();
        }

        private void CreateSettings()
        {
            _settings = new XmlWriterSettings
            {
                Encoding = Encoding.UTF8,
            };
        }

        private void CreateNamespace()
        {
            _namespace = new XmlSerializerNamespaces();
            _namespace.Add("", "");
        }

        public T DecodeToStatic<T>(string input, string contentType)
        {
            var serializer = new XmlSerializer(typeof(T));
            var reader = new StringReader(input);

            return (T)serializer.Deserialize(reader);
        }

        public dynamic DecodeToDynamic(string input, string contentType)
        {
            return new DynamicXml(input);
        }
    }
}

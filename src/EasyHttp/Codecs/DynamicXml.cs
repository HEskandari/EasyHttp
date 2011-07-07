using System;
using System.Dynamic;
using System.IO;
using System.Xml.Linq;

namespace EasyHttp.Codecs
{
    public class DynamicXml : DynamicObject
    {
        private readonly XElement _element;
        private readonly XAttribute _attribute;

        public DynamicXml(string xmlContent)
        {
            var reader = new StringReader(xmlContent);
            _element = XElement.Load(reader);
        }
        
        public DynamicXml(XElement element)
        {
            _element = element;
        }

        public DynamicXml(XAttribute attribute)
        {
            _attribute = attribute;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if(_element == null)
            {
                result = null;
                return false;
            }

            var child = _element.Element(binder.Name);

            if (child == null)
            {
                var attrib = _element.Attribute(binder.Name);
                if (attrib != null)
                {
                    result = new DynamicXml(attrib);
                    return true;
                }

                result = null;
                return false;
            }
            
            result = new DynamicXml(child);
            return true;
        }

        public override string ToString()
        {
            return this;
        }

        public static implicit operator string(DynamicXml dynamicXml)
        {
            if (dynamicXml != null)
            {
                if (dynamicXml._element != null)
                    return Convert.ToString(dynamicXml._element.Value);

                if (dynamicXml._attribute != null)
                    return Convert.ToString(dynamicXml._attribute.Value);
            }

            return null;
        }
    }
}

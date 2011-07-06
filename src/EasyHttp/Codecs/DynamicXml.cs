using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Dynamic;
using System.Xml;
using System.IO;
using System.Xml.Linq;

namespace EasyHttp.Codecs
{
    public class DynamicXml : DynamicObject
    {
        private XElement _element;
        private XAttribute _attribute;

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
            else
            {
                result = new DynamicXml(child);
                return true;
            }
        }

        public override string ToString()
        {
            if (_element != null)
                return _element.Value;

            if (_attribute != null)
                return _attribute.Value;

            return null;
        }
    }
}

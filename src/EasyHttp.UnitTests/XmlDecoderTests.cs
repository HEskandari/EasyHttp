﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using EasyHttp.Http;
using EasyHttp.Codecs;
using System.Xml.Serialization;

namespace EasyHttp.UnitTests
{
    [TestFixture]
    public class XmlDecoderTests
    {
        [Test]
        public void can_decode_xml_response_as_static_object()
        {
            IDecoder decoder = new XmlDecoder();

            var xml = GetXmlContent();
            var customer = decoder.DecodeToStatic<Customer>(xml, HttpContentTypes.ApplicationXml);

            Assert.NotNull(customer);
            Assert.AreEqual("Hadi", customer.Firstname);
            Assert.AreEqual("Eskandari", customer.Lastname);
        }

        [Test]
        public void Can_decode_xml_to_dynamic_object()
        {
            IDecoder decoder = new XmlDecoder();

            var xml = GetXmlContent();
            var customer = decoder.DecodeToDynamic(xml, HttpContentTypes.ApplicationXml);

            Assert.NotNull(customer);
            Assert.AreEqual("Hadi", customer.Firstname.ToString());
            Assert.AreEqual("Eskandari", customer.Lastname.ToString());
            Assert.AreEqual("HEskandari", customer.Info.Twitter.ToString());
            Assert.IsEmpty(customer.Info.Email.ToString());
        }

        [Test]
        public void Can_decode_xml_with_attributes_to_dynamic_object()
        {
            IDecoder decoder = new XmlDecoder();

            var xml = GetXmlContent();
            var customer = decoder.DecodeToDynamic(xml, HttpContentTypes.ApplicationXml);

            Assert.NotNull(customer);
            Assert.AreEqual("1234", customer.Id.ToString());
            Assert.AreEqual("3", customer.Info.Count.ToString());
        }

        private string GetXmlContent()
        {
            return @"<Customer Id='1234'>
                        <Firstname>Hadi</Firstname>
                        <Lastname>Eskandari</Lastname>
                        <Info Count='3'>
                            <Email></Email>
                            <WebSite>http://hadi.es</WebSite>
                            <Twitter>HEskandari</Twitter>
                        </Info>
                     </Customer>";
        }

        [Serializable]
        public class Customer
        {
            [XmlElement]
            public string Firstname { get; set; }

            [XmlElement]
            public string Lastname { get; set; }
        }
    }
}

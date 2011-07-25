using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using JsonFx.Xml;
using NUnit.Framework;

namespace EasyHttp.UnitTests
{
[TestFixture]
public class DefaultDecoderTests
{
    [Test]
    public void XmlAttributes_Are_Not_Converted_To_Elements()
    {
        var customerRaw = @"<Customer Mode=""Add"">
                                <CustomerNo>02121V</CustomerNo>
                            </Customer>";

        var xmlSerializer = new XmlSerializer(typeof (Customer));
        var reader = new StringReader(customerRaw);
        var customer = (Customer)xmlSerializer.Deserialize(reader);

        var writer = new XmlWriter();
        var serialized = writer.Write(customer);

        Assert.NotNull(serialized);
        Assert.That(serialized, Is.StringContaining("Mode=\"Add\""));
    }

    [Serializable]
    public class Customer
    {
        [XmlAttribute]
        public string Mode { get; set; }

        [XmlElement]
        public string CustomerNo { get; set; }
    }
}
}
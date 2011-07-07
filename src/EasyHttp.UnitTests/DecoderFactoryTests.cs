using EasyHttp.Codecs;
using EasyHttp.Http;
using JsonFx.Serialization.Providers;
using NSubstitute;
using NUnit.Framework;
using StructureMap;

namespace EasyHttp.UnitTests
{
    [TestFixture]
    public class DecoderFactoryTests
    {
        [Test]
        public void Uses_XmlDecoder_when_contenttype_is_xml()
        {
            var container = Substitute.For<IContainer>();
            var factory = new DecoderFactory(container);
            var decoder = Substitute.For<XmlDecoder>();

            container.GetInstance<XmlDecoder>().Returns(x => decoder);
            var foundDecoder = factory.Create(HttpContentTypes.ApplicationXml);

            Assert.AreSame(decoder, foundDecoder);
        }

        [Test]
        public void Uses_JsonDecoder_when_contenttype_is_not_xml()
        {
            var container = Substitute.For<IContainer>();
            var factory = new DecoderFactory(container);
            var reader = Substitute.For<IDataReaderProvider>();
            var decoder = Substitute.For<JsonDecoder>(reader);

            container.GetInstance<JsonDecoder>().Returns(x => decoder);
            var foundDecoder = factory.Create(HttpContentTypes.ApplicationJson);

            Assert.AreSame(decoder, foundDecoder);
        }
    }
}
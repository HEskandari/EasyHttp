using EasyHttp.Http;
using StructureMap;

namespace EasyHttp.Codecs
{
    public class DecoderFactory : IDecoderFactory
    {
        private readonly IContainer _container;

        public DecoderFactory(IContainer container)
        {
            _container = container;
        }

        public IDecoder Create(string contentType)
        {
            if(contentType.Contains(HttpContentTypes.TextXml))
            {
                return _container.GetInstance<XmlDecoder>();
            }

            return _container.GetInstance<JsonDecoder>();
        }
    }
}
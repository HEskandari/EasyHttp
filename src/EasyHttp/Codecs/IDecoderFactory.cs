namespace EasyHttp.Codecs
{
    public interface IDecoderFactory
    {
        IDecoder Create(string contentType);
    }
}
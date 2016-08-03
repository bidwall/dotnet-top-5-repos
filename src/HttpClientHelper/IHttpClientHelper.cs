namespace HttpClientHelpers
{
    public interface IHttpClientHelper
    {
        T GetDataFromUrl<T>(string url);
    }
}
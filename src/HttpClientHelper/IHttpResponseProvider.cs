using System.Threading.Tasks;

namespace HttpClientHelpers
{
    public interface IHttpResponseProvider
    {
        Task<T> GetResponse<T>(HttpClientConfig httpClientConfig);
    }
}
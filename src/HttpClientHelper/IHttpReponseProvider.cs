using System.Threading.Tasks;

namespace HttpClientHelpers
{
    public interface IHttpReponseProvider
    {
        Task<T> GetResponse<T>(HttpClientConfig httpClientConfig);
    }
}
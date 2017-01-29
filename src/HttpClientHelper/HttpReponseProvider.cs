using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HttpClientHelpers
{
    public class HttpReponseProvider : IHttpReponseProvider
    {
        public async Task<T> GetResponse<T>(HttpClientConfig httpClientConfig)
        {
            HttpResponseMessage httpResponseMessage;

            using (var client = new HttpClient())
            {
                client.BaseAddress = httpClientConfig.BaseAddress;

                httpClientConfig.AcceptHeaders.ForEach(x => client.DefaultRequestHeaders.Accept.Add(x));
                httpClientConfig.UserAgentHeaders.ForEach(x => client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(x.Key, x.Value)));

                httpResponseMessage = await client.GetAsync(httpClientConfig.RequestUri).ConfigureAwait(false);
            }

            if (!httpResponseMessage.IsSuccessStatusCode) return await Task.FromResult(default(T));

            return await httpResponseMessage.Content.ReadAsAsync<T>();
        }
    }
}
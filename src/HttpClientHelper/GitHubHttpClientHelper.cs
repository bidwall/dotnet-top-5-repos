using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HttpClientHelpers
{
    public class GitHubHttpClientHelper : IHttpClientHelper
    {
        public T GetDataFromUrl<T>(string url)
        {
            return GetHttpResponse<T>(url).Result;
        }

        private static async Task<T> GetHttpResponse<T>(string requestUri)
        {
            HttpResponseMessage httpResponseMessage;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(requestUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "");

                httpResponseMessage = await client.GetAsync("").ConfigureAwait(false);
            }

            if (!httpResponseMessage.IsSuccessStatusCode) return await Task.FromResult(default(T));

            return await httpResponseMessage.Content.ReadAsAsync<T>();
        }
    }
}

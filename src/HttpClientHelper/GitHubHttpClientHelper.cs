using System;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace HttpClientHelpers
{
    public class GitHubHttpClientHelper : IHttpClientHelper
    {
        private readonly IHttpReponseProvider _httpReponseProvider;

        public GitHubHttpClientHelper(IHttpReponseProvider httpReponseProvider)
        {
            _httpReponseProvider = httpReponseProvider;
        }

        public T GetDataFromUrl<T>(string url)
        {
            var httpClientConfig = new HttpClientConfig
            {
                BaseAddress = new Uri(url),
                RequestUri = string.Empty
            };
            httpClientConfig.AcceptHeaders.Add(new MediaTypeWithQualityHeaderValue(Constants.JsonContentType));
            httpClientConfig.UserAgentHeaders.Add(new KeyValuePair<string, string>(Constants.UserAgentHeaderKey, string.Empty));

            return _httpReponseProvider.GetResponse<T>(httpClientConfig).Result;
        }
    }
}

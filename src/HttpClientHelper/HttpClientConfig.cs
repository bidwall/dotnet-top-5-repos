using System;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace HttpClientHelpers
{
    public class HttpClientConfig
    {
        public Uri BaseAddress { get; set; }
        public string RequestUri { get; set; }
        public List<MediaTypeWithQualityHeaderValue> AcceptHeaders { get; set; } = new List<MediaTypeWithQualityHeaderValue>();
        public List<KeyValuePair<string, string>> UserAgentHeaders { get; set; } = new List<KeyValuePair<string, string>>();
    }
}
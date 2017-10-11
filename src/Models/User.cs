using Newtonsoft.Json;

namespace Models
{
    public class User
    {
        public string Name { get; set; }
        public string Location { get; set; }
        [JsonProperty("Avatar_url")]
        public string AvatarUrl { get; set; }
        [JsonProperty("Repos_Url")]
        public string ReposUrl { get; set; }
    }
}
using Newtonsoft.Json;

namespace Models
{
    public class Repo
    {
        public string Name { get; set; }

        [JsonProperty("StarGazers_Count")]
        public int Stars { get; set; }
    }
}
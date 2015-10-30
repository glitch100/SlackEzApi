using Newtonsoft.Json;

namespace SlackEzAPI.Models
{
    public class BaseSlackResponse
    {
        public bool Ok { get; set; }
        [JsonProperty("ts")]
        public string Timestamp { get; set; }
        public string Error { get; set; }
    }
}
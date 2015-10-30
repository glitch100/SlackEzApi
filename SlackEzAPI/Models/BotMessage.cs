using Newtonsoft.Json;

namespace SlackEzAPI.Models
{
    public class BotMessage
    {
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("channel")]
        public string Channel { get; set; }

        [JsonProperty("attachment")]
        public string Attachment { get; set; }

        [JsonProperty("as_user")]
        public bool AsUser { get; set; }
    }
}
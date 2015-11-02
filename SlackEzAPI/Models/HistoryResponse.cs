using System.Collections.Generic;

namespace SlackEzAPI.Models
{
    public class HistoryResponse : BaseSlackResponse
    {
        public string Timestamp { get; set; }
        public IEnumerable<Message> Messages { get; set; }
        public bool has_more { get; set; }
    }
}
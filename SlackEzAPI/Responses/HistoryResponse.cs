using System.Collections.Generic;

namespace SlackEzAPI.Responses
{
    public class HistoryResponse : BaseSlackResponse
    {
        /// <summary>
        /// Timestamp of the specified History Response
        /// </summary>
        public string Timestamp { get; set; }
        /// <summary>
        /// Collection of Slack Messages in the response
        /// </summary>
        public IEnumerable<Message> Messages { get; set; }
        /// <summary>
        /// Whether this response is all the messages, or if there are more
        /// </summary>
        public bool has_more { get; set; }
    }
}
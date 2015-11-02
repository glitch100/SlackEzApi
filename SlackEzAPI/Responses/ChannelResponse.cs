using System.Collections.Generic;

namespace SlackEzAPI.Responses
{
    public class ChannelResponse : BaseSlackResponse
    {
        /// <summary>
        /// Members of a channel
        /// </summary>
        public IEnumerable<string> Members { get; set; } 
    }
}
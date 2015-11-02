using System.Collections.Generic;
using SlackEzAPI.Models;

namespace SlackEzAPI.Models
{
    public class ChannelResponse : BaseSlackResponse
    {
        /// <summary>
        /// Members of a channel
        /// </summary>
        public IEnumerable<string> Members { get; set; } 
    }
}
using System.Collections.Generic;

namespace SlackEzAPI
{
    public class MessageHistory
    {
        /// <summary>
        /// Messages of a channel
        /// </summary>
        public IEnumerable<Message> Messages { get; set; } 
    }
}
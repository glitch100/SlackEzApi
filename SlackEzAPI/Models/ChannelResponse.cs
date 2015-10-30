using SlackEzAPI.Models;

namespace SlackEzAPI.Models
{
    public class ChannelResponse : BaseSlackResponse
    {
        public ChannelInfo Channel { get; set; }
    }
}
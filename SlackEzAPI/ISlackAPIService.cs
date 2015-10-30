using System.Collections.Generic;
using System.Threading.Tasks;
using SlackEzAPI.Models;

namespace SlackEzAPI
{
    public interface ISlackAPIService
    {
        Task<SlackResponse> DeleteChatMessage(string userId, string channelId, string timeStamp);
        Task<BaseSlackResponse> SendChatMessage(string text, string channelId, bool asUser = true);
        Task<BaseSlackResponse> SendChatMessageWithAttachment(string text, string channelId, IEnumerable<Attachment> attachments, bool asUser = true);
        Task<ChannelResponse> GetChannelInfo(string channelId);
    }
}
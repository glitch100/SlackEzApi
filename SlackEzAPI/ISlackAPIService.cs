using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SlackEzAPI.Models;

namespace SlackEzAPI
{
    public interface ISlackAPIService
    {
        Task<SlackResponse> DeleteChatMessage(string userId, string channelId, string timeStamp);
        Task<BaseSlackResponse> SendChatMessage(string text, string channelId, bool asUser = true);
        Task<BaseSlackResponse> SendChatMessageWithImage(string text, string channelId, IEnumerable<Attachment> attachments, bool asUser = true);
        Task<ChannelResponse> GetChannelInfo(string channelId);
        Task<HistoryResponse> GetChannelHistory(string channelId, decimal latest = 0, decimal oldest = 0, bool inclusive = false, int count = 100, bool unreads = false); 
    }
}

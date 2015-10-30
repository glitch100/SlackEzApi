using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Configuration;
using Newtonsoft.Json;
using SlackEzAPI.Models;

namespace SlackEzAPI
{
    internal static class Endpoints
    {
        private static string _prefix = "https://slack.com/";
                private static readonly string ApiToken = WebConfigurationManager.AppSettings.Get("SlackAPIToken");
        private static readonly string SlashToken = WebConfigurationManager.AppSettings.Get("SlackSlashCommandToken");

        public static string ChatMessage(string text, string channelId, bool asUser = true)
        {
            return string.Format("{0}{1}?token={2}&text={3}&channel={4}&as_user={5}", _prefix, "/api/chat.postMessage",
                ApiToken, text, channelId, asUser);
        }

        public static string ChatMessageWithAttachment(string text, string channelId, Attachment[] attachments,
            bool asUser = true)
        {
            var jsonAttachment = JsonConvert.SerializeObject(attachments);
            return string.Format("{0}{1}?token={2}&text={3}&channel={4}&as_user={5}&attachments={6}", _prefix,
                "/api/chat.postMessage", ApiToken, text, channelId, asUser, jsonAttachment);
        }

        public static string ChatInvite(string userId, string channelId)
        {
            return string.Format("{0}{1}?token={2}&user={3}&channel={4}", _prefix, "/api/channels.invite", SlashToken,
                userId, channelId);
        }

        public static string ChannelInfo(string channelId)
        {
            return string.Format("{0}{1}?token={2}&channel={3}", _prefix, "/api/channels.info", ApiToken, channelId);
        }

        public static string ChannelKick(string userId, string channelId)
        {
            return string.Format("{0}{1}?token={2}&user={3}&channel={4}", _prefix, "/api/channels.kick", ApiToken,
                userId, channelId);
        }

        public static string ChatDelete(string userId, string channelId, string timeStamp)
        {
            return string.Format("{0}{1}?token={2}&user={3}&channel={4}&ts={5}", _prefix, "/api/chat.delete", ApiToken,
                userId, channelId, timeStamp);
        }

    }

    public class SlackAPIService : ISlackAPIService
    {
        private readonly HttpClient _httpClient;

        public SlackAPIService()
        {
            _httpClient = new HttpClient(new HttpClientHandler());
        }     

        public async Task<SlackResponse> DeleteChatMessage(string userId, string channelId, string timeStamp)
        {
            await _httpClient.GetAsync(Endpoints.ChatDelete(userId, channelId, timeStamp));
            return new SlackResponse() { Success = true };
        }

        public async Task<BaseSlackResponse> SendChatMessage(string text, string channelId, bool asUser = true)
        {
            var message = await _httpClient.GetAsync(Endpoints.ChatMessage(text, channelId, asUser));
            var messageJson = await message.Content.ReadAsStringAsync();
            var messageObject = JsonConvert.DeserializeObject<BaseSlackResponse>(messageJson);
            return messageObject ?? new BaseSlackResponse() { Ok= false };
        }


        public async Task<ChannelResponse> GetChannelInfo(string channelId)
        {
            var message = await _httpClient.GetAsync(Endpoints.ChannelInfo(channelId));
            var messageJson = await message.Content.ReadAsStringAsync();
            var messageObject = JsonConvert.DeserializeObject<ChannelResponse>(messageJson);
            return messageObject ?? new ChannelResponse() { Ok = false };
        }

        public async Task<BaseSlackResponse> SendChatMessageWithAttachment(string text, string channelId,IEnumerable<Attachment> attachments, bool asUser = true)
        {
            var message = await _httpClient.GetAsync(Endpoints.ChatMessageWithAttachment(text, channelId,attachments.ToArray(), asUser));
            var messageJson = await message.Content.ReadAsStringAsync();
            var messageObject = JsonConvert.DeserializeObject<BaseSlackResponse>(messageJson);
            return messageObject ?? new BaseSlackResponse() { Ok = false };
        }
    } 

}
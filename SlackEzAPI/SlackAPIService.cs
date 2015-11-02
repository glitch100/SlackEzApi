using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        
        public static string ChatHistory(string channelId, string latest, string oldest, int inclusive, int count, int unreads)
        {
            return string.Format("{0}{1}?token={2}&channel={3}&latest={4}&oldest={5}&inclusive={6}&count={7}&unreads={8}", _prefix, "/api/channels.history", ApiToken,
                channelId, latest, oldest, inclusive, count, unreads);
        }

    }

    /// <summary>
    /// Slack Service responsible for communicating with the SlackAPI
    /// </summary>
    public class SlackAPIService : ISlackAPIService
    {
        private readonly HttpClient _httpClient;

        public SlackAPIService()
        {
            _httpClient = new HttpClient(new HttpClientHandler());
        }     

        /// <summary>
        /// Delete a chat message from a slack channel
        /// </summary>
        /// <param name="userId">Id of the User who posted it </param>
        /// <param name="channelId">Id of the channel the message exists in</param>
        /// <param name="timeStamp">Timestamp of the message</param>
        /// <returns>SlackResponse with information regarding the success of the delete</returns>
        public async Task<SlackResponse> DeleteChatMessage(string userId, string channelId, string timeStamp)
        {
            await _httpClient.GetAsync(Endpoints.ChatDelete(userId, channelId, timeStamp));
            return new SlackResponse() { Success = true };
        }

        /// <summary>
        /// Send a chat message to a channel as a user
        /// </summary>
        /// <param name="text">Text to send in the message</param>
        /// <param name="channelId">Channel to send the message too</param>
        /// <param name="asUser">Whether we should send the message as the user</param>
        /// <returns>BaseSlackRepsonse indicating the success as well as the timestamp of the message</returns>
        public async Task<BaseSlackResponse> SendChatMessage(string text, string channelId, bool asUser = true)
        {
            var message = await _httpClient.GetAsync(Endpoints.ChatMessage(text, channelId, asUser));
            var messageJson = await message.Content.ReadAsStringAsync();
            var messageObject = JsonConvert.DeserializeObject<BaseSlackResponse>(messageJson);
            return messageObject ?? new BaseSlackResponse() { Ok= false };
        }

        /// <summary>
        /// Get information about a channel, based on its Channel Id
        /// </summary>
        /// <param name="channelId">The channel to get information for</param>
        /// <returns>A Channel response with a list of members and other information</returns>
        public async Task<ChannelResponse> GetChannelInfo(string channelId)
        {
            var message = await _httpClient.GetAsync(Endpoints.ChannelInfo(channelId));
            var messageJson = await message.Content.ReadAsStringAsync();
            var messageObject = JsonConvert.DeserializeObject<ChannelResponse>(messageJson);
            return messageObject ?? new ChannelResponse() { Ok = false };
        }

        /// <summary>
        /// Get the message history of a specified channel
        /// </summary>
        /// <param name="channelId">he channel to get message history for</param>
        /// <param name="latest">End of time range of messages to include in results</param>
        /// <param name="oldest">Start of time range of messages to include in results</param>
        /// <param name="inclusive">Include messages with latest/oldest timestamp</param>
        /// <param name="count">Number of messages to return, between 1 and 1000</param>
        /// <param name="unreads">Include unread count display in output</param>
        /// <returns></returns>
        public async Task<HistoryResponse> GetChannelHistory(string channelId, decimal latest = 0, decimal oldest = 0, bool inclusive = false, int count = 100,
            bool unreads = false)
        {
            var message = await _httpClient.GetAsync(Endpoints.ChatHistory(channelId,latest == 0 ? "now" : latest.ToString(), oldest.ToString(), Convert.ToInt32(inclusive),count, Convert.ToInt32(unreads)));
            var messageJson = await message.Content.ReadAsStringAsync();
            var messageObject = JsonConvert.DeserializeObject<HistoryResponse>(messageJson);
            return messageObject ?? new HistoryResponse() { Ok = false };
        }

        /// <summary>
        /// Sends a Chat Message with an Image Attached
        /// </summary>
        /// <param name="text">Text for the attachment</param>
        /// <param name="channelId">Id of the channel to post to</param>
        /// <param name="attachments">Attachments to include</param>
        /// <param name="asUser">Whether to post as the user</param>
        /// <returns>BaseSlackRepsonse indicating the success as well as the timestamp of the message</returns>
        public async Task<BaseSlackResponse> SendChatMessageWithImage(string text, string channelId,IEnumerable<Attachment> attachments, bool asUser = true)
        {
            var message = await _httpClient.GetAsync(Endpoints.ChatMessageWithAttachment(text, channelId,attachments.ToArray(), asUser));
            var messageJson = await message.Content.ReadAsStringAsync();
            var messageObject = JsonConvert.DeserializeObject<BaseSlackResponse>(messageJson);
            return messageObject ?? new BaseSlackResponse() { Ok = false };
        }
    } 

}
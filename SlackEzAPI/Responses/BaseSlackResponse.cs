namespace SlackEzAPI.Responses
{
    /// <summary>
    /// The most basic Slack Response, but also the base of more complex responses
    /// </summary>
    public class BaseSlackResponse
    {
        /// <summary>
        /// Whether the request was Ok or failed.
        /// </summary>
        public bool Ok { get; set; }
        /// <summary>
        /// Timestamp with a successful action, such as the timestamp associated with a chat message
        /// </summary>
        public string ts { get; set; }
        /// <summary>
        /// Any associated error message
        /// </summary>
        public string Error { get; set; }
    }
}
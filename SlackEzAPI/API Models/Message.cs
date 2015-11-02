namespace SlackEzAPI
{
    public class Message
    {
        /// <summary>
        /// Type of message
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// Timestamp of the message
        /// </summary>
        public string Ts { get; set; }
        /// <summary>
        /// User Id of the person who sent the message
        /// </summary>
        public string User { get; set; }
        /// <summary>
        /// Text within the message
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Whether the specific message is starred
        /// </summary>
        public bool is_starred { get; set; }
    }
}

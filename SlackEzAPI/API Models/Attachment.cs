namespace SlackEzAPI
{
    public class Attachment
    {
        /// <summary>
        /// URL of image/gif to show
        /// </summary>
        public string image_url { get; set; }
        /// <summary>
        /// Optional Text that appears with attachment
        /// </summary>
        public string text { get; set; }
        /// <summary>
        /// Optional text that appears above the attachment block
        /// </summary>
        public string pretext { get; set; }
        /// <summary>
        /// Title of the Attachment
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// Link associated with the title
        /// </summary>
        public string title_link { get; set; }
    }
}
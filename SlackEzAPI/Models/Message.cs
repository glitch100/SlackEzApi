namespace SlackEzAPI.Models
{
    public class Message
    {
        public string Type { get; set; }
        public string Ts { get; set; }
        public string User { get; set; }
        public string Text { get; set; }
        public bool is_starred { get; set; }
    }
}

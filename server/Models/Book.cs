namespace server.Models
{
    public class Book
    {
        public string ID { get; set; } = string.Empty;
        public string Title  { get; set; } = string.Empty;
        public string Publisher  { get; set; } = string.Empty;
        public DateTime DateOfPublication  { get; set; }
        public bool IsBorrowed { get; set; } = false;

    }
}
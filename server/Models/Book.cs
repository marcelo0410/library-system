namespace server.Models
{
    public class Book
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Title  { get; set; } = string.Empty;
        public string Publisher  { get; set; } = string.Empty;
        public string DateOfPublication  { get; set; } = string.Empty;
        public bool IsBorrowed { get; set; } = false;
    }
}
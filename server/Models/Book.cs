namespace server.Models
{
    public class Book
    {
        public string Id { get; set; } = string.Empty;
        public string Title  { get; set; } = string.Empty;
        public string Publisher  { get; set; } = string.Empty;
        public DateTime DateOfPublication  { get; set; }
        public bool IsBorrowed { get; set; } = false;

        public ICollection<Borrowing> Borrowings { get; set; } = new List<Borrowing>();



    }
}
namespace server.Models
{
    public class Borrowing
    {
        public string Id { get; set; } = string.Empty;
        public string BorrowNo  { get; set; } = string.Empty;
        public string UserId  { get; set; } = string.Empty;
        public string BookId  { get; set; } = string.Empty;
        public DateTime DateOfBorrow  { get; set; }
        public DateTime DueDate { get; set; }
        public Book BookForeignKey { get; set; } = new Book();

    }
}
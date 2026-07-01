namespace Library.BLL.DTOs.BookDTO
{
    public class BookDetailsDto
    {
        public int Id { get; set; }

        public required string Title { get; set; }

        public required string ISBN { get; set; }

        public int PublicationYear { get; set; }

        public int TotalCopies { get; set; }

        public int AvailableCopies { get; set; }

        public decimal Price { get; set; }

        public int AuthorId { get; set; }

        public required string AuthorName { get; set; }

        public int CategoryId { get; set; }

        public required string CategoryName { get; set; }

        public int PublisherId { get; set; }

        public required string PublisherName { get; set; }
    }
}
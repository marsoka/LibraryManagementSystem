namespace Library.BLL.DTOs.BookDTO
{
    public class UpdateBookDto
    {
        public required string Title { get; set; }

        public required string ISBN { get; set; }

        public int PublicationYear { get; set; }

        public int TotalCopies { get; set; }

        public decimal Price { get; set; }

        public int AuthorId { get; set; }

        public int CategoryId { get; set; }

        public int PublisherId { get; set; }
    }
}
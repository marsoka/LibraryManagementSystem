namespace Library.BLL.DTOs.BorrowingDTO
{
    public class BorrowingDto
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public required string BookTitle { get; set; }

        public int MemberId { get; set; }

        public required string MemberName { get; set; }

        public DateTime BorrowDate { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        public BorrowingStatus Status { get; set; }
    }
}
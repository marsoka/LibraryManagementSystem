namespace Library.BLL.DTOs.MemberDTO
{
    public class MemberDto
    {
        public int Id { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public required string Address { get; set; }
        public DateOnly RegistrationDate { get; set; }
    }
}
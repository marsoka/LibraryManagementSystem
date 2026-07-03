using Library.BLL.Exceptions.StatusCodesExeptions;

namespace Library.BLL.Exceptions.NotFoundExceptions
{
    public class BorrowingNotFoundException : NotFoundException
    {
        public BorrowingNotFoundException(int id)
            : base($"Borroing with id {id} was not found.")
        {
        }
    }
}
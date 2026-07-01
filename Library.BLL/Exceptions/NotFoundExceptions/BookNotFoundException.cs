using Library.BLL.Exceptions.StatusCodesExeptions;

namespace Library.BLL.Exceptions.NotFoundExceptions
{
    public class BookNotFoundException : NotFoundException
    {
        public BookNotFoundException(int id)
            : base($"Book with id {id} was not found.")
        {
        }
    }
}
using Library.BLL.Exceptions.StatusCodesExeptions;

namespace Library.BLL.Exceptions.NotFoundExceptions
{
    public class CategoryNotFoundException : NotFoundException
    {
        public CategoryNotFoundException(int id)
            : base($"Category with id {id} was not found.")
        {
        }
    }
}
using Library.BLL.Exceptions.StatusCodesExeptions;

namespace Library.BLL.Exceptions.NotFoundExceptions
{
    public class MemberNotFoundException : NotFoundException
    {
        public MemberNotFoundException(int id)
            : base($"Member with id {id} was not found.")
        {
        }
    }
}
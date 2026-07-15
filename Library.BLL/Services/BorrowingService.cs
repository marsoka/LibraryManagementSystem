using AutoMapper;
using Library.BLL.DTOs.BookDTO;
using Library.BLL.DTOs.BorrowingDTO;
using Library.BLL.DTOs.MemberDTO;
using Library.BLL.Exceptions.BusinessRuleExceptions;
using Library.BLL.Exceptions.NotFoundExceptions;
using Library.BLL.Exceptions.StatusCodesExeptions;
using Library.BLL.Interfaces;
using Library.DAL.Repositories.Interfaces;

namespace Library.BLL.Services
{
    public class BorrowingService : IBorrowingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BorrowingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task BorrowingBookAsync(CreateBorrowingDto dto)
        {
            Member? member = await _unitOfWork.Members.GetByIdAsync(dto.MemberId);
            if (member == null)
                throw new MemberNotFoundException(dto.MemberId);


            Book? book = await _unitOfWork.Books.GetByIdAsync(dto.BookId);
            if (book == null)
                throw new BookNotFoundException(dto.BookId);

            if (book.AvailableCopies <= 0)
                throw new BookNotAvailableException(book.Id);

            var alreadyBorrowed =
                    await _unitOfWork.Borrowings.IsExistsAsync(b => b.MemberId == dto.MemberId
                    && b.BookId == dto.BookId
                    && b.Status == BorrowingStatus.Borrowed);

            if (alreadyBorrowed)
                throw new BusinessRuleException("Member already borrowed this book.");



            var borrowing = _mapper.Map<Borrowing>(dto);
            borrowing.BorrowDate = DateTime.UtcNow;
            borrowing.DueDate = DateTime.UtcNow.AddDays(14);
            borrowing.ReturnDate = null;
            borrowing.Status = BorrowingStatus.Borrowed;

            book.AvailableCopies--;

            await _unitOfWork.Borrowings.AddAsync(borrowing);
            await _unitOfWork.Books.UpdateAsync(book);
            await _unitOfWork.CompleteAsync();

        }



        // public async Task DeleteBorrowingAsync(int id)
        // {
        //     if (!await _repo.BorrowingIsExistsAsync(id))
        //     {
        //         throw new BorrowingNotFoundException(id);
        //     }

        //     await _repo.DeleteBorrowingAsync(id);
        // }

        public async Task<BorrowingDetailsDto?> GetBorrowingAsync(int id)
        {
            var borrowing = await _unitOfWork.Borrowings.GetByIdAsync(id);
            if (borrowing == null)
            {
                throw new BorrowingNotFoundException(id);
            }
            return _mapper.Map<BorrowingDetailsDto>(borrowing);
        }

        public async Task<IEnumerable<BorrowingDto>> GetBorrowingsAsync()
        {
            var listOfBorrowing = await _unitOfWork.Borrowings.GetAllAsync();
            return _mapper.Map<IEnumerable<BorrowingDto>>(listOfBorrowing);
        }

        public async Task ReturnBorrowedBook(int id)
        {
            var borrowed = await _unitOfWork.Borrowings.GetByIdAsync(id);
            if (borrowed == null)
                throw new BorrowingNotFoundException(id);

            if (borrowed.Status == BorrowingStatus.Returned)
                throw new BookAlreadyReturned(id);

            borrowed.ReturnDate = DateTime.UtcNow;
            borrowed.Status = BorrowingStatus.Returned;

            borrowed.Book.AvailableCopies++;

            await _unitOfWork.Borrowings.UpdateAsync(borrowed);
            await _unitOfWork.CompleteAsync();

        }
    }
}
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
        private readonly IBorrowingRepository _repo;
        private readonly IMapper _mapper;

        private readonly IBookRepository _repoBook;
        private readonly IMemberRepository _repoMember;

        public BorrowingService(IBorrowingRepository repo, IMapper mapper,
                    IBookRepository bookRepository, IMemberRepository memberRepository)
        {
            _repo = repo;
            _mapper = mapper;
            _repoBook = bookRepository;
            _repoMember = memberRepository;
        }

        public async Task BorrowingBookAsync(CreateBorrowingDto dto)
        {
            Member? member = await _repoMember.GetMemberAsync(dto.MemberId);
            if (member == null)
                throw new MemberNotFoundException(dto.MemberId);


            Book? book = await _repoBook.GetBookAsync(dto.BookId);
            if (book == null)
                throw new BookNotFoundException(dto.BookId);

            if (book.AvailableCopies <= 0)
                throw new BookNotAvailableException(book.Id);

            var alreadyBorrowed =
                    await _repo.HasActiveBorrowingAsync(
                        dto.MemberId,
                        dto.BookId);

            if (alreadyBorrowed)
                throw new BusinessRuleException("Member already borrowed this book.");



            var borrowing = _mapper.Map<Borrowing>(dto);
            borrowing.BorrowDate = DateTime.UtcNow;
            borrowing.DueDate = DateTime.UtcNow.AddDays(14);
            borrowing.ReturnDate = null;
            borrowing.Status = BorrowingStatus.Borrowed;

            book.AvailableCopies--;

            await _repo.AddBorrowingAsync(borrowing);
            await _repoBook.UpdateBookAsync(book);

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
            var borrowing = await _repo.GetBorrowingAsync(id);
            if (borrowing == null)
            {
                throw new BorrowingNotFoundException(id);
            }
            return _mapper.Map<BorrowingDetailsDto>(borrowing);
        }

        public async Task<IEnumerable<BorrowingDto>> GetBorrowingsAsync()
        {
            var listOfBorrowing = await _repo.GetBorrowingsAsync();
            return _mapper.Map<IEnumerable<BorrowingDto>>(listOfBorrowing);
        }

        public async Task ReturnBorrowedBook(int id)
        {
            var borrowed = await _repo.GetBorrowingAsync(id);
            if (borrowed == null)
                throw new BorrowingNotFoundException(id);

            if (borrowed.Status == BorrowingStatus.Returned)
                throw new BookAlreadyReturned(id);

            borrowed.ReturnDate = DateTime.UtcNow;
            borrowed.Status = BorrowingStatus.Returned;

            borrowed.Book.AvailableCopies++;

            await _repo.UpdateBorrowingAsync(borrowed);

        }
    }
}
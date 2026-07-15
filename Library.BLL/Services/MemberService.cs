using AutoMapper;
using Library.BLL.DTOs.MemberDTO;
using Library.BLL.Exceptions.AlreadyAlreadyExistsException;
using Library.BLL.Exceptions.NotFoundExceptions;
using Library.BLL.Interfaces;
using Library.DAL.Repositories.Interfaces;

namespace Library.BLL.Services
{
    public class MemberService : IMemberService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MemberService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateMemberAsync(CreateMemberDto dto)
        {
            var member = _mapper.Map<Member>(dto);
            if (await _unitOfWork.Members.IsExistsAsync(m => m.Email == member.Email))
            {
                throw new EmailAlreadyExistsException(member.Email);
            }
            if (await _unitOfWork.Members.IsExistsAsync(m => m.Phone == member.Phone))
            {
                throw new PhoneAlreadyExistsException(member.Phone);
            }

            member.RegistrationDate = DateOnly.FromDateTime(DateTime.UtcNow);
            await _unitOfWork.Members.AddAsync(member);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteMemberAsync(int id)
        {
            var member = await _unitOfWork.Members.GetByIdAsync(id);
            if (member == null)
            {
                throw new MemberNotFoundException(id);
            }

            await _unitOfWork.Members.DeleteAsync(member);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<MemberDto>> GetMembersAsync()
        {
            var listOfMember = await _unitOfWork.Members.GetAllAsync();
            return _mapper.Map<IEnumerable<MemberDto>>(listOfMember);
        }

        public async Task<MemberDto?> GetMemberAsync(int id)
        {
            var member = await _unitOfWork.Members.GetByIdAsync(id);
            if (member == null)
            {
                throw new MemberNotFoundException(id);
            }

            return _mapper.Map<MemberDto>(member);
        }

        public async Task UpdateMemberAsync(int id, UpdateMemberDto dto)
        {
            var member = await _unitOfWork.Members.GetByIdAsync(id);
            if (member == null)
            {
                throw new MemberNotFoundException(id);
            }

            _mapper.Map(dto, member);
            await _unitOfWork.Members.UpdateAsync(member);
            await _unitOfWork.CompleteAsync();
        }
    }
}
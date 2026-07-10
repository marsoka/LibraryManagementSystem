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
        private readonly IMemberRepository _repo;
        private readonly IMapper _mapper;

        public MemberService(IMemberRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task CreateMemberAsync(CreateMemberDto dto)
        {
            var member = _mapper.Map<Member>(dto);
            if (await _repo.EmailIsExistsAsync(member.Email))
            {
                throw new EmailAlreadyExistsException(member.Email);
            }
            if (await _repo.PhoneIsExistsAsync(member.Phone))
            {
                throw new PhoneAlreadyExistsException(member.Phone);
            }

            member.RegistrationDate = DateOnly.FromDateTime(DateTime.UtcNow);
            await _repo.AddMemberAsync(member);
        }

        public async Task DeleteAuthorAsync(int id)
        {
            if (!await _repo.MemberIsExistsAsync(id))
            {
                throw new MemberNotFoundException(id);
            }

            await _repo.DeleteMemberAsync(id);
        }

        public async Task<IEnumerable<MemberDto>> GetMembersAsync()
        {
            var listOfMember = await _repo.GetMembersAsync();
            return _mapper.Map<IEnumerable<MemberDto>>(listOfMember);
        }

        public async Task<MemberDto?> GetMemberAsync(int id)
        {
            var member = await _repo.GetMemberAsync(id);
            if (member == null)
            {
                throw new MemberNotFoundException(id);
            }

            return _mapper.Map<MemberDto>(member);
        }

        public async Task UpdateMemberAsync(int id, UpdateMemberDto dto)
        {
            var member = await _repo.GetMemberAsync(id);
            if (member == null)
            {
                throw new MemberNotFoundException(id);
            }

            _mapper.Map(dto, member);
            await _repo.UpdateMemberAsync(member);
        }
    }
}
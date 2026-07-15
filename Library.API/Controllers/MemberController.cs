using Library.BLL.DTOs.MemberDTO;
using Library.BLL.Interfaces;
using Library.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = UserRoles.Admin + "," + UserRoles.Librarian)]
public class MemberController : ControllerBase
{
    private readonly IMemberService _service;

    public MemberController(IMemberService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IEnumerable<MemberDto>> Get()
    {
        return await _service.GetMembersAsync();
    }

    [HttpGet("{id}")]
    public async Task<MemberDto> GetById(int id)
    {
        return await _service.GetMemberAsync(id);
    }

    [HttpPost]
    public async Task CreateMember(CreateMemberDto createMemberDto)
    {
        await _service.CreateMemberAsync(createMemberDto);
    }

    [HttpPut("{id}")]
    public async Task UpdateMember(int id, UpdateMemberDto updateMemberDto)
    {
        await _service.UpdateMemberAsync(id, updateMemberDto);
    }

    [HttpDelete("{id}")]
    public async Task DeleteMember(int id)
    {
        await _service.DeleteMemberAsync(id);
    }
}
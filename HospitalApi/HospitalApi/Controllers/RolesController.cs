using AutoMapper;
using HospitalApi.DTOs;
using HospitalApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalApi.Controllers
{
    [Authorize]
    public class RolesController : BaseApiController
    {
        private readonly RoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public RolesController(RoleRepository roleRepository, IMapper mapper)
            : base()
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleDto>>> GetAllRoles()
        {
            var roles = await _roleRepository.GetRoles();
            return Ok(_mapper.Map<IEnumerable<RoleDto>>(roles));
        }
    }
}

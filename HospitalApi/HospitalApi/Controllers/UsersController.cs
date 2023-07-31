using AutoMapper;
using HospitalApi.DTOs;
using HospitalApi.Models;
using HospitalApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalApi.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly UserRepository _userRepository;
        private readonly HospitalRepository _hospitalRepository;
        private readonly IMapper _mapper;

        public UsersController(UserRepository repository, HospitalRepository hospitalRepository, IMapper mapper)
            : base()
        {
            _userRepository = repository;
            _hospitalRepository = hospitalRepository;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
        {
            var users = await _userRepository.GetUsers(true);
            return Ok(_mapper.Map<IEnumerable<UserDto>>(users));
        }

        [AllowAnonymous]
        [HttpGet("Doctors")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetDoctors()
        {
            var result = new List<DoctorDto>();
            var doctors = await _userRepository.GetUserByRole("Doctor");
            var hospitals = await _hospitalRepository.GetHospitals();

            foreach (var doctor in doctors)
            {
                doctor.Hospital = hospitals.FirstOrDefault(h => h.HospitalId == doctor.HospitalId);
                result.Add(_mapper.Map<DoctorDto>(doctor));
            }

            return Ok(result);
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            await _userRepository.AddUser(user);
            return Ok();
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<UserDto>> GetUser(string userName)
        {
            User user = await _userRepository.GetUserByUsername(userName);
            return _mapper.Map<UserDto>(user);
        }
    }
}

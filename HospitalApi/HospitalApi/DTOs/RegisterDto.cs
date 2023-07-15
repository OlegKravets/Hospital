using System.ComponentModel.DataAnnotations;

namespace HospitalApi.DTOs
{
    public class RegisterDto : LoginDto
    {
        [Range(0, 150, ErrorMessage = "Age is invalid!")]
        public int Age { get; set; }

        public int RoleId { get; set; }
    }
}

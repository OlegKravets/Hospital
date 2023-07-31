using API.Entities;

namespace HospitalApi.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public int Age { get; set; }

        public decimal Salary { get; set; }

        public int HospitalId { get; set; }

        public string Token { get; set; }

        public Photo Photo { get; set; }
    }
}

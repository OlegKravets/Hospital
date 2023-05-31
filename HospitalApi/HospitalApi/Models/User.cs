using System.ComponentModel.DataAnnotations;

namespace HospitalApi.Models
{
    public class User
    {
        [Required]
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public byte[]? PasswordHash { get; set; }

        public byte[]? PasswordSalt { get; set; }

        public decimal Salary { get; set; }
    }
}

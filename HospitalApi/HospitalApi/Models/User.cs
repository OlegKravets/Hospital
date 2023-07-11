namespace HospitalApi.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public byte[]? PasswordHash { get; set; }

        public byte[]? PasswordSalt { get; set; }

        public decimal Salary { get; set; }

        public int HospitalId { get; set; }

        public List<UserRole> UserRoles { get; set; }

        public Hospital Hospital { get; set; }
    }
}

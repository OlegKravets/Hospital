using System.ComponentModel.DataAnnotations;

namespace HospitalApi.Models
{
    public class Doctor
    {
        [Required]
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public decimal Salary { get; set; }
    }
}

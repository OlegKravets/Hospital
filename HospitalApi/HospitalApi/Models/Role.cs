using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalApi.Models
{
    public class Role
    {
        public int Id { get; set; }

        public string RoleName { get; set; }

        public List<UserRole> UserRoles { get; set; }
    }
}

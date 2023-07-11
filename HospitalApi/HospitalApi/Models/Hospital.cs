namespace HospitalApi.Models
{
    public class Hospital
    {
        public int HospitalId { get; set; }

        public string HospitalName { get; set; }

        public string Address { get; set; }

        public List<User> Doctors;
    }
}

namespace HospitalApi.Scripts
{
    public static class HospitalScripts
    {
        public const string InsertHospital = "INSERT INTO Hospitals(HospitalName, Address) VALUES(@HospitalName, @Address);";
        public const string AnyHospital = "SELECT COUNT(1) FROM Hospitals;";
        public const string SelectAllHospital = "SELECT * FROM Hospitals;";
    }
}

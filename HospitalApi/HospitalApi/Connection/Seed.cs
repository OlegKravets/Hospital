using HospitalApi.Models;
using HospitalApi.Repositories;
using System.Security.Cryptography;
using System.Text;

namespace HospitalApi
{
    public static class Seed
    {
        private static int _currentUserId = 1;

        public static async Task SeedRoles(RoleRepository roleRepository)
        {
            if (await roleRepository.Any())
            {
                return;
            }

            var roles = new List<Role>
            {
                new Role { Id = 1, RoleName = "Doctor" },
                new Role { Id = 2, RoleName = "Patient" },
                new Role { Id = 3, RoleName = "Administrator" }
            };

            await roleRepository.AddRoles(roles);
        }

        public static async Task SeedUsers(UserRepository userRepository, HospitalRepository hospitalRepository)
        {
            if (await userRepository.Any())
            {
                return;
            }

            await CreateDoctors(userRepository, hospitalRepository);
            await CreateAdministrators(userRepository);
            await CreatePatients(userRepository);
        }

        public static async Task SeedHospitals(HospitalRepository hospitalRepository)
        {
            if (await hospitalRepository.Any())
            {
                return;
            }

            var hospitals = new List<Hospital>
            {
                new Hospital
                {
                    HospitalId = 1,
                    HospitalName = "St. John",
                    Address = "632 Greenwood st, BC, Canada"
                },
                new Hospital
                {
                    HospitalId = 2,
                    HospitalName = "Medical Ostan",
                    Address = "765 Hostweel ave, BC, Canada"
                }
            };

            await hospitalRepository.AddHospitals(hospitals);
        }

        private static async Task CreateDoctors(UserRepository userRepository, HospitalRepository hospitalRepository)
        {
            using var hmac = new HMACSHA512();

            var doctors = new List<User>();

            var hospitals = await hospitalRepository.GetHospitals();
            var rand = new Random();

            doctors.Add(
                new User
                {
                    Id = _currentUserId++,
                    Name = "Mike",
                    Age = 40,
                    Salary = 70,
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("mike")),
                    PasswordSalt = hmac.Key,
                    HospitalId = hospitals.ElementAt(rand.Next(0, hospitals.Count())).HospitalId
                });

            doctors.Add(
                new User
                {
                    Id = _currentUserId++,
                    Name = "Alan",
                    Age = 34,
                    Salary = 50,
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("alan")),
                    PasswordSalt = hmac.Key,
                    HospitalId = hospitals.ElementAt(rand.Next(0, hospitals.Count())).HospitalId
                });

            doctors.Add(
                new User
                {
                    Id = _currentUserId++,
                    Name = "Ella",
                    Age = 18,
                    Salary = 26,
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("ella")),
                    PasswordSalt = hmac.Key,
                    HospitalId = hospitals.ElementAt(rand.Next(0, hospitals.Count())).HospitalId
                });

            await userRepository.AddUsers(doctors);
        }

        private static async Task CreateAdministrators(UserRepository userRepository)
        {
            using var hmac = new HMACSHA512();

            var admins = new List<User>();

            admins.Add(
                new User
                {
                    Id = _currentUserId++,
                    Name = "admin",
                    Age = 30,
                    Salary = 50,
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("admin")),
                    PasswordSalt = hmac.Key
                });

            admins.Add(
                new User
                {
                    Id = _currentUserId++,
                    Name = "admin1",
                    Age = 33,
                    Salary = 40,
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("admin2")),
                    PasswordSalt = hmac.Key
                });

            await userRepository.AddUsers(admins);
        }

        private static async Task CreatePatients(UserRepository userRepository)
        {
            using var hmac = new HMACSHA512();

            var patients = new List<User>();

            patients.Add(
                new User
                {
                    Id = _currentUserId++,
                    Name = "Lora",
                    Age = 18,
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("lora")),
                    PasswordSalt = hmac.Key
                });

            patients.Add(
                new User
                {
                    Id = _currentUserId++,
                    Name = "John",
                    Age = 76,
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("john")),
                    PasswordSalt = hmac.Key
                });

            patients.Add(
                new User
                {
                    Id = _currentUserId++,
                    Name = "Iren",
                    Age = 51,
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("iren")),
                    PasswordSalt = hmac.Key
                });

            patients.Add(
                new User
                {
                    Id = _currentUserId++,
                    Name = "Lisa",
                    Age = 39,
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("lisa")),
                    PasswordSalt = hmac.Key
                });

            await userRepository.AddUsers(patients);
        }
    }
}

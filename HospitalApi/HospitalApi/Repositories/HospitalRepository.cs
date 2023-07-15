using Dapper;
using HospitalApi.Connection;
using HospitalApi.Models;
using HospitalApi.Scripts;

namespace HospitalApi.Repositories
{
    public class HospitalRepository : RepositoryBase<Hospital>
    {
        private const string HospitalIdParam = "@HospitalId";
        public HospitalRepository(HospitalConnection connection)
            : base(connection)
        {
        }

        protected override string AnyScript => HospitalScripts.AnyHospital;

        public async Task AddHospital(Hospital hospital)
        {
            await ExecuteQuery(HospitalScripts.InsertHospital, hospital);
        }

        public async Task AddHospitals(IEnumerable<Hospital> hospitals)
        {
            foreach (var hospital in hospitals)
            {
                await AddHospital(hospital);
            }
        }

        public async Task<IEnumerable<Hospital>> GetHospitals()
        {
            return await GetData(HospitalScripts.SelectAllHospital);
        }

        public async Task<Hospital> GetHospitalById(int id)
        {
            return await GetFirstOrDefault(HospitalScripts.SelectHospitalById, new { HospitalIdParam, id } );
        }
    }
}

using Dapper;
using HospitalApi.Connection;
using HospitalApi.Models;
using HospitalApi.Scripts;

namespace HospitalApi.Repositories
{
    public class HospitalRepository : RepositoryBase<Hospital>
    {
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
    }
}

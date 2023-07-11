namespace HospitalApi.Connection
{
    public class HospitalConnection
    {
        public HospitalConnection(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString(Constants.DbConnectionKey);
        }

        public string ConnectionString { get; }
    }
}

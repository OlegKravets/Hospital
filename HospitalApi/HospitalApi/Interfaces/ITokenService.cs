using HospitalApi.Models;

namespace HospitalApi.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}

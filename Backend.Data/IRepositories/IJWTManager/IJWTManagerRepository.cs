using Backend.Data.Entities.tokens;
using Backend.Data.Entities;
using Backend.Application.DataTransferObjects.Register;
using System.Threading.Tasks;

namespace Backend.Data.IRepositories.IJWTManager
{
    public interface IJWTManagerRepository
    {
        Token Authenticate(users user);

        Task<Backend.Data.Entities.registration.Register> registration(RegisterDto reg);
    }
}

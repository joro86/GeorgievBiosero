using Biosero.Service.Models;

namespace Biosero.Api.Utilities
{
    public interface IJwtHandler
    {
        string GenerateToken(UserDto user);
    }
}

using Showtime.Core.Enums;

namespace Showtime.Core.Interfaces.Services.Common
{
    public interface IAuthService
    {
        string? CreateToken(string id, SystemRoles role);
    }
}

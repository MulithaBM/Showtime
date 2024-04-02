using MediatR;
using Showtime.Application.Models.Tenant;
using Showtime.Core.Models;

namespace Showtime.Application.Commands.Tenant
{
    public record TenantRegisterCommand(TenantRegisterRequest TenantRegister) : IRequest<ServiceResponse<string?>>
    {
    }
}

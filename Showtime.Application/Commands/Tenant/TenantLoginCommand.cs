using MediatR;
using Showtime.Application.Models.Tenant;
using Showtime.Core.Models;

namespace Showtime.Application.Commands.Tenant
{
    public record TenantLoginCommand(TenantLoginRequest TenantLogin) : IRequest<ServiceResponse<string?>>
    {
    }
}

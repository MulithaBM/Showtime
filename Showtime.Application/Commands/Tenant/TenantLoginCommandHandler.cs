using MediatR;
using Showtime.Core.Interfaces.Services.Tenant;
using Showtime.Core.Models;

namespace Showtime.Application.Commands.Tenant
{
    public class TenantLoginCommandHandler(ITenantService tenantService) : IRequestHandler<TenantLoginCommand, ServiceResponse<string?>>
    {
        private readonly ITenantService _tenantService = tenantService;

        public async Task<ServiceResponse<string?>> Handle(TenantLoginCommand request, CancellationToken cancellationToken)
        {
            return await _tenantService.Login(
                request.TenantLogin.Email,
                request.TenantLogin.Password);
        }
    }
}

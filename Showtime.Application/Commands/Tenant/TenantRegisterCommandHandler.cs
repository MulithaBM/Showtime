using MediatR;
using Showtime.Core.Interfaces.Services.Tenant;
using Showtime.Core.Models;

namespace Showtime.Application.Commands.Tenant
{
    public class TenantRegisterCommandHandler(ITenantService tenantService) : IRequestHandler<TenantRegisterCommand, ServiceResponse<string?>>
    {
        private readonly ITenantService _tenantService = tenantService;

        public async Task<ServiceResponse<string?>> Handle(TenantRegisterCommand request, CancellationToken cancellationToken)
        {
            return await _tenantService.Register(
                request.TenantRegister.Name, 
                request.TenantRegister.Email, 
                request.TenantRegister.Phone, 
                request.TenantRegister.Password, 
                request.TenantRegister.CustomerCareEmail, 
                request.TenantRegister.CustomerCarePhone);
        }
    }
}

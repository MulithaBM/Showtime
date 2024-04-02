using Microsoft.Extensions.Logging;
using Showtime.Core.Entities;
using Showtime.Core.Enums;
using Showtime.Core.Interfaces.Repository;
using Showtime.Core.Interfaces.Services.Common;
using Showtime.Core.Interfaces.Services.Tenant;
using Showtime.Core.Interfaces.UnitOfWork;
using Showtime.Core.Models;
using Showtime.Core.ValueObjects;
using Showtime.Infrastructure.Repository;
using System.Linq.Expressions;

namespace Showtime.Application.Services.TenantServices
{
    public class TenantService : ITenantService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Tenant> _tenantRepository;
        private readonly IAuthService _authService;
        private readonly ILogger<TenantService> _logger;

        public TenantService(IUnitOfWork unitOfWork, IAuthService authService, ILogger<TenantService> logger)
        {
            _unitOfWork = unitOfWork;
            _tenantRepository = (Repository<Tenant>) _unitOfWork.GetRepository<Tenant>();
            _authService = authService;
            _logger = logger;
        }

        public async Task<ServiceResponse<string?>> Login(string email, string password)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                {
                    throw new Exception("Email and password are required.");
                }

                Tenant? tenant = _tenantRepository.GetAllAsync().Result.FirstOrDefault(t => t.Email == email) ?? throw new Exception("Tenant not found.");

                if (!Password.VerifyPassword(password, tenant.PasswordHash, tenant.PasswordSalt))
                {
                    throw new Exception("Invalid password.");
                }

                string? token = _authService.CreateToken(tenant.Id.ToString(), SystemRoles.Tenant) ?? throw new Exception("Error occured during login");

                return ServiceResponse<string?>.CreateSuccessResponse(token, null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return ServiceResponse<string?>.CreateErrorResponse(null, ex.Message);
            }
        }

        public async Task<ServiceResponse<string?>> Register(
            string name, 
            string email, 
            string phone, 
            string password, 
            string? customerCareEmail,
            string? customerCarePhone)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                {
                    throw new Exception("Name, email and password are required.");
                }

                Expression<Func<Tenant, bool>> emailFilter = t => t.Email == email;

                if (_tenantRepository.Find(filters: [emailFilter], includes: null).Any())
                {
                    throw new Exception("The provided email is already in use.");
                }

                Password pwd= new(password);

                Tenant tenant = new(
                    name, 
                    new Email(email), 
                    new Phone(phone), 
                    pwd.PasswordHash, 
                    pwd.PasswordSalt, 
                    customerCareEmail != null ? new Email(customerCareEmail) : null, 
                    customerCarePhone != null ? new Phone(customerCarePhone) : null);

                await _tenantRepository.InsertAsync(tenant);

                await _unitOfWork.SaveAsync();

                return ServiceResponse<string?>.CreateSuccessResponse(null, null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return ServiceResponse<string?>.CreateErrorResponse(null, ex.Message);
            }
        }
    }
}

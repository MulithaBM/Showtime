using Showtime.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Showtime.Core.Interfaces.Services.Tenant
{
    public interface ITenantService
    {
        Task<ServiceResponse<string?>> Login(string email, string password);
        Task<ServiceResponse<string?>> Register(
            string name,
            string email,
            string phone,
            string password,
            string? customerCareEmail,
            string? customerCarePhone);
    }
}

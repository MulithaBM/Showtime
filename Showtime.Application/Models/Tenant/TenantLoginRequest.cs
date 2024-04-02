namespace Showtime.Application.Models.Tenant
{
    public class TenantLoginRequest
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}

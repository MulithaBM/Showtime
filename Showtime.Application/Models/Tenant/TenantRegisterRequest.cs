namespace Showtime.Application.Models.Tenant
{
    public class TenantRegisterRequest
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public required string Password { get; set; }
        public string? CustomerCareEmail { get; set; } = null;
        public string? CustomerCarePhone { get; set; } = null;
    }
}

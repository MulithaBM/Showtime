using Showtime.Core.ValueObjects;

namespace Showtime.Core.Entities
{
    public class Tenant(
        string name,
        Email email,
        Phone? phone,
        byte[] passwordHash,
        byte[] passwordSalt,
        Email? customerCareEmail,
        Phone? customerCarePhone) : BaseUser(name, email, phone, passwordHash, passwordSalt)
    {
        public Email? CustomerCareEmail { get; set; } = customerCareEmail;
        public Phone? CustomerCarePhone { get; set; } = customerCarePhone;

        // Navigation properties
        public ICollection<Branch>? Branches { get; set; }
    }
}

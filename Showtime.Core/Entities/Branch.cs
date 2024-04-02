using Showtime.Core.ValueObjects;

namespace Showtime.Core.Entities
{
    public class Branch(
        string name,
        Email email,
        Phone? phone,
        byte[] passwordHash,
        byte[] passwordSalt,
        Phone? customerCarePhone,
        string address,
        string city,
        string province,
        string zipCode,
        Guid tenantId) : BaseUser(name, email, phone, passwordHash, passwordSalt)
    {
        public Phone? CustomerCarePhone { get; set; } = customerCarePhone;
        public string Address { get; set; } = address;
        public string City { get; set; } = city;
        public string Province { get; set; } = province;
        public string ZipCode { get; set; } = zipCode;

        // Foreign keys
        public Guid TenantId { get; set; } = tenantId;

        // Navigation properties
        public Tenant Tenant { get; set; }
        public ICollection<Schedule>? Showtimes { get; set; }
    }

}

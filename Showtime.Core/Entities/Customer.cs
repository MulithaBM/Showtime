using Showtime.Core.ValueObjects;

namespace Showtime.Core.Entities
{
    public class Customer(
        string name,
        Email email,
        Phone? phone,
        byte[] passwordHash,
        byte[] passwordSalt) : BaseUser(name, email, phone, passwordHash, passwordSalt)
    {
        // Navigation properties
        public ICollection<Reservation>? Reservations { get; set; }
    }
}

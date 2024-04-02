using Showtime.Core.ValueObjects;

namespace Showtime.Core.Entities
{
    public class Admin(Email email, string passwordHash, string passwordSalt) : BaseEntity
    {
        public Email Email { get; set; } = email;
        public string PasswordHash { get; set; } = passwordHash;
        public string PasswordSalt { get; set; } = passwordSalt;
    }
}

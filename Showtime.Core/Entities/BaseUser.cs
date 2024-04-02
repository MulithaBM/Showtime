using Showtime.Core.ValueObjects;

namespace Showtime.Core.Entities
{
    public class BaseUser(
        string name,
        Email email,
        Phone? phone,
        byte[] passwordHash,
        byte[] passwordSalt
        ) : BaseEntity
    {
        public string Name { get; init; } = name;
        public Email Email { get; init; } = email;
        public Phone? Phone { get; init; } = phone;
        public byte[] PasswordHash { get; init; } = passwordHash;
        public byte[] PasswordSalt { get; init; } = passwordSalt;
    }
}

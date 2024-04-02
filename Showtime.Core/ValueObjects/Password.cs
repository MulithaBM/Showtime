using System.Security.Cryptography;
using System.Text;

namespace Showtime.Core.ValueObjects
{
    public record Password
    {
        public byte[] PasswordHash { get; }
        public byte[] PasswordSalt { get; }

        public Password (string password)
        {
            // Whitespaces?
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException(nameof(password), "Password cannot be null or empty.");
            }

            const int minimumLength = 8;
            if (password.Length < minimumLength)
            {
                throw new ArgumentException($"Password must be at least {minimumLength} characters long.");
            }

            var hasUpperCase = password.Any(char.IsUpper);
            var hasLowerCase = password.Any(char.IsLower);
            var hasDigit = password.Any(char.IsDigit);
            var hasSymbol = password.Any(c => !char.IsLetterOrDigit(c));

            // Separate conditional checks?
            if (!hasUpperCase || !hasLowerCase || !hasDigit || !hasSymbol)
            {
                throw new ArgumentException("Password must contain at least one uppercase letter, lowercase letter, digit, and symbol.");
            }

            HashPassword(password, out byte[] passwordHash, out byte[] passwordSalt);

            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
        }

        private static void HashPassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using HMACSHA512 hmac = new();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        public static bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using HMACSHA512 hmac = new(passwordSalt);
            byte[] computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computeHash.SequenceEqual(passwordHash);
        }

        public override int GetHashCode()
        {
            return PasswordHash.GetHashCode();
        }
    }

}

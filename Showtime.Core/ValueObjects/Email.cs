using System.Text.RegularExpressions;

namespace Showtime.Core.ValueObjects
{
    public record Email
    {
        public string Value { get; private set; }

        private static readonly Regex EmailRegex = new(
            @"^(?("")(""[^""\\]|\\\\.)*""|[\w!#\$%&'*+/=?^`{|}~-]+(?:\.[^""\\]|\\\\.)*)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public Email(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Email address cannot be null, empty, or whitespace.");
            }

            if (!IsValidEmail(value))
            {
                throw new ArgumentException("Invalid email address format.");
            }

            Value = value.Trim().ToLowerInvariant();
        }

        public static implicit operator Email(string value) => new(value);

        public override int GetHashCode()
        {
            return Value != null ? Value.GetHashCode() : 0;
        }

        public override string ToString() => Value;

        private static bool IsValidEmail(string email)
        {
            return EmailRegex.IsMatch(email);
        }
    }
}

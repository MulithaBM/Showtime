using System.Text.RegularExpressions;

namespace Showtime.Core.ValueObjects
{
    // TODO: Update class to allow for international phone numbers with country code
    public record Phone
    {
        public string Number { get; }
        public string CountryCode { get; }

        public Phone(string number, string countryCode = "+94")
        {
            if (string.IsNullOrEmpty(number))
            {
                throw new ArgumentNullException(nameof(number), "Phone number cannot be null or empty.");
            }

            const string pattern = @"^(?:\+?94)?(?:\d{2}\s?\d{7})|(?:\d{9})$"; // @"^\+?[1-9]\d{1,14}$" => Most international formats
            if (!Regex.IsMatch(number, pattern))
            {
                throw new ArgumentException("Invalid phone number format.");
            }

            Number = number;
            CountryCode = countryCode;
        }

        public override int GetHashCode()
        {
            return Number.GetHashCode();
        }

        public override string ToString()
        {
            return Number;
        }
    }

}

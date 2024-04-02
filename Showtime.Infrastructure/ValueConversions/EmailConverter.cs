using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Showtime.Core.ValueObjects;

namespace Showtime.Infrastructure.ValueConversions
{
    public class EmailConverter : ValueConverter<Email, string>
    {
        public EmailConverter() : base(
            e => e.Value,
            v => new Email(v))
        { }
    }
}

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Showtime.Core.ValueObjects;

namespace Showtime.Infrastructure.ValueConversions
{
    public class PhoneConverter : ValueConverter<Phone, string>
    {
    public PhoneConverter() : base(
            e => e.Number,
            v => new Phone(v, "+94"))
        { }
    }
}

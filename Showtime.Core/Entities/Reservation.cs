using Showtime.Core.Enums;

namespace Showtime.Core.Entities
{
    public class Reservation(
        ReservationStatus status,
        Guid showtimeId,
        Guid customerId)
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public ReservationStatus Status { get; set; } = status;
        public Guid ShowtimeId { get; set; } = showtimeId;
        public Guid CustomerId { get; set; } = customerId;

        // Navigation properties
        public Schedule Schedules { get; set; }
        public Customer Customer { get; set; }
        public ICollection<Seat> Seats { get; set; }
    }
}

namespace Showtime.Core.Entities
{
    public class Seat(
        Guid hallId,
        int rowNumber,
        int seatNumber)
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid HallId { get; set; } = hallId;
        public int RowNumber { get; set; } = rowNumber;
        public int SeatNumber { get; set; } = seatNumber;

        // Navigation properties
        public Hall Hall { get; set; }
        public ICollection<Reservation>? Reservations { get; set; }
    }
}

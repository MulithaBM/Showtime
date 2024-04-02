namespace Showtime.Core.Entities
{
    public class Schedule(
        Guid movieId,
        Guid branchId,
        DateOnly date,
        TimeOnly startTime,
        TimeOnly endTime)
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid MovieId { get; set; } = movieId;
        public Guid BranchId { get; set; } = branchId;
        public DateOnly Date { get; set; } = date;
        public TimeOnly StartTime { get; set; } = startTime;
        public TimeOnly EndTime { get; set; } = endTime;

        // Navigation properties
        public Movie Movie { get; set; }
        public Branch Branch { get; set; }
        public ICollection<Reservation>? Reservations { get; set; }
    }
}

namespace Showtime.Core.Entities
{
    public class Movie(
        string title,
        string description,
        DateOnly? releaseDate,
        int runningTime,
        string? trailerUrl,
        string? posterUrl)
    {
        public required Guid Id { get; set; } = Guid.NewGuid();
        public required string Title { get; set; } = title;
        public required string Description { get; set; } = description;
        public DateOnly? ReleaseDate { get; set; } = releaseDate;
        public required int RunningTime { get; set; } = runningTime;
        public string? TrailerUrl { get; set; } = trailerUrl;
        public string? PosterUrl { get; set; } = posterUrl;

        // Navigation properties
        public ICollection<Schedule>? Showtimes { get; set; }
    }
}

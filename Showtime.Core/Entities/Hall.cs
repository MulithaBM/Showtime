namespace Showtime.Core.Entities
{
    public class Hall(
        Guid id, 
        Guid branchId, 
        int number, 
        string? name, 
        string? description)
    {
        public Guid Id { get; set; } = id;
        public Guid BranchId { get; set; } = branchId;
        public int Number { get; set; } = number;
        public string? Name { get; set; } = name;
        public string? Description { get; set; } = description;

        // Navigation properties
        public Branch Branch { get; set; }
        public ICollection<Seat>? Seats { get; set; }
    }
}

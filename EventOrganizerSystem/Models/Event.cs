namespace EventOrganizerSystem.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public int VenueId { get; set; }
        public int OrganizerId { get; set; }
    }
}
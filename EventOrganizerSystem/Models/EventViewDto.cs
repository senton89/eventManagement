namespace EventOrganizerSystem.Models;

public class EventViewDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public string Time { get; set; }
    public string Venue { get; set; }
    public string Organizer { get; set; }
}
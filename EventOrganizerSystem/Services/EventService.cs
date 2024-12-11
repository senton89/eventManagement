using System.Collections.Generic;
using System.Data;
using Npgsql;
using EventOrganizerSystem.Models;

namespace EventOrganizerSystem.Services
{
    public class EventService
    {
        private string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=postgres;Database=event_organizer";

        public void AddEvent(Event newEvent)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("INSERT INTO events (name, event_date, event_time, venue_id, organizer_id) VALUES (@name, @date, @time, @venue_id, @organizer_id)", connection))
                {
                    command.Parameters.AddWithValue("name", newEvent.Name);
                    command.Parameters.AddWithValue("date", newEvent.Date);
                    command.Parameters.AddWithValue("time", newEvent.Time);
                    command.Parameters.AddWithValue("venue_id", newEvent.VenueId);
                    command.Parameters.AddWithValue("organizer_id", newEvent.OrganizerId);
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Event> GetAllEvents()
        {
            var events = new List<Event>();
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("SELECT * FROM events", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        events.Add(new Event
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Date = reader.GetDateTime(2),
                            Time = reader.GetString(3),
                            VenueId = reader.GetInt32(4),
                            OrganizerId = reader.GetInt32(5)
                        });
                    }
                }
            }
            return events;
        }

        public void RemoveEvent(int eventId)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("DELETE FROM events WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("id", eventId);
                    command.ExecuteNonQuery();
                }
            }
        }
        
        public void UpdateEvent(Event updatedEvent)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("UPDATE events SET name = @name, event_date = @date, event_time = @time, venue_id = @venue_id, organizer_id = @organizer_id WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("id", updatedEvent.Id);
                    command.Parameters.AddWithValue("name", updatedEvent.Name);
                    command.Parameters.AddWithValue("date", updatedEvent.Date);
                    command.Parameters.AddWithValue("time", updatedEvent.Time);
                    command.Parameters.AddWithValue("venue_id", updatedEvent.VenueId);
                    command.Parameters.AddWithValue("organizer_id", updatedEvent.OrganizerId);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
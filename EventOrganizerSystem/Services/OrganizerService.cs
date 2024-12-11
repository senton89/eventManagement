using System.Collections.Generic;
using Npgsql;
using EventOrganizerSystem.Models;

namespace EventOrganizerSystem.Services
{
    public class OrganizerService
    {
        private string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=postgres;Database=event_organizer";

        public void AddOrganizer(Organizer newOrganizer)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("INSERT INTO organizers (name, contact_info) VALUES (@name, @contact)", connection))
                {
                    command.Parameters.AddWithValue("name", newOrganizer.Name);
                    command.Parameters.AddWithValue("contact", newOrganizer.ContactInfo);
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Organizer> GetAllOrganizers()
        {
            var organizers = new List<Organizer>();
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("SELECT * FROM organizers", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        organizers.Add(new Organizer
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            ContactInfo = reader.GetString(2)
                        });
                    }
                }
            }
            return organizers;
        }

        public void RemoveOrganizer(int organizerId)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("DELETE FROM organizers WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("id", organizerId);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateOrganizer(Organizer updatedOrganizer)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("UPDATE organizers SET name = @name, contact_info = @contact WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("name", updatedOrganizer.Name);
                    command.Parameters.AddWithValue("contact", updatedOrganizer.ContactInfo);
                    command.Parameters.AddWithValue("id", updatedOrganizer.Id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
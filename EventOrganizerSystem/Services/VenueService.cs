using System.Collections.Generic;
using Npgsql;
using EventOrganizerSystem.Models;

namespace EventOrganizerSystem.Services
{
    public class VenueService
    {
        private string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=postgres;Database=event_organizer";

        public void AddVenue(Venue newVenue)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("INSERT INTO venues (name, address, capacity) VALUES (@name, @address, @capacity)", connection))
                {
                    command.Parameters.AddWithValue("name", newVenue.Name);
                    command.Parameters.AddWithValue("address", newVenue.Address);
                    command.Parameters.AddWithValue("capacity", newVenue.Capacity);
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Venue> GetAllVenues()
        {
            var venues = new List<Venue>();
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("SELECT * FROM venues", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        venues.Add(new Venue
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Address = reader.GetString(2),
                            Capacity = reader.GetInt32(3)
                        });
                    }
                }
            }
            return venues;
        }

        public void RemoveVenue(int venueId)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("DELETE FROM venues WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("id", venueId);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateVenue(Venue updatedVenue)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("UPDATE venues SET name = @name, address = @address, capacity = @capacity WHERE id = @id", connection))
                {
                    command.Parameters.AddWithValue("name", updatedVenue.Name);
                    command.Parameters.AddWithValue("address", updatedVenue.Address);
                    command.Parameters.AddWithValue("capacity", updatedVenue.Capacity);
                    command.Parameters.AddWithValue("id", updatedVenue.Id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
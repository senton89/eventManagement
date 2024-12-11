using System.Collections.ObjectModel;
using EventOrganizerSystem.Models;
using EventOrganizerSystem.Services;

namespace EventOrganizerSystem.ViewModels
{
    public class VenueViewModel
    {
        private VenueService venueService;

        public ObservableCollection<Venue> Venues { get; set; }

        public VenueViewModel()
        {
            venueService = new VenueService();
            Venues = new ObservableCollection<Venue>(venueService.GetAllVenues());
        }

        public void AddVenue(Venue newVenue)
        {
            venueService.AddVenue(newVenue);
            Venues.Add(newVenue);
        }

        public void RemoveVenue(Venue venueToRemove)
        {
            venueService.RemoveVenue(venueToRemove.Id);
            Venues.Remove(venueToRemove);
        }

        public void UpdateVenue(Venue venueToUpdate)
        {
            venueService.UpdateVenue(venueToUpdate);
        }
    }
}
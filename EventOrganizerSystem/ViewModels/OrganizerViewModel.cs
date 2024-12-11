using System.Collections.ObjectModel;
using EventOrganizerSystem.Models;
using EventOrganizerSystem.Services;

namespace EventOrganizerSystem.ViewModels
{
    public class OrganizerViewModel
    {
        private OrganizerService organizerService;

        public ObservableCollection<Organizer> Organizers { get; set; }

        public OrganizerViewModel()
        {
            organizerService = new OrganizerService();
            Organizers = new ObservableCollection<Organizer>(organizerService.GetAllOrganizers());
        }

        public void AddOrganizer(Organizer newOrganizer)
        {
            organizerService.AddOrganizer(newOrganizer);
            Organizers.Add(newOrganizer);
        }

        public void RemoveOrganizer(Organizer organizerToRemove)
        {
            organizerService.RemoveOrganizer(organizerToRemove.Id);
            Organizers.Remove(organizerToRemove);
        }

        public void UpdateOrganizer(Organizer organizerToUpdate)
        {
            organizerService.UpdateOrganizer(organizerToUpdate);
            Organizers = new ObservableCollection<Organizer>(organizerService.GetAllOrganizers());
        }
    }
}
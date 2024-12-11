using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using EventOrganizerSystem.Models;
using EventOrganizerSystem.Services;

namespace EventOrganizerSystem.ViewModels
{
    public class EventViewModel
    {
        private EventService eventService;

        public ObservableCollection<Event> Events { get; set; }

        public EventViewModel()
        {
            eventService = new EventService();
            Events = new ObservableCollection<Event>(eventService.GetAllEvents());
        }

        public void AddEvent(Event newEvent)
        {
            eventService.AddEvent(newEvent);
            Events.Add(newEvent);
        }

        public void RemoveEvent(Event eventToRemove)
        {
            eventService.RemoveEvent(eventToRemove.Id);
            Events.Remove(eventToRemove);
        }

        public void UpdateEvent(Event updatedEvent)
        {
            // Обновление в базе данных
            eventService.UpdateEvent(updatedEvent);
            // Обновление в коллекции
            var index = Events.IndexOf(Events.First(e => e.Id == updatedEvent.Id));
            if (index >= 0)
            {
                Events[index] = updatedEvent;
            }
        }
    }
}
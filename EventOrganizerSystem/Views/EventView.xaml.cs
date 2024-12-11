using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using EventOrganizerSystem.Models;
using EventOrganizerSystem.ViewModels;
using EventOrganizerSystem.Services;

namespace EventOrganizerSystem.Views
{
    public partial class EventView : UserControl
    {
        private EventViewModel viewModel;
        private OrganizerService _organizerService = new();
        private VenueService _venueService = new();
        private List<Organizer> organizers;
        private List<Venue> venues;
        private ObservableCollection<EventViewDto> eventViews;
        private Event selectedEvent; // Для хранения выбранного события

        public EventView()
        {
            InitializeComponent();
            viewModel = new EventViewModel();
            DataContext = viewModel;
            LoadData();
            eventViews = new ObservableCollection<EventViewDto>(viewModel.Events.Select(e => ConvertToEventView(e)));
            EventList.ItemsSource = eventViews;
            // Подписка на событие двойного нажатия
            EventList.MouseDoubleClick += EventList_MouseDoubleClick;
        }

        private void EventList_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (EventList.SelectedItem is EventViewDto selectedEventDto)
            {
                // Найдите событие по Id
                selectedEvent = viewModel.Events.FirstOrDefault(ev => ev.Id == selectedEventDto.Id);
                if (selectedEvent != null)
                {
                    // Заполните поля данными из выбранного события
                    EventName.Text = selectedEvent.Name;
                    EventDate.SelectedDate = selectedEvent.Date;
                    EventTime.Text = selectedEvent.Time;
                    OrganizerComboBox.SelectedValue =
                        organizers.FirstOrDefault(o => o.Id == selectedEvent.OrganizerId)?.Name;
                    VenueComboBox.SelectedValue = venues.FirstOrDefault(v => v.Id == selectedEvent.VenueId)?.Name +
                                                  " at " + venues.FirstOrDefault(v => v.Id == selectedEvent.VenueId)
                                                      ?.Address;
                }
            }
        }

        private EventViewDto ConvertToEventView(Event eventView)
        {
            return new EventViewDto
            {
                Id = eventView.Id,
                Name = eventView.Name,
                Date = eventView.Date,
                Time = eventView.Time,
                Venue = venues.FirstOrDefault(venue => venue.Id == eventView.VenueId)?.Address,
                Organizer = organizers.FirstOrDefault(organizer => organizer.Id == eventView.OrganizerId)?.Name
            };
        }

        private void LoadData()
        {
            // Загрузка организаторов
            organizers = _organizerService.GetAllOrganizers();
            OrganizerComboBox.ItemsSource = organizers.Select(o => o.Name).ToList();

            // Загрузка мест
            venues = _venueService.GetAllVenues();
            VenueComboBox.ItemsSource = venues.Select(v => v.Name + " at " + v.Address).ToList();

            eventViews = new ObservableCollection<EventViewDto>(viewModel.Events.Select(e => ConvertToEventView(e)));
            EventList.ItemsSource = eventViews;
        }

        private void AddEvent_Click(object sender, RoutedEventArgs e)
        {
            if (selectedEvent != null) // Если событие выбрано для обновления
            {
                // Логика обновления события
                selectedEvent.Name = EventName.Text;
                selectedEvent.Date = EventDate.SelectedDate.Value;
                selectedEvent.Time = EventTime.Text;
                selectedEvent.OrganizerId =
                    organizers.FirstOrDefault(o => o.Name == OrganizerComboBox.SelectedValue.ToString()).Id;
                selectedEvent.VenueId = venues.FirstOrDefault(v =>
                    (v.Name + " at " + v.Address) == VenueComboBox.SelectedValue.ToString()).Id;

                viewModel.UpdateEvent(selectedEvent); // Предполагается, что у вас есть метод обновления в ViewModel
                selectedEvent = null; // Сбросить выбранное событие
            }
            else
            {
                // Проверка на пустые поля
                if (string.IsNullOrWhiteSpace(EventName.Text) ||
                    string.IsNullOrWhiteSpace(EventTime.Text) ||
                    string.IsNullOrWhiteSpace(EventDate.Text) ||
                    OrganizerComboBox.SelectedIndex == -1 ||
                    VenueComboBox.SelectedIndex == -1)
                {
                    MessageBox.Show("Please fill in all fields.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                // Логика добавления нового события
                var newEvent = new Event
                {
                    Name = EventName.Text,
                    Date = EventDate.SelectedDate.Value,
                    Time = EventTime.Text,
                    OrganizerId = organizers.FirstOrDefault(o => o.Name == OrganizerComboBox.SelectedValue.ToString())
                        .Id,
                    VenueId = venues.FirstOrDefault(v =>
                        (v.Name + " at " + v.Address) == VenueComboBox.SelectedValue.ToString()).Id
                };
                viewModel.AddEvent(newEvent);
            }

            // Очистка полей после добавления или обновления
            EventName.Clear();
            EventDate.SelectedDate = null;
            EventTime.Clear();
            OrganizerComboBox.SelectedValue = null;
            VenueComboBox.SelectedValue = null;
            LoadData();
            EventList.UpdateLayout();
        }

        private void EventTime_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Проверка на ввод только цифр и двоеточия
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void EventTime_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // Разрешить клавишу Backspace
            if (e.Key == Key.Back)
            {
                e.Handled = false;
            }
        }

        private void EventTime_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Форматирование текста в формате HH:mm
            var textBox = sender as TextBox;
            if (textBox != null)
            {
                string input = textBox.Text.Replace(":", "");
                if (input.Length > 4)
                {
                    input = input.Substring(0, 4);
                }

                if (input.Length >= 2)
                {
                    input = input.Insert(2, ":");
                }

                textBox.Text = input;
                textBox.SelectionStart = textBox.Text.Length; // Установить курсор в конец
            }
        }

        private static bool IsTextAllowed(string text)
        {
            // Разрешить только цифры и двоеточие
            Regex regex = new Regex("[^0-9:]");
            return !regex.IsMatch(text);
        }
        
    }

}
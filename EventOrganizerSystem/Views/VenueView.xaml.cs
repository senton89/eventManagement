using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using EventOrganizerSystem.Models;
using EventOrganizerSystem.ViewModels;

namespace EventOrganizerSystem.Views
{
    public partial class VenueView : UserControl
    {
        private VenueViewModel viewModel;
        private Venue selectedVenue; // Для хранения выбранного места

        public VenueView()
        {
            InitializeComponent();
            viewModel = new VenueViewModel();
            DataContext = viewModel;
            LoadVenues(); // Загрузка мест при инициализации
        }

        private void LoadVenues()
        {
            VenueList.ItemsSource = viewModel.Venues; // Предполагается, что у вас есть метод для получения всех мест
        }
        

        private void AddVenue_Click(object sender, RoutedEventArgs e)
        {
            if (selectedVenue != null) // Если место выбрано для обновления
            {
                // Обновление существующего места
                selectedVenue.Name = VenueName.Text;
                selectedVenue.Address = VenueAddress.Text;
                selectedVenue.Capacity = int.Parse(VenueCapacity.Text);
                viewModel.UpdateVenue(selectedVenue); // Предполагается, что у вас есть метод обновления в ViewModel
                selectedVenue = null; // Сбросить выбранное место
            }
            else
            {
                // Проверка на пустые поля
                if (string.IsNullOrWhiteSpace(VenueName.Text) ||
                    string.IsNullOrWhiteSpace(VenueAddress.Text) ||
                    string.IsNullOrWhiteSpace(VenueCapacity.Text))
                {
                    MessageBox.Show("Please fill in all fields.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                // Логика добавления нового места
                var newVenue = new Venue
                {
                    Name = VenueName.Text,
                    Address = VenueAddress.Text,
                    Capacity = int.Parse(VenueCapacity.Text)
                };
                viewModel.AddVenue(newVenue);
            }

            // Очистка полей после добавления или обновления
            VenueName.Clear();
            VenueAddress.Clear();
            VenueCapacity.Clear();
            LoadVenues(); // Обновление списка мест
        }

        private void VenueList_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (VenueList.SelectedItem is Venue selectedVenueDto)
            {
                // Найдите место по Id
                selectedVenue = viewModel.Venues.FirstOrDefault(v => v.Id == selectedVenueDto.Id);
                if (selectedVenue != null)
                {
                    // Заполните поля данными из выбранного места
                    VenueName.Text = selectedVenue.Name;
                    VenueAddress.Text = selectedVenue.Address;
                    VenueCapacity.Text = selectedVenue.Capacity.ToString();
                }
            }
        }

        private void VenueCapacity_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Регулярное выражение для проверки, является ли вводимое значение числом
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

    }
}
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using EventOrganizerSystem.Models;
using EventOrganizerSystem.ViewModels;

namespace EventOrganizerSystem.Views
{
    public partial class OrganizerView : UserControl
    {
        private OrganizerViewModel viewModel;
        private ObservableCollection<Organizer> organizers; // Для хранения списка организаторов
        private Organizer selectedOrganizer; // Для хранения выбранного организатора

        public OrganizerView()
        {
            InitializeComponent();
            viewModel = new OrganizerViewModel();
            DataContext = viewModel;
            LoadData();
            OrganizerList.MouseDoubleClick += OrganizerList_MouseDoubleClick; // Подписка на событие двойного нажатия
        }

        private void LoadData()
        {
            organizers = new ObservableCollection<Organizer>(viewModel.Organizers);
            OrganizerList.ItemsSource = organizers; // Установка источника данных для списка организаторов
        }

        private void OrganizerList_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (OrganizerList.SelectedItem is Organizer selectedOrganizerDto)
            {
                // Найдите организатора по Id
                selectedOrganizer = viewModel.Organizers.FirstOrDefault(o => o.Id == selectedOrganizerDto.Id);
                if (selectedOrganizer != null)
                {
                    // Заполните поля данными из выбранного организатора
                    OrganizerName.Text = selectedOrganizer.Name;
                    OrganizerContact.Text = selectedOrganizer.ContactInfo;
                }
            }
        }

        private void AddOrganizer_Click(object sender, RoutedEventArgs e)
        {
            if (selectedOrganizer != null) // Если организатор выбран для обновления
            {
                // Логика обновления организатора
                selectedOrganizer.Name = OrganizerName.Text;
                selectedOrganizer.ContactInfo = OrganizerContact.Text;

                viewModel.UpdateOrganizer(selectedOrganizer); // Предполагается, что у вас есть метод обновления в ViewModel
                selectedOrganizer = null; // Сбросить выбранного организатора
            }
            else
            {
                // Проверка на пустые поля
                if (string.IsNullOrWhiteSpace(OrganizerContact.Text) ||
                    string.IsNullOrWhiteSpace(OrganizerName.Text))
                {
                    MessageBox.Show("Please fill in all fields.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                // Логика добавления нового организатора
                var newOrganizer = new Organizer
                {
                    Name = OrganizerName.Text,
                    ContactInfo = OrganizerContact.Text
                };
                viewModel.AddOrganizer(newOrganizer);
            }

            // Очистка полей после добавления или обновления
            OrganizerName.Clear();
            OrganizerContact.Clear();
            LoadData(); // Обновление списка организаторов
        }
    }
}
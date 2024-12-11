using System.Windows;
using System.Windows.Controls;

namespace EventOrganizerSystem
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ManageEvents_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new Views.EventView();
        }

        private void ManageOrganizers_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new Views.OrganizerView();
        }

        private void ManageVenues_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new Views.VenueView();
        }
    }
}
// LoginWindow.xaml.cs
using System.Windows;

namespace EventOrganizerSystem.Views
{
    public partial class LoginWindow : Window
    {
        private UserService userService;

        public LoginWindow(string connectionString)
        {
            InitializeComponent();
            userService = new UserService(connectionString);
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            if (userService.ValidateUser (username, password))
            {
                // Успешный вход
                MessageBox.Show("Login successful!");
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                // Ошибка входа
                ErrorMessage.Text = "Invalid username or password.";
            }
        }
        
        // LoginWindow.xaml.cs
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var registerWindow = new RegisterWindow("Host=localhost;Port=5432;Username=postgres;Password=postgres;Database=event_organizer");
            this.Close();
            registerWindow.Show();
        }
    }
}
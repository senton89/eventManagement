// RegisterWindow.xaml.cs
using System.Windows;

namespace EventOrganizerSystem.Views
{
    public partial class RegisterWindow : Window
    {
        private UserService userService;

        public RegisterWindow(string connectionString)
        {
            InitializeComponent();
            userService = new UserService(connectionString);
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            if (userService.RegisterUser (username, password))
            {
                MessageBox.Show("Registration successful!");
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                // Ошибка регистрации
                ErrorMessage.Text = "Registration failed. Username may already exist.";
            }
        }
    }
}
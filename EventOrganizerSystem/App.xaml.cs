using System.Configuration;
using System.Data;
using System.Windows;

namespace EventOrganizerSystem;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=postgres;Database=event_organizer";
        var loginWindow = new Views.LoginWindow(connectionString);
        loginWindow.Show();
    }
}
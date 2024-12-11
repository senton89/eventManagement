using System.Security.Cryptography;
using System.Text;
using Npgsql;

// UserService.cs
public class UserService
{
    private string _connectionString;

    public UserService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public bool ValidateUser (string username, string password)
    {
        string hashedPassword = HashPassword(password);
        
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();
            string query = "SELECT COUNT(*) FROM users WHERE username = @username AND password = @password";

            using (var command = new NpgsqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("username", username);
                command.Parameters.AddWithValue("password", hashedPassword);

                var result = (long)command.ExecuteScalar();
                return result > 0;
            }
        }
    }

    public bool RegisterUser (string username, string password)
    {
        string hashedPassword = HashPassword(password);
        
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            connection.Open();
            string query = "INSERT INTO users (username, password) VALUES (@username, @password)";

            using (var command = new NpgsqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("username", username);
                command.Parameters.AddWithValue("password", hashedPassword);

                try
                {
                    command.ExecuteNonQuery();
                    return true;
                }
                catch (NpgsqlException ex)
                {
                    // Обработка ошибок, например, если пользователь уже существует
                    return false;
                }
            }
        }
    }

    private string HashPassword(string password)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder builder = new StringBuilder();
            foreach (byte b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }
    }
}
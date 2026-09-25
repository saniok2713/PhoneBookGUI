using MySql.Data.MySqlClient;

namespace PhoneBookGUI {
    internal class Database {
        string connectionString = "server=localhost;user=root;password=5623;database=phonebook;";
        public MySqlConnection GetConnection() {
            return new MySqlConnection(connectionString);
        }
    }
}

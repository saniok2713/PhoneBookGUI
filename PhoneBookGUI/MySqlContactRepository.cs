using MySql.Data.MySqlClient;

namespace PhoneBookGUI {
    internal class MySqlContactRepository : IContactRepository {
        private Database db = new Database();
        string query = "";

        public void Add(Contact contact) {
            using (MySqlConnection connection = db.GetConnection()) {
                connection.Open();
                query = "INSERT INTO contacts (user_name, phone_number) VALUES (@name,@phone)";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@name", contact.Name);
                cmd.Parameters.AddWithValue("@phone", contact.Phone);
                cmd.ExecuteNonQuery();

            }
        }

        public void Delete(int id) {
            using (MySqlConnection connection = db.GetConnection()) {
                connection.Open();
                query = "DELETE FROM contacts WHERE id = @id";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public List<Contact> GetAll() {
            List<Contact> contacts = new List<Contact>();
            using (MySqlConnection connection = db.GetConnection()) {
                connection.Open();
                query = "SELECT * FROM contacts";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                using MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()) {
                    Contact contact = new Contact();
                    contact.ID = Convert.ToInt32(reader["id"]);//i can use here Mapper but i will leave for double ways of doing this
                    contact.Name = reader["user_name"].ToString();
                    contact.Phone = reader["phone_number"].ToString();
                    contacts.Add(contact);
                }
            }
            return contacts;
        }

        public Contact GetByID(int id) {
            Contact contact = new Contact();
            using (MySqlConnection connection = db.GetConnection()) {
                connection.Open();
                query = "SELECT * FROM contacts WHERE id = @id";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("id", id);
                using MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()) {
                    contact = MapContact(reader);
                }
            }
            return contact;
        }

        public List<Contact> Search(string name) {
            List<Contact> contacts = new List<Contact>();
            using (MySqlConnection connection = db.GetConnection()) {
                connection.Open();
                query = "SELECT * FROM contacts WHERE user_name LIKE @name";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@name", name + "%");
                using MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()) {
                    contacts.Add(MapContact(reader));
                }
            }
            return contacts;
        }

        public void Update(Contact contact) {
            using (MySqlConnection connection = db.GetConnection()) {
                connection.Open();
                query = "UPDATE contacts SET user_name = @name, phone_number = @number WHERE id= @id";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@name", contact.Name);
                cmd.Parameters.AddWithValue("@number", contact.Phone);
                cmd.Parameters.AddWithValue("@id", contact.ID);
                cmd.ExecuteNonQuery();
            }
        }

        private Contact MapContact(MySqlDataReader reader) {
            return new Contact {
                ID = Convert.ToInt32(reader["ID"]),
                Name = reader["user_name"].ToString(),
                Phone = reader["phone_number"].ToString()
            };

        }
    }
}

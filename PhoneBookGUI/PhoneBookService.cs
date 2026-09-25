using MySql.Data.MySqlClient;

namespace PhoneBookGUI {
    internal class PhoneBookService {

        private MySqlContactRepository cp = new MySqlContactRepository();

        public void AddContact(string name, string phone) {
            Contact contact = new Contact();

            contact.Name = name;
            contact.Phone = phone;

            cp.Add(contact);
        }

        public void DeleteContact(int id) {
            cp.Delete(id);
        }

        public List<Contact> GetContactsList() {
            return cp.GetAll();
        }

        public List<Contact> SearchContactList(string name) {
            return cp.Search(name);
        }

        public void UpdateContact(string name, string phone, int id) {
            Contact contact = new Contact { ID = id, Name = name, Phone = phone };
            cp.Update(contact);
        }

        public Contact SearchContactID(int id) {
            return cp.GetByID(id);
        }
    }
}

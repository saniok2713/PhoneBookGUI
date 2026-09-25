
namespace PhoneBookGUI {
    internal interface IContactRepository {
        void Add(Contact contact);
        void Update(Contact contact);
        void Delete(int id);
        List<Contact> GetAll();
        List<Contact> Search(string name);
        Contact GetByID(int id);
    }
}

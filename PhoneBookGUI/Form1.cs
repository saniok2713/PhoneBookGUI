using System.Drawing.Text;

namespace PhoneBookGUI {
    public partial class Form1 : Form {
        PhoneBookService phoneBookService = new PhoneBookService();
        public Form1() {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e) {
            LoadContacts();
        }


        private void LoadContacts() {
            dgvContacts.DataSource = phoneBookService.GetContactsList();
        }

        private void btnAdd_Click(object sender, EventArgs e) {
            phoneBookService.AddContact(txtName.Text, txtPhone.Text);

            LoadContacts();
            txtName.Clear();
            txtPhone.Clear();
        }

        private void btnDelete_Click(object sender, EventArgs e) {
            int id = Convert.ToInt32(dgvContacts.CurrentRow.Cells["ID"].Value);
            phoneBookService.DeleteContact(id);
            LoadContacts();
        }

        private void btnUpdate_Click(object sender, EventArgs e) {
            int id = Convert.ToInt32(dgvContacts.CurrentRow.Cells["ID"].Value);

            phoneBookService.UpdateContact(txtName.Text, txtPhone.Text, id);
            LoadContacts();

        }

        private void dgvContacts_CellClick(object sender, DataGridViewCellEventArgs e) {
            txtName.Text = dgvContacts.CurrentRow.Cells["Name"].Value.ToString();
            txtPhone.Text = dgvContacts.CurrentRow.Cells["Phone"].Value.ToString();
        }

        private void btnSearch_Click(object sender, EventArgs e) {
            if (string.IsNullOrWhiteSpace(txtName.Text)) {
                LoadContacts();
                return;
            }
            dgvContacts.DataSource = phoneBookService.SearchContactList(txtName.Text);
        }

        private void button1_Click(object sender, EventArgs e) {
            LoadContacts();
        }
    }
}

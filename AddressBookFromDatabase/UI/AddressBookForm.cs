using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AddressBookFromDatabase.DAL;
using AddressBookFromDatabase.Model;

namespace AddressBookFromDatabase
{
    public partial class AddressBookForm : Form
    {
        DataAccess dataAccess = new DataAccess();

        public AddressBookForm()
        {
            InitializeComponent();
            LoadAddressBook();
            AddContactTypeDropdownItems();
        }

        private void AddContactTypeDropdownItems()
        {
            ContactTypeComboBox.Items.Add("Personlig kontakt");
            ContactTypeComboBox.Items.Add("Jobb kontakt");
            ContactTypeComboBox.Items.Add("Övriga kontakter");
        }

        private void ClearText()
        {
            NameTextBox.Clear();
            AddressTextBox.Clear();
            PostalCodeTextBox.Clear();
            CityTextBox.Clear();
            TelephoneTextBox.Clear();
            EmailTextBox.Clear();
            ContactTypeComboBox.SelectedIndex = -1;
        }

        private void LoadAddressBook()
        {
            var contacts = dataAccess.RetrieveContacts();
            AddressBookDataGridView.DataSource = contacts;
        }

        private void AddressBookDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowSelected = AddressBookDataGridView.CurrentCell.RowIndex;

            if (AddressBookDataGridView.Columns[1].HeaderText == "Namn")
            {
                NameTextBox.Text = AddressBookDataGridView.Rows[rowSelected].Cells[1].Value.ToString();
                TelephoneTextBox.Text = AddressBookDataGridView.Rows[rowSelected].Cells[2].Value.ToString();
                EmailTextBox.Text = AddressBookDataGridView.Rows[rowSelected].Cells[3].Value.ToString();
                ContactTypeComboBox.Text = AddressBookDataGridView.Rows[rowSelected].Cells[4].Value.ToString();
            }
            else
            {
                AddressTextBox.Text = AddressBookDataGridView.Rows[rowSelected].Cells[1].Value.ToString();
                PostalCodeTextBox.Text = AddressBookDataGridView.Rows[rowSelected].Cells[2].Value.ToString();
                CityTextBox.Text = AddressBookDataGridView.Rows[rowSelected].Cells[3].Value.ToString();
            }
        }

        private void CreateContactButton_Click(object sender, EventArgs e)
        {
            var newContact = new Contact
            {
                Namn = NameTextBox.Text,
                Telefon = TelephoneTextBox.Text,
                Epost = EmailTextBox.Text,
                KontaktTyp = ContactTypeComboBox.Text
            };

            var newAddress = new Address
            {
                Gatuadress = AddressTextBox.Text,
                Postnummer = PostalCodeTextBox.Text,
                Postort = CityTextBox.Text
            };

            if (String.IsNullOrEmpty(newContact.Namn ?? newContact.Telefon ?? newContact.Epost ?? newContact.KontaktTyp ??
                                     newAddress.Gatuadress ?? newAddress.Postnummer ?? newAddress.Postort))
            {
                MessageBox.Show("Please fill in all fields.");
            }
            else
            {
                dataAccess.CreateContact(newContact, newAddress);
            }

            LoadAddressBook();
            ClearText();
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            var inputName = NameTextBox.Text;
            var inputCity = CityTextBox.Text;
            var inputContactType = ContactTypeComboBox.Text;

            var searchResult = dataAccess.RetrieveAddresses(inputName, inputCity, inputContactType);
            AddressBookDataGridView.DataSource = searchResult;

            ClearText();
        }

        private void UpdateAddressButton_Click(object sender, EventArgs e)
        {
            int id;

            if (AddressBookDataGridView.Columns[1].HeaderText == "Namn")
            {
                id = int.Parse(AddressBookDataGridView.CurrentRow.Cells[0].Value.ToString());
            }
            else
            {
                id = int.Parse(AddressBookDataGridView.CurrentRow.Cells[4].Value.ToString());
            }

            var updateAddress = new Address
            {
                Gatuadress = AddressTextBox.Text,
                Postnummer = PostalCodeTextBox.Text,
                Postort = CityTextBox.Text
            };

            dataAccess.UpdateAddress(id, updateAddress);
            ClearText();
            LoadAddressBook();
        }

        private void DeleteAddressButton_Click(object sender, EventArgs e)
        {
            var id = int.Parse(AddressBookDataGridView.CurrentRow.Cells[0].Value.ToString());

            dataAccess.DeleteAddress(id);
            ClearText();
            LoadAddressBook();
        }
    }
}

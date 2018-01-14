using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBookFromDatabase.Model
{
    class Contact
    {
        public int Id { get; set; }
        public string Namn { get; set; }
        public string Telefon { get; set; }
        public string Epost { get; set; }
        public string KontaktTyp { get; set; }
    }
}

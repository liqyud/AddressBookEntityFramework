﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBookFromDatabase.Model
{
    class Address
    {
        public int AdressId { get; set; }
        public string Gatuadress { get; set; }
        public string Postnummer { get; set; }
        public string Postort { get; set; }
        public Nullable<int> KontaktId { get; set; }
    }
}

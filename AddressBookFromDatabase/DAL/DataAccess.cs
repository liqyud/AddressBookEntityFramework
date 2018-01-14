using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddressBookFromDatabase.Model;

namespace AddressBookFromDatabase.DAL
{
    class DataAccess
    {
        public BindingList<Contact> RetrieveContacts()
        {
            BindingList<Contact> kontakter;

            using (var context = new AddressBookContext())
            {
                var query = from kontakt in context.Kontakts
                            select new Contact()
                            {
                                Id = kontakt.KontaktId,
                                Namn = kontakt.Namn,
                                Telefon = kontakt.Telefon,
                                Epost = kontakt.Epost,
                                KontaktTyp = kontakt.KontaktTyp,
                            };

                kontakter = new BindingList<Contact>(query.ToList());
            }
            return kontakter;
        }

        public BindingList<Address> RetrieveAddresses(string inputName, string inputCity, string inputContactType)
        {
            BindingList<Address> adresser;

            using (var context = new AddressBookContext())
            {
                var query = from kontakt in context.Kontakts
                            join adress in context.Adresses on kontakt.KontaktId equals adress.KontaktId
                            where kontakt.Namn.Contains(inputName) &
                            adress.Postort.Contains(inputCity) &
                            kontakt.KontaktTyp.Contains(inputContactType)
                            select new Address()
                            {
                                AdressId = adress.AdressId,
                                KontaktId = adress.KontaktId,
                                Gatuadress = adress.Gatuadress,
                                Postnummer = adress.Postnummer,
                                Postort = adress.Postort,
                            };
                List<Address> addressFound = query.ToList();
                adresser = new BindingList<Address>(addressFound);
            }
            return adresser;
        }

        public void CreateContact(Contact contact, Address address)
        {
            using (var context = new AddressBookContext())
            {
                var newContact = new Kontakt();
                newContact.Namn = contact.Namn;
                newContact.Telefon = contact.Telefon;
                newContact.Epost = contact.Epost;
                newContact.KontaktTyp = contact.KontaktTyp;

                var newAddress = new Adress();             
                newAddress.Gatuadress = address.Gatuadress;
                newAddress.Postnummer = address.Postnummer;
                newAddress.Postort = address.Postort;
                newAddress.KontaktId = contact.Id;

                context.Kontakts.Add(newContact);
                context.Adresses.Add(newAddress);
                context.SaveChanges();
            }
        }

        public void UpdateAddress(int id, Address address)
        {
            using (var context = new AddressBookContext())
            {

                var updateAddress = context.Adresses.SingleOrDefault(a => a.KontaktId == id);
                if (updateAddress != null)
                {
                    updateAddress.Gatuadress = address.Gatuadress;
                    updateAddress.Postnummer = address.Postnummer;
                    updateAddress.Postort = address.Postort;
                }
                else
                {
                    var newAddress = new Adress();
                    newAddress.Gatuadress = address.Gatuadress;
                    newAddress.Postnummer = address.Postnummer;
                    newAddress.Postort = address.Postort;
                    newAddress.KontaktId = id;

                    context.Adresses.Add(newAddress);
                }
                context.SaveChanges();
            }
        }

        public void DeleteAddress(int Id)
        {
            using (var context = new AddressBookContext())
            {
                var addressToRemove = context.Adresses.SingleOrDefault(a => a.AdressId == Id);

                if (addressToRemove != null)
                {
                    context.Adresses.Remove(addressToRemove);
                }
                context.SaveChanges();
            }
        }
    }
}

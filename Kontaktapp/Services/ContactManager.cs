using System.Collections.Generic;
using Kontaktapp.Models;

namespace Kontaktapp.Services
{
    public class ContactManager
    {
        private readonly IContactStorage _storage;
        private readonly IContactValidator _validator;
        private List<Contact> _contacts;

        public ContactManager(IContactStorage storage, IContactValidator validator)
        {
            _storage = storage;
            _validator = validator;
            _contacts = _storage.LoadContacts();
        }

        public List<Contact> GetAllContacts() => _contacts;

        public (bool Success, List<string> Errors) AddContact(Contact contact)
        {
            var (isValid, errors) = _validator.Validate(contact);

            if (!isValid)
                return (false, errors);

            _contacts.Add(contact);
            _storage.SaveContacts(_contacts);
            return (true, new List<string>());
        }

        public bool DeleteContact(Guid id)
        {
            var contact = _contacts.FirstOrDefault(c => c.Id == id);
            if (contact == null)
                return false;

            _contacts.Remove(contact);
            _storage.SaveContacts(_contacts);
            return true;
        }
    }
}
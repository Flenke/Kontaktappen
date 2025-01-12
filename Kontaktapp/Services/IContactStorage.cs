using System.Collections.Generic;
using Kontaktapp.Models;

namespace Kontaktapp.Services
{
    public interface IContactStorage
    {
        List<Contact> LoadContacts();
        void SaveContacts(List<Contact> contacts);
    }
}
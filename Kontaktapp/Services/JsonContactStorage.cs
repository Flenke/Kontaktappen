using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Kontaktapp.Models;

namespace Kontaktapp.Services
{
    public class JsonContactStorage : IContactStorage
    {
        private readonly string _filePath = "contacts.json";

        public List<Contact> LoadContacts()
        {
            if (!File.Exists(_filePath))
                return new List<Contact>();

            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Contact>>(json) ?? new List<Contact>();
        }

        public void SaveContacts(List<Contact> contacts)
        {
            var json = JsonSerializer.Serialize(contacts, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }
    }
}
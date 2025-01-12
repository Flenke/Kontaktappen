using System;
using System.Linq;
using Kontaktapp.Models;
using Kontaktapp.Services;

namespace Kontaktapp.UI
{
    public class ConsoleUI
    {
        private readonly ContactManager _contactManager;

        public ConsoleUI(ContactManager contactManager)
        {
            _contactManager = contactManager;
        }

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                ShowMenu();
                var choice = Console.ReadLine();

                Console.Clear();

                switch (choice)
                {
                    case "1":
                        ShowContacts();
                        break;
                    case "2":
                        AddContact();
                        break;
                    case "3":
                        DeleteContact();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Ogiltigt val. Försök igen.");
                        break;
                }

                Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
                Console.ReadKey();
            }
        }

        private void ShowMenu()
        {
            Console.WriteLine("=== Kontaktlista ===");
            Console.WriteLine("1. Visa alla kontakter");
            Console.WriteLine("2. Lägg till ny kontakt");
            Console.WriteLine("3. Ta bort kontakt");
            Console.WriteLine("4. Avsluta");
            Console.Write("Välj ett alternativ: ");
        }

        private void ShowContacts()
        {
            var contacts = _contactManager.GetAllContacts();
            if (!contacts.Any())
            {
                Console.WriteLine("Inga kontakter finns sparade.");
                return;
            }

            Console.WriteLine("=== Sparade Kontakter ===");
            foreach (var contact in contacts)
            {
                Console.WriteLine("\n---------------------------");
                Console.WriteLine($"ID: {contact.Id}");
                Console.WriteLine($"Namn: {contact.FirstName} {contact.LastName}");
                Console.WriteLine($"E-post: {contact.Email}");
                Console.WriteLine($"Telefon: {contact.PhoneNumber}");
                Console.WriteLine($"Adress: {contact.StreetAddress}");
                Console.WriteLine($"Postnummer: {contact.PostalCode}");
                Console.WriteLine($"Ort: {contact.City}");
                Console.WriteLine("---------------------------");
            }
        }

        private void AddContact()
        {
            Console.WriteLine("=== Lägg till ny kontakt ===\n");
            var contact = new Contact();

            Console.Write("Förnamn: ");
            contact.FirstName = Console.ReadLine() ?? string.Empty;

            Console.Write("Efternamn: ");
            contact.LastName = Console.ReadLine() ?? string.Empty;

            Console.Write("E-post: ");
            contact.Email = Console.ReadLine() ?? string.Empty;

            Console.Write("Telefonnummer: ");
            contact.PhoneNumber = Console.ReadLine() ?? string.Empty;

            Console.Write("Gatuadress: ");
            contact.StreetAddress = Console.ReadLine() ?? string.Empty;

            Console.Write("Postnummer: ");
            contact.PostalCode = Console.ReadLine() ?? string.Empty;

            Console.Write("Ort: ");
            contact.City = Console.ReadLine() ?? string.Empty;

            var (success, errors) = _contactManager.AddContact(contact);

            if (success)
            {
                Console.WriteLine("\nKontakten har lagts till!");
            }
            else
            {
                Console.WriteLine("\nFel vid tillägg av kontakt:");
                foreach (var error in errors)
                {
                    Console.WriteLine($"- {error}");
                }
            }
        }

        private void DeleteContact()
        {
            Console.WriteLine("=== Ta bort kontakt ===\n");

            var contacts = _contactManager.GetAllContacts();
            if (!contacts.Any())
            {
                Console.WriteLine("Inga kontakter finns sparade.");
                return;
            }

            // Visa alla kontakter med index för enklare val
            for (int i = 0; i < contacts.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {contacts[i].FirstName} {contacts[i].LastName} ({contacts[i].Email})");
            }

            Console.Write("\nAnge nummer på kontakten du vill ta bort (eller tryck Enter för att avbryta): ");
            var input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Borttagning avbruten.");
                return;
            }

            if (int.TryParse(input, out int index) && index > 0 && index <= contacts.Count)
            {
                var contactToDelete = contacts[index - 1];
                if (_contactManager.DeleteContact(contactToDelete.Id))
                {
                    Console.WriteLine($"\nKontakten {contactToDelete.FirstName} {contactToDelete.LastName} har tagits bort.");
                }
                else
                {
                    Console.WriteLine("\nNågot gick fel vid borttagning av kontakten.");
                }
            }
            else
            {
                Console.WriteLine("\nOgiltigt val.");
            }
        }
    }
}
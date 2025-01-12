using System;
using System.Collections.Generic;
using Kontaktapp.Models;

namespace Kontaktapp.Services
{
    public class ContactValidator : IContactValidator
    {
        public (bool IsValid, List<string> Errors) Validate(Contact contact)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(contact.FirstName))
                errors.Add("Förnamn måste anges.");

            if (string.IsNullOrWhiteSpace(contact.LastName))
                errors.Add("Efternamn måste anges.");

            if (string.IsNullOrWhiteSpace(contact.Email) || !contact.Email.Contains("@"))
                errors.Add("En giltig e-postadress måste anges.");

            if (string.IsNullOrWhiteSpace(contact.PhoneNumber))
                errors.Add("Telefonnummer måste anges.");

            if (string.IsNullOrWhiteSpace(contact.StreetAddress))
                errors.Add("Gatuadress måste anges.");

            if (string.IsNullOrWhiteSpace(contact.PostalCode))
                errors.Add("Postnummer måste anges.");

            if (string.IsNullOrWhiteSpace(contact.City))
                errors.Add("Ort måste anges.");

            return (errors.Count == 0, errors);
        }
    }
}
using System.Collections.Generic;
using Kontaktapp.Models;

namespace Kontaktapp.Services
{
    public interface IContactValidator
    {
        (bool IsValid, List<string> Errors) Validate(Contact contact);
    }
}
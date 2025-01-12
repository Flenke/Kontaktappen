using System;
using System.Collections.Generic;
using System.Linq;
using Kontaktapp.Models;
using Kontaktapp.Services;
using Kontaktapp.UI;

namespace Kontaktapp
{
    class Program
    {
        static void Main(string[] args)
        {
            var storage = new JsonContactStorage();
            var validator = new ContactValidator();
            var contactManager = new ContactManager(storage, validator);
            var ui = new ConsoleUI(contactManager);

            ui.Run();
        }
    }
}
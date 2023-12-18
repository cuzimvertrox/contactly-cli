using System;
using System.Collections.Generic;
using System.IO;
using contactly_cli.Functions;

namespace contactly_cli.UI
{
    public class AdressbookUI
    {
        private static readonly List<string> addressBookLogoLines = new List<string>
        {
            "",
            "   _       _                   _                _     ",
            "  /_\\   __| |_ __ ___  ___ ___| |__  _   _  ___| |__  ",
            " //_\\\\ / _` | '__/ _ \\/ __/ __| '_ \\| | | |/ __| '_ \\ ",
            "/  _  \\ (_| | | |  __/\\__ \\__ \\ |_) | |_| | (__| | | |",
            "\\_/ \\_/\\__,_|_|  \\___||___/___/_.__/ \\__,_|\\___|_| |_|",
            ""
        };

        public static void ShowAddressBookScreen()
        {
            Console.Clear();
            PrintLines(addressBookLogoLines);
            DisplayContacts();
            ShowMenu();
        }

        private static void PrintLines(List<string> lines)
        {
            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }
        }

        private static void DisplayContacts()
        {
            var contacts = VCFController.ReadContacts();

            if (contacts.Count == 0)
            {
                Console.WriteLine("   Keine Kontakte im Ordner gefunden. Erstellen Sie einen mit 'C'.");
                return;
            }

            Console.WriteLine("   Seite 1/X\n");
            Console.WriteLine("   Nummer\tFirma\t\tName\t\tVorname\t\tEmail");
            Console.WriteLine("   ----------------------------------------------------------------");

            int number = 1;
            foreach (var contact in contacts)
            {
                Console.WriteLine($"   {number}\t\t{contact.Company}\t\t{contact.LastName}\t\t{contact.FirstName}\t\t{contact.Email}");
                number++;
            }
        }

        private static void ShowMenu()
        {
            List<MenuOption> addressBookMenuOptions = new List<MenuOption>
            {
                new MenuOption(ConsoleKey.E, EditContactUI.ShowEditContactScreen, "Bearbeiten"),
                new MenuOption(ConsoleKey.C, CreateContactUI.ShowCreateContactScreen, "Erstellen"),
                new MenuOption(ConsoleKey.RightArrow, NextPage, "Weiter"),
                new MenuOption(ConsoleKey.LeftArrow, PreviousPage, "Zurück"),
                new MenuOption(ConsoleKey.D, CreateContact, "Löschen"),
                new MenuOption(ConsoleKey.X, HomeUI.ShowHomeScreen, "Home")
            };

            AppControlMenu.ShowMenu(addressBookMenuOptions);
        }

        private static void EditContact()
        {
            // Logik für das Bearbeiten eines Kontakts implementieren
        }

        private static void CreateContact()
        {
            // Logik für das Erstellen eines neuen Kontakts implementieren
        }

        private static void NextPage()
        {
            // Logik für die Anzeige der nächsten Seite implementieren
        }

        private static void PreviousPage()
        {
            // Logik für die Anzeige der vorherigen Seite implementieren
        }
    }
}

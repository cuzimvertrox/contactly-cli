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
            "           _                   _                _     ",
            "  __ _  __| |_ __ ___  ___ ___| |__  _   _  ___| |__  ",
            " / _` |/ _` | '__/ _ \\/ __/ __| '_ \\| | | |/ __| '_ \\ ",
            "| (_| | (_| | | |  __/\\__ \\__ \\ |_) | |_| | (__| | | |",
            " \\__,_|\\__,_|_|  \\___||___/___/_.__/ \\__,_|\\___|_| |_|",
            ""
        };


        public static void ShowAddressBookScreen()
        {
            Console.Clear();
            Console.WriteLine("");
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

        private const int ContactsPerPage = 12;
        private static int currentPage = 1;


        private static void DisplayContacts()
        {
            var contacts = VCFController.ReadContacts();
            int totalContacts = contacts.Count;
            int totalPages = (int)Math.Ceiling(totalContacts / (double)ContactsPerPage);

            if (totalContacts == 0)
            {
                Console.WriteLine("   Keine Kontakte im Ordner gefunden. Erstellen Sie einen mit 'C'.");
                return;
            }

            Console.WriteLine($"   Seite {currentPage}/{totalPages}\n");
            Console.WriteLine("   Nummer\tFirma\t\tName\t\tVorname\t\tEmail");
            Console.WriteLine(new string('-', Console.WindowWidth));

            int start = (currentPage - 1) * ContactsPerPage;
            int end = Math.Min(start + ContactsPerPage, totalContacts);

            for (int i = start; i < end; i++)
            {
                var contact = contacts[i];
                Console.WriteLine($"   {i + 1}\t\t{contact.Company}\t\t{contact.LastName}\t\t{contact.FirstName}\t\t{contact.Email}");
            }
        }

        private static void ShowMenu()
        {
            List<MenuOption> addressBookMenuOptions = new List<MenuOption>
            {
                new MenuOption(ConsoleKey.O, OpenContact, "Öffnen"),
                new MenuOption(ConsoleKey.C, CreateContactUI.ShowCreateContactScreen, "Erstellen"),
                new MenuOption(ConsoleKey.RightArrow, () => ChangePage(1), "Weiter"),
                new MenuOption(ConsoleKey.LeftArrow, () => ChangePage(-1), "Zurück"),
                new MenuOption(ConsoleKey.X, HomeUI.ShowHomeScreen, "Home")
            };

            AppControlMenu.ShowMenu(addressBookMenuOptions);
        }

        private static void ChangePage(int direction)
        {
            var contacts = VCFController.ReadContacts();
            int totalPages = (int)Math.Ceiling(contacts.Count / (double)ContactsPerPage);

            currentPage = Math.Max(1, Math.Min(currentPage + direction, totalPages));
            ShowAddressBookScreen();
        }

        private static void OpenContact()
        {
            var contacts = VCFController.ReadContacts();
            Console.Clear();
            PrintLines(addressBookLogoLines);
            DisplayContacts();
            Console.SetCursorPosition(0, Console.WindowHeight - 4);
            Console.WriteLine("Bitte geben Sie die Nummer des zu öffnenden Kontakts ein:");
            int number = AppInputMenuController.ShowInputField<int>();
            number--; // Umwandlung von 1-basierter zu 0-basierter Indexierung

            if (number >= 0 && number < contacts.Count)
            {
                OpenContactUI.ShowContactDetails(contacts[number]);
            }
            else
            {
                Console.Clear();
                PrintLines(addressBookLogoLines);
                Console.WriteLine("Ungültige Nummer. Drücken Sie eine beliebige Taste, um zurückzukehren.");
                Console.ReadKey();
                ShowAddressBookScreen();
            }
        }
    }
}

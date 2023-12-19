/*  Projektname: contactly-cli
 *  Erstellt: 2023-12-17
 * 
 *  Autor(en): Benjamin Kollmer, Samuel Hekler
 *  
 *  Beschreibung der Funktionen dieser Datei:
 *  - Anzeige der Kontaktliste
 *  - Anzeige von Kontakten in Seiten
 *  - Öffnen von Kontakten zur Anzeige von Details
 *  - Anzeige des Adressbuch-Hauptmenüs
 *  - Blättern durch die Kontaktseiten
 */

using System;
using System.Collections.Generic;
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

        private const int ContactsPerPage = 12;
        private static int currentPage = 1;

        public static void ShowAddressBookScreen()
        {
            Console.Clear();
            Console.WriteLine("");
            PrintLinesController.PrintLines(addressBookLogoLines);
            DisplayContacts();
            ShowMenu();
        }

        // Diese Methode zeigt das Adressbuch-Hauptmenü an
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

        // Diese Methode zeigt die Liste der Kontakte an
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

        // Diese Methode öffnet einen Kontakt zur Anzeige von Details
        private static void OpenContact()
        {
            var contacts = VCFController.ReadContacts();
            Console.Clear();
            PrintLinesController.PrintLines(addressBookLogoLines);
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
                PrintLinesController.PrintLines(addressBookLogoLines);
                Console.WriteLine("Ungültige Nummer. Drücken Sie eine beliebige Taste, um zurückzukehren.");
                Console.ReadKey();
                ShowAddressBookScreen();
            }
        }

        // Diese Methode ermöglicht das Blättern durch die Kontaktseiten
        private static void ChangePage(int direction)
        {
            var contacts = VCFController.ReadContacts();
            int totalPages = (int)Math.Ceiling(contacts.Count / (double)ContactsPerPage);

            currentPage = Math.Max(1, Math.Min(currentPage + direction, totalPages));
            ShowAddressBookScreen();
        }
    }
}

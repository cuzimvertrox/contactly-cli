/*  Projektname: contactly-cli
 *  Erstellt: 2023-12-16
 * 
 *  Autor(en): Samuel Hekler
 *  
 *  Beschreibung der Funktionen dieser Datei:
 *  - Anzeigen der Details eines Kontakts
 *  - Bereitstellen von Optionen zur Bearbeitung, Löschung und zum Versenden von E-Mails
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using contactly_cli.Functions;

namespace contactly_cli.UI
{
    public class OpenContactUI
    {
        private static readonly List<string> openContactLogo = new List<string>
        {
            " _               _        _    _   ",
            "| | _____  _ __ | |_ __ _| | _| |_ ",
            "| |/ / _ \\| '_ \\| __/ _` | |/ / __|",
            "|   < (_) | | | | || (_| |   <| |_ ",
            "|_|\\_\\___/|_| |_|\\__\\__,_|_|\\_\\__|",
            ""
        };

        // Methode zur Anzeige von Kontaktdetails
        public static void ShowContactDetails(Contact contact)
        {
            Console.Clear();
            Console.WriteLine("\n");
            PrintLinesController.PrintLines(openContactLogo);
            Console.WriteLine($"\tKontaktdetails für {contact.FirstName} {contact.LastName}:\n");
            DisplayContactInfo(contact);
            ShowOptions(contact);
        }

        // Methode zur Anzeige der Menüoptionen
        private static void ShowOptions(Contact contact)
        {
            List<MenuOption> options = new List<MenuOption>
            {
                new MenuOption(ConsoleKey.E, () => EditContactUI.ShowEditContactScreen(contact), "Kontakt bearbeiten"),
                new MenuOption(ConsoleKey.D, () => DeleteContactUI.ShowDeleteContactScreen(contact), "Kontakt löschen"),
                new MenuOption(ConsoleKey.M, () => SendEmail(contact), "E-Mail an Kontakt"),
                new MenuOption(ConsoleKey.X, HomeUI.ShowHomeScreen, "Zurück zum Hauptmenü")
            };
            AppControlMenu.ShowMenu(options);
        }

        // Methode zur Anzeige der Kontaktinformationen
        private static void DisplayContactInfo(Contact contact)
        {
            Console.WriteLine($"\tName: {contact.LastName}");
            Console.WriteLine($"\tVorname: {contact.FirstName}");
            Console.WriteLine($"\tFirma: {contact.Company}");
            Console.WriteLine($"\tE-Mail: {contact.Email}");
            Console.WriteLine($"\tTelefon: {contact.Phone}");
            Console.WriteLine($"\tAdresse: {contact.Address}");
            Console.WriteLine($"\tGeburtstag: {contact.Birthday}\n");
        }

        // Methode zum Senden einer E-Mail an den Kontakt
        private static void SendEmail(Contact contact)
        {
            try
            {
                Process.Start($"mailto:{contact.Email}");
                Console.Clear();
                HomeUI.ShowHomeScreen();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fehler beim Öffnen des E-Mail-Clients: " + ex.Message);
            }
        }
    }
}





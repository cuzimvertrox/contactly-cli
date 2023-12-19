/*  Projektname: contactly-cli
 *  Erstellt: 2023-12-XX
 * 
 *  Autor(en): Tobias Springborn
 *  
 *  Beschreibung der Funktionen dieser Datei:
 *  - Bereitstellung einer Benutzeroberfläche zum Löschen von Kontakten
 *  - Bestätigungsaufforderung, bevor ein Kontakt gelöscht wird
 *  - Rückkehr zum Hauptmenü nach dem Löschvorgang
 */

using System;
using System.Collections.Generic;
using contactly_cli.Functions;

namespace contactly_cli.UI
{
    public class DeleteContactUI
    {
        // Definiert das Logo für die DeleteContactUI
        private static readonly List<string> deleteContactLogo = new List<string>
        {
            "\n",
            " _ _   _          _                ",
            "| (_)_(_)___  ___| |__   ___ _ __  ",
            "| |/ _ \\ __|/ __| '_ \\ / _ \\ '_ \\ ",
            "| | (_) \\__ \\ (__| | | |  __/ | | |",
            "|_|\\___/|___/\\___|_| |_|\\___|_| |_|",
            ""
        };

        // Zeigt das Lösch-Bestätigungsfenster für einen bestimmten Kontakt an
        public static void ShowDeleteContactScreen(Contact contact)
        {
            Console.Clear();
            PrintLinesController.PrintLines(deleteContactLogo);
            Console.WriteLine($"\n\tSind Sie sicher, dass Sie den Kontakt {contact.FirstName} {contact.LastName} löschen möchten? (J/N)");

            // Erfasst die Bestätigung des Benutzers
            Console.SetCursorPosition(0, Console.WindowHeight - 3);
            Console.Write("Bestätigung (J/N): ");
            var input = AppInputMenuController.ShowInputField<string>();

            // Verarbeitet die Bestätigung und löscht den Kontakt oder bricht den Vorgang ab
            if (input.Equals("J", StringComparison.OrdinalIgnoreCase) || input.Equals("Y", StringComparison.OrdinalIgnoreCase))
            {
                VCFController.DeleteContact(contact.FileName);
                Console.Clear();
                PrintLinesController.PrintLines(deleteContactLogo);
                Console.WriteLine($"\n\tKontakt {contact.FirstName} {contact.LastName} wurde erfolgreich gelöscht.");
                ShowOptions();
            }
            else
            {
                Console.Clear();
                PrintLinesController.PrintLines(deleteContactLogo);
                Console.WriteLine("\nLöschvorgang abgebrochen.");
                ShowOptions();
            }

            Console.WriteLine("Drücken Sie eine beliebige Taste, um zurückzukehren.");
            Console.ReadKey();
            HomeUI.ShowHomeScreen();
        }

        // Zeigt Optionen nach dem Löschvorgang
        private static void ShowOptions()
        {
            List<MenuOption> options = new List<MenuOption>
            {
                new MenuOption(ConsoleKey.X, HomeUI.ShowHomeScreen, "Zurück zum Hauptmenü")
            };

            AppControlMenu.ShowMenu(options);
        }
    }
}

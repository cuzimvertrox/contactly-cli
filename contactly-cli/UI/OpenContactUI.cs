using System;
using System.Collections.Generic;
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

        public static void ShowContactDetails(Contact contact)
        {
            Console.Clear();
            Console.WriteLine("\n");
            PrintLines(openContactLogo);
            Console.WriteLine($"\tKontaktdetails für {contact.FirstName} {contact.LastName}:\n");
            Console.WriteLine($"\tName: {contact.LastName}");
            Console.WriteLine($"\tVorname: {contact.FirstName}");
            Console.WriteLine($"\tFirma: {contact.Company}");
            Console.WriteLine($"\tE-Mail: {contact.Email}");
            Console.WriteLine($"\tTelefon: {contact.Phone}");
            Console.WriteLine($"\tAdresse: {contact.Address}");
            Console.WriteLine($"\tGeburtstag: {contact.Birthday}\n");
            ShowReturnToHomeMenu();
        }

        private static void PrintLines(List<string> lines)
        {
            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }
        }

        private static void ShowReturnToHomeMenu()
        {
            List<MenuOption> options = new List<MenuOption>
            {
                new MenuOption(ConsoleKey.X, HomeUI.ShowHomeScreen, "Zurück zum Hauptmenü")
            };

            AppControlMenu.ShowMenu(options);
        }
    }
}

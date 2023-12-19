using System;
using System.Collections.Generic;
using contactly_cli.Functions;

namespace contactly_cli.UI
{
    public class DeleteContactUI
    {

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


        public static void ShowDeleteContactScreen(Contact contact)
        {
            Console.Clear();
            PrintLines(deleteContactLogo);
            Console.WriteLine($"\n\tSind Sie sicher, dass Sie den Kontakt {contact.FirstName} {contact.LastName} löschen möchten? (J/N)");

            Console.SetCursorPosition(0, Console.WindowHeight - 3);
            Console.Write("Bestätigung (J/N): ");
            var input = AppInputMenuController.ShowInputField<string>();

            if (input.Equals("J", StringComparison.OrdinalIgnoreCase) || input.Equals("Y", StringComparison.OrdinalIgnoreCase))
            {
                VCFController.DeleteContact(contact.FileName);
                Console.Clear();
                PrintLines(deleteContactLogo);
                Console.WriteLine($"\n\tKontakt {contact.FirstName} {contact.LastName} wurde erfolgreich gelöscht.");
                ShowOptions();
            }
            else
            {
                Console.Clear();
                PrintLines(deleteContactLogo);
                Console.WriteLine("\nLöschvorgang abgebrochen.");
                ShowOptions();

            }

            Console.WriteLine("Drücken Sie eine beliebige Taste, um zurückzukehren.");
            Console.ReadKey();
            HomeUI.ShowHomeScreen();
        }

        private static void ShowOptions()
        {
            List<MenuOption> options = new List<MenuOption>
            {
                new MenuOption(ConsoleKey.X, HomeUI.ShowHomeScreen, "Zurück zum Hauptmenü")

            };

            AppControlMenu.ShowMenu(options);

        }

        private static void PrintLines(List<string> lines)
        {
            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }
        }
    }
}

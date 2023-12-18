using System;
using System.Collections.Generic;
using contactly_cli.Functions;

namespace contactly_cli.UI
{
    public class EditContactUI
    {
        private static readonly List<string> bearbeitenLogo = new List<string>
        {
            " _                     _          _ _             ",
            "| |__   ___  __ _ _ __| |__   ___(_) |_ ___ _ __  ",
            "| '_ \\ / _ \\/ _` | '__| '_ \\ / _ \\ | __/ _ \\ '_ \\ ",
            "| |_) |  __/ (_| | |  | |_) |  __/ | ||  __/ | | |",
            "|_.__/ \\___|\\__,_|_|  |_.__/ \\___|_|\\__\\___|_| |_|",
            ""
        };

        public static void ShowEditContactScreen()
        {
            Console.Clear();
            PrintLines(bearbeitenLogo);
            Console.WriteLine("\n\tDieses Feature kommt im nächsten Release.\n");

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

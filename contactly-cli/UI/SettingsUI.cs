/*  Projektname: contactly-cli
 *  Erstellt: 2023-12-16
 * 
 *  Autor(en): Tobias Springborn, Benjmain Kollmer
 *  
 *  Beschreibung der Funktionen dieser Datei:
 *  - Anzeigen der Einstellungsoberfläche
 *  - Möglichkeit zur Pfadkonfiguration für Kontaktbuch
 *  - Navigation zurück zum Hauptmenü
 */

using contactly_cli.Functions;
using System;
using System.Collections.Generic;

namespace contactly_cli.UI
{
    public class SettingsUI
    {
        private static readonly List<string> settingsLogoLines = new List<string> {
            // Logo für Einstellungsmenü
            "",
            " _____ _           _       _ _                              ",
            "|  ___(_)         | |     | | |                             ",
            "| |__  _ _ __  ___| |_ ___| | |_   _ _ __   __ _  ___ _ __  ",
            "|  __|| | '_ \\/ __| __/ _ \\ | | | | | '_ \\ / _` |/ _ \\ '_ \\ ",
            "| |___| | | | \\__ \\ ||  __/ | | |_| | | | | (_| |  __/ | | |",
            "\\____/|_|_| |_|___/\\__\\___|_|_|\\__,_|_| |_|\\__, |\\___|_| |_|",
            "                                            __/ |           ",
            "                                           |___/            "
        };

        private static readonly List<string> settingsMessage = new List<string> {
            // Nachricht für Benutzer
            "   In den Einstellungen wird der Pfad für den Ordner gesetzt,",
            "   welcher die VCF Datein beinhaltet."
        };

        // Methode zum Anzeigen des Einstellungsmenüs
        public static void ShowSettingsScreen()
        {
            Console.Clear();
            PrintLinesController.PrintLines(settingsLogoLines);
            PrintLinesController.PrintLines(settingsMessage);
            ShowConfiguredPath();

            List<MenuOption> settingsOptions = new List<MenuOption> {
                new MenuOption(ConsoleKey.P, SetContactBookPath, "Pfad zum Kontaktbuch setzen"),
                new MenuOption(ConsoleKey.X, HomeUI.ShowHomeScreen, "Zurück zum Hauptmenü")
            };

            AppControlMenu.ShowMenu(settingsOptions);
        }

        public static void ShowConfigScreen()
        {
            Console.Clear();
            PrintLinesController.PrintLines(settingsLogoLines);
            PrintLinesController.PrintLines(settingsMessage);
            ShowConfiguredPath();

            List<MenuOption> settingsOptions = new List<MenuOption> {
                new MenuOption(ConsoleKey.P, SetContactBookPath, "Pfad zum Kontaktbuch setzen"),
                new MenuOption(ConsoleKey.X, HomeUI.ShowHomeScreen, "Zurück zum Hauptmenü")
            };

            AppControlMenu.ShowMenu(settingsOptions);
        }

        // Methode zum Anzeigen des konfigurierten Pfads
        public static void ShowConfiguredPath()
        {
            string currentPath = ConfigController.ReadConfig("path");
            Console.WriteLine("   Aktueller Pfad: " + currentPath);
        }

        // Methode zum Setzen des Pfads zum Kontaktbuch
        private static void SetContactBookPath()
        {
            bool validPathEntered = false;

            List<MenuOption> settingsOptions = new List<MenuOption> {
                new MenuOption(ConsoleKey.X, HomeUI.ShowHomeScreen, "Zurück zum Hauptmenü")
            };

            while (!validPathEntered)
            {
                Console.Clear();
                PrintLinesController.PrintLines(settingsLogoLines);
                Console.Write("   Eingabe des neuen Pfads zum Kontaktbuch: ");

                string newPath = AppInputMenuController.ShowInputField<string>();

                if (ConfigController.IsValidPath(newPath))
                {
                    ConfigController.WriteConfig("path", newPath);
                    Console.Clear();
                    PrintLinesController.PrintLines(settingsLogoLines);
                    Console.WriteLine("   Neuer Pfad gespeichert.");
                    ShowConfiguredPath();
                    AppControlMenu.ShowMenu(settingsOptions);


                   /* Console.Clear();
                    PrintLinesController.PrintLines(settingsLogoLines);
                    PrintLinesController.PrintLines(settingsMessage);
                    ShowConfiguredPath();
                   */
                }
                else
                {
                    Console.WriteLine("   Ungültiger Pfad eingegeben. Bitte versuchen Sie es erneut.");
                    Console.ReadKey();
                }
            }
        }
    }
}

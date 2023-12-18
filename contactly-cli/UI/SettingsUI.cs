using contactly_cli.Functions;
using System;
using System.Collections.Generic;

namespace contactly_cli.UI
{
    public class SettingsUI
    {

        private static readonly List<string> settingsLogoLines = new List<string> {
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
            "",
            "   In den Einstellungen wird der Pfad für den Ordner gesetzt,",
            "   welcher die VCF Datein beinhaltet.",
            ""
        };
        public static void ShowConfiguredPath()
        {
            string currentPath = ConfigController.ReadConfig("path");
            Console.WriteLine("   Aktueller Pfad: " + currentPath);

        }
        public static void ShowSettingsScreen()
        {
            Console.Clear();
            PrintLines(settingsLogoLines);
            PrintLines(settingsMessage);
            ShowConfiguredPath();



            // Liste der Einstellungsoptionen erstellen
            List<MenuOption> settingsOptions = new List<MenuOption> {
                new MenuOption(ConsoleKey.P, SetContactBookPath, "Pfad zum Kontaktbuch setzen"),
                new MenuOption(ConsoleKey.X, HomeUI.ShowHomeScreen, "Zurück zum Hauptmenü")
            };

            // Aufrufen des AppControlMenu mit den Einstellungsoptionen
            AppControlMenu.ShowMenu(settingsOptions);
        }

        private static void PrintLines(List<string> lines)
        {
            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }
        }
        private static void SetContactBookPath()
        {
            bool validPathEntered = false;

            while (!validPathEntered)
            {
                Console.Clear();
                PrintLines(settingsLogoLines);
                Console.WriteLine("");
                Console.Write("   Eingabe des neuen Pfads zum Kontaktbuch: ");

                string newPath = AppInputController.ShowInputField<string>(); // Benutzereingabe für den neuen Pfad abrufen

                // Überprüfen, ob der eingegebene Pfad gültig ist
                if (ConfigController.IsValidPath(newPath))
                {
                    // Speichern des neuen Pfads in der Konfiguration
                    ConfigController.WriteConfig("path", newPath);
                    Console.Clear();
                    PrintLines(settingsLogoLines);
                    Console.WriteLine("");
                    Console.WriteLine("   Neuer Pfad gespeichert.");
                    Console.WriteLine("   Drücken Sie eine beliebige Taste, um fortzufahren.");
                    validPathEntered = true;
                    Console.ReadKey(); // Warten, bis eine Taste gedrückt wird

                    // Clear the console and show the updated settings
                    Console.Clear();
                    PrintLines(settingsLogoLines);
                    PrintLines(settingsMessage);
                    ShowConfiguredPath();
                }
                else
                {
                    Console.WriteLine("   Ungültiger Pfad eingegeben. Bitte versuchen Sie es erneut.");
                    Console.WriteLine("   Drücken Sie eine beliebige Taste, um fortzufahren.");
                    Console.ReadKey(); // Warten, bis eine Taste gedrückt wird
                }
            }
        }
    }
}
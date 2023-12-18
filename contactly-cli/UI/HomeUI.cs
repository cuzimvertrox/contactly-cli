using System;
using System.Collections.Generic;
using contactly_cli.Functions;

namespace contactly_cli.UI
{
    public class HomeUI
    {
        private static readonly List<string> logoLines = new List<string> {
            "",
            "                 _             _   _                  _ _ ",
            "  ___ ___  _ __ | |_ __ _  ___| |_| |_   _        ___| (_)",
            " / __/ _ \\| '_ \\| __/ _` |/ __| __| | | | |_____ / __| | |",
            "| (_| (_) | | | | || (_| | (__| |_| | |_| |_____| (__| | |",
            " \\___\\___/|_| |_|\\__\\__,_|\\___|\\__|_|\\__, |      \\___|_|_|",
            "                                      |___/                "
        };

        private static readonly List<string> welcomeMessage = new List<string> {
            "",
            "   Willkommen bei Contacly -",
            "   Der Kontaktverwaltung im Terminal",
            "   Version 1.0",
            ""
        };

        private static readonly List<string> menuOptions = new List<string> {
            "   (key)   Drücken des Buchstabens auf der Tastatur",
            "           Öffnet das entsprechende Menü.",
            "",
            "   ------------------------------",
            "   (A) Adressbuch Öffnen",
            "   (C) Erstelle einen neuen Kontakt",
            "",

        };
        private static readonly List<string> pathWarning = new List<string> {
            "   Hinweis: Der Pfad zu den Kontakten ist noch nicht gesetzt."
        };

        private static readonly List<string> developerInfo = new List<string> {
            "   Entwickelt von: Benjamin Kollmer, Tobias Springborn, Samuel Hekler."
        };

        public static void ShowHomeScreen()
        {
            Console.Clear();
            PrintLines(logoLines);
            PrintLines(welcomeMessage);
            PrintLines(menuOptions);
            ShowPathWarning();
            PrintDeveloperInfoAtBottom();
            ShowMainMenu();
        }

        private static void PrintLines(List<string> lines)
        {
            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }
        }

        private static void ShowPathWarning()
        {
            // Hier können Sie die Logik einfügen, um zu überprüfen, ob der Pfad gesetzt ist.
            // Zum Beispiel: if (Settings.ContactPathIsSet) { ... } else { ... }
            // Für den Moment verwenden wir einen Platzhalter-Text.

            PrintLines(pathWarning);
        }

        private static void PrintDeveloperInfoAtBottom()
        {
            // Berechne, wie viele Zeilen vom unteren Rand entfernt die Entwicklerinfo stehen soll
            // Dies hängt von der Anzahl der Zeilen im Menü ab
            int menuLinesCount = 3; // Ändern Sie dies entsprechend der Anzahl der Zeilen in Ihrem Menü
            int positionFromBottom = menuLinesCount + developerInfo.Count;

            // Setze die Cursorposition
            Console.SetCursorPosition(0, Console.WindowHeight - positionFromBottom);

            // Drucke die Entwicklerinfo
            PrintLines(developerInfo);
        }

        private static void ShowMainMenu()
        {
            // Bauen der Menüleiste
            List<MenuOption> mainMenuOptions = new List<MenuOption> {
                new MenuOption(ConsoleKey.A, AdressbookUI.ShowAddressBookScreen, "Adressbuch öffnen"),
                new MenuOption(ConsoleKey.C, CreateContactUI.ShowCreateContactScreen, "Kontakt erstellen"),
                new MenuOption(ConsoleKey.S, SettingsUI.ShowSettingsScreen, "Einstellungen"),
                new MenuOption(ConsoleKey.X, ExitApplication, "Beenden")
            };

            // Menü aufrufen
            AppControlMenu.ShowMenu(mainMenuOptions);
        }

        // Methode um die Anwendung zu schließen
        private static void ExitApplication()
        {
            Console.WriteLine("Anwendung wird beendet...");
            Environment.Exit(0);
        }
    }
}

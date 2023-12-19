/*  Projektname: contactly-cli
 *  Erstellt: 2023-12-16
 * 
 *  Autor(en): Samuel Hekler
 *  
 *  Beschreibung der Funktionen dieser Datei:
 *  - Anzeigen des Hauptmenüs der Anwendung
 *  - Navigation zu verschiedenen Teilen der Anwendung
 *  - Anzeige von Entwicklerinformationen und Anwendungsversion
 */
using System;
using System.Collections.Generic;
using contactly_cli.Functions;

namespace contactly_cli.UI
{
    public class HomeUI
    {
        // Logo für Hauptmenü
        private static readonly List<string> logoLines = new List<string> {
            "",
            "                 _             _   _                  _ _ ",
            "  ___ ___  _ __ | |_ __ _  ___| |_| |_   _        ___| (_)",
            " / __/ _ \\| '_ \\| __/ _` |/ __| __| | | | |_____ / __| | |",
            "| (_| (_) | | | | || (_| | (__| |_| | |_| |_____| (__| | |",
            " \\___\\___/|_| |_|\\__\\__,_|\\___|\\__|_|\\__, |      \\___|_|_|",
            "                                     |___/                "
        };

        // Willkommensnachricht für Benutzer
        private static readonly List<string> welcomeMessage = new List<string> {

            "",
            "   Willkommen bei Contacly -",
            "   Der Kontaktverwaltung im Terminal",
            "   Version 1.0",
            ""
        };

        // Menüoptionen
        private static readonly List<string> menuOptions = new List<string> {
            "   (key)   Drücken des Buchstabens auf der Tastatur",
            "           Öffnet das entsprechende Menü.",
            "",
            "   ------------------------------",
            "   (A) Adressbuch Öffnen",
            "   (C) Erstelle einen neuen Kontakt",
            "",

        };

        // Warnung, wenn Pfad nicht gesetzt ist
        private static readonly List<string> pathWarning = new List<string> {
            "   Hinweis: Der Pfad zu den Kontakten sollte gesetzt werden,",
            "            um Fehler zu vermeiden."
        };

        // Informationen über Entwickler
        private static readonly List<string> developerInfo = new List<string> {
            "   Entwickelt von: Benjamin Kollmer, Tobias Springborn, Samuel Hekler."
        };

        // Methode zum Anzeigen des Hauptbildschirms
        public static void ShowHomeScreen()
        {
            Console.Clear();
            PrintLinesController.PrintLines(logoLines);
            PrintLinesController.PrintLines(welcomeMessage);
            PrintLinesController.PrintLines(menuOptions);
            PrintLinesController.PrintLines(pathWarning);
            Console.SetCursorPosition(0, Console.WindowHeight - 4);
            PrintLinesController.PrintLines(developerInfo);
            ShowMainMenu();
        }

        // Methode zum Anzeigen des Hauptmenüs
        private static void ShowMainMenu()
        {
            List<MenuOption> mainMenuOptions = new List<MenuOption> {
                new MenuOption(ConsoleKey.A, AdressbookUI.ShowAddressBookScreen, "Adressbuch öffnen"),
                new MenuOption(ConsoleKey.C, CreateContactUI.ShowCreateContactScreen, "Kontakt erstellen"),
                new MenuOption(ConsoleKey.S, SettingsUI.ShowSettingsScreen, "Einstellungen"),
                new MenuOption(ConsoleKey.X, ExitApplication, "Beenden")
            };
            AppControlMenu.ShowMenu(mainMenuOptions);
        }

        // Methode zum Beenden der Anwendung
        private static void ExitApplication()
        {
            Environment.Exit(0);
        }
    }
}
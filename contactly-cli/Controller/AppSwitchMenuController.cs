/*  Projektname: contactly-cli
 *  Erstellt: 2023-12-16
 * 
 *  Autor(en): Benjamin Kollmer
 *  
 *  Beschreibung der Funktionen dieser Datei:
 *  - Definition der MenuOption-Klasse zur Darstellung von Menüoptionen
 *  - Implementierung der AppControlMenu-Klasse zur Anzeige und Verwaltung des Anwendungsmenüs
 *  - Erweiterungsmethode zur Zeichenfolienformatierung in der StringExtensions-Klasse
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace contactly_cli.Functions
{
    public class MenuOption
    {
        public ConsoleKey Key { get; }
        public Action Action { get; }
        public string Description { get; }

        public MenuOption(ConsoleKey key, Action action, string description = "")
        {
            Key = key;
            Action = action;
            Description = description;
        }
    }

    public class AppControlMenu
    {
        public static void ShowMenu(List<MenuOption> options)
        {
            // Cursor ausblenden
            Console.CursorVisible = false;

            RenderMenu(options);

            while (true)
            {
                ConsoleKeyInfo keyPress = Console.ReadKey(true);

                // Überprüfen, ob der gedrückte Schlüssel einer der Menüoptionen entspricht
                var matchedOption = options.FirstOrDefault(option => option.Key == keyPress.Key);

                if (matchedOption != null)
                {
                    Console.Clear();
                    matchedOption.Action();

                    // Menü neu rendern nach Ausführung einer Aktion
                    RenderMenu(options);
                }
            }
        }

        // Die Methode RenderMenu wird verwendet, um das Anwendungsmenü auf der Konsole anzuzeigen.
        // Sie akzeptiert eine Liste von Menüoptionen und rendert diese entsprechend auf der Konsole.
        private static void RenderMenu(List<MenuOption> options)
        {
            int consoleWidth, menuWidth;
            StringBuilder menuLine, borderLine;

            consoleWidth = Console.WindowWidth;
            menuWidth = consoleWidth / options.Count;
            menuLine = new StringBuilder();
            borderLine = new StringBuilder();

            // Erstellen der Rahmenlinie
            borderLine.Append(new string('-', consoleWidth));

            // Hinzufügen jedes Menüelements zur Menüzeile
            foreach (var option in options)
            {
                string menuItem, paddedMenuItem;

                menuItem = $"({option.Key}) {option.Description}";
                paddedMenuItem = menuItem.PadBoth(menuWidth - 2);
                menuLine.Append(paddedMenuItem).Append("|");
            }

            // Letzten senkrechten Strich am Ende der Menüzeile entfernen
            if (menuLine.Length > 0)
            {
                menuLine.Remove(menuLine.Length - 1, 1);
            }

            // Cursorposition für das Menü einstellen
            Console.SetCursorPosition(0, Console.WindowHeight - 2);
            Console.WriteLine(borderLine.ToString());
            Console.SetCursorPosition(0, Console.WindowHeight - 1);
            Console.WriteLine(menuLine.ToString());
        }

    }

    // Diese statische Klasse enthält eine Erweiterungsmethode für Zeichenfolien, um sie zu formatieren.
    public static class StringExtensions
    {
        // Die Methode PadBoth fügt Leerzeichen zu einer Zeichenfolie hinzu, um sie auf die angegebene Länge zu zentrieren.
        // Sie berechnet die Anzahl der Leerzeichen, die links und rechts hinzugefügt werden müssen, um das gewünschte Längenlimit zu erreichen.
        // Diese Methode ist nützlich, um Menüoptionen im Anwendungsmenü gleichmäßig zu verteilen.
        public static string PadBoth(this string str, int length)
        {
            int spaces, padLeft;

            spaces = length - str.Length;
            padLeft = spaces / 2 + str.Length;
            return str.PadLeft(padLeft).PadRight(length);
        }
    }
}

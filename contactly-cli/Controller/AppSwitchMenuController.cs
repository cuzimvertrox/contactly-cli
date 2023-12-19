using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contactly_cli.Functions
{
    public class MenuOption
    {
        private ConsoleKey x;
        private object v1;
        private string v2;

        public ConsoleKey Key { get; }
        public Action Action { get; }
        public string Description { get; }

        public MenuOption(ConsoleKey key, Action action, string description = "")
        {
            Key = key;
            Action = action;
            Description = description;
        }

        public MenuOption(ConsoleKey x, object v1, string v2)
        {
            this.x = x;
            this.v1 = v1;
            this.v2 = v2;
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

        private static void HandleMenuInput(List<MenuOption> options)
        {
            ConsoleKeyInfo keyPress;

            keyPress = Console.ReadKey(true);
            foreach (var option in options)
            {
                if (option.Key == keyPress.Key)
                {
                    Console.Clear();
                    option.Action();
                    break;
                }
            }
        }
    }

    public static class StringExtensions
    {
        public static string PadBoth(this string str, int length)
        {
            int spaces, padLeft;

            spaces = length - str.Length;
            padLeft = spaces / 2 + str.Length;
            return str.PadLeft(padLeft).PadRight(length);
        }
    }

}

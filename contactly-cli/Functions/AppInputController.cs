using System;
using System.Text;

namespace contactly_cli.Functions
{
    public class AppInputController
    {
        public static T ShowInputField<T>()
        {
            // Cursor einblenden
            Console.CursorVisible = true;

            string input;
            bool isValidInput;
            T convertedValue = default;

            do
            {
                RenderInputField();
                input = HandleInputField();
                isValidInput = TryConvertInput<T>(input, out convertedValue);

                if (!isValidInput)
                {
                    Console.WriteLine("Ungültige Eingabe. Bitte erneut versuchen.");
                }

            } while (!isValidInput);

            // Cursor ausblenden
            Console.CursorVisible = false;

            // Rückgabe des konvertierten Eingabewerts
            return convertedValue;
        }

        private static void RenderInputField()
        {
            int consoleWidth = Console.WindowWidth;
            StringBuilder borderLine = new StringBuilder(new string('-', consoleWidth));

            Console.SetCursorPosition(0, Console.WindowHeight - 3);
            Console.WriteLine(borderLine.ToString());
            Console.SetCursorPosition(0, Console.WindowHeight - 2);
            Console.Write("Eingabe: ");
        }

        private static string HandleInputField()
        {
            Console.SetCursorPosition("\tEingabe: ".Length, Console.WindowHeight - 2);
            return Console.ReadLine();
        }

        private static bool TryConvertInput<T>(string input, out T result)
        {
            try
            {
                result = (T)Convert.ChangeType(input, typeof(T));
                return true;
            }
            catch
            {
                result = default;
                return false;
            }
        }
    }
}

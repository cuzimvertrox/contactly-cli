/*  Projektname: contactly-cli
 *  Erstellt: 2023-12-14
 * 
 *  Autor(en): Benjamin Kollmer
 *  
 *  Beschreibung der Funktionen dieser Datei:
 *  - Die AppInputMenuController-Klasse ist verantwortlich für die Anzeige eines Eingabefelds auf der Konsole,
 *    die Verarbeitung der Benutzereingabe und die Konvertierung der Eingabe in den gewünschten Datentyp.
 */

using System;
using System.Text;

namespace contactly_cli.Functions
{
    public class AppInputMenuController
    {
        // Diese Klasse ist verantwortlich für die Anzeige eines Eingabefelds auf der Konsole,
        // die Verarbeitung der Benutzereingabe und die Konvertierung der Eingabe in den gewünschten Datentyp.

        // Die Methode ShowInputField<T> zeigt ein Eingabefeld auf der Konsole an und erwartet eine Eingabe vom Benutzer.
        // Sie akzeptiert den generischen Typ 'T' und versucht, die eingegebene Zeichenfolie in diesen Typ umzuwandeln.
        // Wenn die Eingabe gültig ist, wird der konvertierte Wert zurückgegeben.
        public static T ShowInputField<T>()
        {
            // Cursor einblenden, um die Eingabe sichtbar zu machen
            Console.CursorVisible = true;

            string input;
            bool isValidInput;
            T convertedValue = default;

            do
            {
                RenderInputField(); // Rendern des Eingabefelds auf der Konsole
                input = HandleInputField(); // Verarbeiten der Benutzereingabe
                isValidInput = TryConvertInput<T>(input, out convertedValue);

                if (!isValidInput)
                {
                    Console.WriteLine("Ungültige Eingabe. Bitte erneut versuchen.");
                }

            } while (!isValidInput);

            // Cursor ausblenden, um die Eingabe abzuschließen
            Console.CursorVisible = false;

            // Rückgabe des konvertierten Eingabewerts
            return convertedValue;
        }

        // Diese Methode rendert das Eingabefeld auf der Konsole, indem sie eine obere Rahmenlinie und eine Eingabeaufforderung anzeigt.
        private static void RenderInputField()
        {
            int consoleWidth = Console.WindowWidth;
            StringBuilder borderLine = new StringBuilder(new string('-', consoleWidth));

            Console.SetCursorPosition(0, Console.WindowHeight - 3); // Position für die obere Rahmenlinie
            Console.WriteLine(borderLine.ToString());
            Console.SetCursorPosition(0, Console.WindowHeight - 2); // Position für die Eingabeaufforderung
            Console.Write("Eingabe: ");
        }

        // Diese Methode verarbeitet die Benutzereingabe und gibt die eingegebene Zeichenfolie zurück.
        private static string HandleInputField()
        {
            Console.SetCursorPosition("\tEingabe: ".Length, Console.WindowHeight - 2); // Position für die Eingabe
            return Console.ReadLine();
        }

        // Diese Methode versucht, die eingegebene Zeichenfolie in den gewünschten Datentyp 'T' umzuwandeln.
        // Wenn die Konvertierung erfolgreich ist, wird 'true' zurückgegeben, andernfalls 'false'.
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

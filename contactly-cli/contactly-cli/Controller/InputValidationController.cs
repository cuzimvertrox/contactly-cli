/*  Projektname: contactly-cli
 *  Erstellt: 2023-12-18
 * 
 *  Autor(en): Benjamin Kollmer
 *  
 *  Beschreibung der Funktionen dieser Datei:
 *  - Bereitstellung von Methoden zur Validierung von E-Mail-Adressen, Telefonnummern und Geburtstagen
 */

using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace contactly_cli.Functions
{
    // Klasse zur Validierung von Benutzereingaben
    public static class InputValidationController
    {
        // Überprüft, ob eine E-Mail-Adresse gültig ist
        public static bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        // Überprüft, ob eine Telefonnummer gültig ist (mindestens 4 Ziffern)
        public static bool IsValidPhoneNumber(string phone)
        {
            return Regex.IsMatch(phone, @"^\d{4,}$");
        }

        // Überprüft, ob ein Geburtstag gültig ist (TT.MM.JJJJ) und in der Vergangenheit liegt
        public static bool IsValidBirthday(string birthday)
        {
            if (!Regex.IsMatch(birthday, @"^\d{2}\.\d{2}\.\d{4}$"))
            {
                return false;
            }

            if (DateTime.TryParseExact(birthday, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate))
            {
                return parsedDate <= DateTime.Today;
            }

            return false;
        }
    }
}

using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace contactly_cli.Functions
{
    public static class InputValidationController
    {
        public static bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        public static bool IsValidPhoneNumber(string phone)
        {
            return Regex.IsMatch(phone, @"^\d{4,}$");
        }

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

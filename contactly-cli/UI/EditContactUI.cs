/*  Projektname: contactly-cli
 *  Erstellt: 2023-12-19
 * 
 *  Autor(en): Benjamin Kollmer, Samuel Hekler, Tobias Springborn
 *  
 *  Beschreibung der Funktionen dieser Datei:
 *  - Bereitstellung einer Benutzeroberfläche zum Bearbeiten der Kontaktinformationen
 *  - Validierung der Eingaben
 *  - Speichern der aktualisierten Kontaktinformationen
 */

using System;
using System.Collections.Generic;
using contactly_cli.Functions;

namespace contactly_cli.UI
{
    public class EditContactUI
    {
        private static readonly List<string> editContactLogo = new List<string>
        {
            " _                     _          _ _             ",
            "| |__   ___  __ _ _ __| |__   ___(_) |_ ___ _ __  ",
            "| '_ \\ / _ \\/ _` | '__| '_ \\ / _ \\ | __/ _ \\ '_ \\ ",
            "| |_) |  __/ (_| | |  | |_) |  __/ | ||  __/ | | |",
            "|_.__/ \\___|\\__,_|_|  |_.__/ \\___|_|\\__\\___|_| |_|",
            ""
        };

        // Diese Methode zeigt das Bearbeitungsmenü für einen Kontakt an
        public static void ShowEditContactScreen(Contact contact)
        {
            Console.Clear();
            PrintLinesController.PrintLines(editContactLogo);
            Console.WriteLine("\n\tBearbeiten Sie die Kontaktinformationen:\n");

            contact.FirstName = GetInputFromUser("Vorname", ref contact, contact.FirstName);
            contact.LastName = GetInputFromUser("Nachname", ref contact, contact.LastName);
            contact.Company = GetInputFromUser("Firma", ref contact, contact.Company);
            contact.Email = GetInputFromUser("E-Mail", ref contact, contact.Email);
            contact.Phone = GetInputFromUser("Telefon", ref contact, contact.Phone);
            contact.Address = GetInputFromUser("Adresse", ref contact, contact.Address);
            contact.Birthday = GetInputFromUser("Geburtstag", ref contact, contact.Birthday);

            RenderContactScreen(contact);
            ShowSaveOrCancelMenu(contact);
        }

        // Diese Methode erfasst die Benutzereingabe für einen bestimmten Kontaktbereich und validiert sie
        private static string GetInputFromUser(string fieldName, ref Contact contact, string currentValue)
        {
            string input;
            bool isValidInput;
            Func<string, bool> validation = null;

            switch (fieldName)
            {
                case "E-Mail":
                    validation = InputValidationController.IsValidEmail;
                    break;
                case "Telefon":
                    validation = InputValidationController.IsValidPhoneNumber;
                    break;
                case "Geburtstag":
                    validation = InputValidationController.IsValidBirthday;
                    break;
            }

            do
            {
                RenderContactScreen(contact);
                Console.SetCursorPosition(0, Console.WindowHeight - 4);
                Console.WriteLine($"Bitte gib {fieldName} ein (aktuell: {currentValue}): ");
                Console.SetCursorPosition(0, Console.WindowHeight - 1);
                input = AppInputMenuController.ShowInputField<string>();

                isValidInput = string.IsNullOrWhiteSpace(input) || validation == null || validation(input);
                if (!isValidInput)
                {
                    Console.SetCursorPosition(0, Console.WindowHeight - 6);
                    Console.WriteLine($"Ungültige Eingabe für {fieldName}. Bitte erneut versuchen.");
                    Console.ReadKey();
                }
            } while (!isValidInput);

            return string.IsNullOrWhiteSpace(input) ? currentValue : input;
        }

        // Diese Methode zeigt die Kontaktinformationen auf dem Bildschirm an
        private static void RenderContactScreen(Contact contact)
        {
            Console.Clear();
            PrintLinesController.PrintLines(editContactLogo);
            Console.WriteLine("\tBearbeiten Sie die Kontaktinformationen:\n");
            DisplayContactInfo(contact);
        }

        // Diese Methode zeigt die Kontaktinformationen auf dem Bildschirm an
        private static void DisplayContactInfo(Contact contact)
        {
            Console.WriteLine($"\tVorname: {contact.FirstName}");
            Console.WriteLine($"\tNachname: {contact.LastName}");
            Console.WriteLine($"\tFirma: {contact.Company}");
            Console.WriteLine($"\tE-Mail: {contact.Email}");
            Console.WriteLine($"\tTelefon: {contact.Phone}");
            Console.WriteLine($"\tAdresse: {contact.Address}");
            Console.WriteLine($"\tGeburtstag: {contact.Birthday}\n");
        }

        // Diese Methode zeigt das Menü zum Speichern oder Abbrechen der Bearbeitung an
        private static void ShowSaveOrCancelMenu(Contact contact)
        {
            List<MenuOption> options = new List<MenuOption>
            {
                new MenuOption(ConsoleKey.S, () => SaveContact(contact), "Speichern"),
                new MenuOption(ConsoleKey.X, HomeUI.ShowHomeScreen, "Zurück zum Hauptmenü")
            };

            AppControlMenu.ShowMenu(options);
        }

        // Diese Methode speichert die bearbeiteten Kontaktinformationen
        private static void SaveContact(Contact contact)
        {
            VCFController.UpdateContact(contact);
            OpenContactUI.ShowContactDetails(contact);
        }
    }
}

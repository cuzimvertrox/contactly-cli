/*  Projektname: contactly-cli
 *  Erstellt: 2023-12-16
 * 
 *  Autor(en): Benjamin Kollmer, Samuel Hekler
 *  
 *  Beschreibung der Funktionen dieser Datei:
 *  - Bereitstellung einer Benutzeroberfläche zum Erstellen neuer Kontakte
 *  - Validierung der Eingaben
 *  - Speichern des neu erstellten Kontakts
 */

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using contactly_cli.Functions;

namespace contactly_cli.UI
{
    public class CreateContactUI
    {
        private static readonly List<string> createContactLogo = new List<string>
        {
            "               _       _ _            ",
            "  ___ _ __ ___| |_ ___| | | ___ _ __  ",
            " / _ \\ '__/ __| __/ _ \\ | |/ _ \\ '_ \\ ",
            "|  __/ |  \\__ \\ ||  __/ | |  __/ | | |",
            " \\___|_|  |___/\\__\\___|_|_|\\___|_| |_|",
            ""
        };

        // Diese Methode zeigt das Menü zum Erstellen eines neuen Kontakts an
        public static void ShowCreateContactScreen()
        {
            var contact = new Contact();
            RenderContactScreen(contact);

            contact.FirstName = GetInputFromUser("Vorname", ref contact);
            contact.LastName = GetInputFromUser("Nachname", ref contact);
            contact.Company = GetInputFromUser("Firma", ref contact);
            contact.Email = GetInputFromUser("E-Mail", ref contact);
            contact.Phone = GetInputFromUser("Telefon", ref contact);
            contact.Address = GetInputFromUser("Adresse ", ref contact);
            contact.Birthday = GetInputFromUser("Geburtstag", ref contact);

            RenderContactScreen(contact);

            ShowSaveOrCancelMenu(contact);
        }

        // Diese Methode zeigt den Bildschirm zur Bearbeitung des Kontakts an
        private static void RenderContactScreen(Contact contact)
        {
            Console.Clear();
            PrintLinesController.PrintLines(createContactLogo);
            Console.WriteLine("\tLege einen neuen Kontakt in contactly an:\n");
            DisplayContactInfo(contact);
        }

        // Diese Methode erfasst die Benutzereingabe für einen bestimmten Kontaktbereich und validiert sie
        private static string GetInputFromUser(string fieldName, ref Contact contact)
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
                Console.Write($"Bitte gib {fieldName} ein: ");
                Console.SetCursorPosition(0, Console.WindowHeight - 1);
                input = AppInputMenuController.ShowInputField<string>();

                isValidInput = !string.IsNullOrWhiteSpace(input) && (validation == null || validation(input));
                if (!isValidInput)
                {
                    Console.SetCursorPosition(0, Console.WindowHeight - 5);
                    Console.WriteLine($"\tUngültige Eingabe für {fieldName}. Bitte erneut versuchen.");
                    Console.ReadKey();
                }
            } while (!isValidInput);

            return input;
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

        // Diese Methode zeigt das Menü zum Speichern oder Abbrechen des Erstellens eines Kontakts an
        private static void ShowSaveOrCancelMenu(Contact contact)
        {
            List<MenuOption> options = new List<MenuOption>
            {
                new MenuOption(ConsoleKey.S, () => SaveContact(contact), "Speichern"),
                new MenuOption(ConsoleKey.X, HomeUI.ShowHomeScreen, "Zurück zum Hauptmenü")
            };

            AppControlMenu.ShowMenu(options);
        }

        // Diese Methode speichert den neu erstellten Kontakt
        private static void SaveContact(Contact contact)
        {
            List<MenuOption> options = new List<MenuOption>
            {
                new MenuOption(ConsoleKey.C, ShowCreateContactScreen, "Erstellen"),
                new MenuOption(ConsoleKey.X, HomeUI.ShowHomeScreen, "Zurück zum Hauptmenü")
            };

            VCFController.CreateContact(contact);
            Console.Clear();
            Console.WriteLine("\n");
            PrintLinesController.PrintLines(createContactLogo);
            Console.WriteLine("\tKontakt gespeichert.");
            AppControlMenu.ShowMenu(options);
        }
    }
}

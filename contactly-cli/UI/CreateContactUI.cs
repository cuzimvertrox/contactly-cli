﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using contactly_cli.Functions;

namespace contactly_cli.UI
{
    public class CreateContactUI
    {
        private static readonly List<string> createContactLogo = new List<string> {
            "               _       _ _            ",
            "  ___ _ __ ___| |_ ___| | | ___ _ __  ",
            " / _ \\ '__/ __| __/ _ \\ | |/ _ \\ '_ \\ ",
            "|  __/ |  \\__ \\ ||  __/ | |  __/ | | |",
            " \\___|_|  |___/\\__\\___|_|_|\\___|_| |_|",
            ""
        };



        public static void ShowCreateContactScreen()
        {
            var contact = new Contact();
            RenderContactScreen(contact);

            contact.FirstName = GetInputFromUser("Vorname", ref contact);
            contact.LastName = GetInputFromUser("Nachname", ref contact);
            contact.Company = GetInputFromUser("Firma", ref contact);
            contact.Email = GetInputFromUser("E-Mail", ref contact);
            contact.Phone = GetInputFromUser("Telefon", ref contact);
            contact.Address = GetInputFromUser("Adresse", ref contact);
            contact.Birthday = GetInputFromUser("Geburtstag", ref contact);

            RenderContactScreen(contact);


            ShowSaveOrCancelMenu(contact);
        }

        private static void RenderContactScreen(Contact contact)
        {
            Console.Clear();
            PrintLines(createContactLogo);
            Console.WriteLine("\tLege einen neuen Kontakt in contacly an:\n");
            DisplayContactInfo(contact);
        }

        private static void PrintLines(List<string> lines)
        {
            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }
        }


        private static string GetInputFromUser(string fieldName, ref Contact contact)
        {
            string input;
            bool isValidInput;
            Func<string, bool> validation = null;

            switch (fieldName)
            {
                case "E-Mail":
                    validation = IsValidEmail;
                    break;
                case "Telefon":
                    validation = IsValidPhoneNumber;
                    break;
                case "Geburtstag":
                    validation = IsValidBirthday;
                    break;
            }

            do
            {
                RenderContactScreen(contact);
                Console.SetCursorPosition(0, Console.WindowHeight - 4);
                Console.Write($"Bitte gib {fieldName} ein: ");
                Console.SetCursorPosition(0, Console.WindowHeight - 1);
                input = AppInputController.ShowInputField<string>();

                isValidInput = validation == null || validation(input);
                if (!isValidInput)
                {
                    Console.SetCursorPosition(0, Console.WindowHeight - 5);
                    Console.WriteLine($"\tUngültige Eingabe für {fieldName}. Bitte erneut versuchen.");
                    Console.ReadKey(); // Warte auf Benutzereingabe, um die Nachricht zu bestätigen
                }
            } while (!isValidInput);

            return input;
        }


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

        private static void ShowSaveOrCancelMenu(Contact contact)
        {
            List<MenuOption> options = new List<MenuOption>
            {
                new MenuOption(ConsoleKey.S, () => SaveContact(contact), "Speichern"),
                new MenuOption(ConsoleKey.X, HomeUI.ShowHomeScreen, "Zurück zum Hauptmenü")
            };

            AppControlMenu.ShowMenu(options);
        }

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
            PrintLines(createContactLogo);
            Console.WriteLine("\tKontakt gespeichert.");
            AppControlMenu.ShowMenu(options);

        }

        private static bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        private static bool IsValidPhoneNumber(string phone)
        {
            return Regex.IsMatch(phone, @"^\d{4,}$");
        }

        private static bool IsValidBirthday(string birthday)
        {
            return Regex.IsMatch(birthday, @"^\d{2}\.\d{2}\.\d{4}$");
        }
    }
}





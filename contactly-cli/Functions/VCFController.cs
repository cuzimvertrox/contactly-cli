using System;
using System.Collections.Generic;
using System.IO;

namespace contactly_cli.Functions
{
    public struct Contact
    {
        public string FirstName;
        public string LastName;
        public string Company;
        public string Email;
        public string Phone;
        public string Address;
        public string Birthday;
        public string FileName;

        public Contact(string firstName, string lastName, string company, string email,
                       string phone, string address, string birthday, string fileName)
        {
            FirstName = firstName;
            LastName = lastName;
            Company = company;
            Email = email;
            Phone = phone;
            Address = address;
            Birthday = birthday;
            FileName = fileName;
        }
    }

    public class VCFController
    {
        public static List<Contact> ReadContacts()
        {
            var contacts = new List<Contact>();
            string path = ConfigController.ReadConfig("path");

            if (!ConfigController.IsValidPath(path))
            {
                Console.WriteLine("The path to the contacts is not valid or not set.");
                return contacts;
            }

            foreach (var file in Directory.GetFiles(path, "*.vcf"))
            {
                var contact = ParseVCF(file);
                contacts.Add(contact);
            }

            return contacts;
        }

        private static Contact ParseVCF(string filePath)
        {
            var contact = new Contact();
            var lines = File.ReadAllLines(filePath);

            foreach (var line in lines)
            {
                if (line.StartsWith("N:"))
                {
                    var names = line.Substring(2).Split(';');
                    contact.LastName = names.Length > 0 ? names[0].Trim() : "";
                    contact.FirstName = names.Length > 1 ? names[1].Trim() : "";
                }
                else if (line.StartsWith("ORG:"))
                {
                    contact.Company = line.Substring(4).Trim();
                }
                else if (line.StartsWith("EMAIL:"))
                {
                    contact.Email = line.Substring(6).Trim();
                }
                else if (line.StartsWith("TEL:"))
                {
                    contact.Phone = line.Substring(4).Trim();
                }
                else if (line.StartsWith("ADR:"))
                {
                    contact.Address = line.Substring(4).Trim();
                }
                else if (line.StartsWith("BDAY:"))
                {
                    contact.Birthday = line.Substring(5).Trim();
                }
                // Weitere Felder können hier hinzugefügt werden
            }

            contact.FileName = Path.GetFileName(filePath);
            return contact;
        }

        public static void SaveContact(Contact contact)
        {
            string path = ConfigController.ReadConfig("path");

            if (!ConfigController.IsValidPath(path))
            {
                Console.WriteLine("The path to the contacts is not valid or not set.");
                return;
            }

            if (string.IsNullOrWhiteSpace(contact.FileName))
            {
                contact.FileName = GenerateUniqueFileName(path, contact.LastName, contact.FirstName);
            }

            string vcfContent = FormatContactAsVCF(contact);
            File.WriteAllText(Path.Combine(path, contact.FileName), vcfContent);
            Console.WriteLine($"Contact '{contact.FirstName} {contact.LastName}' saved to {contact.FileName}");
        }

        private static string GenerateUniqueFileName(string path, string lastName, string firstName)
        {
            string fileName;
            Random random = new Random();

            do
            {
                int randomNumber = random.Next(1000000, 9999999);
                fileName = $"{lastName}_{firstName}_{randomNumber}.vcf";
            }
            while (File.Exists(Path.Combine(path, fileName)));

            return fileName;
        }

        private static string FormatContactAsVCF(Contact contact)
        {
            var vcfContent = $"BEGIN:VCARD\n" +
                             "VERSION:3.0\n" +
                             $"N:{contact.LastName};{contact.FirstName}\n" +
                             $"FN:{contact.FirstName} {contact.LastName}\n" +
                             (!string.IsNullOrWhiteSpace(contact.Company) ? $"ORG:{contact.Company}\n" : "") +
                             (!string.IsNullOrWhiteSpace(contact.Email) ? $"EMAIL:{contact.Email}\n" : "") +
                             (!string.IsNullOrWhiteSpace(contact.Phone) ? $"TEL:{contact.Phone}\n" : "") +
                             (!string.IsNullOrWhiteSpace(contact.Address) ? $"ADR:{contact.Address}\n" : "") +
                             (!string.IsNullOrWhiteSpace(contact.Birthday) ? $"BDAY:{contact.Birthday}\n" : "") +
                             "END:VCARD";
            return vcfContent;
        }

        public static void CreateContact(Contact contact)
        {
            string path = ConfigController.ReadConfig("path");

            if (!ConfigController.IsValidPath(path))
            {
                Console.WriteLine("The path for contacts is not valid or not set.");
                return;
            }

            contact.FileName = GenerateUniqueFileName(path, contact.LastName, contact.FirstName);
            SaveContact(contact);
        }

        // Weitere Methoden wie UpdateContact und DeleteContact können hier hinzugefügt werden
    }
}

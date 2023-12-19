/*  Projektname: contactly-cli
 *  Erstellt: 2023-12-14
 * 
 *  Autor(en): Benjamin Kollmer
 *  
 *  Beschreibung der Funktionen dieser Datei:
 *  - Überprüfung und Verwaltung der Konfigurationsdatei
 *  - Erstellen, Schreiben und Lesen von Konfigurationswerten
 *  - Überprüfung der Gültigkeit von Verzeichnispfaden
 *  - Erstellung des Standardverzeichnispfads für Kontaktdateien
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace contactly_cli.Functions
{
    public class ConfigController
    {
        // Pfad zur Konfigurationsdatei
        private static readonly string ConfigFilePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "contactly-cli.config");

        // Überprüft die Konfigurationsdatei und erstellt sie bei Bedarf
        public static void CheckConfig()
        {
            try
            {
                if (!File.Exists(ConfigFilePath))
                {
                    CreateConfigFile();
                }
                else
                {
                    string pathValue = ReadConfig("path");
                    if (!IsValidPath(pathValue))
                    {
                        string defaultPath = CreateDefaultPath();
                        WriteConfig("path", defaultPath);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fehler beim Lesen der Konfigurationsdatei: " + ex.Message);
                CreateConfigFile();
            }
        }

        // Diese Methode erstellt eine neue Konfigurationsdatei, falls sie nicht vorhanden ist
        private static void CreateConfigFile()
        {
            File.Create(ConfigFilePath).Dispose();
        }

        // Diese Methode schreibt einen Wert in die Konfigurationsdatei
        public static void WriteConfig(string key, string value)
        {
            // Überprüfen, ob die Datei existiert. Wenn nicht, erstelle sie.
            if (!File.Exists(ConfigFilePath))
            {
                CreateConfigFile();
            }

            var configContent = new Dictionary<string, string>();

            // Vorhandene Konfigurationseinträge lesen
            if (File.Exists(ConfigFilePath))
            {
                foreach (var line in File.ReadAllLines(ConfigFilePath))
                {
                    var keyValue = line.Split('=');
                    if (keyValue.Length == 2)
                    {
                        configContent[keyValue[0].Trim()] = keyValue[1].Trim();
                    }
                }
            }

            // Konfigurationswert aktualisieren oder hinzufügen
            configContent[key] = value;

            // Konfigurationsdatei neu schreiben
            File.WriteAllLines(ConfigFilePath, configContent.Select(kvp => $"{kvp.Key}={kvp.Value}"));
        }

        // Diese Methode liest einen Wert aus der Konfigurationsdatei
        public static string ReadConfig(string key)
        {
            if (File.Exists(ConfigFilePath))
            {
                foreach (var line in File.ReadAllLines(ConfigFilePath))
                {
                    var keyValue = line.Split('=');
                    if (keyValue.Length == 2 && keyValue[0] == key)
                    {
                        return keyValue[1];
                    }
                }
            }
            return "Not Found"; // Schlüssel nicht gefunden
        }

        // Diese Methode überprüft, ob ein Pfad gültig ist
        public static bool IsValidPath(string path)
        {
            return !string.IsNullOrEmpty(path) && Directory.Exists(path);
        }

        // Diese Methode erstellt den Standardpfad für die Konfigurationsdatei
        private static string CreateDefaultPath()
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string contactlyPath = Path.Combine(documentsPath, "Contactly", "Kontakte");

            if (!Directory.Exists(contactlyPath))
            {
                Directory.CreateDirectory(contactlyPath);
            }

            return contactlyPath;
        }
    }
}

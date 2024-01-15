<p align="center">
  <img src="Logo_Contactly.png" alt="Contactly Logo" width="25%">
</p>

# Contactly CLI - Einfaches Kontaktdatenmanagement per Kommandozeile

## 1. Übersicht

Contactly CLI ist ein unkompliziertes Kommandozeilentool zum Verwalten von Kontakten. Mit diesem Programm können Sie Kontaktdaten speichern, bearbeiten und löschen, ohne auf eine komplizierte Anwendung angewiesen zu sein.

## 2. Funktionen

- **Kontakte anzeigen:** Ihre gespeicherten Kontakte in einer übersichtlichen Liste anzeigen.
- **Kontakte erstellen:** Neue Kontakte hinzufügen, indem Sie Vorname, Nachname, Firma, E-Mail, Telefonnummer, Adresse und Geburtstag eingeben.
- **Kontakte bearbeiten:** Kontaktinformationen aktualisieren, wenn sich etwas ändert.
- **Kontakte löschen:** Kontakte entfernen, die Sie nicht mehr benötigen.
- **Konfiguration:** Den Speicherort Ihrer Kontaktdateien konfigurieren.

## 3. Installation

Eine einfache Installation kann über diese [Anleitung](https://github.com/cuzimvertrox/contactly-cli/wiki/Setup-und-Installation-des-Projektes-in-VS) erfolgen

1. Klonen Sie das Repository auf Ihren Computer:

   ```bash
   git clone https://github.com/your-username/contactly-cli.git

2. Stellen Sie sicher, dass .NET Core auf Ihrem Computer installiert ist.
3. Navigieren Sie zum Hauptverzeichnis des Projekts und erstellen Sie die Anwendung:
   ```bash
   cd contactly-cli
   dotnet build

## 4. Verwendung

1. Führen Sie die Anwendung nach dem Erstellen aus:
   ```bash
   dotnet run
2. Das Hauptmenü wird angezeigt und ermöglicht Ihnen die Verwendung der verschiedenen Funktionen von Contactly CLI.

## 5. Konfiguration
Bevor Sie die Anwendung verwenden, stellen Sie sicher, dass Sie den Speicherort Ihrer Kontaktdateien in der Konfigurationsdatei festgelegt haben. Standardmäßig werden Kontaktdateien im "Contacts"-Verzeichnis im Dokumentenordner gespeichert. Sie können diesen Pfad in der Datei "contactly-cli.config" ändern.

## 6. Mitwirkende
- [Benjamin Kollmer](https://github.com/cuzimvertrox)
- [Tobias Springborn](https://github.com/Hummelholz)
- [Samuel Hekler](https://github.com/notFound)

## 7. Lizenz
Dieses Projekt ist unter der MIT-Lizenz lizenziert. Weitere Details finden Sie in der [LICENSE](LICENSE)-Datei.

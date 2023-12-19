/*  Projektname: contactly-cli
 *  Erstellt: 2023-12-14
 * 
 *  Autor(en): Benjamin Kollmer
 *  
 *  Beschreibung der Funktionen dieser Datei:
 *  - Die ContactlyMain-Klasse dient als Einstiegspunkt für die Anwendung.
 *  - Sie führt die initiale Konfigurationsprüfung durch und zeigt die Startbildschirmoberfläche an.
 */
using contactly_cli.Functions;
using contactly_cli.UI;


namespace contactly_cli
{
    class ContactlyMain
    {
        static void Main(string[] args)
        {
            // Initiale Prüfung ob für den Start die Config vorhanden ist. 
            ConfigController.CheckConfig();

            // Render HomeUI
            HomeUI.ShowHomeScreen();
        }
    }
}

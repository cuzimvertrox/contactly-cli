/*  Projektname: contactly-cli
 *  Erstellt: 2023-12-19
 * 
 *  Autor(en): Tobias Springborn, Benjamin Kollmer
 *  
 *  Beschreibung der Funktionen dieser Datei:
 *  - Zentralisierte Methode zum Drucken von Listen von Zeilen auf der Konsole
 */

using System;
using System.Collections.Generic;

namespace contactly_cli.Functions
{
    // Controller-Klasse zur Handhabung des Ausdrucks von Zeilen in der Konsole
    public static class PrintLinesController
    {
        // Druckt eine Liste von Zeichenketten auf der Konsole aus
        public static void PrintLines(List<string> lines)
        {
            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }
        }
    }
}

using contactly_cli.Functions;
using contactly_cli.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contactly_cli
{
    class ContactlyMain
    {
        static void Main(string[] args)
        {
            // Initiale Prüfung ob für den System Start alles vorhanden ist. 
            ConfigController.CheckConfig();

            // Render HomeUI
            HomeUI.ShowHomeScreen();
        }
    }
}

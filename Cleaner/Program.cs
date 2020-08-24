using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Cleaner
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Vysavač | vytvořen Domčou s láskou <3";
            Console.WriteLine("------ Vycistit ------");
            Console.WriteLine("1. Internetovou mezipamět");
            Console.WriteLine("2. Věci zpomalujicí počítač");
            Console.WriteLine("3. Historii vyhledávaní");
            Console.WriteLine("4. Dočasně soubory");
            Console.WriteLine("5. Spamující soubory");
            Console.WriteLine("6. Vysypat koš");
            Console.WriteLine("------------------------------------");
            Console.Write("Jsi si jistý, že chces smazat uvedené věci? A/N: ");
            Thread.Sleep(3000);
            string str = Console.ReadLine();

            if (str.Equals("N", StringComparison.InvariantCultureIgnoreCase))
                return;

            System.IO.DirectoryInfo di = null;
            //Čištění internetove mezipaměti
            string path = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache);
            di = new DirectoryInfo(path);
            Console.WriteLine("Čištění internetove mezipaměti");
            ClearTempData(di);
            Console.WriteLine("Vyčišteno = ÚSPĚŠNĚ");
            //Čištění veci zpomalujici tvůj počítač
            string cookie = Environment.GetFolderPath(Environment.SpecialFolder.Cookies);
            di = new DirectoryInfo(path);
            Console.WriteLine("Čištění veci zpomalujici tvůj počítač");
            ClearTempData(di);
            Console.WriteLine("Vyčišteno = ÚSPĚŠNĚ");
            //Čištění historie vyhledavani
            string history = Environment.GetFolderPath(Environment.SpecialFolder.History);
            di = new DirectoryInfo(path);
            Console.WriteLine("Čištění historie vyhledavaní");
            ClearTempData(di);
            Console.WriteLine("Vyčišteno = ÚSPĚŠNĚ");
            //Čištění docasnych souboru
            di = new DirectoryInfo(@"C:\Windows\Temp");
            Console.WriteLine("Čištění dočasných souborů");
            ClearTempData(di);
            Console.WriteLine("Vyčišteno = ÚSPĚŠNĚ");
            di = new DirectoryInfo(System.IO.Path.GetTempPath());
            Console.WriteLine("Čištění spamujících souboru");
            ClearTempData(di);
            Console.WriteLine("Vyčišteno = ÚSPĚŠNĚ");
            //Čištění kose
            Console.WriteLine("Čištění kose");
            uint result = SHEmptyRecycleBin(IntPtr.Zero, null, 0);
            Console.WriteLine("Vyčišteno = ÚSPĚŠNĚ");
            Console.Clear();
            Console.WriteLine("Celý proces byl úspěšný");
            Console.WriteLine(" ");
            Console.WriteLine("Klikni na jakoukoliv klaáesu, pro odchod");
            Console.ReadKey();
        }

        private static void ClearTempData(DirectoryInfo di)
        {
            foreach (FileInfo file in di.GetFiles())
            {
                try
                {
                    file.Delete();
                    Console.WriteLine(file.FullName);
                }
                catch (Exception ex)
                {
                    continue;
                }
            }

            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                try
                {
                    dir.Delete(true);
                    Console.WriteLine(dir.FullName);
                }
                catch (Exception ex)
                {
                    continue;
                }
            }
        }

        [DllImport("Shell32.dll", CharSet = CharSet.Unicode)]
        static extern uint SHEmptyRecycleBin(IntPtr hwnd, string pszRootPath,
        RecycleFlags dwFlags);

        enum RecycleFlags : uint
        {
            SHERB_NOCONFIRMATION = 0x00000001,
            SHERB_NOPROGRESSUI = 0x00000001,
            SHERB_NOSOUND = 0x00000004
        }
    }
}

/*
 * Created by SharpDevelop.
 * User: petr1
 * Date: 23.01.2020
 * Time: 9:42
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Threading;

namespace Pexeso
{
	class Program
    {
        // hrac
        public static string hrac1;
        public static string hrac2;
        public static string[] jmenaHracu = {"Hrac 1","Hrac 2"}; // hrac si bude moci zvolit sve jmeno
        public static ConsoleColor[] barvyHracu = { ConsoleColor.Red, ConsoleColor.Green };
        public static int aktualniHrac = 0;
        public static string[] ziskaneKarty = { "", "" };
        // hra
        public static char[] karty = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' }; // prepisovatelne pole karet k zamichani
        public static char[] puvodni = new char[16];
        public static char[] rozehrane = new char[16];
        public static int cisloKarty = 0;
        public static int cisloKarty1 = 0;
        public static bool konec = false;
        private static bool _pojmenovano = false;
        
        public static void Main(string[] args)
        {           
            while (true)
            {
                NaplnPole(rozehrane,'#');
                NaplnPole(puvodni, '-');
                NaplnKartami();
                ziskaneKarty[0] = "";
                ziskaneKarty[1] = "";
                while (true)
                {
                    Console.Clear();
                    Vypisy(aktualniHrac);
                    Konec();
                    if (konec == true)
                    {
                        break;
                    } 
                }
            }
        }
        public static void ZvoleniJmena()
        {
        	if (_pojmenovano)
      		{ return; }
        	Console.ResetColor();
        	Console.WriteLine("Zvol jmeno prvniho hrace: ");
        	hrac1 = Console.ReadLine();
        	Console.WriteLine("Zvol jmeno druheho hrace: ");
        	hrac2 = Console.ReadLine();
        	Console.ForegroundColor = ConsoleColor.Red;
        	Console.Clear();
        	
        	_pojmenovano = true;
        }
        public static void ZamichejKarty(char karta)
        {
            Random rnd = new Random();
            bool obsazeno = false;
            
            do
            {
                int cislo = rnd.Next(0, 16);
                if (puvodni[cislo] == '-')
                {
                    puvodni[cislo] = karta;
                    obsazeno = true;
                }
                else
                {
                    continue;
                }
            } 
            while (!obsazeno);
            {
            }
        }
        
        public static void NaplnPole(char[] pole, char symbol)
        {
            for (int i = 0; i < pole.Length; i++)
            {
                pole[i] = symbol;
            }
        }
    
        public static void NaplnKartami()
        {
            for (int i = 0; i < karty.Length; i++)
            {
                ZamichejKarty(karty[i]);
                ZamichejKarty(karty[i]);
            }
        }
        
        public static void HraciPlocha(char[] pole)
        {
        	ZvoleniJmena();
            Console.WriteLine(">>> PEXESO <<<\n");
            Console.WriteLine("Hraje: " + jmenaHracu[aktualniHrac] + "\n");
            Console.WriteLine(hrac1 + " (Hrac1)");
            Console.Write("- ziskane karty: " + ziskaneKarty[0] + "\n");
            Console.WriteLine();
            Console.WriteLine(hrac2 + " (Hrac2)");
            Console.Write("- ziskane karty: " + ziskaneKarty[1] + "\n");
            Console.WriteLine();
            Console.WriteLine();
            for (int i = 0; i < pole.Length; i++)
            {
                Console.Write(pole[i] + " ");
                if (i % 4 == 3)
                {
                    Console.WriteLine(); // rozcleni karty do radku
                }
            }
        }
        
        public static void Vysledek()
        {
            bool uhodnuto = true;
            if (puvodni[cisloKarty] == puvodni[cisloKarty1])
            {
            	Console.ResetColor();
            	Console.SetCursorPosition(50, 5);
            	Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("TREFIL JSI SE, HRAJES JESTE JEDNOU");
                Thread.Sleep(2000);
                rozehrane[cisloKarty] = '-';
                rozehrane[cisloKarty1] = '-';
                ziskaneKarty[aktualniHrac] += puvodni[cisloKarty];
            }
            else
            {
            	Console.ResetColor();
            	Console.SetCursorPosition(50, 5);
            	Console.ForegroundColor = ConsoleColor.DarkMagenta;
            	Console.WriteLine("TO NEVYSLO! NA TAHU JE DRUHY HRAC");
                Thread.Sleep(2000);
                rozehrane[cisloKarty] = '#';
                rozehrane[cisloKarty1] = '#';
                uhodnuto = false;
            }
            if (uhodnuto == false)
            {
                if (aktualniHrac == 0)
                {
                    aktualniHrac = 1;
                }
                else if (aktualniHrac == 1)
                {
                    aktualniHrac = 0;
                }
            } 
        }
        
        public static void Konec()
        {
            if (ziskaneKarty[0].Length + ziskaneKarty[1].Length == 8)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("!Hra skoncila!");
                if (ziskaneKarty[0].Length > ziskaneKarty[1].Length)
                {
                    Console.WriteLine("Vitez je: " + jmenaHracu[0]);
                    Console.WriteLine("Ziskane karty: " + ziskaneKarty[0]);
                }
                else if (ziskaneKarty[1].Length > ziskaneKarty[0].Length)
                {
                    Console.WriteLine("Vitez je: " + jmenaHracu[1]);
                    Console.WriteLine("Ziskane karty: " + ziskaneKarty[1]);
                }
                else if (ziskaneKarty[0].Length == ziskaneKarty[1].Length)
                {
                    Console.WriteLine("Remiza");
                }
                konec = true;
                Console.WriteLine("Pro zahajeni nove hry stiskni - ENTER -");
                Console.ReadLine();
            }
        }
        
        public static void Vypisy(int aktualniHrac)
        {
            Console.ForegroundColor = barvyHracu[aktualniHrac];
            // cekani na vstup...
            bool nacteno = false;
            string vstup = "";
            
            while (!nacteno)
            {
                Console.Clear();
                HraciPlocha(rozehrane);
                try
                {
                    Console.Write("\nNapis cislo prvni hadane karty (0-15): ");
                    vstup = Console.ReadLine();
                    cisloKarty = Int32.Parse(vstup);
                    // test, jestli je cislo pouzitelne.. 
					
					// ...neni --> continue
					// ...ano, je: nacteno = true;
                    nacteno = true;
                    if (rozehrane[cisloKarty] == '-')
                    {  
                        Console.WriteLine("Tuto kartu uz nekdo uhodl!");
                        Thread.Sleep(1500);
                        continue;
                    }
                    rozehrane[cisloKarty] = puvodni[cisloKarty];
                    Console.Clear();
                    HraciPlocha(rozehrane);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                
                try
                {
                    Console.Write("\nNapis cislo druhe hadane karty (0-15): ");
                    vstup = Console.ReadLine();
                    cisloKarty1 = Int32.Parse(vstup);
                    nacteno = true;
                    if (rozehrane[cisloKarty1] == '-')
                    {
                        Console.WriteLine("Tuto kartu uz nekdo uhodl!");
                        Thread.Sleep(1500);
                        rozehrane[cisloKarty] = '#';
                        continue;
                    }
                    rozehrane[cisloKarty1] = puvodni[cisloKarty1];
                    Console.Clear();
                    HraciPlocha(rozehrane);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    rozehrane[cisloKarty] = '#';
                }
                Vysledek();
            }
        }
    }
}
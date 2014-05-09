using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace tortek
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("1. feladat: Adja meg a számlálót és a nevezőt szóközzel elválasztva!: ");
            string[] beirt = Console.ReadLine().Split(' ');
            int szamlalo = Convert.ToInt16(beirt[0]);
            int nevezo = Convert.ToInt16(beirt[1]);
            float tort = (float)szamlalo / (float)nevezo;
            if(egesz(tort))
            {
                Console.WriteLine("{0}", tort);
            }
            else
            {
                Console.WriteLine("Nem egész");
            }
            int osztnev;
            int osztszam;
            egyszerusit(szamlalo, nevezo, out osztnev, out osztszam);
            if (egesz(tort))
                Console.WriteLine("{0}/{1} = {2}", szamlalo, nevezo, tort);
            else
            {
                Console.WriteLine("{0}/{1} = {2}/{3}", szamlalo, nevezo, osztszam, osztnev);
            }
            Console.Write("4. feladat: Adja meg a számlálót és a nevezőt szóközzel elválasztva!: ");
            beirt = Console.ReadLine().Split(' ');
            int szamlalo2 = Convert.ToInt16(beirt[0]);
            int nevezo2 = Convert.ToInt16(beirt[1]);
            int szorozszam = szamlalo * szamlalo2;
            int szoroznev = nevezo * nevezo2;
            int osztnev2;
            int osztszam2;
            egyszerusit(szorozszam, szoroznev, out osztnev2, out osztszam2);
            float tort2 = (float)osztnev2 / (float)osztszam2;
            if (egesz(tort2))
                Console.WriteLine("{0}/{1} * {2}/{3} = {4}/{5} = {6}", szamlalo, nevezo, szamlalo2, nevezo2, szamlalo*szamlalo2, nevezo*nevezo2, tort2);
            else
            {
                Console.WriteLine("{0}/{1} * {2}/{3} = {4}/{5} = {6}/{7}", szamlalo, nevezo, szamlalo2, nevezo2, szamlalo*szamlalo2, nevezo*nevezo2, osztszam2, osztnev2);
            }
            
            int koznev = LKKT(nevezo, nevezo2);
            int szorzo = koznev / nevezo;
            int szorzo2 = koznev / nevezo2;
            int adszam = szorzo * szamlalo;
            int adszam2 = szorzo2 * szamlalo2;
            int osztszam3;
            int osztnev3;
            egyszerusit(adszam+adszam2, koznev, out osztnev3, out osztszam3);
            float tort3 = (float)(adszam + adszam2) / (float)koznev;
            if (egesz(tort3))
                Console.WriteLine("{0}/{1} + {2}/{3} = {4}/{5} + {6}/{7} = {8}", szamlalo, nevezo, szamlalo2, nevezo2, adszam, koznev, adszam2, koznev, tort3);
            else
            {
                Console.WriteLine("{0}/{1} + {2}/{3} = {4}/{5} + {6}/{7} = {8}/{9}", szamlalo, nevezo, szamlalo2, nevezo2, adszam, koznev, adszam2, koznev, osztszam3, osztnev3);
            }
            StreamReader reader = new StreamReader("adat.txt");
            StreamWriter writer = new StreamWriter("eredmeny.txt");
            while(!reader.EndOfStream)
            {
                string[] currLine = reader.ReadLine().Split(' ');
                if(currLine[4] == "+")
                {
                    osszead(Convert.ToInt16(currLine[0]), Convert.ToInt16(currLine[1]), Convert.ToInt16(currLine[2]), Convert.ToInt16(currLine[3]), writer);
                }
                else
                {
                    szoroz(Convert.ToInt16(currLine[0]), Convert.ToInt16(currLine[1]), Convert.ToInt16(currLine[2]), Convert.ToInt16(currLine[3]), writer);
                }
            }
            reader.Close();
            writer.Close();
            Console.WriteLine("Nyomjon ENTER-t a kilépéshez!");
            Console.ReadLine();
        }
        private static void szoroz(int szamlalo, int nevezo, int szamlalo2, int nevezo2, StreamWriter writer)
        {
            int szorozszam = szamlalo * szamlalo2;
            int szoroznev = nevezo * nevezo2;
            int osztnev2;
            int osztszam2;
            egyszerusit(szorozszam, szoroznev, out osztnev2, out osztszam2);
            float tort2 = (float)osztnev2 / (float)osztszam2;
            if (egesz(tort2))
                writer.WriteLine("{0}/{1} * {2}/{3} = {4}/{5} = {6}", szamlalo, nevezo, szamlalo2, nevezo2, szamlalo * szamlalo2, nevezo * nevezo2, tort2);
            else
            {
                writer.WriteLine("{0}/{1} * {2}/{3} = {4}/{5} = {6}/{7}", szamlalo, nevezo, szamlalo2, nevezo2, szamlalo * szamlalo2, nevezo * nevezo2, osztszam2, osztnev2);
            }
        }

        private static void osszead(int szamlalo, int nevezo, int szamlalo2, int nevezo2, StreamWriter writer)
        {
            int koznev = LKKT(nevezo, nevezo2);
            int szorzo = koznev / nevezo;
            int szorzo2 = koznev / nevezo2;
            int adszam = szorzo * szamlalo;
            int adszam2 = szorzo2 * szamlalo2;
            int osztszam3;
            int osztnev3;
            egyszerusit(adszam + adszam2, koznev, out osztnev3, out osztszam3);
            float tort3 = (float)(adszam + adszam2) / (float)koznev;
            if (egesz(tort3))
                writer.WriteLine("{0}/{1} + {2}/{3} = {4}/{5} + {6}/{7} = {8}", szamlalo, nevezo, szamlalo2, nevezo2, adszam, koznev, adszam2, koznev, tort3);
            else
            {
                writer.WriteLine("{0}/{1} + {2}/{3} = {4}/{5} + {6}/{7} = {8}/{9}", szamlalo, nevezo, szamlalo2, nevezo2, adszam, koznev, adszam2, koznev, osztszam3, osztnev3);
            }
        }

        private static void egyszerusit(int szamlalo, int nevezo, out int osztnev, out int osztszam)
        {
            int legnagyobb = LNKO(szamlalo, nevezo);
            osztnev = nevezo / legnagyobb;
            osztszam = szamlalo / legnagyobb;
        }

        private static bool egesz(float tort)
        {
            if (tort % 1 == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static int LNKO(int a, int b)
        {
            if(a == b)
            {
                return a;
            }
            if(a < b)
            {
                return LNKO(a, b - a);
            }
            if(a > b)
            {
                return LNKO(a - b, b);
            }
            return 1;
        }
        public static int LKKT(int a, int b)
        {
            return a * b / LNKO(a, b);
        }
    }
}

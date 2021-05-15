using System;
using System.Collections.Generic;
using System.IO;

namespace utca
{
    class utca
    {
        class Adatok
        {
            public bool paros;
            public int meret;
            public char szin;
            public int hazszam;

            public Adatok(bool paros, int meret, char szin, int hazszam)
            {
                this.paros = paros;
                this.meret = meret;
                this.szin = szin;
                this.hazszam = hazszam;
            }
        }
        static void Main(string[] args)
        {
            #region 1.feladat
            StreamReader file = new StreamReader("kerites.txt");
            List<Adatok> listaadat = new List<Adatok>();
            bool readlinesuc = true;
            var hazsz = new int[2] { 2, 1 };
            while (readlinesuc)
            {
                string line = file.ReadLine();
                if (line == null)
                {
                    readlinesuc = false;
                    break;
                }
                string[] tordeltsor = line.Split(' ');
                int hindex = tordeltsor[0] == "0" ? 0 : 1;
                listaadat.Add(new Adatok(tordeltsor[0] == "0" ? true : false, Int32.Parse(tordeltsor[1]), char.Parse(tordeltsor[2]), hazsz[hindex]));
                hazsz[hindex] += 2;
            }
            file.Close();
            #endregion
            #region 2.feladat
            Console.WriteLine("2.feladat");
            Console.WriteLine($"Az eladott telkek száma: {listaadat.Count}");
            Console.Write('\n');
            #endregion
            #region 3.feladat
            Console.WriteLine("3.feladat");
            int utolsohazszami = 0;
            string parose = "";
            for (int i = listaadat.Count - 1; i >= 0; i--)
            {
                utolsohazszami = i;
                if (listaadat[i].paros)
                {
                    parose = "paros";
                    break;
                    
                }
                else
                {
                    parose = "páratlan";
                    break;
                }
            }
            Console.WriteLine($"A {parose} oldalon adták el az utolsó telket.");
            Console.WriteLine($"Az utolsó telek házszáma: {listaadat[utolsohazszami].hazszam}");
            Console.Write('\n');
            #endregion
            #region 4.feladat
            Console.WriteLine("4.feladat");
            char elozosz = ' ';
            for (int i = 0; i < listaadat.Count; i++)
            {
                if (!listaadat[i].paros)
                {
                    var most = listaadat[i].szin;
                    if (elozosz == most) 
                    {
                        Console.WriteLine($"A szomszédossal egyezik a kerítés színe: {listaadat[i].hazszam-2}");
                        break;
                    }
                    if (most != '#' && most != ':')
                    {
                        elozosz = most;
                    }
                    else
                    {
                        elozosz = ' ';
                    }
                }
            }
            Console.Write('\n');
            #endregion
            #region 5.feladat
            Console.WriteLine("5.feladat");
            Console.Write("Adjon meg egy házszámot! ");
            int adotthazszam = Int32.Parse(Console.ReadLine());
            int idx = 0;
            foreach (var adh in listaadat)
            {
                if (adotthazszam == adh.hazszam)
                {
                    Console.WriteLine($"A kerítés színe / állapota: {adh.szin}");
                    break;
                }
                idx++;
            }
            char jelszin = listaadat[idx].szin;
            char bszom =' ';
            char jszom = ' ';
            for (int i = idx+1; i < listaadat.Count; i++)
            {
                if(listaadat[i].hazszam == adotthazszam + 2)
                {
                    jszom = listaadat[i].szin;
                    break;
                }
            }
            for (int i = idx - 1; i >= 0; i--)
            {
                if (listaadat[i].hazszam == adotthazszam - 2)
                {
                    bszom = listaadat[i].szin;
                    break;
                }
            }
            char ujsz = ' ';
            for (char szin = 'A';  szin < 'Z'; ++szin)
            {
                if (szin != bszom && szin != jszom && szin != jelszin)
                {
                    ujsz = szin;
                    break;
                }
            }
            Console.WriteLine($"Egy lehetséges festési szín: {ujsz}");
            #endregion
            #region 6.feladat
            StreamWriter utcakep = new StreamWriter("utcakep.txt");
            foreach (var utca in listaadat)
            {
                if (!utca.paros)
                {
                    utcakep.Write(new string(utca.szin, utca.meret));
                    
                }
            }
            utcakep.Write('\n');
            foreach (var utca in listaadat)
            {
                if (!utca.paros)
                {
                    var hs = string.Format($"{utca.hazszam}");
                    utcakep.Write($"{hs}{ new string(' ', utca.meret - hs.Length)}");
                }
            }
            utcakep.Write('\n');
            utcakep.Close();
            #endregion
        }
    }
}

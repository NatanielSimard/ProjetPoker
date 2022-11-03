using System;
using System.Collections.Generic;

namespace JeuxPoker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("combien les joueurs ont comme mise de depart");
            //bool testMise;
            //int nb;
            //do
            //{
            //    testMise = Int32.TryParse(Console.ReadLine(),out nb);
            //    if (!testMise)
            //    {
            //        Console.WriteLine("entrer un nombre valide");
            //    }
            //} while (!testMise);
            //Partie part = new Partie(nb);
            //bool continuer = true;
            //while(continuer)
            //{
            //    continuer = part.jouerPartie();
            //}

            List<Carte> test = new List<Carte>();
            test.Add(new Carte(0, 2));
            test.Add(new Carte(2, 2));
            test.Add(new Carte(4, 2));
            test.Add(new Carte(3, 2));
            test.Add(new Carte(7, 2));
            List<Carte> test2 = new List<Carte>();
            test2.Add(new Carte(2, 2));
            test2.Add(new Carte(2, 1));
            test2.Add(new Carte(4, 1));
            test2.Add(new Carte(8, 0));
            test2.Add(new Carte(7, 1));
            Int64 Res,res2;
            res2 = MainJoueur.DeterminerForceMain(test2);
             Res = MainJoueur.DeterminerForceMain(test);
            Console.WriteLine(Res);
            Console.WriteLine(res2);





            ;


        }
    }
}

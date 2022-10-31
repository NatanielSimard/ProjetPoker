using System;

namespace JeuxPoker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("combien les joueurs ont comme mise de depart");
            bool testMise;
            int nb;
            do
            {
                testMise = Int32.TryParse(Console.ReadLine(),out nb);
                if (!testMise)
                {
                    Console.WriteLine("entrer un nombre valide");
                }
            } while (!testMise);
            Partie part = new Partie(nb);
            bool continuer = true;
            while(continuer)
            {
                continuer = part.jouerPartie();
            }
           

        }
    }
}

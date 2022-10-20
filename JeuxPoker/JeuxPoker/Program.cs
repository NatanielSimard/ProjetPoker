using System;

namespace JeuxPoker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Paquet paq = new Paquet();
            foreach (Carte c in paq.TableauInitial)
            {
                Partie.afficherCarte(c);
                Console.WriteLine("");
            }
            
        }
    }
}

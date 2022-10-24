using System;

namespace JeuxPoker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("combien les joueurs ont comme mise de depart");
            int nb =Convert.ToInt32(Console.ReadLine());
            Partie part = new Partie(nb);
            part.jouerPartie();

        }
    }
}

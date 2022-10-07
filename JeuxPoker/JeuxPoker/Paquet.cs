using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeuxPoker
{
    internal class Paquet

    // Infos Cartes
    // Chiffre 0 pour 2 et 12 pour As
    // Couleur 0 pour Coeur , 1 pour Carreau , 2 pour Trefle , 3 pour Pique.

    {
        Carte[] TableauInitial = new Carte[52];
        Carte[] TableauTampon = new Carte[52];
        List<int> TableauTamponList = new List<int>();
        public Paquet()
        {

        }

        public void Distribuer(Joueur j)
        {
        }
        public void Melanger()
        {
        }
        public void Reinitialiser()
        {
            for (int i = 0; i < 13; i++)
            {

            }
        }
        public void FlopTurnRiver(Tour t)
        {
        }
    }
}

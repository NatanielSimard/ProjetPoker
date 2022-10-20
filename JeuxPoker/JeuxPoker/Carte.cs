using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeuxPoker
{
    enum couleur
    { 
    Coeur=0,
    Carreau=1,
    Trefle=2,
    Pique=3,
    }
    enum nbCarte
    {
     Deux=2,
     Trois=3,
     Quatre=4,
     Cinq=5,
     Six=6,
     Sept=7,
     Huit=8,
     Neuf=9,
     Dix=10,
     Valet=11,
     Dame=12,
     Roi=13,
     As=14,
    }
    internal class Carte
    {
        public string lechiffre { get; private set; }
        public string laCouleur { get; private set; }

        public bool visible { get; private set; }
        // Chiffre 2 pour 2 et 14 pour As
        // Couleur 0 pour Coeur , 1 pour Carreau , 2 pour Trefle , 3 pour Pique.
        public Carte(int chiffre , int couleur)
        {
            lechiffre = Enum.GetName(typeof(nbCarte), chiffre);
            laCouleur = Enum.GetName(typeof(couleur), couleur);
        }
        public void retourner()
        {
            visible = true;
        }
        public void Comparer()
        {

        }
    }
}

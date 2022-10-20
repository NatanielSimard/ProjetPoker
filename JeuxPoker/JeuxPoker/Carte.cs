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
     Deux=0,
     Trois=1,
     Quatre=2,
     Cinq=3,
     Six=4,
     Sept=5,
     Huit=6,
     Neuf=7,
     Dix=8,
     Valet=9,
     Dame=10,
     Roi=11,
     As=12,
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

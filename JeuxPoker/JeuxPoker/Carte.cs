using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Common;
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
    public enum nbCarte
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
    public class Carte
    {
        public string lechiffre { get; private set; }
        public string laCouleur { get; private set; }

        public bool visible { get; private set; }
        // Chiffre 2 pour 2 et 14 pour As
        // Couleur 0 pour Coeur , 1 pour Carreau , 2 pour Trefle , 3 pour Pique.
        public Carte(int chiffre, int couleur)
        {
            lechiffre = Enum.GetName(typeof(nbCarte), chiffre);
            laCouleur = Enum.GetName(typeof(couleur), couleur);
            visible = false;
        }
        public void retourner()
        {
            visible = true;
        }
        public void Comparer()
        {

        }
        static public bool operator ==(Carte c1, Carte c2)
        {
            if (c1.lechiffre == c2.lechiffre)
            {
                return true;
            }
            else {return false;}
        }
        static public bool operator !=(Carte c1, Carte c2)
        {
            return !(c1 == c2);
        }
        static public bool operator <(Carte c1,Carte c2)
        {
            bool valide;
            object ch;
            int c1I, c2I;
            valide=Enum.TryParse(typeof(nbCarte),c1.lechiffre, out ch);
            c1I = Convert.ToInt32(ch);

            valide = Enum.TryParse(typeof(nbCarte), c2.lechiffre, out ch);
            c2I = Convert.ToInt32(ch);
            return c1I < c2I;

            //faire operation
        }
        static public bool operator >(Carte c1, Carte c2)
        {
            return !(c1 < c2);
        }
        public int Getvalue()
        {
            object ch;
            int c2;
            Enum.TryParse(typeof(nbCarte), lechiffre, out ch);
            c2 = Convert.ToInt32(ch);
            return c2;
        }
        
    }
}

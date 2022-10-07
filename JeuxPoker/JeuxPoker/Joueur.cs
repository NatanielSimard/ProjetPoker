using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeuxPoker
{
    internal class Joueur
    {
        public string nom { get; private set; }
        public string pseudo { get; private set; }

        public int argent { get; private set; }

        public bool actif { get; private set; }

        public MainJoueur maMain;

        public Joueur(string nom, string pseudo, int argent)
        {
            this.nom = nom;
            this.pseudo = pseudo;
            this.argent = argent;
            this.actif = true;
            maMain = new MainJoueur();
        }
        public int miser(int montantMiser)
        {
            if (argent >= montantMiser)
            {
                argent = argent - montantMiser;
                return montantMiser;
            }
            else
            {
                return -1;
            }

        }
        public void Check()
        {

        }

        public int call(int montantDu)
        {
            if (argent >= montantDu)
            {
                argent = argent - montantDu;
                return montantDu;
            }
            else
            {
                coucher();
                return 0;
            }
        }

        public void coucher()
        {
            actif = false;
        }

        public int Raise(int montantDu, int montantRaise)
        {
            call(montantDu);
            if (actif)
            {
                miser(montantRaise);
                return montantRaise;
            }
            return -1;


        }

        public void ResetMain()
        {
            maMain = new MainJoueur();
        }



    }
}

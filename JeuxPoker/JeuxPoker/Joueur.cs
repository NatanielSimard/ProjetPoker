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
        public bool miser(int montantMiser, out int mise)
        {
            if (argent >= montantMiser)
            {
                argent = argent - montantMiser;
                mise = montantMiser;
                return true;
            }
            else
            {
                mise = -1;
                return false;
            }

        }
        public void Check()
        {

        }

        public bool call(int montantDu, out int montantRetour)
        {
            //regarde si il a assez d'argent si oui il enleve le montant quil doit, sort en out le montant quil a payer et retourne true sinon il retourne false;
            if (argent >= montantDu)
            {
                argent = argent - montantDu;
                montantRetour =  montantDu;
                return true;
            }
            else
            {
                montantRetour= -1;
                return false;
            }
        }

        public void coucher()
        {
            actif = false;
        }

        public bool Raise(int montantDu, int montantRaise, out int montanRetourner)
        {
            int montantCall;
            if (call(montantDu,out montantCall))
            {
                if (miser(montantRaise, out montantRaise))
                {
                    montanRetourner = montantRaise + montantCall;
                    return true;
                }
                else
                {
                    //note ATTENTION FAIRE QUELQUE CHOSE AVEC SE CODE D'ERREUR / DESACTIVER LA FONCTION CALL 
                    montanRetourner = -2;
                    return false;
                }
             
            }
            montanRetourner = -1;
            return false;


        }

        public void ResetMain()
        {
            maMain = new MainJoueur();
        }



    }
}

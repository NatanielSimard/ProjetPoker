using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeuxPoker
{
    public class Joueur
    {
        public string nom { get; private set; }
        public string pseudo { get; private set; }

        public int argent { get; private set; }

        public bool actif { get; set; }

        public MainJoueur maMain;

        public Joueur(string nom, int argent)
        {
            this.nom = nom;
            this.argent = argent;
            this.actif = true;
            maMain = new MainJoueur();
        }
        /// <summary>
        /// si le joueur à assez d'argent pour sa mise se montant est dédui de son total et retourne se meme montant
        /// </summary>
        /// <param name="montantMiser"></param>
        /// <param name="mise"></param>
        /// <returns></returns>
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
        /// <summary>
        /// si le montant a payer est de 0 il est retourner true, sinon false
        /// </summary>
        /// <param name="montantAPayer"></param>
        /// <returns></returns>
        public bool Check(int montantAPayer)
        {
            if (montantAPayer == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// si le joueur a assez d'argent pour call se montant lui est retirer, sinon false est retourner
        /// </summary>
        /// <param name="montantDu"></param>
        /// <param name="montantRetour"></param>
        /// <returns></returns>
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
        /// <summary>
        /// l'attribut actif est mis a false
        /// </summary>
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

        public void ajouterArgent(int argentAjouter)
        {
            argent = argent + argentAjouter;
        }



    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace JeuxPoker
{
    internal class Partie
    {
        Joueur[] joueur;
        public int indiceJoueurCourant { get; private set; }

        Paquet lePaquet;
        Tour leTour;
        int etatTour;
        int pot;
        int miseParJoueur;
        

        Partie(int montantDedepart)
        {
            joueur = new Joueur[4];
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine("Quelle est le nom du joueur #" + (i + 1));
                string nom =Console.ReadLine();
                Console.WriteLine("Quelle est le pseudo du joueur #" + (i + 1));
                string pseudo = Console.ReadLine();
                joueur[i] = new Joueur(nom, pseudo, montantDedepart);
            }
            lePaquet = new Paquet();
            leTour = new Tour();
            pot = 0;
            miseParJoueur = 0;

            
        }
        /// <summary>
        /// la fonction permet de jouer une partie , elle retourne l'intention des joueurs de jouer un nouvelle partie dans un bool
        /// </summary>
        /// <returns></returns>
        public bool jouerPartie()
        {
            bool continuer;
            demarrerPartie();
            for (int i = 0; i < 5; i++)
            {
                JouerRotation();
                //retourner Carte
            }
            continuer = FinPartie();
            return continuer;
        }
        /// <summary>
        /// Fait jouer une rotation aux au joueur e s'occupe de tous les validation possible
        /// </summary>
        public void JouerRotation()
        {
            bool RepeterUneRotation = false;
            do
            {
                RepeterUneRotation = false;
                //boucler pour chaque joueur
                for (int i = 0; i < 4; i++)
                {
                    int montantMinDuJoueur = miseParJoueur;
                    if (joueur[i].actif)
                    {
                        bool retour;
                        //boucler pour s'assurer d'une selection valide du joueur
                        do
                        {
                            AfficherJeu(joueur[i], montantMinDuJoueur);
                            //[1:Coucher][2:check][3:call][4:Raise][5:miser]
                            int mode = SelectionDansMenu(1, 5);
                            retour = true;
                            //selecteur et verif du choix
                            switch (mode)
                            {
                                //coucher
                                case 1:
                                    joueur[i].coucher();
                                    break;
                                //check
                                case 2:
                                    if (joueur[i].Check(montantMinDuJoueur))
                                    {

                                    }
                                    else
                                    {
                                        retour = false;
                                        Console.WriteLine("option invalide Mise est de plus que 0");
                                        Console.ReadKey();
                                    }
                                    break;
                                //call
                                case 3:
                                    int ajouterPot;
                                    if (joueur[i].call(montantMinDuJoueur, out ajouterPot))
                                    {
                                        pot = pot + ajouterPot;
                                    }
                                    else
                                    {
                                        retour = false;
                                        Console.WriteLine("option invalide Mise est de 0");
                                        Console.ReadKey();
                                    }

                                    break;
                                //raise
                                case 4:
                                    if (montantMinDuJoueur != 0)
                                    {
                                        int mise = DemanderMise();
                                        int montant;

                                        if (joueur[i].Raise(montantMinDuJoueur, mise, out montant))
                                        {
                                            if (montant == -2)
                                            {
                                                pot = pot + montantMinDuJoueur;
                                                montantMinDuJoueur = 0;
                                                retour = false;
                                                Console.WriteLine("vous N'avez pas assez d'argent pour miser");
                                                Console.ReadKey();
                                            }
                                            else
                                            {
                                                RepeterUneRotation = true;
                                                pot = pot + montantMinDuJoueur + montant;
                                                miseParJoueur = montant;
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("veuiller choisir un autre mode pas assez d'argent");
                                            retour = false;
                                            Console.ReadKey();
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("utiliser miser, il n'y pas de mise");
                                        retour = false;
                                    }



                                    break;
                                //miser
                                case 5:
                                    //si raise est favorable il est indiqués a l'utilisateur et reboucle
                                    if (montantMinDuJoueur == 0)
                                    {
                                        int mise = DemanderMise();
                                        int montant;

                                        if (joueur[i].miser(mise, out montant))
                                        {
                                            if (i != 1)
                                            {
                                                RepeterUneRotation = true;
                                            }
                                            pot = pot + montant;
                                            miseParJoueur = montant;
                                        }
                                        else
                                        {
                                            Console.WriteLine("veuiller choisir un autre mode pas assez d'argent");
                                            retour = false;
                                            Console.ReadKey();
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("une mise est deja crée");
                                        retour = false;
                                        Console.ReadKey();
                                    }
                                    break;



                                default:
                                    break;
                            }
                        } while (!retour);
                    }
                }
            } while (RepeterUneRotation);
            
        }
        public void afficherMain(Joueur joueurAfficher)
        {

        }
        /// <summary>
        /// demande la mise des joueurs et la valide retourne un INT
        /// </summary>
        /// <returns></returns>
        private int DemanderMise()
        {
            int mise;
            Console.WriteLine("combien voulez vous miser");
            bool verif1 = true;
            do
            {
                verif1 = int.TryParse(Console.ReadLine(), out mise);
                if (!verif1)
                {
                    Console.WriteLine("montant invalide");
                }
            } while (!verif1);
            return mise;

        }
        /// <summary>
        /// affiche le jeu completement, donc tous les partie, la main, les cartes communes les mise, le pot et le montant du joueur
        /// 
        /// affichage voulue
        /// 
        /// ------------------------------------------------------------------------------------------------------
        /// Nom Du Joueur                       pot:xx$ | Montant devant ètre miser: xx$ | Montant Du joueur: xxx$
        /// 
        /// Carte commune:          5P ## ## ## ##
        /// 
        /// Votre Main                  3Cr 6P
        /// 
        /// [1:Coucher][2:check][3:call][4:Raise][5:miser]
        /// </summary>
        /// <param name="joueurAfficher"></param>
        /// <param name="montantApayer"></param>
        private void AfficherJeu(Joueur joueurAfficher,int montantApayer)
        {

        }
        public int getGagnant()
        {
            return 1;
        }
        private void UpdaterGagnant(Joueur joueurUp)
        { 

        }
        private void demarrerPartie()
        {
            lePaquet.Melanger();
            //distribuerCarte
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 4; i++)
                {
                    lePaquet.Distribuer(joueur[j]);
                }
            }
            //initialiser le tour
            leTour = new Tour();
                //distribuer les cartes communes
        }
        private bool FinPartie()
        {
            int idGagnant = getGagnant();
            joueur[idGagnant].ajouterArgent(pot);
            Console.WriteLine("Bravo Joueur "+ idGagnant);
            pot = 0;
            //remise a zéro des joueurs
            for (int i = 0; i < 4; i++)
            {
                joueur[i].ResetMain();
                joueur[i].actif = true;
            }
            miseParJoueur = 0;
            lePaquet.Reinitialiser();

            //continuez ??
            Console.WriteLine("voulez vous continuez une nouvelle partie [1:oui][2:non]");
            int choix = SelectionDansMenu(1, 2);
            if (choix==1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private void AfficherCarte(Tour tourAfficher)
        {

        }
        private void updateEtatTour()
        {

        }
        static private int SelectionDansMenu(int min, int max)
        {
            int reponse;
            bool test;
            do
            {
                test = int.TryParse(Console.ReadLine(), out reponse);
                if (!test)
                {
                    Console.WriteLine("saisie invalide");
                }
            } while (!test || !(reponse <= max) || !(reponse >= min));
            return reponse;
        }

    }
}

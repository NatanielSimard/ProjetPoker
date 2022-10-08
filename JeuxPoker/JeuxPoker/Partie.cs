using System;
using System.Collections.Generic;
using System.Linq;
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

        public void JouerTour()
        {
            for (int j = 0; j < 5; j++)
            {
                AfficherCarte(leTour);
                //rotation de table
                for (int i = 0; i < 4; i++)
                {
                    int montantMinDuJoueur = miseParJoueur;
                    if (joueur[i].actif)
                    {
                        AfficherJeu(joueur[i]);
                        //[1:Coucher][2:check][3:call][4:Raise][5:miser]
                        int mode = SelectionDansMenu(1, 5);
                        bool retour = true;
                        //selecteur et verif du choix
                        do
                        {
                            switch (mode)
                            {
                                case 1:
                                    joueur[i].coucher();
                                    break;
                                case 2:
                                    joueur[i].Check();
                                    break;
                                case 3:
                                    int ajouterPot;
                                    if (joueur[i].call(montantMinDuJoueur,out ajouterPot))
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
                                case 4:
                                    if (montantMinDuJoueur != 0)
                                    {
                                        int mise = DemanderMise();
                                        int montant;

                                        if (joueur[1].Raise(montantMinDuJoueur, mise, out montant))
                                        {
                                            if (montant == -1)
                                            {
                                                pot = pot + montantMinDuJoueur;
                                                montantMinDuJoueur = 0;
                                            }
                                            else
                                            {
                                                pot = pot + montantMinDuJoueur+montant;
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
                                case 5:
                                    if (montantMinDuJoueur == 0)
                                    {
                                        int mise = DemanderMise();
                                        int montant;

                                        if (joueur[1].miser(mise, out montant))
                                        {
                                            pot = pot + montantMinDuJoueur + montant;
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

            }
            

        }
        public void afficherMain(Joueur joueurAfficher)
        {

        }
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
        private void AfficherJeu(Joueur joueurAfficher)
        {

        }
        public int getGagnant()
        {
            return 1;
        }
        private void UpdaterGagnant(Joueur joueurUp)
        { 
            
        
        }
        private bool FinPartie()
        {
            return true;
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

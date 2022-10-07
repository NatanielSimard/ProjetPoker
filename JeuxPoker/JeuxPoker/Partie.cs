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
                    if (joueur[i].actif)
                    {
                        AfficherJeu();
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
                                    if (miseParJoueur != 0)
                                    {
                                        pot = pot + joueur[i].call(miseParJoueur);
                                    }
                                    else
                                    {
                                        retour = false;
                                        Console.WriteLine("option invalide Mise est de 0");
                                        Console.ReadKey();
                                    }

                                    break;
                                case 4:
                                    if (miseParJoueur != 0)
                                    {
                                        Console.WriteLine("combien Voulez vous misé ?");
                                        bool verif = true;

                                        int montantRaiser;
                                        do
                                        {
                                            verif = int.TryParse(Console.ReadLine(), out montantRaiser);
                                            if (!verif)
                                            {
                                                Console.WriteLine("montant invalide");
                                            }
                                        } while (!verif);
                                        if (!(montantRaiser > joueur[i].argent))
                                        {
                                            pot = pot + joueur[i].Raise(miseParJoueur, montantRaiser);
                                        }
                                        else
                                        {
                                            retour = false;
                                            Console.WriteLine("montant trop haut pour votre total");
                                            Console.ReadKey();
                                        }


                                    }
                                    else
                                    {
                                        Console.WriteLine("veuiller choisir miser, aucune mise est créer");
                                        retour = false;
                                    }


                                    break;
                                case 5:
                                    Console.WriteLine("combien Voulez vous misé ?");
                                    bool verif1 = true;

                                    int montantMiser;
                                    do
                                    {
                                        verif1 = int.TryParse(Console.ReadLine(), out montantMiser);
                                        if (!verif1)
                                        {
                                            Console.WriteLine("montant invalide");
                                        }
                                    } while (!verif1);
                                    if (!(montantMiser > joueur[i].argent))
                                    {
                                        pot = pot + joueur[i].miser(montantMiser);
                                    }
                                    else
                                    {
                                        retour = false;
                                        Console.WriteLine("montant trop haut pour votre total");
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
        private void AfficherJeu()
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

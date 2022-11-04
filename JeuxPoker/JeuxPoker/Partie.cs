using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
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
        

        public Partie(int montantDedepart)
        {
            joueur = new Joueur[4];
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine("Quelle est le nom du joueur #" + (i + 1));
                string nom =Console.ReadLine();
                joueur[i] = new Joueur(nom, montantDedepart);
            }
            lePaquet = new Paquet();
            leTour = new Tour();
            pot = 0;
            miseParJoueur = 2;

            
        }

        //---section des fonction de partie


        /// <summary>
        /// la fonction permet de jouer une partie , elle retourne l'intention des joueurs de jouer un nouvelle partie dans un bool
        /// </summary>
        /// <returns></returns>
        public bool jouerPartie()
        {
            bool continuer;
            demarrerPartie();
            for (int i = 0; i < 4; i++)
            {
                JouerRotation();
                //retourner Carte
                leTour.changerEtat();
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
                if (!RepeterUneRotation)
                {
                    miseParJoueur = 0;
                    for (int h = 0; h < 4; h++)
                    {
                        joueur[h].DernièreMise = 0;
                    }
                }
                RepeterUneRotation = false;

                //boucler pour chaque joueur
                for (int i = 0; i < 4; i++)
                {
                    int montantMinDuJoueur = miseParJoueur - joueur[i].DernièreMise;
                    float testMise=0;
                    //est un morceau de code qui permet l'arret du tour quand tous les joueurs on atteint la mise demander de l84 a l98
                    for (int h = 0; h < 4; h++)
                    {
                        testMise = testMise + joueur[h].DernièreMise;
                    }
                    int intJoueurActif=0;
                    for (int g = 0; g < 4; g++)
                    {
                        if (joueur[i].actif)
                        {
                            intJoueurActif++;
                        }
                    }
                    if (!(testMise != 0 && testMise/intJoueurActif == joueur[1].DernièreMise ))

                    {
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
                                            joueur[i].DernièreMise = ajouterPot;
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
                                                    joueur[i].DernièreMise = montant;
                                                    Console.WriteLine("vous N'avez pas assez d'argent pour miser");
                                                    Console.ReadKey();
                                                }
                                                else
                                                {
                                                    RepeterUneRotation = true;
                                                    pot = pot + montantMinDuJoueur + montant;
                                                    miseParJoueur = montant;
                                                    joueur[i].DernièreMise = montant;
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
                                                if (i != 0)
                                                {
                                                    RepeterUneRotation = true;
                                                }
                                                pot = pot + montant;
                                                miseParJoueur = montant;
                                                joueur[i].DernièreMise = montant;
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
            } while (RepeterUneRotation);
            
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
        public Joueur getGagnant()
        {
            List<Int64> listResultat = new List<Int64>();
            for (int i = 0; i < 4; i++)
            {
                joueur[i].maMain.mainFinale = MainFinale(joueur[i]);
            }
            for (int j = 0; j < 4; j++)
            {
                if (joueur[j].actif)
                {
                    joueur[j].maMain.DeterminerForceMain();
                    listResultat.Add(joueur[j].maMain.valeurMain);
                }
            }
            Int64 resultatHaut;
            listResultat.Sort();
            resultatHaut = listResultat[listResultat.Count-1];
            for (int i = 0; i < 4; i++)
            {
                if (joueur[i].actif)
                {
                    if (joueur[i].maMain.valeurMain==resultatHaut)
                    {
                        return joueur[i];

                    }
                }
            }
            return null;


            
        }

        public List<Carte> MainFinale(Joueur joueurChoix)
        {
            List<Carte> mainFinale = new List<Carte>();
            List<Carte> mainChoix2 = new List<Carte>();
            mainChoix2.Add(joueurChoix.maMain.cartes[0]);
            mainChoix2.Add(joueurChoix.maMain.cartes[1]);
            mainChoix2.Add(leTour.carteCommune[0]);
            mainChoix2.Add(leTour.carteCommune[1]);
            mainChoix2.Add(leTour.carteCommune[2]);
            mainChoix2.Add(leTour.carteCommune[3]);
            mainChoix2.Add(leTour.carteCommune[4]);

            Console.WriteLine("quelle cartes voulez vous choisir ?");
            Console.WriteLine();

            int choixCarte;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < mainChoix2.Count; j++)
                {
                    afficherCarte(mainChoix2[j]);
                    Console.Write(" [" + (j+1) + "]");
                    Console.WriteLine("");
                }
                Console.WriteLine("Choisir la carte #" + (i+1));
                choixCarte = SelectionDansMenu(1, 7);
                mainFinale.Add(mainChoix2[choixCarte-1]);
                mainChoix2.RemoveAt(choixCarte-1);
                Console.Clear();
            }
            return mainFinale;

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
                for (int j = 0; j < 4; j++)
                {
                    lePaquet.Distribuer(joueur[j]);
                }
            }
            //initialiser le tour
            leTour = new Tour();
            //distribuer les cartes communes
            lePaquet.FlopTurnRiver(leTour);
        }
        private bool FinPartie()
        {
            Joueur jG = getGagnant();
            jG.ajouterArgent(pot);
            Console.WriteLine("Bravo " + jG.nom);
            pot = 0;
            //remise a zéro des joueurs
            for (int i = 0; i < 4; i++)
            {
                joueur[i].ResetMain();
                joueur[i].actif = true;
            }
            miseParJoueur = 0;
            lePaquet.Reinitialiser();
            leTour.ResetTour();
            lePaquet.Reinitialiser();
            //continuez ??
            Console.WriteLine("voulez vous continuez une nouvelle partie [1:oui][2:non]");
            int choix = SelectionDansMenu(1, 2);
            if (choix == 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }



        //section de l'affichage--------------------------------------------



        /// <summary>
        /// affiche le jeu completement, donc tous les partie, la main, les cartes communes les mise, le pot et le montant du joueur
        /// 
        /// </summary>
        /// <param name="joueurAfficher"></param>
        /// <param name="montantApayer"></param>
        private void AfficherJeu(Joueur joueurAfficher,int montantApayer)
        {
            Console.Clear();
            Console.WriteLine("tour: "+leTour.etatTour);
            Console.Write("nom: " + joueurAfficher.nom);
            Console.Write("\t\t pot:"+pot+" |");
            Console.Write(" montant devant etre miser: " + montantApayer + " |");
            Console.Write(" montant en banque: " + joueurAfficher.argent + " |");
            Console.WriteLine("");
            Console.Write("carte commune: \t\t");
            AfficherCarteTour();
            Console.WriteLine("");
            Console.Write("\t\t");
            afficherMain(joueurAfficher);
            Console.WriteLine("");
            Console.WriteLine("[1:Coucher][2:check][3:call][4:Raise][5:miser]");
        }
        public void afficherMain(Joueur joueurAfficher)
        {
            afficherCarte(joueurAfficher.maMain.cartes[0]);
            Console.Write(" ");
            afficherCarte(joueurAfficher.maMain.cartes[1]);
        }

        static public void afficherCarte(Carte carteAafficher)
        {
            switch (carteAafficher.laCouleur)
            {
                case "Coeur":
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("♥");
                    break;
                case "Carreau":
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("♦");
                    break;
                case "Trefle":
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("♣");
                    break;
                case "Pique":
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("♠");
                    break;
                default:
                    break;
            }
            switch (carteAafficher.lechiffre)
            {
                case "As":
                    Console.Write("A");
                    break;
                case "Roi":
                    Console.Write("K");
                    break;
                case "Dame":
                    Console.Write("Q");
                    break;
                case "Valet":
                    Console.Write("J");
                    break;
                case "Dix":
                    Console.Write("10");
                    break;
                case "Neuf":
                    Console.Write("9");
                    break;
                case "Huit":
                    Console.Write("8");
                    break;
                case "Sept":
                    Console.Write("7");
                    break;
                case "Six":
                    Console.Write("6");
                    break;
                case "Cinq":
                    Console.Write("5");
                    break;
                case "Quatre":
                    Console.Write("4");
                    break;
                case "Trois":
                    Console.Write("3");
                    break;
                case "Deux":
                    Console.Write("2");
                    break;

                default:
                    Console.Write("#");
                    break;
                    
            }
            Console.ForegroundColor = ConsoleColor.Gray;

        }
        private void AfficherCarteTour()
        {
            Tour tourAfficher = leTour;
            for (int i = 0; i < 5; i++)
            {
                Console.Write("\t");
                if (tourAfficher.carteCommune[i].visible)
                {
                    afficherCarte(tourAfficher.carteCommune[i]);
                }
                else
                {
                    Console.Write("##");
                }
            }
        }



        //---Section d'aide a la classe

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

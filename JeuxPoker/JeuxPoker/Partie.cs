using System;
using System.Collections.Generic;
using System.Linq;
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
            for (int i = 0; i < 4; i++)
            {
                if (joueur[i].actif)
                {
                    AfficherJeu();
                    //[1:Coucher][2:check][3:call][4:Raise][5:miser]
                    int mode = SelectionDansMenu(1, 5);
                    int retour = 0;
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
                                if (miseParJoueur!=0)
                                {
                                    joueur[i].call(miseParJoueur);
                                }
                                else
                                {
                                    Console.WriteLine("choisissez un autre option");
                                    Console.ReadKey();
                                }
      
                                break;
                            case 4:
                                joueur[i].coucher();
                                break;
                            case 5:
                                joueur[i].coucher();
                                break;



                            default:
                                break;
                        }
                    } while (retour == -1);

                }

            }
        }
        private void AfficherJeu()
        {

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

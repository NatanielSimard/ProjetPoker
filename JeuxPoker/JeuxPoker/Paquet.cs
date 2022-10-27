using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeuxPoker
{
     public class Paquet

    // Infos Cartes
    // Chiffre 0 pour 2 et 12 pour As
    // Couleur 0 pour Coeur , 1 pour Carreau , 2 pour Trefle , 3 pour Pique.

    {
        Random random;
        public Carte[] TableauInitial = new Carte[52];
        Carte[] TableauTampon = new Carte[52];
        List<int> TableauTamponList = new List<int>();
        public Paquet()
        {
            Reinitialiser();
        }

        public void Distribuer(Joueur j)
        {
            if (j.maMain.cartes[0] == null)
            {
                j.maMain.cartes[0] = GetTopCard();
            }
            else
            {
                j.maMain.cartes[1] = GetTopCard();
            }

        }

        
        public void Melanger()
        {
            //Mise en tampon
            TableauTampon = TableauInitial;
            //Vider le Tableau
            TableauInitial = new Carte[52];
            //Numération de la liste
            for(int i = 0; i < 52; i++)
            {
                TableauTamponList.Add(i);
            }
            random = new Random();
            for (int i = 0; i < 52; i++)
            {
                // Le count du tableau s'actualisera a chaque boucle.
                int nb = TableauTamponList.Count;
                int index = random.Next(0, nb);
                //On met la carte du tableauTampon qui en position index random de la liste dans le tableau initial.
                TableauInitial[i] = TableauTampon[TableauTamponList[index]];
                TableauTamponList.Remove(index);
            }


        }
        public void Reinitialiser()
        {
            TableauInitial = new Carte[52] ;
            
            for (int i = 0; i < 4; i++)
            {
                //Création Coeur
                if (i == 0)
                {
                    for (int j = 0; j < 13; j++)
                    {
                        TableauInitial[j] = new Carte(j, i);
                    }
                }
                //Création Carreau
                else if (i == 1)
                {
                    for (int j = 0; j < 13; j++)
                    {
                        TableauInitial[j+13] = new Carte(j, i);
                    }
                }
                //Création Trefle
                else if (i == 2)
                {
                    for (int j = 0; j < 13; j++)
                    {
                        TableauInitial[j + 26] = new Carte(j, i);
                    }
                }
                //Création Pique
                else if (i == 3)
                {
                    for (int j = 0; j < 13; j++)
                    {
                        TableauInitial[j + 39] = new Carte(j, i);
                    }
                }
            }
        }
        public void FlopTurnRiver(Tour t)
        {
            for (int i = 0; i < 3; i++)
            {
                t.carteCommune[i] = GetTopCard();
            }
            GetTopCard();
            t.carteCommune[3] = GetTopCard();
            GetTopCard();
            t.carteCommune[4] = GetTopCard();

        }
        public Carte GetTopCard()
        {
            Carte laCarte;          
            for (int i = 0; i < 52; i++)
            {
                if (TableauInitial[i].lechiffre != null)
                {
                    laCarte = TableauInitial[i];
                    TableauInitial[i] = new Carte(-1, -1);
                    return laCarte;
                }
            }
            return null;
        }
    }
}

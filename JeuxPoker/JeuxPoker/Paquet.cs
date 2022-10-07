using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeuxPoker
{
    internal class Paquet

    // Infos Cartes
    // Chiffre 0 pour 2 et 12 pour As
    // Couleur 0 pour Coeur , 1 pour Carreau , 2 pour Trefle , 3 pour Pique.

    {
        Random random;
        Carte[] TableauInitial = new Carte[52];
        Carte[] TableauTampon = new Carte[52];
        List<int> TableauTamponList = new List<int>();
        public Paquet()
        {

        }

        public void Distribuer(Joueur j)
        {
        }
        public void Melanger()
        {
            //Mise en tampon
            TableauTampon = TableauInitial;
            //Vider le Tableau
            TableauInitial = null;
            //Numération de la liste
            for(int i = 0; i < 52; i++)
            {
                TableauTamponList.Add(i);
            }

            for (int i = 0; i < 52; i++)
            {
                // Le count du tableau s'actualisera a chaque boucle.
                int nb = TableauTamponList.Count;
                random = new Random(nb);
                int index = random.Next(nb);
                //On met la carte du tableauTampon qui en position index random de la liste dans le tableau initial.
                TableauInitial[i] = TableauTampon[TableauTamponList[index]];
            }


        }
        public void Reinitialiser()
        {
            TableauInitial = null;
            
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
        }
    }
}

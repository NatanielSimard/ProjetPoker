using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeuxPoker
{
   

    public class MainJoueur
    {
        public Carte[] cartes;
        public Int64 valeurMain;
        public MainJoueur()
        {
            cartes = new Carte[2];
        }
        public List<Carte> mainFinale = new List<Carte>();

        public Int64 DeterminerForceMain()
        {
            List<int> tabVal = new List<int>();
            for (int i = 0; i < mainFinale.Count; i++)
            {
                tabVal.Add(mainFinale[i].Getvalue());
            }
            tabVal.Sort();
            int c1,c2,c3,c4,c5;
            c1=tabVal[0];
            c2 = tabVal[1];
            c3 = tabVal[2];
            c4 = tabVal[3];
            c5 = tabVal[4];
            if (mainFinale[0].laCouleur == mainFinale[1].laCouleur && mainFinale[1].laCouleur == mainFinale[2].laCouleur && mainFinale[2].laCouleur == mainFinale[3].laCouleur && mainFinale[3].laCouleur == mainFinale[4].laCouleur)
            {
                if (c5 == c4 + 1 && c4 == c3 + 1 && c3 == c2 + 1 && c2 == c1 + 1)
                {
                    if (c5 == 12)
                    {
                        //Quinte Flush Royale
                        valeurMain = 0x900000;
                    }
                    else
                    {
                        //Quinte Flush
                        valeurMain =0x800000 + creationForce(c1, c2, c3, c4, c5);
                    }
                }
                //couleur
                else
                {
                    valeurMain = 0x500000 + creationForce(c1, c2, c3, c4, c5);
                }
            }
            else
            {
                //pair
                int valPair1, valPair2,rejet;
                if (FindPaire(c1, c2, c3, c4, c5, out valPair1,out valPair2,out rejet))
                {
                    //2 pair diff minimum
                    if (valPair1!=-1 && valPair2!=-1 && valPair1 !=valPair2)
                    {
                        int valTriple, valPair;
                        if (findTriple(c1,c2,c3,c4,c5,out valTriple,out valPair))
                        {
                            //full
                            valeurMain = 0x600000 + creationForce(valTriple, valTriple, valTriple, valPair, valPair);
                            

                        }
                        else
                        {
                            //double pair
                            valeurMain = 0x200000 + creationForce(valPair1, valPair1, valPair2, valPair2, rejet);
                            
                        }
                    }
                    else
                    {
                        if (valPair1==valPair2)
                        {
                            valeurMain = 0x700000 + creationForce(valPair1, valPair1, valPair1, valPair1, rejet);
                            
                        }
                        else
                        {
                            int triple;
                            if (findTriple(c1,c2,c3,c4,c5 ,out triple, out int r))
                            {
                                for (int i = 0; i < tabVal.Count; i++)
                                {
                                    if (tabVal[i] == triple)
                                    {
                                        tabVal.RemoveAt(i);
                                    }
                                    tabVal.Sort();
                                    valeurMain = 0x300000 + creationForce(triple, triple, triple, tabVal[1], tabVal[0]);
                                    
                                }
                            }
                            else
                            {
                                FindPaire(c1, c2, c3, c4, c5, out  int pair, out int rej,out int re);
                                for (int i = 0; i < tabVal.Count; i++)
                                {
                                    if (tabVal[i] == pair)
                                    {
                                        tabVal.RemoveAt(i);
                                    }
                                    tabVal.Sort();
                                    valeurMain = 0x100000 + creationForce(pair, pair, tabVal[2], tabVal[1], tabVal[0]);
                                    
                                }
                            }
                        }
                    }
                }
                else
                {
                    if(c5 == c4 + 1 && c4 == c3 + 1 && c3 == c2 + 1 && c2 == c1 + 1)
                    {
                        //Quinte
                        valeurMain = 0x400000 + creationForce(c1, c2, c3, c4, c5);
                    }
                    else
                    {
                        //Hauteur
                        valeurMain = 0x000000 + creationForce(c1, c2, c3, c4, c5);
                        
                    }
                }
            }




            return valeurMain;
        }
        private int DeciToHexa(int deci)
        {
            int hexa=0x0;
            switch (deci)
            {
                case 0:
                    hexa = 0x2;
                    break;
                case 1:
                    hexa = 0x3;
                    break;
                case 2:
                    hexa = 0x4;
                    break;
                case 3:
                    hexa = 0x5;
                    break;
                case 4:
                    hexa = 0x6;
                    break;
                case 5:
                    hexa = 0x7;
                    break;
                case 6:
                    hexa = 0x8;
                    break;
                case 7:
                    hexa = 0x9;
                    break;
                case 8:
                    hexa = 0xA;
                    break;
                case 9:
                    hexa = 0xB;
                    break;
                case 10:
                    hexa = 0xC;
                    break;
                case 11:
                    hexa = 0xD;
                    break;
                case 12:
                    hexa = 0xE;
                    break;
                    default:
                    break;

            }
            return hexa;
            
        }
        private int creationForce(int c1, int c2, int c3, int c4, int c5)
        {
            int retour;
            retour = DeciToHexa(c5) * 0x10000 + DeciToHexa(c4) * 0x1000 + DeciToHexa(c3) * 0x100 + DeciToHexa(c2) * 0x10 + DeciToHexa(c1);
            return retour;
        }
        private bool FindPaire(int c1, int c2, int c3, int c4, int c5, out int pair1 , out int pair2, out int rejet)
        {
            List<int> list = new List<int>();
            list.Add(c1);
            list.Add(c2);
            list.Add(c3);
            list.Add(c4);
            list.Add(c5);
            pair1 =-1;
            pair2 =-1;
            rejet = -1;
            bool trouverPair=false;
            while (list.Count != 0)
            {
                for (int i = 1; i < list.Count(); i++)
                {
                    if (list[0] == list[i])
                    {
                        if (trouverPair)
                        {
                            list.RemoveAt(i);
                            list.RemoveAt(0);
                            pair2 = i;
                        }
                        else
                        {
                            list.RemoveAt(i);
                            list.RemoveAt(0);
                            trouverPair = true;
                            pair1 = i;
                        }


                    }

                }
                if (list.Count !=0)
                {
                    rejet = list[0];
                    list.RemoveAt(0);
                }


                
            }
            if (pair2 > pair1)
            {
                int buffer = pair2;
                pair1 = pair2;
                pair2 = buffer;
            }
            return trouverPair;

        }
        private bool findTriple(int c1, int c2, int c3, int c4, int c5, out int triple,out int doubleValeur)
        {
            FindPaire(c1, c2, c3, c4, c5, out int p1 ,out int p2,out int rej);
            List<int> list = new List<int>();
            list.Add(c1);
            list.Add(c2);
            list.Add(c3);
            list.Add(c4);
            list.Add(c5);
            triple = -1;
            doubleValeur = -1;
            int p1Cpt = 0;
            int p2Cpt = 0;
            for (int i = 0; i < 5; i++)
            {
                if (list[i] == p1)
                {
                    p1Cpt++;
                }
                else if (list[i] == p2)
                {
                    p1Cpt++;
                }
            }
            if (p1Cpt == 3)
            {
                triple = p1;
                doubleValeur = p2;
                return true;
            }
            else if (p2Cpt == 3)
            {
                triple = p1;
                doubleValeur = p2;
                return true;
            }
            else
            {
                return false;
            }
        }
    }

}

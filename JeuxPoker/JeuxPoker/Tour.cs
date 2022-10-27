using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeuxPoker
{
    public class Tour
    {
        public Carte[] carteCommune { get;  set; }
        public int etatTour { get; private set; }

        public Tour()
        {
            etatTour = 1;
            carteCommune = new Carte[5];
        }
        public void changerEtat()
        {
            if (etatTour <=4)
            {
                if (etatTour==1)
                {
                    carteCommune[0].retourner();
                    carteCommune[1].retourner();
                    carteCommune[2].retourner();
                    etatTour++;
                }
                else if(etatTour==2)
                {
                    carteCommune[3].retourner();
                    etatTour++;
                }
                else if (etatTour == 3)
                {
                    carteCommune[4].retourner();
                    etatTour++;
                }

            }
        }
        public void ResetTour()
        {
            etatTour = 0;
        }
    }
}

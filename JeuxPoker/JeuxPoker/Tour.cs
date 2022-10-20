using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeuxPoker
{
    internal class Tour
    {
        Carte[] carteCommune;
        int etatTour;

        Tour()
        {
            etatTour = 0;
            carteCommune = new Carte[5];
        }
        public void changerEtat()
        {
            if (etatTour <=5)
            {
                carteCommune[etatTour].Retourner();
                etatTour++;
            }
        }
        public void ResetTour()
        {
            etatTour = 0;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeuxPoker
{
    internal class Carte
    {
        // Chiffre 0 pour 2 et 12 pour As
        // Couleur 0 pour Coeur , 1 pour Carreau , 2 pour Trefle , 3 pour Pique.
        int[,] infoCarte;
        public Carte(int[,] infoCarte)
        {
            this.infoCarte = infoCarte;
        }
    }
}

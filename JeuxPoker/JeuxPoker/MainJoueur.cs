using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeuxPoker
{
    internal class MainJoueur
    {
        Carte[] cartes = new Carte[2];
        public MainJoueur(Carte[] les2Cartes)
        {
            cartes= les2Cartes;
        }
    }
}

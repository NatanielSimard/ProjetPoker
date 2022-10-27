using Microsoft.VisualStudio.TestTools.UnitTesting;
using JeuxPoker;
using System.Net.Http.Headers;

namespace TestProject2
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestReinatialiserPaquet()
        {
            //paquet du program
            Paquet paqTest = new Paquet();
            paqTest.Reinitialiser();

            //generer la paquet a la main

            Carte[] trueTab = new Carte[52];
            // créer tableau a la main
            int cpt = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    trueTab[cpt] = new Carte(j, i);
                    cpt++;
                    
                }
            }

            //comparer

            for (int i = 0; i < 52; i++)
            {
                if (!(trueTab[i].lechiffre == paqTest.TableauInitial[i].lechiffre || trueTab[i].laCouleur == paqTest.TableauInitial[i].laCouleur))
                {
                    Assert.Fail();
                }
            }

        }
    }
}

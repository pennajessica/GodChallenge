using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jogo;
using NUnit.Framework;

namespace TesteJogo
{
    [TestFixture]
    public class TesteJogo
    {
        [Test]
        public void TesteRevolver()
        {
            //Instacia de revolver
            Revolver rev = new Revolver();
            //verifica se o revolver disparou os 6 tiros
            for (int i = 0; i < 6; i++)
                Assert.IsTrue(rev.disparar());
            //verifica que o revolver não atira mais quando está vazio
            Assert.IsFalse(rev.disparar());
        }

        [Test]
        public void TesteFaca()
        {
            Faca faca = new Faca();
            //verifica que uma faca não pode disparar
            Assert.IsFalse(faca.disparar());

        }

    }
}

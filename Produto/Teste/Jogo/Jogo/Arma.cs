using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jogo
{
    public class Arma
    {
        protected String Nome;
        protected String tipo;
        protected int qtdeMunicao;

        public Boolean disparar()
        {
            if (tipo == "Faca" || qtdeMunicao <= 0)
                return false;
            else
            {
                qtdeMunicao--;
                return true;
            }


        }
    }
    public class Revolver : Arma
    {
        public Revolver()
        {
            this.Nome = "Revolver 38";
            this.tipo = "Revolver";
            this.qtdeMunicao = 6;

        }
    }
    public class Faca : Arma
    {
        public Faca()
        {
            this.Nome = "Peixeira";
            this.tipo = "Faca";
            this.qtdeMunicao = 0;
        }
       
    }
}

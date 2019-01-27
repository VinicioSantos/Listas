using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listas.Exercicio5
{
    public class ContaCorrente
    {
        public ContaCorrente(string iD, string numero, bool especial, decimal limite)
        {
            ListaMovimentacao = new List<Movimentacao>();
            ID = iD;
            Numero = numero;
            Especial = especial;
            Limite = limite;
        }

        public string ID { get; set; }

        public string Numero { get; set; }

        public bool Especial { get; set; }

        public decimal Limite { get; set; }

        public List<Movimentacao> ListaMovimentacao { get; set; }
    }
}

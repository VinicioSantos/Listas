using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listas.Exercicio5
{
    public class Transferencia
    {
        public ContaCorrente ContaOrigem { get; set; }

        public ContaCorrente ContaDestino { get; set; }

        public decimal Valor { get; set; }

        public DateTime DataOperacao { get; set; }
    }
}

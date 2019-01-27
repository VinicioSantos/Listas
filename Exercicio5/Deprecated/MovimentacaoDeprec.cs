using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listas.Exercicio5
{
    public class MovimentacaoDeprec
    {

        public string ID { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }

        public enum TipoMovimentacao { Credito = 1, Debito = 2 }

        public TipoMovimentacao Tipo { get; set; }
    }
}

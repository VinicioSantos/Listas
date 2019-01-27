using Listas.Exercicio5.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listas.Exercicio5
{
    public class Movimentacao 
    {
        public Movimentacao(string iD, string descricao, decimal valor, EnumTipoMovimentacao tipo)
        {
            ID = iD;
            Descricao = descricao;
            Valor = valor;
            Tipo = tipo;
        }

        public string ID { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public EnumTipoMovimentacao Tipo { get; set; }


    }
}

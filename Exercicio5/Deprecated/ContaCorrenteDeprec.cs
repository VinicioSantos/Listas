using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listas.Exercicio5
{
    public class ContaCorrenteDeprec
    {
        public ContaCorrenteDeprec()
        {
            ListaMovimentacao = new List<MovimentacaoDeprec>();
        }

        public string ID { get; set; }
        public string Numero { get; set; }
        public bool Especial { get; set; }
        public decimal Limite { get; set; }
        public List<MovimentacaoDeprec> ListaMovimentacao { get; set; }

        public static bool IDExiste(BancoDeprec banco, string idConta)
        {
            var consulta = banco.ListaContas.FirstOrDefault(x => x.ID == idConta);

            if (consulta == null)
                return false;
            else
                return true;
        }



    }
}

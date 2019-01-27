using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listas.Exercicio4
{
    public class ItemVenda
    {

        public string ID { get; set; }

        public string Descricao { get; set; }

        public int Quantidade { get; set; }

        public decimal PrecoUnit { get; set; }


        public static bool IDExiste(List<ItemVenda> listaItens, string idItem)
        {
            var consulta = listaItens.FirstOrDefault(x => x.ID == idItem);
            if (consulta == null)
                return false;
            else
                return true;
        }

        public static List<ItemVenda> CadastraItemVenda(List<ItemVenda> listaItemvenda, ItemVenda itemVenda)
        {

            listaItemvenda.Add(itemVenda);

            return listaItemvenda;
        }

        public static bool ItemExisteVenda(Venda venda, string idItem)
        {
            var consulta = venda.ItensVenda.FirstOrDefault(x => x.ID == idItem);

            if (consulta == null)
                return false;
            else
                return true;
        }

    }
}

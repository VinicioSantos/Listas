using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listas.Exercicio4
{
    public class Venda
    {

        public Venda()
        {
            ItensVenda = new List<ItemVenda>();
        }

        public string ID { get; set; }

        public DateTime DataVenda { get; set; }

        public string Vendedor { get; set; }

        public List<ItemVenda> ItensVenda { get; set; }

        public static bool IDExiste(List<Venda> listaVenda, string idVenda)
        {
            var consulta = listaVenda.FirstOrDefault(x => x.ID == idVenda);
            if (consulta == null)
                return false;
            else
                return true;
        }

        public static List<Venda> ExcluirVenda(List<Venda> listaVenda, string idVenda)
        {
            var consulta = listaVenda.FirstOrDefault(x => x.ID == idVenda);

            listaVenda.Remove(consulta);

            return listaVenda;
        }

        public static List<Venda> RemoverItemVenda(List<Venda> listaVenda, string idVenda, string idItem)
        {
            var consulta = listaVenda.FirstOrDefault(x => x.ID == idVenda);
            var itemPExcluir = consulta.ItensVenda.FirstOrDefault(x => x.ID == idItem);

            consulta.ItensVenda.Remove(itemPExcluir);
            return listaVenda;

        }

        public static List<Venda> AlterarItemVenda(List<Venda> listaVenda, string idVenda, string idItem, int qtdItem)
        {
            var consulta = listaVenda.FirstOrDefault(x => x.ID == idVenda);
            var itemPAlterar = consulta.ItensVenda.FirstOrDefault(x => x.ID == idItem);

            itemPAlterar.Quantidade = qtdItem;
            return listaVenda;

        }

        public static Venda GetVenda(List<Venda> listaVenda, string idVenda)
        {
            var consulta = listaVenda.FirstOrDefault(x => x.ID == idVenda);

            return consulta;

        }

        public static List<Venda> PesquisarVenda(List<Venda> listaVenda, string vendedor)
        {
            var consulta = listaVenda.Where(x => x.Vendedor == vendedor).ToList();

            return consulta;
        }

        public static List<Venda> PesquisarVenda(List<Venda> listaVenda, DateTime data)
        {
            var consulta = listaVenda.Where(x => x.DataVenda == data.Date).ToList();

            return consulta;
        }

        public static List<Venda> PesquisarVenda(List<Venda> listaVenda, DateTime dataInicio, DateTime dataFim)
        {
            var consulta = listaVenda.Where(x => x.DataVenda > dataInicio && x.DataVenda < dataFim).ToList();

            return consulta;
        }

    }
}

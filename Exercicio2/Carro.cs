using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listas.Exercicio2
{
    public class Carro
    {
        public string ID { get; set; }
        public string Marca { get; set; }
        public string Cor { get; set; }
        public string AnoFabricacao { get; set; }
        public decimal Preco { get; set; }
        public int QtdePortas { get; set; }
        public decimal Kilometragem { get; set; }



        public List<Carro> CriarCarro(List<Carro> listaCarros, Carro carro)
        {
            listaCarros.Add(carro);

            return listaCarros;
        }

        public static List<Carro> ExcluirCarro(List<Carro> listaCarros, string idCarro)
        {
                 var consulta = listaCarros.FirstOrDefault(x => x.ID == idCarro);

                listaCarros.Remove(consulta);

                return listaCarros;
        }

        public static bool idExiste(List<Carro> listaCarros, string idCarro)
        {

            var consulta = listaCarros.FirstOrDefault(x => x.ID == idCarro);

            if (consulta == null)
                return false;
            else
                return true;


        }
    }
}

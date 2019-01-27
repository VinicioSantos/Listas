using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listas.Exercicio1
{
    public class Pessoa
    {
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public DateTime DataNascimento { get; set; }
        public decimal Salario { get; set; }
        public int QtdeFilhos { get; set; }
        public decimal Altura { get; set; }
        public decimal Peso { get; set; }
        public string NomePai { get; set; }
        public string NomeMae { get; set; }

        public List<Pessoa> CriarPessoa(Pessoa pessoa, List<Pessoa> listaPessoa)
        {
            listaPessoa.Add(pessoa);
            return listaPessoa;
        }
    }
}

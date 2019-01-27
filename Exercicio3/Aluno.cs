using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listas.Exercicio3
{
    public class Aluno
    {
        public string ID { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
        public string NomePai { get; set; }
        public string NomeMae { get; set; }
        public string RA { get; set; }
        public DateTime DataNascimento { get; set; }

        public List<Aluno> CadastraAluno(List<Aluno> listaAlunos, Aluno aluno)
        {
            listaAlunos.Add(aluno);

            return listaAlunos;
        }

        public static bool IDExiste(List<Aluno> listaAlunos, string idAluno)
        {
            var consulta = listaAlunos.FirstOrDefault(x => x.ID == idAluno);
            if (consulta == null)
                return false;
            else
                return true;
        }

        public static List<Aluno> ExcluirAluno(List<Aluno> listaAlunos, string idAluno)
        {
            var consulta = listaAlunos.FirstOrDefault(x => x.ID == idAluno);

            listaAlunos.Remove(consulta);

            return listaAlunos;

        }

        public static List<Aluno> AlterarNome(List<Aluno> listaAlunos, string idAluno, string novoNome)
        {
            var consulta = listaAlunos.FirstOrDefault(x => x.ID == idAluno);

            consulta.Nome = novoNome;

            return listaAlunos;
       
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listas.Exercicio5.Repositories.Contracts
{
    public interface IContaRepository
    {

        void AdicionarConta(string iD, string numero, bool especial, decimal limite);

        void ExcluirConta(string id);

        ContaCorrente GetConta(string id);

        decimal GetSaldo(ContaCorrente conta);

        bool IDExiste(string idConta);

        void Movimentacao(string idConta, Movimentacao movimentacao);


    }
}

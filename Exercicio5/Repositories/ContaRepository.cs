using Listas.Exercicio5.Repositories.Contracts;
using Listas.Exercicio5.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listas.Exercicio5.Repositories
{
    public class ContaRepository : IContaRepository
    {

        private readonly IMovimentacaoService _movimentacao;

        public ContaRepository(IMovimentacaoService movimentacao)
        {
            _movimentacao = movimentacao;
        }

        public ContaRepository()
        {
            ListaContas = new List<ContaCorrente>();
        }

        private List<ContaCorrente> ListaContas { get; set; }

        public void AdicionarConta(string iD, string numero, bool especial, decimal limite)
        {
            ContaCorrente conta = new ContaCorrente(iD,numero,especial,limite);
            ListaContas.Add(conta);
        }

        public void ExcluirConta(string id)
        {
            ListaContas.Remove(GetConta(id));
        }

        public ContaCorrente GetConta(string id)
        {
            var consulta = ListaContas.FirstOrDefault(x => x.ID == id);

            return consulta;
        }

        public decimal GetSaldo(ContaCorrente conta)
        {
            decimal saldo = 0;
            foreach (var movimentacao in conta.ListaMovimentacao)
            {
                if (movimentacao.Tipo == EnumTipoMovimentacao.Credito)
                    saldo += movimentacao.Valor;
                else
                    saldo -= movimentacao.Valor;
            }
            return saldo;
        }

        public bool IDExiste(string idConta)
        {
            var consulta = ListaContas.FirstOrDefault(x => x.ID == idConta);

            if (consulta == null)
                return false;
            else
                return true;
        }

        public void Movimentacao(string idConta, Movimentacao movimentacao)
        {
            var consulta = GetConta(idConta).ListaMovimentacao;
            consulta.Add(movimentacao);
        }

    }
}

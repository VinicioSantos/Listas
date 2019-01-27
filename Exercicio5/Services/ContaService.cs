using Listas.Exercicio5.Repositories.Contracts;
using Listas.Exercicio5.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listas.Exercicio5.Services
{
    public class ContaService : IContaService
    {
        private readonly IContaRepository _contaRepository;

        public ContaService(IContaRepository contaRepository)
        {
            _contaRepository = contaRepository;
        }

        public bool ChecarSaldoLimite(string idConta, Movimentacao movimentacao)
        {
            var consulta = _contaRepository.GetConta(idConta).ListaMovimentacao;
            var conta = _contaRepository.GetConta(idConta);
            

            if (movimentacao.Valor <= _contaRepository.GetSaldo(conta) + conta.Limite)
                return true;
            else
                return false;
        }

        public void Movimentacao(string idConta, Movimentacao movimentacao)
        {
            _contaRepository.Movimentacao(idConta,movimentacao);
        }

        public bool Transferencia(string idContaOrigem, string idContaDestino, decimal valorTransferencia)
        {
            throw new NotImplementedException();
        }
    }
}

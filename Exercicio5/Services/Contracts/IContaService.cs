using Listas.Exercicio5.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listas.Exercicio5.Services.Contracts
{
    public interface IContaService
    {
        bool Transferencia(string idContaOrigem, string idContaDestino, decimal valorTransferencia);

        bool ChecarSaldoLimite(string idConta, Movimentacao movimentacao);

        void Movimentacao(string idConta, Movimentacao movimentacao);

    }
}

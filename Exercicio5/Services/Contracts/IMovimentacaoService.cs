using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listas.Exercicio5.Services.Contracts
{
    public interface IMovimentacaoService
    {

        Movimentacao CriarMovimentacao(string IDConta ,string ID, string Descricao, decimal Valor, EnumTipoMovimentacao Tipo);

    }
}

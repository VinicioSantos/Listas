using Listas.Exercicio5.Repositories.Contracts;
using Listas.Exercicio5.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listas.Exercicio5.Services
{
    public class MovimentacaoService : IMovimentacaoService
    {
        private readonly IMovimentacaoRepository _movimentacaoRepository;

        public MovimentacaoService(IMovimentacaoRepository movimentacaoService)
        {
            _movimentacaoRepository = movimentacaoService;
        }

        public void CriarMovimentacao(string IDConta, string ID, string Descricao, decimal Valor, EnumTipoMovimentacao Tipo)
        {
            Movimentacao movimentacao = new Movimentacao(ID,Descricao,Valor,Tipo);
            _movimentacaoRepository.AdicionarMovimentacao(IDConta, movimentacao);


        }
    }
}

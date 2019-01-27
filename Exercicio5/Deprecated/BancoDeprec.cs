using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listas.Exercicio5
{
    public class BancoDeprec
    {
        public BancoDeprec()
        {
            ListaContas = new List<ContaCorrenteDeprec>();
        }

        public List<ContaCorrenteDeprec> ListaContas { get; set; }


        public void CadastrarConta(ContaCorrenteDeprec conta)
        {
            ListaContas.Add(conta);
        }



        public void ExclusaoConta(string id)
        {
            ListaContas.Remove(GetConta(id));
        }

        public ContaCorrenteDeprec GetConta(string id)
        {
            var consulta = ListaContas.FirstOrDefault(x => x.ID == id);

            return consulta;
        }

        public void Movimentacao(string idConta, MovimentacaoDeprec movimentacao)
        {
            var consulta = GetConta(idConta).ListaMovimentacao;
            consulta.Add(movimentacao);
        }

        public bool ChecarSaldoLimite(string idConta, MovimentacaoDeprec movimentacao)
        {
            var consulta = GetConta(idConta).ListaMovimentacao;

            if (movimentacao.Valor <= GetSaldo(GetConta(idConta)) + GetConta(idConta).Limite){
                return true;
            }
            else
            {
                return false;
            }

        }

        public decimal GetSaldo(ContaCorrenteDeprec conta)
        {
            decimal saldo = 0;
            foreach(var movimentacao in conta.ListaMovimentacao)
            {
                if (movimentacao.Tipo == Exercicio5.MovimentacaoDeprec.TipoMovimentacao.Credito)
                {
                    saldo += movimentacao.Valor;
                }
                else
                {
                    saldo -= movimentacao.Valor;
                }
            }
            return saldo;

        }

        public bool Transferencia(string idConta1, string idConta2, decimal valorTransferencia)
        {
            MovimentacaoDeprec movDebito = new MovimentacaoDeprec();
            Random rand1 = new Random();
            movDebito.ID = rand1.Next(1, 1000).ToString();
            movDebito.Tipo = Exercicio5.MovimentacaoDeprec.TipoMovimentacao.Debito;
            movDebito.Valor = valorTransferencia;
            movDebito.Descricao = String.Format("Transferencia da conta {0} para a conta {1}", idConta1, idConta2);
            

            MovimentacaoDeprec movCredito = new MovimentacaoDeprec();
            Random rand2 = new Random();
            movCredito.ID = rand2.Next(1, 1000).ToString();
            movCredito.Tipo = Exercicio5.MovimentacaoDeprec.TipoMovimentacao.Credito;
            movCredito.Valor = valorTransferencia;
            movCredito.Descricao = String.Format("Transferencia da conta {0} para a conta {1}", idConta1, idConta2);


            if (ChecarSaldoLimite(idConta1, movDebito))
            {
                GetConta(idConta1).ListaMovimentacao.Add(movDebito);
                GetConta(idConta2).ListaMovimentacao.Add(movCredito);


                return true;
            }
            return false;
        }
    }
}

using Listas.Exercicio1;
using Listas.Exercicio2;
using Listas.Exercicio3;
using Listas.Exercicio4;
using Listas.Exercicio5;
using Listas.Exercicio5.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using System.Reflection;
using Listas.Exercicio5.Repositories;
using Listas.Exercicio5.Services.Contracts;

namespace Listas
{
    class Program
    {
        #region Contrutores

        private readonly IContaRepository _contaRepository;
        private readonly IContaService _contaService;
        private readonly IMovimentacaoService _movimentacaoService;

        public Program(){}

        public Program(IContaRepository contarepository, IContaService contaService, IMovimentacaoService movimentacaoService)
        {
            _contaRepository = contarepository;
            _contaService = contaService;
            _movimentacaoService = movimentacaoService;
        }

        #endregion

        static void Main(string[] args)
        {
            Program start = new Program();
            
            string opcao = string.Empty;


            do
            {
                Console.Clear();
                Console.WriteLine(" =================== Listas ===================");
                Console.WriteLine("0) Sair da Aplicação");
                Console.WriteLine("1) Exercício 1 (Pessoas)");
                Console.WriteLine("2) Exercício 2 (Carros)");
                Console.WriteLine("3) Exercício 3 (Alunos)");
                Console.WriteLine("4) Exercício 4 (Vendas)");
                Console.WriteLine("5) Exercício 5 (Banco)");
                opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "0":
                        Environment.Exit(0);
                        break;
                    case "1":
                        Pessoas();
                        break;
                    case "2":
                        Carros();
                        break;
                    case "3":
                        Alunos();
                        break;
                    case "4":
                        Vendas();
                        break;
                    case "5":
                        start.Banco();
                        break;
                    default:
                        Console.WriteLine("Opção Inválida!");
                        break;
                }

            } while (opcao != "0");
        }


        public static Program Config()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            var contaRepository = kernel.Get<IContaRepository>();
            var contaService = kernel.Get<IContaService>();
            var movimentacaoService = kernel.Get<IMovimentacaoService>();
            var formHandler = new Program(contaRepository,contaService,);
            return formHandler;
        }

        private void Banco()
        {
            BancoDeprec banco = new BancoDeprec();




            string opcao = string.Empty;
            do
            {
                Console.Clear();
                Console.WriteLine(" =================== Menu Banco ===================");
                Console.WriteLine("0) Retornar ao menu");
                Console.WriteLine("1) Criar Conta");
                Console.WriteLine("2) Excluir Conta");
                Console.WriteLine("3) Saques");
                Console.WriteLine("4) Depósitos");
                Console.WriteLine("5) Transferências");
                Console.WriteLine("6) Emissão de Saldo/Extrato");
                opcao = Console.ReadLine();
                switch (opcao)
                {
                    case "0":
                        return;
                    case "1":
                        AdicionarConta();
                        Console.ReadKey();
                        break;
                    case "2":
                        ExcluirConta();
                        Console.ReadKey();
                        break;
                    case "3":
                        
                        Console.ReadKey();
                        break;
                    case "4":
                        MovimentacaoDeprec movimentacaoDepto = new MovimentacaoDeprec();
                        Console.Clear();
                        Console.WriteLine("=================== Depositar na Conta ===================");
                        Console.WriteLine("Digite o ID da conta que deseja depositar o Dinheiro: ");
                        string idContaDepto = Console.ReadLine();
                        if (!ContaCorrenteDeprec.IDExiste(banco, idContaDepto))
                            Console.WriteLine("Não existe nenhuma conta cadastrada com este ID!");
                        else
                        {
                            Console.WriteLine("Digite o ID para movimentação: ");
                            movimentacaoDepto.ID = Console.ReadLine();
                            Console.WriteLine("Digite o valor que deseja depositar: ");
                            movimentacaoDepto.Valor = decimal.Parse(Console.ReadLine());
                            Console.WriteLine("Digite uma descrição para o saque: ");
                            movimentacaoDepto.Descricao = Console.ReadLine();
                            movimentacaoDepto.Tipo = MovimentacaoDeprec.TipoMovimentacao.Credito;
                            banco.Movimentacao(idContaDepto, movimentacaoDepto);
                            Console.WriteLine("R${0} depositado com sucesso!", movimentacaoDepto.Valor);
                        }
                        Console.ReadKey();
                        break;
                    case "5":
                        MovimentacaoDeprec movimentacaoTransf = new MovimentacaoDeprec();
                        Console.Clear();
                        Console.WriteLine("=================== Transferências entre Contas ===================");
                        Console.WriteLine("Digite o ID da sua conta: ");
                        string idContaOrigem = Console.ReadLine();
                        if (!ContaCorrenteDeprec.IDExiste(banco, idContaOrigem))
                            Console.WriteLine("Não existe nenhuma conta cadastrada com este ID!");
                        else
                        {
                            Console.WriteLine("Digite o ID da conta para qual pretende transferir: ");
                            string idContaDestino = Console.ReadLine();
                            if (!ContaCorrenteDeprec.IDExiste(banco, idContaDestino))
                                Console.WriteLine("Não existe nenhuma conta cadastrada com este ID!");
                            else
                            {
                                Console.WriteLine("Digite o valor da transferencia: ");
                                decimal valorTransferencia = decimal.Parse(Console.ReadLine());

                                if(banco.Transferencia(idContaOrigem, idContaDestino, valorTransferencia))
                                {
                                    Console.WriteLine("Transferencia feita com sucesso!");
                                }
                                else
                                {
                                    Console.WriteLine("Não foi possível fazer a transferencia.");
                                }


                            }
                        }
                        Console.ReadKey();
                        break;
                    case "6":
                        Console.Clear();
                        Console.WriteLine("===================  Saldo/Extrato ===================");
                        Console.WriteLine("Digite o ID da conta que deseja tirar o extrato: ");
                        string idContaExtrato = Console.ReadLine();
                        if(!ContaCorrenteDeprec.IDExiste(banco, idContaExtrato))
                            Console.WriteLine("Não existe nenhuma conta cadastrada com este ID!");
                        else
                        {
                            decimal saldo = banco.GetSaldo(banco.GetConta(idContaExtrato));
                            Console.WriteLine("Seu saldo atual é de R$ {0}", saldo);
                            Console.WriteLine("Extrato: ");
                            foreach(var mov in banco.GetConta(idContaExtrato).ListaMovimentacao)
                            {
                                Console.WriteLine("ID: {0}, Descrição: {1}, Valor: {2}, Tipo Movimentação: {3}", mov.ID, mov.Descricao, mov.Valor, mov.Tipo);
                                Console.WriteLine("==============================================================");
                            }
                        }
                        Console.ReadKey();
                        break;

                }


            } while (opcao != "0");
            Console.ReadKey();
        }

        public static void AdicionarConta()
        {
            var _conta = Config();

            Console.Clear();
            Console.WriteLine("=================== Criar Contas ===================");
            Console.WriteLine("Digite o ID da conta: ");
            string ID = Console.ReadLine();
            Console.WriteLine("Digite o número da conta: ");
            string Numero = Console.ReadLine();
            Console.WriteLine("A conta é especial? (Sim/Não)");
            string especial = Console.ReadLine();
            bool bEspecial;
            if (especial.ToLower() == "não")
                bEspecial = false;
            else if (especial.ToLower() == "sim")
                bEspecial = true;
            else
                bEspecial = false;

            Console.WriteLine("Digite o limite da conta: ");
            decimal limite = decimal.Parse(Console.ReadLine());

            _conta._contaRepository.AdicionarConta(ID,Numero,bEspecial,limite);

        }

        public static void ExcluirConta()
        {
            var formHandler = Config();
            Console.Clear();
            Console.WriteLine("=================== Excluir Contas ===================");
            Console.WriteLine("Digite o ID da conta que deseja excluir: ");
            string idContaExclusao = Console.ReadLine();
            if (formHandler._contaRepository.IDExiste(idContaExclusao))
                Console.WriteLine("Não existe nenhuma conta cadastrada com este ID!");
            else
            {
                formHandler._contaRepository.ExcluirConta(idContaExclusao);
                Console.WriteLine("Conta Excluida com sucesso!");
            }
        }

        public static void Sacar()
        {
            var _conta = Config();
            MovimentacaoDeprec movimentacao = new MovimentacaoDeprec();
            Console.Clear();
            Console.WriteLine("=================== Sacar da Conta ===================");
            Console.WriteLine("Digite o ID da conta que deseja sacar Dinheiro: ");
            string idContaSaque = Console.ReadLine();
            if (_conta._contaRepository.IDExiste(idContaSaque))
                Console.WriteLine("Não existe nenhuma conta cadastrada com este ID!");
            else
            {
                Console.WriteLine("Digite o ID para movimentação: ");
                string ID = Console.ReadLine();
                Console.WriteLine("Digite o valor que deseja sacar: ");
                decimal Valor = decimal.Parse(Console.ReadLine());
                Console.WriteLine("Digite uma descrição para o saque: ");
                string Descricao = Console.ReadLine();
                movimentacao.Tipo = MovimentacaoDeprec.TipoMovimentacao.Debito;
                if ( /*banco.ChecarSaldoLimite(idContaSaque, movimentacao)*/)
                {
                    banco.Movimentacao(idContaSaque, movimentacao);
                    Console.WriteLine("R${0} depositado com sucesso!", movimentacao.Valor);
                }
                else
                    Console.WriteLine("Limite Insuficiente!");

            }
        }

        #region Outros

        private static void Vendas()
        {
            Console.Clear();
            string opcao = string.Empty;
            List<Venda> listaVendas = new List<Venda>();

            do
            {
                Console.Clear();
                Console.WriteLine(" =================== Menu Vendas ===================");
                Console.WriteLine("0) Retornar ao menu");
                Console.WriteLine("1) Criar Venda");
                Console.WriteLine("2) Listar Vendas");
                Console.WriteLine("3) Excluir Venda (por ID)");
                Console.WriteLine("4) Excluir Item Venda (por ID da Venda)");
                Console.WriteLine("5) Pesquisar Venda");
                Console.WriteLine("6) Alterar quantidade de um item (por ID da Venda)");
                opcao = Console.ReadLine();
                switch (opcao)
                {
                    case "0":
                        return;
                    case "1":
                        Console.Clear();
                        string opcaoVenda = string.Empty;
                        Venda venda = new Venda();
                        List<ItemVenda> listaItemVenda = new List<ItemVenda>();
                        Console.WriteLine(" =================== Criar Vendas ===================");
                        Console.WriteLine("Digite o nome do Vendedor: ");
                        venda.Vendedor = Console.ReadLine();
                        Console.WriteLine("Digite o ID da Venda: ");
                        venda.ID = Console.ReadLine();
                        venda.DataVenda = DateTime.UtcNow.Date;
                        bool flag = true;
                        while (flag)
                        {
                            Console.Clear();
                            Console.WriteLine(" =================== Criar Vendas ===================");
                            //Console.WriteLine("0) Retornar ao Menu");
                            Console.WriteLine("1) Adicionar Item");
                            Console.WriteLine("2) Finalizar Venda");
                            opcaoVenda = Console.ReadLine();
                            switch (opcaoVenda)
                            {
                                //case "0":
                                //    return;
                                case "1":
                                    ItemVenda itemVenda = new ItemVenda();
                                    Console.Clear();
                                    Console.WriteLine(" =================== Adicionar Produto ===================");
                                    Console.WriteLine("Digite o ID do item: ");
                                    itemVenda.ID = Console.ReadLine();
                                    Console.WriteLine("Digite a descrição do item: ");
                                    itemVenda.Descricao = Console.ReadLine();
                                    Console.WriteLine("Digite a quantidade do item: ");
                                    itemVenda.Quantidade = int.Parse(Console.ReadLine());
                                    Console.WriteLine("Digite o Preço Unitario do item: ");
                                    itemVenda.PrecoUnit = decimal.Parse(Console.ReadLine());
                                    if (!ItemVenda.IDExiste(listaItemVenda, itemVenda.ID))
                                    {
                                        listaItemVenda = ItemVenda.CadastraItemVenda(listaItemVenda, itemVenda);
                                        Console.WriteLine("Item Adicionado com sucesso!");
                                    }else
                                    {
                                        Console.WriteLine("Já existe um item com este ID");
                                    }
                                    Console.ReadKey();
                                    break;
                                case "2":
                                    Console.Clear();
                                    venda.ItensVenda = listaItemVenda;
                                    listaVendas.Add(venda);
                                    flag = false;
                                    Console.ReadKey();
                                    break;
                                default:
                                    Console.WriteLine("Opção Inválida!");
                                    break;
                            }
                        }
                        break;
                    case "2":
                        Console.Clear();
                        Console.WriteLine(" =================== Listar Vendas ===================");
                        foreach(Venda vendaLista in listaVendas)
                        {
                            Console.WriteLine("ID da Venda: {0}, Data da Venda: {1}, Vendedor: {2}", vendaLista.ID, vendaLista.DataVenda, vendaLista.Vendedor);
                            Console.WriteLine("Itens da Venda: ");
                            foreach(var item in vendaLista.ItensVenda)
                            {
                                Console.WriteLine("ID: {0}, Descrição: {1}, Quantidade: {2}, Preço Unitário: {3}", item.ID, item.Descricao, item.Quantidade, item.PrecoUnit);
                            }
                        }
                        Console.ReadKey();
                        break;
                    case "3":
                        Console.Clear();
                        Console.WriteLine("=================== Excluir Vendas ===================");
                        Console.WriteLine("Digite o ID da Venda que deseja excluir: ");
                        string idVendaExclusao = Console.ReadLine();
                        if (!Venda.IDExiste(listaVendas, idVendaExclusao))
                            Console.WriteLine("Não existe nenhuma venda cadastrada com esse ID!");
                        else
                        {
                            Venda.ExcluirVenda(listaVendas, idVendaExclusao);
                            Console.WriteLine("Venda excluída com sucesso!");

                        }
                        Console.ReadKey();
                        break;
                    case "4":
                        Console.Clear();
                        Console.WriteLine("=================== Excluir Itens de Vendas ===================");
                        Console.WriteLine("Digite o ID da Venda que deseja alterar: ");
                        string idVendaExcluir = Console.ReadLine();
                        if (!Venda.IDExiste(listaVendas, idVendaExcluir))
                            Console.WriteLine("Não existe nenhuma venda cadastrada com esse ID!");
                        else
                        {
                            var vendaAlterar = Venda.GetVenda(listaVendas, idVendaExcluir);
                            Console.WriteLine("Digite o ID do produto que deseja excluir: ");
                            string idItemExcluir = Console.ReadLine();
                            if (!ItemVenda.ItemExisteVenda(vendaAlterar, idItemExcluir))
                                Console.WriteLine("O item não existe dentro da venda!");
                            else { 
                                listaVendas = Venda.RemoverItemVenda(listaVendas, idVendaExcluir, idItemExcluir);
                                Console.WriteLine("Item excluído da venda com sucesso!");
                            }
                        }
                        Console.ReadKey();
                        break;
                    case "5":
                        string opcaoPesquisa = string.Empty;
                        Console.Clear();
                        do
                        {
                            Console.WriteLine(" =================== Pesquisar Vendas ===================");
                            Console.WriteLine("0) Retornar ao menu");
                            Console.WriteLine("1) Por Data");
                            Console.WriteLine("2) Por Período");
                            Console.WriteLine("3) Por Vendedor");
                            opcaoPesquisa = Console.ReadLine();
                            switch (opcaoPesquisa)
                            {
                                case "0":
                                    return;
                                case "1":
                                    Console.WriteLine("Digite a data que deseja pesquisar: ");
                                    DateTime dataPesquisa = DateTime.ParseExact(Console.ReadLine().ToString(),
                            "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                                    if(Venda.PesquisarVenda(listaVendas, dataPesquisa) == null)
                                        Console.WriteLine("Não existe nenhuma venda dentro desta data!");
                                    else
                                    {
                                        List<Venda> resultadoVenda = Venda.PesquisarVenda(listaVendas, dataPesquisa);
                                        foreach(var vendaPesquisa in resultadoVenda)
                                        {
                                            Console.WriteLine("ID da Venda: {0}, Data da Venda: {1}, Vendedor: {2}", vendaPesquisa.ID, vendaPesquisa.DataVenda, vendaPesquisa.Vendedor);
                                            Console.WriteLine("Itens da Venda: ");
                                            foreach (var item in vendaPesquisa.ItensVenda)
                                            {
                                                Console.WriteLine("ID: {0}, Descrição: {1}, Quantidade: {2}, Preço Unitário: {3}", item.ID, item.Descricao, item.Quantidade, item.PrecoUnit);
                                            }
                                        }
                                    }
                                    break;
                                case "2":
                                    Console.WriteLine("Digite a primeira data do período que deseja pesquisar: ");
                                    DateTime periodo1 = DateTime.ParseExact(Console.ReadLine().ToString(),
                            "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                                    Console.WriteLine("Digite a segunda data do período que deseja pesquisar: ");
                                    DateTime periodo2 = DateTime.ParseExact(Console.ReadLine().ToString(),
                            "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                                    if (Venda.PesquisarVenda(listaVendas, periodo1, periodo2) == null)
                                        Console.WriteLine("Não existe nenhuma venda dentro deste período!");
                                    else
                                    {
                                        List<Venda> resultadoVenda = (Venda.PesquisarVenda(listaVendas, periodo1, periodo2));
                                        foreach (var vendaPesquisa in resultadoVenda)
                                        {
                                            Console.WriteLine("ID da Venda: {0}, Data da Venda: {1}, Vendedor: {2}", vendaPesquisa.ID, vendaPesquisa.DataVenda, vendaPesquisa.Vendedor);
                                            Console.WriteLine("Itens da Venda: ");
                                            foreach (var item in vendaPesquisa.ItensVenda)
                                            {
                                                Console.WriteLine("ID: {0}, Descrição: {1}, Quantidade: {2}, Preço Unitário: {3}", item.ID, item.Descricao, item.Quantidade, item.PrecoUnit);
                                            }
                                        }
                                    }
                                    break;
                                case "3":
                                    Console.WriteLine("Digite o nome do Vendedor: ");
                                    string vendedor = Console.ReadLine();
                                    if(Venda.PesquisarVenda(listaVendas, vendedor) == null)
                                        Console.WriteLine("Não existe nenhuma venda com este vendedor!");
                                    else
                                    {
                                        List<Venda> resultadoVenda = Venda.PesquisarVenda(listaVendas, vendedor);
                                        foreach (var vendaPesquisa in resultadoVenda)
                                        {
                                            Console.WriteLine("ID da Venda: {0}, Data da Venda: {1}, Vendedor: {2}", vendaPesquisa.ID, vendaPesquisa.DataVenda, vendaPesquisa.Vendedor);
                                            Console.WriteLine("Itens da Venda: ");
                                            foreach (var item in vendaPesquisa.ItensVenda)
                                            {
                                                Console.WriteLine("ID: {0}, Descrição: {1}, Quantidade: {2}, Preço Unitário: {3}", item.ID, item.Descricao, item.Quantidade, item.PrecoUnit);
                                            }
                                        }
                                    }
                                    break;
                            }
                            Console.ReadKey();
                        } while (opcaoPesquisa != "0");
                        break;
                    case "6":
                        Console.Clear();
                        Console.WriteLine("=================== Alterar Itens de Vendas ===================");
                        Console.WriteLine("Digite o ID da Venda que deseja alterar: ");
                        string idVendaAlterar = Console.ReadLine();
                        if (!Venda.IDExiste(listaVendas, idVendaAlterar))
                            Console.WriteLine("Não existe nenhuma venda cadastrada com esse ID!");
                        else
                        {
                            var vendaAlterar = Venda.GetVenda(listaVendas, idVendaAlterar);
                            Console.WriteLine("Digite o ID do produto que deseja alterar");
                            string idItemAlterar= Console.ReadLine();
                            if (!ItemVenda.ItemExisteVenda(vendaAlterar, idItemAlterar))
                                Console.WriteLine("O item não existe dentro da venda!");
                            else
                            {
                                Console.WriteLine("Digite o novo número para a quantidade :");
                                int quantidadeNova = int.Parse(Console.ReadLine());
                                listaVendas = Venda.AlterarItemVenda(listaVendas, idVendaAlterar, idItemAlterar, quantidadeNova);
                                Console.WriteLine("Item alterado da venda com sucesso!");
                            }
                        }
                        break;
                    default:
                        Console.WriteLine("Opção Invalida");
                        break;
                }

            } while (opcao != "0");
        }

        private static void Alunos()
        {
            Console.Clear();
            string opcao = string.Empty;
            List<Aluno> listaAlunos = new List<Aluno>();

            do
            {
                Console.Clear();
                Console.WriteLine(" =================== Menu Alunos ===================");
                Console.WriteLine("0) Retornar ao menu");
                Console.WriteLine("1) Criar Aluno");
                Console.WriteLine("2) Listar Alunos");
                Console.WriteLine("3) Ecluir Alunos(Pelo ID)");
                Console.WriteLine("4) Pesquisar Aluno(Pelo ID)");
                Console.WriteLine("5) Alterar Nome Aluno (Pelo ID)");


                opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "0":
                        return;
                    case "1":
                        Aluno aluno = new Aluno();
                        Console.Clear();
                        Console.WriteLine("=================== Criar Alunos ===================");
                        Console.WriteLine("Digite o ID do Aluno: ");
                        aluno.ID = Console.ReadLine();
                        Console.WriteLine("Digite o nome do Aluno: ");
                        aluno.Nome = Console.ReadLine();
                        Console.WriteLine("Digite a idade do Aluno: ");
                        aluno.Idade = int.Parse(Console.ReadLine());
                        Console.WriteLine("Digite o nome do pai do Aluno: ");
                        aluno.NomePai = Console.ReadLine();
                        Console.WriteLine("Digite o nome da mãe do Aluno: ");
                        aluno.NomeMae = Console.ReadLine();
                        Console.WriteLine("Digite o RA do Aluno: ");
                        aluno.RA = Console.ReadLine();
                        Console.WriteLine("Digite a data de nascimento do Aluno: ");
                        aluno.DataNascimento = DateTime.ParseExact(Console.ReadLine().ToString(),
                            "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        if (!Aluno.IDExiste(listaAlunos, aluno.ID))
                            listaAlunos = aluno.CadastraAluno(listaAlunos, aluno);
                        else
                            Console.WriteLine("Aluno com ID já existente");
                        Console.ReadKey();
                        break;
                    case "2":
                        Console.Clear();
                        Console.WriteLine("=================== Listar Alunos ===================");
                        if (listaAlunos.Count == 0)
                            Console.WriteLine("Nenhum Aluno Cadastrado!");
                        else
                        {
                            foreach (Aluno alunoLista in listaAlunos)
                            {
                                Console.WriteLine("Id: {0}, Nome: {1}, Idade: {2}, Nome do Pai: {3}, Nome da Mãe: {4}" +
                                    ", RA: {5}, Data de Nascimento: {6}", alunoLista.ID, alunoLista.Nome, alunoLista.Idade, alunoLista.NomePai
                                    , alunoLista.NomeMae, alunoLista.RA, alunoLista.DataNascimento);
                                Console.WriteLine("===================================================");
                            }
                            Console.ReadKey();
                        }
                        Console.ReadKey();
                        break;
                    case "3":
                        Console.Clear();
                        Console.WriteLine("=================== Excluir Alunos ===================");
                        Console.WriteLine("Digite o ID do Aluno");
                        string idAluno = Console.ReadLine();
                        if (Aluno.IDExiste(listaAlunos, idAluno)) {
                            Aluno.ExcluirAluno(listaAlunos, idAluno);
                            Console.WriteLine("Aluno excluído com sucesso!");
                        }
                        else
                            Console.WriteLine("Não Existe aluno com este ID");

                        Console.ReadKey();
                            break;
                    case "4":
                        Console.Clear();
                        Console.WriteLine("=================== Pesquisar Alunos ===================");
                        Console.WriteLine("Digite o ID do Aluno");
                        string idAlunoPesq = Console.ReadLine();
                        if (Aluno.IDExiste(listaAlunos, idAlunoPesq)) { 
                            var alunoPesquisa = listaAlunos.FirstOrDefault(x => x.ID == idAlunoPesq);
                            Console.WriteLine("Id: {0}, Nome: {1}, Idade: {2}, Nome do Pai: {3}, Nome da Mãe: {4}" +
                                    ", RA: {5}, Data de Nascimento: {6}", alunoPesquisa.ID, alunoPesquisa.Nome, alunoPesquisa.Idade, alunoPesquisa.NomePai
                                    , alunoPesquisa.NomeMae, alunoPesquisa.RA, alunoPesquisa.DataNascimento);
                        }
                        else
                            Console.WriteLine("Não Existe aluno com este ID");

                        Console.ReadKey();
                        break;
                    case "5":
                        Console.Clear();
                        Console.WriteLine("=================== Alterar Nome Alunos ===================");
                        Console.WriteLine("Digite o ID do Aluno");
                        string idAlunoPesqNome = Console.ReadLine();
                        if (Aluno.IDExiste(listaAlunos, idAlunoPesqNome)) {
                            Console.WriteLine("Digite o novo nome: ");
                            string nomeNovo = Console.ReadLine();
                            Aluno.AlterarNome(listaAlunos, idAlunoPesqNome, nomeNovo);
                            Console.WriteLine("Nome alterado com sucesso!");
                        }
                        else
                            Console.WriteLine("Não Existe aluno com este ID");
                        Console.ReadKey();
                        break;
                    default:
                        Console.WriteLine("Opção Inválida!");
                        Console.ReadKey();
                        break;
                }
            } while (opcao != "0");
            Console.ReadKey();



        }
    

        private static void Carros()
        {
            Console.Clear();
            string opcao = string.Empty;
            List<Carro> listaCarros = new List<Carro>();

            do
            {
                Console.Clear();
                Console.WriteLine(" =================== Menu Carros ===================");
                Console.WriteLine("0) Retornar ao menu");
                Console.WriteLine("1) Criar Carro");
                Console.WriteLine("2) Listar Carros");
                Console.WriteLine("3) Excluir Carro (por ID)");

                opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "0":
                        return;
                    case "1":
                        Console.Clear();
                        Carro carro = new Carro();
                        Console.WriteLine("=================== Criar Carros ===================");
                        Console.WriteLine("Digite o ID do Carro: ");
                        carro.ID = Console.ReadLine();
                        Console.WriteLine("Digite a marca do carro: ");
                        carro.Marca = Console.ReadLine();
                        Console.WriteLine("Digite a cor do carro: ");
                        carro.Cor = Console.ReadLine();
                        Console.WriteLine("Digite ano de fabricação do carro: ");
                        carro.AnoFabricacao = Console.ReadLine();
                        Console.WriteLine("Digite o preço do carro: ");
                        carro.Preco = decimal.Parse(Console.ReadLine());
                        Console.WriteLine("Digite a quantidade de portas do carro: ");
                        carro.QtdePortas = int.Parse(Console.ReadLine());
                        Console.WriteLine("Digite a quilometragem do carro: ");
                        carro.Kilometragem = decimal.Parse(Console.ReadLine());
                        if (!Carro.idExiste(listaCarros, carro.ID))
                            listaCarros = carro.CriarCarro(listaCarros, carro);
                        else
                            Console.WriteLine("Carro com ID já existente");
                        Console.ReadKey();
                        break;
                    case "2":
                        Console.Clear();
                        Console.WriteLine("=================== Listar Carros ===================");
                        if (listaCarros.Count == 0)
                            Console.WriteLine("Nenhum Carro Cadastrada!");
                        else
                        {
                            foreach (Carro carroLista in listaCarros)
                            {
                                Console.WriteLine("ID: {0}, Marca: {1}, Cor: {2}, Ano de Fabricação: {3}, Preço: {4}" +
                                    ", Quantidade de Portas: {5}, Kilometragem: {6} KM", carroLista.ID, carroLista.Marca, carroLista.Cor, carroLista.AnoFabricacao
                                    , carroLista.Preco, carroLista.QtdePortas, carroLista.Kilometragem);
                                Console.WriteLine("===================================================");
                            }
                        }
                        Console.ReadKey();
                        break;

                    case "3":
                        Console.Clear();
                        Console.WriteLine("=================== Excluir Carros ===================");
                        Console.WriteLine("Digite o ID do carro que deseja excluir: ");
                        string idProcuraCarro = Console.ReadLine();


                        if (Carro.idExiste(listaCarros, idProcuraCarro))
                        {
                            Carro.ExcluirCarro(listaCarros, idProcuraCarro);
                            Console.WriteLine("Carro Excluido com sucesso!");
                        }
                        else
                            Console.WriteLine("Carro não existe!");
                        Console.ReadKey();
                        break;
                    default:
                        Console.WriteLine("Opção Inválida!");
                        Console.ReadKey();
                        break;

                }

            } while (opcao != "0");
        }

        private static void Pessoas()
        {
            Console.Clear();
            string opcao = string.Empty;
            List<Pessoa> listaPessoas = new List<Pessoa>();

            do
            {
                Console.Clear();
                Console.WriteLine(" =================== Menu Pessoas ===================");
                Console.WriteLine("0) Retornar ao menu");
                Console.WriteLine("1) Criar Pessoa");
                Console.WriteLine("2) Listar Pessoas");

                opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "0":
                        return;
                    case "1":
                        Pessoa pessoa = new Pessoa();
                        Console.Clear();
                        Console.WriteLine("=================== Criar Pessoas ===================");
                        Console.WriteLine("Digite o nome da Pessoa: ");
                        pessoa.Nome = Console.ReadLine();
                        Console.WriteLine("Digite o número de Telefone da Pessoa: ");
                        pessoa.Telefone = Console.ReadLine();
                        Console.WriteLine("Digite a Data de Nascimento da Pessoa: ");
                        pessoa.DataNascimento = DateTime.ParseExact(Console.ReadLine().ToString(),
                            "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        Console.WriteLine("Digite o Salário da Pessoa: ");
                        pessoa.Salario = decimal.Parse(Console.ReadLine());
                        Console.WriteLine("Digite a quantidade de filhos da Pessoa: ");
                        pessoa.QtdeFilhos = int.Parse(Console.ReadLine());
                        Console.WriteLine("Digite a Altura da Pessoa: ");
                        pessoa.Altura = decimal.Parse(Console.ReadLine());
                        Console.WriteLine("Digite o peso da Pessoa: ");
                        pessoa.Peso = decimal.Parse(Console.ReadLine());
                        Console.WriteLine("Digite o nome da mãe da Pessoa: ");
                        pessoa.NomeMae = Console.ReadLine();
                        Console.WriteLine("Digite o nome do pai da Pessoa: ");
                        pessoa.NomePai = Console.ReadLine();
                        listaPessoas = pessoa.CriarPessoa(pessoa, listaPessoas);
                        Console.ReadKey();
                        break;
                    case "2":
                        Console.Clear();
                        Console.WriteLine("=================== Listar Pessoas ===================");
                        if(listaPessoas.Count == 0)
                            Console.WriteLine("Nenhuma Pessoa Cadastrada!");
                        else { 
                            foreach (Pessoa pessoaLista in listaPessoas)
                            {
                                Console.WriteLine("Nome: {0}, Telefone: {1}, Data de Nascimento: {2}, Salário: {3}, Quantidade de Filhos: {4}" +
                                    ", Altura: {5}, Peso: {6}, Nome do Pai: {7}, Nome da Mãe: {8}",pessoaLista.Nome,pessoaLista.Telefone,pessoaLista.DataNascimento,pessoaLista.Salario
                                    ,pessoaLista.QtdeFilhos,pessoaLista.Altura,pessoaLista.Peso,pessoaLista.NomePai,pessoaLista.NomeMae);
                                Console.WriteLine("===================================================");
                            }
                        Console.ReadKey();
                        }
                        Console.ReadKey();
                        break;
                    default:
                        Console.WriteLine("Opção Inválida!");
                        Console.ReadKey();
                        break;
                }
            } while (opcao != "0");
            Console.ReadKey();
            

            
        }

        #endregion
    }
}

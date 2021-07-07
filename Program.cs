using System;
using System.Collections.Generic;

namespace DIO.Bank
{
    class Program
    {
        static List<Conta> listContas = new List<Conta>();
        static void Main(string[] args)
        {
            string opcaoUsuario = ObterOpcaoUsuario();
            while (opcaoUsuario.ToUpper() != "X")
			{
				switch (opcaoUsuario)
				{
					case "1":
						ListarContas();
						break;
					case "2":
						InserirConta();
						break;
					case "3":
						Transferir();
						break;
					case "4":
						Sacar();
						break;
					case "5":
						Depositar();
						break;
                    case "C":
						Console.Clear();
						break;

					default:
						Console.WriteLine("Por favor, digite uma opção válida");
						break;
				}

				opcaoUsuario = ObterOpcaoUsuario();
			}
			
			Console.WriteLine("Obrigado por utilizar nossos serviços.");
			Console.ReadLine();
        }

        private static void Transferir()
        {
			if(!VerificarListaContas())
			{
				return;
			}
			
			int indiceContaOrigem = ObterOpcaoInteiro("Digite o número da conta de origem: ");

			if(!ValidarConta(indiceContaOrigem))
				return;
			
			int indiceContaDestino = ObterOpcaoInteiro("Digite o número da conta de destino: ");
			if(!ValidarConta(indiceContaDestino))
				return;			

			double valorTransferencia = ObterOpcaoDouble("Digite o valor a ser transferido: ");

            listContas[indiceContaOrigem].Transferir(valorTransferencia, listContas[indiceContaDestino]);
        }

        private static void Depositar()
        {
			if(!VerificarListaContas())
			{
				return;
			}

			int indiceConta = ObterOpcaoInteiro("Digite o número da conta: ");
			
			if(!ValidarConta(indiceConta))
				return;

			double valorDeposito = ObterOpcaoDouble("Digite o valor a ser depositado: ");

            listContas[indiceConta].Depositar(valorDeposito);
        }

        private static void Sacar()
        {
			if(!VerificarListaContas())
			{
				return;
			}

			int indiceConta = ObterOpcaoInteiro("Digite o número da conta: ");

			if(!ValidarConta(indiceConta))
				return;

			double valorSaque = ObterOpcaoDouble("Digite o valor a ser sacado: ");

            listContas[indiceConta].Sacar(valorSaque);
        }

        private static void ListarContas()
        {
            Console.WriteLine("Listar contas");

			if(!VerificarListaContas())
			{
				return;
			}

			for (int i = 0; i < listContas.Count; i++)
			{
				Conta conta = listContas[i];
				Console.Write($"#{i} - ");
				Console.WriteLine(conta);
			}
        }

        private static void InserirConta()
        {
			int entradaTipoConta;
			double entradaSaldo;
			double entradaCredito;

            Console.WriteLine("Inserir nova conta");

			do
			{
				entradaTipoConta = ObterOpcaoInteiro("Digite 1 para Conta Fisica ou 2 para Juridica: ");
			} while (entradaTipoConta != 1 && entradaTipoConta != 2);
			

			Console.Write("Digite o Nome do Cliente: ");
			string entradaNome = Console.ReadLine();

			do
			{
				entradaSaldo = ObterOpcaoDouble("Digite o saldo inicial: ");
			} while (entradaSaldo < 0);
			
			do
			{
				entradaCredito = ObterOpcaoDouble("Digite o crédito: ");
			} while (entradaCredito < 0);
			

			Conta novaConta = new Conta(tipoConta: (TipoConta)entradaTipoConta,
										saldo: entradaSaldo,
										credito: entradaCredito,
										nome: entradaNome);

			listContas.Add(novaConta);
        }

        private static string ObterOpcaoUsuario()
        {
            Console.WriteLine();
            Console.WriteLine("DIO Bank a seu dispor");
            Console.WriteLine("Informe a opção desejada: ");

            Console.WriteLine("1 - Listar contas");
            Console.WriteLine("2 - Inserir nova conta");
            Console.WriteLine("3 - Transferir");
            Console.WriteLine("4 - Sacar");
            Console.WriteLine("5 - Depositar");
            Console.WriteLine("C - Limpar Tela");
            Console.WriteLine("X - Sair");
            Console.WriteLine();

            string opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine();
            return opcaoUsuario;
        }

		private static int ObterOpcaoInteiro(string mensagem)
		{
			int opcaoInteiro;
			do
			{
				Console.Write(mensagem);
				try
				{
					opcaoInteiro = int.Parse(Console.ReadLine());
					return opcaoInteiro;
				}
				catch (System.Exception)
				{
					Console.WriteLine("Você não digitou um número válido. Tente novamente");
				}
			} while (true);
		}

		private static double ObterOpcaoDouble(string mensagem)
		{
			double opcaoDouble;
			do
			{
				Console.Write(mensagem);
				try
				{
					opcaoDouble = double.Parse(Console.ReadLine().Replace('.', ','));
					return opcaoDouble;
				}
				catch (System.Exception)
				{
					Console.WriteLine("Você não digitou um número válido. Tente novamente");
				}
			} while (true);
		}

		private static bool ValidarConta(int numeroConta)
        {
			for (int i = 0; i < listContas.Count; i++)
			{
				if(numeroConta == i)
				{
					Console.Write($"#{i} - ");
					Console.WriteLine(listContas[i]);
					return true;
				}
			}
			Console.WriteLine("A conta não existe");
			return false;
        }

		private static bool VerificarListaContas()
		{
			if (listContas.Count == 0)
			{
				Console.WriteLine("Nenhuma conta cadastrada.");
				return false;
			}
			return true;
		}


    }
}

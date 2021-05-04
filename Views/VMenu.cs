using System;
using System.Collections.Generic;
using ConsoleApp1.Menus;
using ConsoleApp1.Telas;
using ConsoleApp1.Interfaces;
using ConsoleApp1.Menus.Opcoes;
using ConsoleApp1.Arquivos;
using System.IO;
using ConsoleApp1.Excessoes.Relatorio;

namespace ConsoleApp1.Views
{
    class VMenu : ConsoleCor
    {
        // obtem itens do menu
        public static IList<Menu> itensDoMenu = GetMenuItens();
        private static int opcao;

        public static object NomeArquivo { get; private set; }

        // ponto de entrada após a Main(próprio)
        public static void Executar()
        {
            try
            {
                Console.Clear();
                // inciando a aplicacao
                if (TipoDeConsulta())
                {
                    ExecutarConsulta(opcao);
                    Executar();
                }
                // encerrando a aplicacao
                else
                {
                    Tela.ImprimeEncerramento();
                    Environment.Exit(0);
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"\n\n--- ATENÇÃO!\n\n Erro ao abrir o arquivo {LerArquivo.NomeArquivo} não foi encontrado.\nPor favor verifique o arquivo e tente novamente.");
                Console.ReadLine();
                Executar();
            }
            catch (IOException)
            {
                Console.WriteLine($"\n\n--- ATENÇÃO!\n\n Erro ao acessar o arquivo {LerArquivo.NomeArquivo} está em uso por outro programa.\nPor favor verifique o arquivo e tente novamente.");
                Console.ReadLine();
                Executar();
            }
            catch (RelatorioException)
            {
                Console.WriteLine($"\n\n--- ATENÇÃO!\n\n Erro ao ler o arquivo {LerArquivo.NomeArquivo} não contêm dados\nPor favor verifique o arquivo e tente novamente.");
                Console.ReadLine();
                Executar();
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine($"\n\n--- ATENÇÃO!\n\n Erro ao escrever o arquivo {LerArquivo.NomeArquivo} contêm dados em excesso.\nPor favor verifique o arquivo e tente novamente.");
                Console.ReadLine();
                Executar();
            }
            
        }
        
        // executar consulta
        public static void ExecutarConsulta(int opcao)
        {
            // selecionar o item do menu
            Menu itemCapturado = itensDoMenu[opcao - 2];
            Type tipoClasse = itemCapturado.TipoClasse;
            IOpcaoMenu itemSelecionado = Activator.CreateInstance(tipoClasse) as IOpcaoMenu;

            // imprime opcao selecionada
            Tela.ImprimeOpcaoSelecionada(itemCapturado);

            itemSelecionado.Executar();
            Console.WriteLine("\n\nTecle algo para continuar...".ToUpper());
            Console.ReadLine();
            //return itemSelecionado;
        }

        // ao digitar caracter fora das opcoes, pede para digitar novamente
        private static void DigiteNovamente(int opcao)
        {
            if (opcao == 0 || opcao > GetMenuItens().Count + 1)
            {
                FonteVermelha();
                Tela.ImprimeSeDigitarOpcaoErrada();
                FonteBranca();
                TipoDeConsulta();
            }
        }

        // imprime opcoes de consulta e retorna false se a opcao for nao existente
        public static bool TipoDeConsulta()
        {
            // imprime opcoes do menu e
            Tela.QuaisOpcoes(GetMenuItens());
            // solicita uma opcao ao usuario
            int.TryParse(Console.ReadLine(), out opcao);
            // numero 1 para fechar aplicativo
            if (opcao != 1)
            {
                // ao digitar caracter fora das opcoes, pede para digitar novamente
                DigiteNovamente(opcao);
                // da sequencia a execucao da opcao selecionada
                return true;
            }
            return false;
        }

        // lista de itens do menu principal
        public static IList<Menu> GetMenuItens()
        {
            return new List<Menu>
            {
                new Menu("Consumo Kit COVID-19", typeof(R_CONS_PREV_KIT_COVID))
            };
        }

    }
}

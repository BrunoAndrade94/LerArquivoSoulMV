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
    class VMenu : /*Menu,*/ IExecutar
    {
        // obtem itens do menu
        public static IList<Menu> itensDoMenu;
        private static int opcao;

        public static object NomeArquivo { get; private set; }

        // ponto de entrada após a Main(próprio)
        public void Executar()
        {
            try
            {
                IniciarAplicacao();
            }
            catch (RelatorioException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
                Executar();
            }
            catch (DirectoryNotFoundException)
            {
                RelatorioException.DiretorioNaoEncontrado();
                Executar();
            }
            catch (FileNotFoundException)
            {
                RelatorioException.ArquivoNaoEncontrado();
                Executar();
            }
            catch (ArgumentOutOfRangeException)
            {
                RelatorioException.ArquivoDadosEmExcesso();
                Executar();
            }
            catch (IOException)
            {
                RelatorioException.ArquivoEmUso();
                Executar();
            }
        }

        private static void IniciarAplicacao()
        {
            Console.Clear();
            // inciando a aplicacao
            if (TipoDeConsulta())
            {
                ExecutaConsulta.EConsulta(opcao, GetMenuItens());
                var menu = new VMenu();
                menu.Executar();
            }
            // encerrando a aplicacao
            else
            {
                Tela.ImprimeEncerramento();
                Environment.Exit(0);
            }
        }


        // ao digitar caracter fora das opcoes, pede para digitar novamente
        public static void DigiteNovamente(int opcao)
        {
            if (opcao == 0 || opcao > GetMenuItens().Count + 1)
            {
                ConsoleCor.FonteVermelha();
                Tela.ImprimeSeDigitarOpcaoErrada();
                ConsoleCor.FonteBranca();
                return;
                //TipoDeConsulta();
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
                ExecutaConsulta.DigiteNovamente(opcao, GetMenuItens(), typeof(VMenu));
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
                new Menu("Consumo Previsto Kit COVID-19", typeof(R_CONS_PREV_KIT_COVID)),
                new Menu("Consumo Médio Mensal", typeof(R_CONS_MED_MEN))
            };
        }

    }
}

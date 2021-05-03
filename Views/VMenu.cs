using System;
using System.Collections.Generic;
using ConsoleApp1.Menus;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Interfaces;
using System.Threading;
using ConsoleApp1.Menus.Opcoes;
using static System.Net.Mime.MediaTypeNames;

namespace ConsoleApp1.Views
{
    class VMenu/* : IOpcaoMenu*/
    {
        public static IList<Menu> itensDoMenu = GetMenuItens();
        private static int opcao;

        public static void Executar()
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
                ImprimeEncerramento();                
                Environment.Exit(0);
            }
        }

        private static void ImprimeEncerramento()
        {
            Console.WriteLine();
            string titulo = $"  Encerrando.";
            var espacos = new string('~', 27);
            Console.WriteLine(espacos);
            Console.Write(titulo);
            ImprimeEspera(titulo);
            Console.WriteLine("\n" + espacos);
            Thread.Sleep(1000);
        }

        // imprime um tempo de espera
        private static void ImprimeEspera(string titulo)
        {
            Thread.Sleep(10);
            for (int i = 0;  i < titulo.Length; i++)
            {
                Console.Write('.');
                Thread.Sleep(60);
            }
        }

        // imprime opcao selecionada
        private static void ImprimeOpcaoSelecionada(Menu itemCapturado)
        {
            Console.WriteLine();
            string titulo = $" Executando..: {itemCapturado.Titulo} ";
            string aguarde = $"Aguarde!";
            Console.WriteLine(new string('~', titulo.Length));
            Console.WriteLine(titulo, aguarde);
            Console.WriteLine(new string('~', titulo.Length));
            ImprimeEspera(titulo);
        }

        // executar consulta
        public static void ExecutarConsulta(int opcao)
        {
            // selecionar o item do menu
            IOpcaoMenu itemSelecionado; // pode tirar essa linha
            Menu itemCapturado = itensDoMenu[opcao - 2];
            Type tipoClasse = itemCapturado.TipoClasse;
            itemSelecionado = Activator.CreateInstance(tipoClasse) as IOpcaoMenu;

            // imprime opcao selecionada
            ImprimeOpcaoSelecionada(itemCapturado);

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
                ConsoleColor padrao = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Digite números de 1 a {GetMenuItens().Count + 1}");
                Console.ReadKey();
                Console.Clear();
                Console.ForegroundColor = padrao;
                TipoDeConsulta();
            }
        }

        // imprime opcoes de consulta e retorna false se a opcao for nao existente
        public static bool TipoDeConsulta()
        {
            // imprime opcoes do menu e
            ImprimirOpcoes(GetMenuItens());
            // solicita uma opcao ao usuario
            int.TryParse(Console.ReadLine(), out opcao);
            // numero 1 para fechar aplicativo
            if (opcao == 1)
            {
                return false;
            }
            // ao digitar caracter fora das opcoes, pede para digitar novamente
            DigiteNovamente(opcao);
            // da sequencia a execucao da opcao selecionada
            return true;
        }

        // lista de itens do menu principal
        public static IList<Menu> GetMenuItens()
        {
            return new List<Menu>
            {
                new Menu("Consumo Kit COVID-19", typeof(ConsumoKitCovid))
            };
        }

        // imprimir opcoes
        public static void ImprimirOpcoes(IList<Menu> menuItens)
        {
            int i = 1;
            ConsoleColor f = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine($"\n  Eai Ruderalis! Agora é {DateTime.Now.ToString("g")}");
            Console.WriteLine("  Qual relatório?");
            Console.WriteLine("  1 - Fechar Janela");
            foreach (var menuItem in menuItens)
            {
                Console.WriteLine("  " + (++i).ToString() + " - " + menuItem.Titulo);
            }
            Console.ForegroundColor = f;
        }
    }
}

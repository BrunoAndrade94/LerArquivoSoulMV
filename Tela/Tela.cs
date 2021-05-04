using ConsoleApp1.Menus;
using ConsoleApp1.Views;
using System;
using System.Collections.Generic;
using System.Threading;

namespace ConsoleApp1.Telas
{
    abstract class Tela : ConsoleCor
    {
        public static void ImprimePrintaProdutos<T>(IEnumerable<T> lista)
        {
            Console.WriteLine();
            foreach (T objeto in lista) Console.WriteLine(objeto);
        }

        public static void ImprimeEncerramento()
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

        public static void ImprimeEspera(string titulo)
        {
            Thread.Sleep(10);
            for (int i = 0; i < titulo.Length; i++)
            {
                Console.Write('.');
                Thread.Sleep(60);
            }
        }

        public static void ImprimeOpcaoSelecionada(Menu itemCapturado)
        {
            Console.WriteLine();
            string titulo = $" Executando..: {itemCapturado.Titulo} ";
            string aguarde = $"Aguarde!";
            Console.WriteLine(new string('~', titulo.Length));
            Console.WriteLine(titulo, aguarde);
            Console.WriteLine(new string('~', titulo.Length));
            ImprimeEspera(titulo);
        }

        // ao digitar caracter fora das opcoes, pede para digitar novamente
        public static void ImprimeDigiteNovamente(int opcao)
        {
            if (opcao == 0 || opcao > VMenu.GetMenuItens().Count + 1)
            {
                FonteVermelha();
                Console.WriteLine($"Digite números de 1 a {VMenu.GetMenuItens().Count + 1}");
                Console.ReadKey();
                Console.Clear();
                FonteBranca();
                VMenu.TipoDeConsulta();
            }
        }

        public static void ImprimeSeDigitarOpcaoErrada()
        {
            Console.WriteLine($"Digite números de 1 a {VMenu.GetMenuItens().Count + 1}");
            Console.ReadKey();
            Console.Clear();
        }

        public static void QuaisOpcoes(IList<Menu> menuItens)
        {
            int i = 1;
            FonteAzulDark();
            Console.WriteLine($"\n  Eai Ruderalis! Agora é {DateTime.Now.ToString("g")}");
            Console.WriteLine("  Qual relatório?");
            Console.WriteLine("  1 - Fechar Janela");
            ImprimeOpcoes(menuItens, i);
            FonteBranca();
        }

        private static void ImprimeOpcoes(IList<Menu> menuItens, int i)
        {
            foreach (var menuItem in menuItens)
            {
                Console.WriteLine("  " + (++i).ToString() + " - " + menuItem.Titulo);
            }
        }
    }
}

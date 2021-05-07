using ConsoleApp1.Entidades;
using ConsoleApp1.Menus;
using ConsoleApp1.Views;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace ConsoleApp1.Telas
{
    abstract class Tela : ConsoleCor
    {
        public static void Imprime_R_CONS_PREV_KIT_COVID<T>(IEnumerable<T> lista)
        {
            Console.WriteLine();
            var prod = lista as List<Produto>;

            var cabecalho = ($"{"Codigo",-10}" +
                             $"{"Nome",-50}" +
                             $"{"Consumo Previsto",-18}" +
                             $"{"Saldo Hospital", -18}" +
                             $"{"Consumo",-10}");
            Console.WriteLine(cabecalho);
            Console.WriteLine(new string('=', cabecalho.Length));

            foreach (var obj in prod)
            {
                Console.WriteLine($"{obj.Codigo,-10}" +
                              $"{obj.Nome,-50}" +
                              $"{obj.ConsumoPrevisto,-18}" +
                              $"{obj.SaldoHospital.ToString("F0"),-18}" +
                              $"{obj.Consumo.ToString("F0"),-10}");
            }
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
            Console.WriteLine("  Qual relatório?\n");
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

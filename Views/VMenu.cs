using System;
using System.Collections.Generic;
using ConsoleApp1.Menus;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Interfaces;
using System.Threading;
using ConsoleApp1.Menus.Opcoes;

namespace ConsoleApp1.Views
{
    class VMenu : IOpcaoMenu
    {
        public static IList<Menu> itensDoMenu = GetMenuItens();
        private int opcao = 2;

        public void Executar()
        {
            do
            {
                ImprimirOpcao(GetMenuItens());
                int.TryParse(Console.ReadLine(), out opcao);
                if (opcao == 0 || opcao > itensDoMenu.Count + 1)
                {
                    Console.WriteLine($"Digite números de 1 a {itensDoMenu.Count + 1}");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                }
                Executar2(opcao);
            }
            while (opcao != 1);
            Console.WriteLine("TCHAAUUU");
        }

        public static IOpcaoMenu Executar2(int opcao)
        {
            // selecionar o item do menu
            IOpcaoMenu itemSelecionado;
            Menu itemCapturado = itensDoMenu[opcao - 2];
            Type tipoClasse = itemCapturado.TipoClasse;
            itemSelecionado = Activator.CreateInstance(tipoClasse) as IOpcaoMenu;

            // imprime opcao selecionada
            Console.WriteLine();
            string titulo = $"Executando..: {itemCapturado.Titulo}";
            //Thread.Sleep(200000);
            Console.WriteLine(new string('~', titulo.Length));
            Console.WriteLine(titulo);
            Console.WriteLine(new string('~', titulo.Length));

            itemSelecionado.Executar();
            Console.WriteLine("\n\nTecle algo para continuar...".ToUpper());
            return itemSelecionado;
        }

        public static void TipoDeConsulta()
        {
            IList<Menu> menUItens = GetMenuItens();
            ImprimirOpcao(menUItens);
            int.TryParse(Console.ReadLine(), out int opc);
            if (opc == 0 || opc > menUItens.Count + 1)
            {
                Console.WriteLine($"Digite números de 1 a {menUItens.Count + 1}");
                Console.ReadKey();
                Console.Clear();
            }
            if (opc == 1)
            {
                Console.WriteLine("TCHAAUUU");
            }

            Executar2(opc);
            Console.ReadKey();
        }

        // lista de itens do menu principal
        public static IList<Menu> GetMenuItens()
        {
            return new List<Menu>
            {
                new Menu("Consumo Kit COVID-19", typeof(ConsumoKitCovid))
            };
        }

        public static void ImprimirOpcao(IList<Menu> menuItens)
        {
            int i = 1;
            ConsoleColor f = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine($"\nEai Ruderalis! Agora é {DateTime.Now.ToString("g")}");
            Console.WriteLine("Qual relatório?");
            Console.WriteLine("1 - Fechar Janela");
            foreach (var menuItem in menuItens)
            {
                Console.WriteLine((++i).ToString() + " - " + menuItem.Titulo);
            }
            Console.ForegroundColor = f;
        }

    }
}

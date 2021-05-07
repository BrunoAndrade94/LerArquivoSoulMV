using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Interfaces;
using ConsoleApp1.Menus;
using ConsoleApp1.Telas;

namespace ConsoleApp1
{
    abstract class ExecutaConsulta
    {
        public static void DigiteNovamente(int opcao, ICollection<Menu> GetMenuItens, Type menu)
        {
            // recebe parametros verifica
            var listaMenu = GetMenuItens as IList<Menu>;
            var menuRecebido = new Menu(menu.FullName, menu);
            Type tipoClasse = menuRecebido.TipoClasse;
            var itemSelecionado = Activator.CreateInstance(tipoClasse) as IExecutar;
            
            if (opcao == 0 || opcao > listaMenu.Count + 1)
            {
                ConsoleCor.FonteVermelha();
                Tela.ImprimeSeDigitarOpcaoErrada();
                ConsoleCor.FonteBranca();
                itemSelecionado.Executar();
            }
            return;
        }

        public static void EConsulta(int opcao, IList<Menu> itensDoMenu)
        {
            // selecionar o item do menu
            Menu itemCapturado = itensDoMenu[opcao - 2];
            Type tipoClasse = itemCapturado.TipoClasse;
            var itemSelecionado = Activator.CreateInstance(tipoClasse) as IExecutar;

            // imprime opcao selecionada
            Tela.ImprimeOpcaoSelecionada(itemCapturado);

            itemSelecionado.Executar();
            Console.WriteLine("\n\nTecle algo para continuar...");
            Console.ReadKey();
            //return itemSelecionado;
        }
    }
}

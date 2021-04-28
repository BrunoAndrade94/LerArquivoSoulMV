using ConsoleApp1.Interfaces;
using ConsoleApp1.Arquivos;
using ConsoleApp1.Views.Printar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Entidades;

namespace ConsoleApp1.Menus.Opcoes
{
    class ConsumoKitCovid : IOpcaoMenu
    {
        private static HashSet<int> ListaMedicKitIntubacao = new HashSet<int> { 55051, 55478, 54544, 55478, 54930, 54930, 55148, 55385, 55441, 55495, 55610, 55791 };
        public void Executar()
        {
            // implementar codigo para gerar relatorio consumo kit covid
            var posicaosaldo = LerArquivo.Ler(LerArquivo.C_R_POS_EST_S);
            var consumopac = LerArquivo.Ler(LerArquivo.C_R_LIST_CONS_PAC);
            TempoDeUso(posicaosaldo, consumopac);
        }

        // criar nova lista com os medicamentos do kit intubacao com consumo previsto
        private static List<Produto> TempoDeUso(List<Produto> listaSaldo, List<Produto> listaConsumo)
        {
            List<Produto> listaTempoUso = new List<Produto>();
            for (int i = 0; i < listaConsumo.Count; i++)
            {                
                for (int j = 0; j < listaSaldo.Count; j++)
                {
                    if (listaConsumo[i].Codigo == listaSaldo[j].Codigo)
                    {
                        // add produto na lista
                        if (ListaMedicKitIntubacao.Contains(listaConsumo[i].Codigo))
                        {
                            listaTempoUso.Add(new Produto(
                              Produto.Construto(listaConsumo[i].Codigo,
                              listaConsumo[i].Nome,
                              listaSaldo[j].SaldoHospital / listaConsumo[i].Consumo,
                              listaSaldo[i].SaldoHospital)));
                        }
                    }
                }
            }
            return listaTempoUso;
        }

        // criar nova lista com os medicamentos do kit intubacao com consumo previsto
        //public static void ListaConsumoPrevisto<T>(List<T> lista)
        //{
        //    Produto[] p = lista.ToArray() as Produto[];
        //    List<Produto> lp = new List<Produto>();
        //    for (int a = 0; a < lista.Count; a++)
        //    {
        //        if (ListaMedicKitIntubacao.Contains(p[a].Codigo))
        //        {
        //            lp.Add(p[a]);
        //        }
        //    }
        //    //Arquivos.EscreverArquivo(lp);
        //    //Tela.PrintaProdutos("consumo", lp);
        //    Console.WriteLine();
        //}

    }
}

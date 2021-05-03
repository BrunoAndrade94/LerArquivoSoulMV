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
            // le arquivo e grava posicaoSaldo
            var posicaoSaldo = LerArquivo.Ler(LerArquivo.C_R_POS_EST_S);

            // le arquivo e grava consumoPaciente
            var consumoPaciente = LerArquivo.Ler(LerArquivo.C_R_LIST_CONS_PAC);

            // gerar lista consumoPrevisto
            var consumoPrevisto = TempoDeUso(posicaoSaldo, consumoPaciente);

            // gerar arquivo consumoPrevisto
            EscreverArquivo.Escrever(consumoPrevisto);
            //PrintaProdutos(consumoPrevisto);
        }

        private static void PrintaProdutos<T>(IEnumerable<T> lista)
        {
            Console.WriteLine();
            foreach (T objeto in lista) Console.WriteLine(objeto);
        }

        // cria nova lista com os medicamentos do kit intubacao com consumo previsto
        private static List<Produto> TempoDeUso(List<Produto> listaSaldo, List<Produto> listaConsumo)
        {
            // cria nova lista de produtos
            List<Produto> listaTempoUso = new List<Produto>();
            // 0 até tamanho da lista de consumo
            for (int i = 0; i < listaConsumo.Count; i++)
            {
                // 0 até tamanho da lista de saldo
                for (int j = 0; j < listaSaldo.Count; j++)
                {
                    // verifica o codigo do medicamentos kit intubacao
                    if (listaConsumo[i].Codigo == listaSaldo[j].Codigo)
                    {
                        // add produto kit intubacao na lista
                        if (ListaMedicKitIntubacao.Contains(listaConsumo[i].Codigo))
                        {
                            listaTempoUso.Add(new Produto(
                              Produto.Construto(listaConsumo[i].Codigo,
                              listaConsumo[i].Nome,
                              Math.Round(listaSaldo[j].SaldoHospital / listaConsumo[i].Consumo),
                              listaSaldo[i].SaldoHospital)));
                        }
                    }
                }
            }
            // retorna a lista de produtos
            return listaTempoUso;
        }
    }
}

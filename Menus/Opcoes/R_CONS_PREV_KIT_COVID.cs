using ConsoleApp1.Interfaces;
using ConsoleApp1.Arquivos;
using System;
using System.Collections.Generic;
using ConsoleApp1.Entidades;

namespace ConsoleApp1.Menus.Opcoes
{
    public class R_CONS_PREV_KIT_COVID : IOpcaoMenu
    {
        // lista com codigo dos medicamentos do kit intubação
        // lista errada , alterar
        private static HashSet<int> ListaMedicKitIntubacao = new HashSet<int> { 55051, 55478, 54544, 54930, 55148, 55385, 55441, 55495, 55610, 55791 };

        // ponto de entrada
        // implementar codigo para gerar relatorio consumo kit covid
        public void Executar()
        {
            // le arquivo e grava posicaoSaldo
            var posicaoSaldo = LerArquivo.Ler(LerArquivo.C_R_POS_EST_S);

            // le arquivo e grava consumoPaciente
            var consumoPaciente = LerArquivo.Ler(LerArquivo.C_R_LIST_CONS_PAC);

            // gerar lista consumoPrevisto
            var consumoPrevisto = TempoDeUso(posicaoSaldo, consumoPaciente);

            // gerar arquivo consumoPrevisto
            EscreverArquivo.Escrever(consumoPrevisto);
            //ImprimePrintaProdutos(consumoPrevisto);
        }

        // cria nova lista com os medicamentos do kit intubacao com consumo previsto
        public static List<Produto> TempoDeUso(List<Produto> listaSaldo, List<Produto> listaConsumo)
        {
            // refazer esse trecho, relatorio gerado com erros

            // cria nova lista de produtos
            var listaTempoUso = new List<Produto>();
            var listaSaldo2 = new List<Produto>();
            var listaConsumo2 = new List<Produto>();
            // 0 até tamanho da lista de consumo
            try
            {
                // ainda falta arrumar
                for (int i = 0; i < listaSaldo.Count; i++)
                {
                    if (ListaMedicKitIntubacao.Contains(listaSaldo[i].Codigo))
                    {
                        listaSaldo2.Add(new Produto()
                        {
                            Codigo = listaSaldo[i].Codigo,
                            Nome = listaSaldo[i].Nome,
                            SaldoHospital = listaSaldo[i].SaldoHospital
                        });
                    }
                }
                // 0 até tamanho da lista de saldo
                for (int j = 0; j < listaConsumo.Count; j++)
                {
                    if (ListaMedicKitIntubacao.Contains(listaConsumo[j].Codigo))
                    {
                        listaConsumo2.Add(new Produto()
                        {
                            Codigo = listaConsumo[j].Codigo,
                            Nome = listaConsumo[j].Nome,
                            Consumo = listaConsumo[j].Consumo
                        });
                    }
                }
                for (int i = 0; i < listaSaldo2.Count; i++)
                {
                    if (listaSaldo2[i].Codigo == listaConsumo2[i].Codigo)
                    {
                        listaConsumo2.Add(new Produto()
                        {
                            Codigo = listaConsumo[i].Codigo,
                            Nome = listaConsumo[i].Nome,
                            ConsumoPrevisto = listaSaldo2[i].SaldoHospital / listaConsumo[i].Consumo,
                            SaldoHospital = listaSaldo2[i].SaldoHospital
                        });
                    }
                }
                // verifica o codigo do medicamentos kit intubacao
                //if (listaSaldo[i].Codigo == listaConsumo[j].Codigo)
                //{
                //    // add produto kit intubacao na lista
                //    if (ListaMedicKitIntubacao.Contains(listaSaldo[i].Codigo))
                //    {
                //        AdicionarNaLista(listaSaldo, listaConsumo, listaTempoUso, i, j);
                //    }

                //}


            }
            catch (Exception) { }




// retorna a lista de produtos
return listaTempoUso;
        }

        private static void AdicionarNaLista(List<Produto> listaSaldo, List<Produto> listaConsumo, List<Produto> listaTempoUso, int i, int j)
{
    listaTempoUso.Add(new Produto(
      Produto.Construto(listaConsumo[i].Codigo,
      listaConsumo[i].Nome,
      Math.Round(listaSaldo[i].SaldoHospital / listaConsumo[j].Consumo + 0),
      listaSaldo[i].SaldoHospital)));
}
    }
}

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
                            AdicionarNaLista(listaSaldo, listaConsumo, listaTempoUso, i, j);
                        }
                    }
                }
            }
            // retorna a lista de produtos
            return listaTempoUso;
        }

        private static void AdicionarNaLista(List<Produto> listaSaldo, List<Produto> listaConsumo, List<Produto> listaTempoUso, int i, int j)
        {
            listaTempoUso.Add(new Produto(
              Produto.Construto(listaConsumo[i].Codigo,
              listaConsumo[i].Nome,
              Math.Round(listaSaldo[j].SaldoHospital / listaConsumo[i].Consumo),
              listaSaldo[i].SaldoHospital)));
        }
    }
}

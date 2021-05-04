using ConsoleApp1.Interfaces;
using ConsoleApp1.Arquivos;
using System;
using System.Collections.Generic;
using ConsoleApp1.Entidades;
using ConsoleApp1.Excessoes.Relatorio;
using System.Collections;
using System.Linq;

namespace ConsoleApp1.Menus.Opcoes
{
    public class R_CONS_PREV_KIT_COVID : IOpcaoMenu
    {
        // lista com codigo dos medicamentos do kit intubação
        // lista errada , alterar
        private static HashSet<int> ListaKI = new HashSet<int> {
            55051, 55478, 54544, 56487,
            54930, 55148, 55385, 55441,
            55495, 55610, 55791, 64164 };

        //private static HashSet<int> ListaKI = GetCodigoKI();       
        //private static HashSet<int> GetCodigoKI()
        //{

        //    ListaKI.Add(55051);
        //    ListaKI.Add(55478);
        //    ListaKI.Add(54544);
        //    ListaKI.Add(56487);
        //    ListaKI.Add(54930);
        //    ListaKI.Add(55148);
        //    ListaKI.Add(55385);
        //    ListaKI.Add(55441);
        //    ListaKI.Add(55495);
        //    ListaKI.Add(55610);
        //    ListaKI.Add(55791);
        //    ListaKI.Add(64164);
        //    return ListaKI;
        //}

        // ponto de entrada chamada pela Main()
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

            RelatorioException.SeEhListaVazia(listaSaldo);
            RelatorioException.SeEhListaVazia(listaConsumo);
            try
            {   // primeira filtragem, gerando lista com medicamentos kit intubação
                foreach (var produtoSaldo in listaSaldo)
                {
                    if (ListaKI.Contains(produtoSaldo.Codigo))
                    {
                        foreach (var produtoConsumo in listaConsumo)
                        {
                            if (ListaKI.Contains(produtoConsumo.Codigo))
                            {
                                if (produtoSaldo.Codigo == produtoConsumo.Codigo)
                                {
                                    ListaKI.Remove(produtoSaldo.Codigo);
                                    AddProdComConsumo(listaTempoUso, produtoSaldo, produtoConsumo);
                                }
                            }
                        }
                    }
                }

                foreach (var produtoSaldo in listaSaldo)
                {
                    if(ListaKI.Contains(produtoSaldo.Codigo))
                    {
                        AddProdSemConsumo(listaTempoUso, produtoSaldo);
                    }
                }
            }
            catch (Exception) { }
            return listaTempoUso;
        }

        private static void AddProdSemConsumo(List<Produto> listaTempoUso, Produto produtoSaldo)
        {
            listaTempoUso.Add(new Produto()
            {
                Codigo = produtoSaldo.Codigo,
                Nome = produtoSaldo.Nome,
                ConsumoPrevisto = 0,
                SaldoHospital = produtoSaldo.SaldoHospital
            });
        }

        private static void AddProdComConsumo(List<Produto> listaTempoUso, Produto produtoSaldo, Produto produtoConsumo)
        {
            listaTempoUso.Add(new Produto()
            {
                Codigo = produtoSaldo.Codigo,
                Nome = produtoSaldo.Nome,
                ConsumoPrevisto = Math.Round(produtoSaldo.SaldoHospital / produtoConsumo.Consumo),
                SaldoHospital = produtoSaldo.SaldoHospital
            });
        }

        //private static void AdicionarNaLista(List<Produto> listaSaldo, List<Produto> listaConsumo, List<Produto> listaTempoUso, int i, int j)
        //{
        //    listaTempoUso.Add(new Produto(
        //      Produto.Construto(listaConsumo[i].Codigo,
        //      listaConsumo[i].Nome,
        //      Math.Round(listaSaldo[i].SaldoHospital / listaConsumo[j].Consumo + 0),
        //      listaSaldo[i].SaldoHospital)));
        //}
    }
}

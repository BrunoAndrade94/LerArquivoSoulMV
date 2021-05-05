using ConsoleApp1.Interfaces;
using ConsoleApp1.Arquivos;
using System;
using System.Collections.Generic;
using ConsoleApp1.Entidades;
using ConsoleApp1.Excessoes.Relatorio;
using System.Collections;
using System.Linq;
using ConsoleApp1.Views;
using ConsoleApp1.Telas;

namespace ConsoleApp1.Menus.Opcoes
{
    public class R_CONS_PREV_KIT_COVID : IOpcaoMenu
    {
        // lista com codigo dos medicamentos do kit intubação
        private static HashSet<int> ListaKI = GetListaKI();

        private static HashSet<int> GetListaKI()
        {
            // medicamentos do kit intubação
            return new HashSet<int> {
            55051, 55478, 54544, 56487,
            54930, 55148, 55385, 55441,
            55495, 55610, 55791, 64164 };
        }

        // ponto de entrada chamada pela Main()
        public void Executar()
        {
            // le arquivo e grava posicaoSaldo
            var posicaoSaldo = LerArquivo.Ler(LerArquivo.C_R_POS_EST_S);

            // le arquivo e grava consumoPaciente
            var consumoPaciente = LerArquivo.Ler(LerArquivo.C_R_LIST_CONS_PAC);

            // gerar lista consumoPrevisto
            var consumoPrevisto = TempoDeUso(posicaoSaldo, consumoPaciente);
            
            // ordena pelo codigo do produto
            consumoPrevisto.Sort();
            
            // gerar arquivo consumoPrevisto
            EscreverArquivo.Escrever(consumoPrevisto);
            
            Tela.Imprime_R_CONS_PREV_KIT_COVID(consumoPrevisto);
        }

        // cria nova lista com os medicamentos do kit intubacao com consumo previsto
        public static List<Produto> TempoDeUso(List<Produto> listaSaldo, List<Produto> listaConsumo)
        {
            // cria nova lista de produtos
            var listaTempoUso = new List<Produto>();
            // verifica se a lista está vazia
            RelatorioException.SeEhListaVazia(listaSaldo);
            RelatorioException.SeEhListaVazia(listaConsumo);
            try
            {
                AddProdSaldoEConsumo(listaSaldo, listaConsumo, listaTempoUso);
                AddProdApenasSaldo(listaSaldo, listaTempoUso);
            }
            catch (RelatorioException e) 
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
                VMenu.Executar();
            }
            finally // realocar a lista novamente
            {
                ListaKI = GetListaKI();
            }
            return listaTempoUso;
        }

        // adiciona produtos que nao tiveram consumo
        private static void AddProdApenasSaldo(List<Produto> listaSaldo, List<Produto> listaTempoUso)
        {
            foreach (var produtoSaldo in listaSaldo)
            {   // verifica se contem o produto na lista
                // que nao foi removido da filtragem anterior
                if (ListaKI.Contains(produtoSaldo.Codigo))
                {
                    AddProdSemConsumo(listaTempoUso, produtoSaldo);
                }
            }
        }

        // adiciona produtos que tiveram consumo
        private static void AddProdSaldoEConsumo(List<Produto> listaSaldo, List<Produto> listaConsumo, List<Produto> listaTempoUso)
        {
            foreach (var produtoSaldo in listaSaldo)
            {   //verifica se o codigo contem na lista de saldo
                if (ListaKI.Contains(produtoSaldo.Codigo))
                {   // percorre a lista de consumo
                    foreach (var produtoConsumo in listaConsumo)
                    {   // verifica se o codigo contem na lista de saldo
                        if (ListaKI.Contains(produtoConsumo.Codigo))
                        {   // verifica se os produtos sao os mesmo nas duas listas
                            if (produtoSaldo.Codigo == produtoConsumo.Codigo)
                            {   // remove da lista o codigo encontrado
                                ListaKI.Remove(produtoSaldo.Codigo);
                                // adiciona produto na lista de tempo de uso
                                AddProdComConsumo(listaTempoUso, produtoSaldo, produtoConsumo);
                            }
                        }
                    }
                }
            }
        }

        // adiciona no arquivo produto que nao teve consumo
        private static void AddProdSemConsumo(List<Produto> listaTempoUso, Produto produtoSaldo)
        {
            listaTempoUso.Add(new Produto()
            {
                Codigo = produtoSaldo.Codigo,
                Nome = produtoSaldo.Nome,
                ConsumoPrevisto = 0,
                SaldoHospital = produtoSaldo.SaldoHospital,
                Consumo = 0
            });
        }

        // adiciona no arquivo produto que teve consumo
        private static void AddProdComConsumo(List<Produto> listaTempoUso, Produto produtoSaldo, Produto produtoConsumo)
        {
            // se o consumo for menor que um mes, ira escrever no arquivo (-1)
            if (Math.Round(produtoSaldo.SaldoHospital / produtoConsumo.Consumo) == 0)
            {
                listaTempoUso.Add(new Produto()
                {
                    Codigo = produtoSaldo.Codigo,
                    Nome = produtoSaldo.Nome,
                    ConsumoPrevisto = -1,
                    SaldoHospital = produtoSaldo.SaldoHospital,
                    Consumo = produtoConsumo.Consumo
                });
            }
            // se o consumo for acima de um mes, ira escrever no arquivo o consumo
            else
            {
                listaTempoUso.Add(new Produto()
                {
                    Codigo = produtoSaldo.Codigo,
                    Nome = produtoSaldo.Nome,
                    ConsumoPrevisto = Math.Round(produtoSaldo.SaldoHospital / produtoConsumo.Consumo),
                    SaldoHospital = produtoSaldo.SaldoHospital,
                    Consumo = produtoConsumo.Consumo
                });
            }
        }
    }
}

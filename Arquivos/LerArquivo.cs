using ConsoleApp1.Entidades;
using ConsoleApp1.Abstratas;
using ConsoleApp1.Views;
using ConsoleApp1.Relatorios;
using System.Collections.Generic;
using System.IO;
using System;
using ConsoleApp1.Excessoes.Relatorio;

namespace ConsoleApp1.Arquivos
{
    class LerArquivo : AArquivo
    {
        public static string NomeArquivo { get; set; }
        public static List<Produto> Ler(string arquivo)
        {
            // cria lista de produtos
            List<Produto> listaProduto = new List<Produto>();
            // le relatorio csv
            LeCSVArquivoRelatorio(arquivo, listaProduto);
            return listaProduto;
        }

        // retorna o nome do arquivo
        private static bool VerificaNomeArquivo(string arquivo)
        {
            var nome = new FileInfo(arquivo).Name;
            if (R_POS_EST_S.EhMeuNome(nome)) return true;
            if (R_LIST_CONS_PAC.EhMeuNome(nome)) return true;
            return false;
        }

        // responsavel por ler e gerar listas e lançar excessões caso tenha
        private static List<Produto> LeCSVArquivoRelatorio(string arquivo, List<Produto> listaProduto)
        {
            if (VerificaNomeArquivo(arquivo))
            {
                try
                {
                    using (var fs = new FileStream(arquivo, FileMode.Open, FileAccess.Read))
                    {
                        using (var ler = new StreamReader(fs))
                        {
                            while (!ler.EndOfStream)
                            {
                                QualArquivo(listaProduto, ler);
                            }
                        }
                    }
                    if (listaProduto.Count == 0) throw new RelatorioException();
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine($"\n\n--- ATENÇÃO! Arquivo {NomeArquivo} não encontrado.");
                    Console.ReadLine();
                    VMenu.Executar();
                }
                catch (IOException)
                {
                    Console.WriteLine("\n\n" + $@"--- ATENÇÃO! Arquivo {NomeArquivo} 
          em uso por outro programa.");
                    Console.ReadLine();
                    VMenu.Executar();
                }
                catch (RelatorioException)
                {
                    Console.WriteLine("\n\n" + @$"ATENÇÃO! Arquivo {LerArquivo.NomeArquivo} não contêm dados
                    Por favor verifique o arquivo
                    e tente novamente");
                    Console.ReadLine();
                    VMenu.Executar();
                }
                return listaProduto;
            }
            return null;
        }

        // responvel por definir o fluxo do programa, qual arquivo direciono?
        private static void QualArquivo(List<Produto> listaProduto, StreamReader ler)
        {
            if (NomeArquivo == R_POS_EST_S.Nome)
            {
                R_POS_EST_S.Escrita(ler.ReadLine(), listaProduto);
            }
            if (NomeArquivo == R_LIST_CONS_PAC.Nome)
            {
                R_LIST_CONS_PAC.Escrita(ler.ReadLine(), listaProduto);
            }
        }
    }
}

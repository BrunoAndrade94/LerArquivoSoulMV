using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Entidades
{
    public class Produto
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Lote { get; set; }
        public string Unidade { get; set; }
        public DateTime Validade { get; set; }
        public double SaldoLote { get; set; }

        public double SaldoLoteTotal { get; set; }
        public double SaldoHospital { get; set; }
        public double Consumo { get; set; }
        public double ConsumoPrevisto { get; set; }

        public Produto() { }
        public Produto(Produto produto)
        {
            Codigo = produto.Codigo;
            Nome = produto.Nome;
            Unidade = produto.Unidade;
            ConsumoPrevisto = produto.ConsumoPrevisto;
            SaldoHospital = produto.SaldoHospital;

        } // podera acrescentar mais atributos
        public Produto(int codigo, string nome, string lote, DateTime validade)
        {
            Codigo = codigo;
            Nome = nome;
            Lote = lote;
            Validade = validade;
        }
        public static Produto Construto(int codigo, string nome, double consumoPrevisto, double saldoHospital)
        {
            Produto prod = new Produto
            {
                Codigo = codigo,
                Nome = nome,
                ConsumoPrevisto = double.Parse(consumoPrevisto.ToString("F2")),
                SaldoHospital = double.Parse(saldoHospital.ToString("F2"))
            };
            return prod;
        }


        // usado em R_LIST_CONS_PAC
        public Produto(int codigo, string nome, string unidade, double consumo)
        {
            Codigo = codigo;
            Nome = nome;
            Unidade = unidade;
            Consumo = consumo;
        }

        public override string ToString()
        {
            return $@"{Codigo, -7} {Nome, -20} {Unidade, -9} {Lote, -10} {Validade.ToString("d"), -10} {SaldoHospital, -8} {ConsumoPrevisto, -8}";
        }
    }
}

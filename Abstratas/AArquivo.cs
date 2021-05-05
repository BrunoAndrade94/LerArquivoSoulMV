using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Abstratas
{
    // classe com caminho dos arquivos
    abstract class ADiretorioArquivos
    {

#if (DEBUG) // diretório em modo DEBUG

        // arquivo posicao simplificada
        public static string C_R_POS_EST_S { get; private set; } = (@"C:\Users\Usuário\Desktop\R_POS_EST_S.csv");

        // arquivo consumo medicamento
        public static string C_R_LIST_CONS_PAC { get; private set; } = (@"C:\Users\Usuário\Desktop\R_LIST_CONS_PAC.csv");

        // novo arquivo cosumo movimento
        public static string R_CONS_PREV_KIT_COVID { get; private set; } = (@"C:\Users\Usuário\Desktop\R_CONS_PREV_KIT_COVID.csv");

#elif (RELEASE) // diretório em modo RELEASE
        // arquivo posicao simplificada
        public static string C_R_POS_EST_S { get; private set; } = (@"C:R_POS_EST_S.csv");
        // arquivo consumo medicamento
        public static string C_R_LIST_CONS_PAC { get; private set; } = (@"C:R_LIST_CONS_PAC.csv");
        // novo arquivo cosumo movimento
        public static string R_CONS_PREV_KIT_COVID { get; private set; } = (@"C:R_CONS_PREV_KIT_COVID.csv");
#endif
    }
}

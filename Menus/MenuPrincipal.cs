using ConsoleApp1.Menus.Opcoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Menus
{
    class Menu
    {
        public string Titulo { get; set; }
        public Type TipoClasse { get; set; }

        public Menu(string titulo, Type tipoClasse)
        {
            Titulo = titulo;
            TipoClasse = tipoClasse;
        }


    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _LFP_Proyecto1
{
    class token
    {
        public int NoToken { get; set; }
        public string Lexema { get; set; }
        public string tipo { get; set; }
        public int fila { get; set; }
        public int columna { get; set; }

        public token(int noToken, String lexema, String tipo, int fila, int columna)
        {
            this.NoToken = noToken;
            this.Lexema = lexema;
            this.tipo = tipo;
            this.fila = fila;
            this.columna = columna;

        }

    }
}

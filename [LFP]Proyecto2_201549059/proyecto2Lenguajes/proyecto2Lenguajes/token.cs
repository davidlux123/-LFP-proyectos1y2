using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto2Lenguajes
{
    public class token
    {

        public int NoToken { get; set; }
        public int iDtoken { get; set; }
        public string Lexema { get; set; }
        public string tipo { get; set; }
        public int fila { get; set; }
        public int columna { get; set; }


        public token(int noToken, int IDtoken, String lexema, String tipo, int fila, int columna)
        {

            this.NoToken = noToken;
            this.iDtoken = IDtoken;
            this.Lexema = lexema;
            this.tipo = tipo;
            this.fila = fila;
            this.columna = columna;


        }

    }
}

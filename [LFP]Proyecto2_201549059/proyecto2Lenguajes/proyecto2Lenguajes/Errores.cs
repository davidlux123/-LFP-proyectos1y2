using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto2Lenguajes
{
   public  class Errores
    {

        public int noError { get; set; }
        public string error { get; set; }
        public string tipo { get; set; }
        public string descripcion { get; set; }
        public int filaError { get; set; }
        public int colunmaError { get; set; }

        public Errores(int noError, string error, string tipo, string descripcion, int filaError, int colunmaError)
        {

            this.noError = noError;
            this.error = error;
            this.tipo = tipo;
            this.descripcion = descripcion;
            this.filaError = filaError;
            this.colunmaError = colunmaError;
        }

    }
}

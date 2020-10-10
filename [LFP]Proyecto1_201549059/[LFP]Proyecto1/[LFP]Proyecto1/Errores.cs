using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _LFP_Proyecto1
{
    class Errores
    {

        public int noError { get; set; }
        public string error { get; set; }
        public string descripcion { get; set; }
        public int filaError { get; set; }
        public int colunmaError { get; set; }

        public Errores(int noError, string error, string descripcion, int filaError, int colunmaError)
        {

            this.noError = noError;
            this.error = error;
            this.descripcion = descripcion;
            this.filaError = filaError;
            this.colunmaError = colunmaError;
        }

    }
}

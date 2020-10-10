using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto2Lenguajes
{
    public class variable
    {

        public string id { get; set; }
        public string tipo { get; set; }
        public string valor { get; set; }

        public variable(string ID, string  TIPO, string VALOR)
        {

            this.id = ID;
            this.tipo = TIPO;
            this.valor = VALOR;

        }

    }
}

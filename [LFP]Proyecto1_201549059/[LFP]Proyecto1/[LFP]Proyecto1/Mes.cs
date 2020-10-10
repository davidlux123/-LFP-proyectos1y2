using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _LFP_Proyecto1
{
    public class Mes
    {
        public string ID_Mes { get; set;}
        internal List<Documento> Ducumentos = new List<Documento>();

        public Mes(string ID)
        {

            this.ID_Mes = ID;
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _LFP_Proyecto1
{
    public class Documento
    {
        public string NombreDoc { get; set; }
        public string Path { get; set; }

        public Documento(string ID, string path)
        {
            this.NombreDoc = ID;
            this.Path = path;
        }
        public Documento()
        {

        }



    }
}

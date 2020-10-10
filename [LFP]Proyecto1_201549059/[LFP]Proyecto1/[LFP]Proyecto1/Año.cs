using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _LFP_Proyecto1
{
    public class Año
    {
    
       public string ID_Año { get; set; }
       internal List<Mes> Meses = new List<Mes>();
       
        public Año(string ID)
        {

            this.ID_Año = ID;
            
        }

        
    }
}

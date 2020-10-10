using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _LFP_Proyecto1
{
    public partial class TablaErrores : Form
    {
        

        public TablaErrores()
        {
            InitializeComponent();
            AñadirdatosTablaErrores();
            
        }

        private void AceptarBtn_Click(object sender, EventArgs e)
        {

            TablaErrores cerrar = new TablaErrores();
            cerrar.Hide();

        }

        public void AñadirdatosTablaErrores()
        {

            for (int i = 0; i < Form1.listaErrores.Count; i++)
            {
                int n = tablaErroresTbl.Rows.Add();

                tablaErroresTbl.Rows[n].Cells[0].Value = Form1.listaErrores[i].noError;
                tablaErroresTbl.Rows[n].Cells[1].Value = Form1.listaErrores[i].error;
                tablaErroresTbl.Rows[n].Cells[2].Value = Form1.listaErrores[i].descripcion;
                tablaErroresTbl.Rows[n].Cells[3].Value = Form1.listaErrores[i].filaError;
                tablaErroresTbl.Rows[n].Cells[4].Value = Form1.listaErrores[i].colunmaError;
                
            }


        }

    }
}

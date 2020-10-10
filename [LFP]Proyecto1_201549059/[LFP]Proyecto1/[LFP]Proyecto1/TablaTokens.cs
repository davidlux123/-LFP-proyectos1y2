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
    public partial class TablaTokens : Form
    {
        public TablaTokens()
        {
            InitializeComponent();
            AñadirdatosTablaTokens();
        }

        private void atrasbtn_Click(object sender, EventArgs e)
        {

            TablaTokens cerrar = new TablaTokens();
            cerrar.Hide();

        }

        public void AñadirdatosTablaTokens()
        {

            for (int i= 0; i< Form1.listaTokens.Count; i++)
            {
                int n = tablaTokensTbl.Rows.Add();

                tablaTokensTbl.Rows[n].Cells[0].Value = Form1.listaTokens[i].NoToken;
                tablaTokensTbl.Rows[n].Cells[1].Value = Form1.listaTokens[i].Lexema;
                tablaTokensTbl.Rows[n].Cells[2].Value = Form1.listaTokens[i].tipo;
                tablaTokensTbl.Rows[n].Cells[3].Value = Form1.listaTokens[i].fila;
                tablaTokensTbl.Rows[n].Cells[4].Value = Form1.listaTokens[i].columna;



            }


        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace _LFP_Proyecto1
{
    public partial class AdminDoc : Form
    {
        string rutaDoc;

        public AdminDoc()
        {
            InitializeComponent();
            inicializarTreeView();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (!expresiontxt.Text.Equals(""))
            {
                if (expresiontxt.Text.Equals("."))
                {

                    MessageBox.Show("la Expresion no es valida");

                }
                else
                {
                    try
                    {
                        int contador = 0;
                        string expresion = @expresiontxt.Text;   //exprecion regular para la busqueda de la palabra
                        foreach (Match coincidencia in Regex.Matches(editorDocumentosRichtxt.Text, expresion))
                        {
                            int selecion = coincidencia.Index;
                            editorDocumentosRichtxt.SelectionStart = selecion;
                            editorDocumentosRichtxt.SelectionLength = coincidencia.Value.Length;
                            editorDocumentosRichtxt.SelectionFont = new Font(editorDocumentosRichtxt.SelectionFont, FontStyle.Bold);
                            //editorDocumentosRichtxt.SelectionColor = Color.Green;
                            contador += 1;
                        }
                        label1.Text = Convert.ToString("" + contador);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("la Expresion no es valida");

                    }
                }
            }
            else
            {
                MessageBox.Show("tienes que ingresar un expresion para loder buscar concidencias ");

            }

        }

        private void inicializarTreeView()
        {
            for (int i = 0; i < Form1.Años.Count; i++)
            {
                treeView1.Nodes.Add(Form1.Años[i].ID_Año);

                for (int j = 0; j < Form1.Años[i].Meses.Count; j++)
                {
                    treeView1.Nodes[i].Nodes.Add(Form1.Años[i].Meses[j].ID_Mes);

                    for (int k = 0; k < Form1.Años[i].Meses[j].Ducumentos.Count; k++)
                    {
                        treeView1.Nodes[i].Nodes[j].Nodes.Add(Form1.Años[i].Meses[j].Ducumentos[k].NombreDoc);
                    }

                }

            }

            treeView1.EndUpdate();

        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {

            if (e.Node.Parent != null && e.Node.Parent.Parent != null)
            {
                OpenFileDialog DesplegarDocu = new OpenFileDialog();
                DesplegarDocu.FileName = Form1.Años[e.Node.Parent.Parent.Index].Meses[e.Node.Parent.Index].Ducumentos[e.Node.Index].Path.Replace('\"', ' ');
                rutaDoc = DesplegarDocu.FileName;

                if (File.Exists(rutaDoc))
                {
                    StreamReader leer = new StreamReader(DesplegarDocu.FileName);
                    editorDocumentosRichtxt.Text = leer.ReadToEnd();
                    leer.Close();
                }
                else
                {

                    MessageBox.Show("no se encuentra el documento en la ubicacion especificada");

                }
            }

        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String texto = editorDocumentosRichtxt.Text;

            String[] lineas = texto.Split('\n');

            File.WriteAllLines(rutaDoc, lineas);

        }
    }
}

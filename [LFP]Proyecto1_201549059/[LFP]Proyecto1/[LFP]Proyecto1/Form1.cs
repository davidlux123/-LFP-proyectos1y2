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

namespace _LFP_Proyecto1
{
    public partial class Form1 : Form
    {

        String ruta;

        String[] palabrasReservadas = new string[5]
        {
            "año",
            "mes",
            "documento",
            "path",
            "nombre"
        };

        internal static List<token> listaTokens = new List<token>();

        internal static List<Errores> listaErrores = new List<Errores>();

        internal static List<Año> Años = new List<Año>();

        public Form1()
        {
            InitializeComponent();
            this.CenterToScreen();

        }

        private void AbrirToolStripMenuItem_Click(object sender, EventArgs e)
        {

            OpenFileDialog abrir = new OpenFileDialog()
            {
                Title = "Seleccione el Archivo",
                Filter = "Documantos de texto.LS|*.LS",
                AddExtension = true,
            };

            if (abrir.ShowDialog() == DialogResult.OK)
            {
                editorTextoRichtxt.Text = File.ReadAllText(abrir.FileName);
                ruta = abrir.FileName;

                /* using ( StreamReader sr = new StreamReader(ruta, Encoding.Default))
                 {

                     editorTextoRichtxt.Text = sr.ReadToEnd();

                 }*/

            }

        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (File.Exists(ruta))
            {

                String texto = editorTextoRichtxt.Text;

                String[] lineas = texto.Split('\n');

                File.WriteAllLines(ruta, lineas);

            }
            else
            {

                SaveFileDialog guardar = new SaveFileDialog()
                {
                    Title = "Seleccione el Destino",
                    Filter = "Documantos de texto.LS|*.LS",
                    AddExtension = true,
                };

                if (guardar.ShowDialog() == DialogResult.OK)
                {

                    String texto = editorTextoRichtxt.Text;

                    String[] lineas = texto.Split('\n');

                    File.WriteAllLines(guardar.FileName, lineas);

                    ruta = guardar.FileName;
                }

            }

        }

        private void guardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {

            SaveFileDialog guardarComo = new SaveFileDialog()
            {
                Title = "Seleccione el Destino",
                Filter = "Documantos de texto.LS|*.LS",
                AddExtension = true,
            };

            if (guardarComo.ShowDialog() == DialogResult.OK)
            {
                String texto = editorTextoRichtxt.Text;

                String[] lineas = texto.Split('\n');

                File.WriteAllLines(guardarComo.FileName, lineas);

                ruta = guardarComo.FileName;
            }

        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {

            MessageBox.Show("Deivid Alexander lux Revolorio, 201549059" + "\n"
               + "Lenguajes Formales de Programacion, Seccion: A-" + "\n"
               + "Inga. Damaris Campos, Aux. Aylin Roche");

        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {

            System.Environment.Exit(0);

        }

        private void analizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*wenfwenfoiwe****wdhiuwqhd**************jiooihih*/

            listaTokens = new List<token>();
            listaErrores = new List<Errores>();

            int fila = 1;
            int columna = 1;

            int noToken = 0;
            int noError = 0;        

            string cadena = editorTextoRichtxt.Text;

            int estado = 0;
            string lexema = "";
            char letra = ' '; //

            for (int i = 0; i < cadena.Length; i++)
            {
                letra = Convert.ToChar(cadena.Substring(i, 1));

                switch (estado)
                {

                    case 0:

                        if (letra == 13 || letra == 9)
                        {
                            //Salto de Carro espacio
                            estado = 0;
                            lexema = "";

                        }
                        else if (letra == 10)//aumentar la fila para los saltos de linea 
                        {
                            columna = 1;
                            fila++;

                        }
                        else if (char.IsLetter(letra))//para las letras (ya sean mayusculas o minusculas)
                        {

                            estado = 1;
                            lexema += letra;

                        }
                        else if (char.IsDigit(letra))// para los digitos 
                        {
                            estado = 2;
                            lexema += letra;

                        }
                        else if (letra == 59 || letra == 123 || letra == 125) // para los simbolos ({,},;)
                        {
                            estado = 6;
                            lexema += letra;
                            i--;
                        }
                        else if (letra == 58)// para los dos puntos
                        {

                            estado = 3;
                            lexema += letra;

                        }
                        else if (letra == 34)//para las comillas
                        {
                            estado = 4;
                            lexema += letra;

                        }
                        else
                        {

                            estado = 0;
                            string error1 = Convert.ToString(letra);
                            guardarErrores(noError, error1, "componente lexico desconocido", fila, columna);
                            noError++;
                            columna++;
                            lexema = "";

                        }

                        break;
                           
                    case 1:

                        if (char.IsLetter(letra) || char.IsDigit(letra) || letra == 95)
                        {

                            lexema += letra;

                        }
                        else
                        {
                            if (!validarPalabraReservada(lexema).Equals(""))
                            {
                                estado = 0;
                                string PalabraReservada = Convert.ToString(lexema);
                                GuadarToken(noToken, PalabraReservada, "Palabra Reservada", fila, columna);
                                noToken++;
                                columna++;
                                lexema = "";
                                i--;
                                // CambiarColor(ID, Color.Orange, 0);
                            }
                            else
                            {

                                estado = 0;
                                string ID = Convert.ToString(lexema);
                                GuadarToken(noToken, ID, "ID", fila, columna);
                                noToken++;
                                columna++;
                                lexema = "";
                                i--;

                            }


                        }

                        break;

                    case 2:

                        if (char.IsDigit(letra))
                        {

                            lexema += letra;

                        }
                        else
                        {

                            estado = 0;
                            string ID = Convert.ToString(lexema);
                            GuadarToken(noToken, ID, "Digito", fila, columna);
                            noToken++;
                            columna++;
                            lexema = "";
                            i--;
                            CambiarColor(ID, Color.Yellow, 0);

                        }

                        break;

                    case 3:

                        if (letra == 58) // dos puntos de nuevo
                        {
                            estado = 5;
                            lexema += letra;

                        }
                        else
                        {

                            estado = 0;
                            string ID = Convert.ToString(lexema);
                            GuadarToken(noToken, ID, "Dos puntos", fila, columna);
                            noToken++;
                            columna++;
                            lexema = "";
                            i--;

                        }

                        break;

                    case 4:

                        if (letra != 34)
                        {
                            estado = 4;
                            lexema += letra;

                        }
                        else if (letra == 34)
                        {
                            estado = 6;
                            lexema += letra;
                            i--;
                        }

                         break;

                    case 5:

                        if (letra == 61)
                        {
                            estado = 6;
                            lexema += letra;
                            i--;

                        }
                        else
                        {
                            estado = 0;
                            string error2 = Convert.ToString(lexema);
                            guardarErrores(noError, error2, "componente lexico desconocido", fila, columna);
                            noError++;
                            columna++;
                            lexema = "";
                            i--;

                        }

                        break;

                    case 6:

                        if (letra == 59 || letra == 123 || letra == 125)
                        {

                            if (letra == 59)
                            {

                                estado = 0;
                                string ID = Convert.ToString(lexema);
                                GuadarToken(noToken, ID, "Punto y coma", fila, columna);
                                noToken++;
                                columna++;
                                lexema = "";

                            }
                            else if (letra == 123)
                            {

                                estado = 0;
                                string ID = Convert.ToString(lexema);
                                GuadarToken(noToken, ID, "llave que abre", fila, columna);
                                noToken++;
                                columna++;
                                lexema = "";

                            }
                            else if (letra == 125)
                            {

                                estado = 0;
                                string ID = Convert.ToString(lexema);
                                GuadarToken(noToken, ID, "llave que cierra", fila, columna);
                                noToken++;
                                columna++;
                                lexema = "";

                            }

                        }
                        else if (letra == 34)
                        {
                            estado = 0;
                            string ID = Convert.ToString(lexema);
                            GuadarToken(noToken, ID, "Cadena", fila, columna);
                            noToken++;
                            columna++;
                            lexema = "";

                        }
                        else if (letra == 61)
                        {
                            estado = 0;
                            string ID = Convert.ToString(lexema);
                            GuadarToken(noToken, ID, "Asignacion", fila, columna);
                            noToken++;
                            columna++;
                            lexema = "";

                        }

                        break;

                }


            }

            if (listaErrores.Count != 0)//si hay errores, muestra tabla de errores
            {

                TablaErrores abrirErr = new TablaErrores();
                abrirErr.Show();

                MessageBox.Show("el codigo se encuentra con errores lexicos" + "\n" +
                        "ver errores en la tabla los errores y corregir todos los errores lexicos " + "\n" +
                        "para generar el Administrador de Documentos");

            }
            else if (listaTokens.Count == 0 && listaErrores.Count == 0)
            {
                MessageBox.Show("El texto ingresado al editor de texto no es valido" + "\n"
                       + "porfavor ingrese codigo correcto al editor");

            }

            if (listaErrores.Count == 0)
            {
                if (listaTokens.Count != 0)
                {

                    CambiarColor(":", Color.Pink, 0);
                    CambiarColor(";", Color.Red, 0);
                    CambiarColor("{", Color.Purple, 0);
                    CambiarColor("}", Color.Purple, 0);
                    CambiarColor("::=", Color.Blue, 0);


                    for (int j = 0; j < listaTokens.Count; j++)// para las palabras reservadas
                    {
                        string token = listaTokens[j].Lexema.ToLower();

                        for (int k = 0; k < palabrasReservadas.Length; k++)
                        {
                            if (token.Equals(palabrasReservadas[k]))
                            {

                                CambiarColor(listaTokens[j].Lexema, Color.Cyan, 0);

                            }


                        }

                    }

                    for (int j = 0; j < listaTokens.Count; j++)// para los ID de mes
                    {
                        string token = listaTokens[j].Lexema.ToLower();

                        if (token.Equals("mes"))
                        {

                            CambiarColor(listaTokens[j + 2].Lexema, Color.Orange, 0);

                        }

                    }

                    for (int j = 0; j < listaTokens.Count; j++)// para los ID de nombre 
                    {
                        string token = listaTokens[j].Lexema.ToLower();

                        if (token.Equals("nombre"))
                        {

                            CambiarColor(listaTokens[j + 2].Lexema, Color.Orange, 0);

                        }

                    }

                    for (int j = 0; j < listaTokens.Count; j++)// para las cadenas de path 
                    {
                        string token = listaTokens[j].Lexema.ToLower();

                        if (token.Equals("path"))
                        {

                            CambiarColor(listaTokens[j + 2].Lexema, Color.Green, 0);

                        }

                    }

                    MessageBox.Show("El texto se ah analizado correctamente, y no se encontraron errores lexicos");

                    ObtenerDatosTreeView();

                    AdminDoc abrir = new AdminDoc();
                    abrir.Show();


                }

            }




        }

        public void GuadarToken(int noToken, String lexema, String tipo, int fila, int columna)
        {

            token nuevoToken = new token(noToken, lexema, tipo, fila, columna);
            listaTokens.Add(nuevoToken);

        }

        public void guardarErrores(int noError, string error, string descripcion, int filaError, int colunmaError)
        {

            Errores nuevoError = new Errores(noError, error, descripcion, filaError, colunmaError);
            listaErrores.Add(nuevoError);

        }

        public string validarPalabraReservada(string lexema)
        {

            lexema = lexema.ToLower();

            for (int i = 0; i < palabrasReservadas.Length; i++)
            {

                if (palabrasReservadas[i].Equals(lexema))
                {
                    return palabrasReservadas[i];
                }

            }

            return "";

        }

        private void tablaDeSimbolosToolStripMenuItem_Click(object sender, EventArgs e)
        {


            if (listaErrores.Count == 0)
            {
                if (listaTokens.Count != 0)
                {

                    TablaTokens abrir = new TablaTokens();
                    abrir.Show();


                }
                else
                {
                    MessageBox.Show("No se ah analizado ningun codigo" + "\n"
                        + "porfavor ingrese codigo al editor de texto y analizelo, para mostrale la tabla de simbolos");

                }

            }
            else
            {
                MessageBox.Show("No se puede acceder a la tabla de simbolos ya que " + "\n" +
                    " el codigo se encuentra con errores lexicos, corriga los errores y vuelva analizar para mmostrar la tabla");

            }


        }

        public void CambiarColor(String palabra, Color color, int incio)
        {

            if (editorTextoRichtxt.Text.Contains(palabra))
            {
                int index = -1;
                int selectStart = editorTextoRichtxt.SelectionStart;

                while ((index = editorTextoRichtxt.Text.IndexOf(palabra, (index + 1))) != -1)
                {
                    this.editorTextoRichtxt.Select((index + incio), palabra.Length);
                    this.editorTextoRichtxt.SelectionColor = color;
                    this.editorTextoRichtxt.Select(selectStart, 0);
                    this.editorTextoRichtxt.SelectionColor = Color.Black;
                }
            }



        }

        public void ObtenerDatosTreeView()
        {

            Año nuevoAño;
            Mes mesNuevo;
            Documento docNuevo;

            for (int i = 0; i < listaTokens.Count; i++)
            {
                string token = listaTokens[i].Lexema.ToLower();

                if (token.Equals("año"))
                {
                    nuevoAño = new Año(listaTokens[i + 2].Lexema);
                    Años.Add(nuevoAño);

                    while (!token.Equals("}"))
                    {
                        //token = listaTokens[i].Lexema.ToLower();

                        if (token.Equals("mes"))
                        {
                            mesNuevo = new Mes(listaTokens[i + 2].Lexema);
                            nuevoAño.Meses.Add(mesNuevo);

                            while (!token.Equals("}"))
                            {
                                //token = listaTokens[i].Lexema.ToLower();

                                if (token.Equals("documento"))
                                {
                                    docNuevo = new Documento();
                                    mesNuevo.Ducumentos.Add(docNuevo);

                                    while (!token.Equals("}"))
                                    {
                                        //token = listaTokens[i].Lexema.ToLower();

                                        if (token.Equals("path"))
                                        {
                                            docNuevo.Path = listaTokens[i + 2].Lexema;
                                            i++;
                                            token = listaTokens[i].Lexema.ToLower();

                                        }
                                        else if (token.Equals("nombre"))
                                        {
                                            docNuevo.NombreDoc = listaTokens[i + 2].Lexema;
                                            i++;
                                            token = listaTokens[i].Lexema.ToLower();

                                        }
                                        else
                                        {
                                            i++;
                                            token = listaTokens[i].Lexema.ToLower();
                                        }

                                    }

                                    i++;
                                    token = listaTokens[i].Lexema.ToLower();

                                }
                                else
                                {
                                    i++;
                                    token = listaTokens[i].Lexema.ToLower();
                                }

                            }

                            i++;
                            token = listaTokens[i].Lexema.ToLower();

                        }
                        else
                        {
                            i++;
                            token = listaTokens[i].Lexema.ToLower();
                        }
                        
                    }//aca es el primer while que guarda meses

                }
                
            }// aca se acaba el for que guarda años


        }

    }
}

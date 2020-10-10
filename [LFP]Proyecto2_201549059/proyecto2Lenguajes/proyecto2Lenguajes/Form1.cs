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
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Diagnostics;
using System.Globalization;

namespace proyecto2Lenguajes
{
    public partial class Form1 : Form
    {
        String ruta;

        String[] palabrasReservadas = new string[16]
        {
            "instrucciones",
            "variables",
            "texto",
            "interlineado",
            "tamanio_letra",
            "nombre_archivo",
            "direccion_archivo",
            "cadena",
            "entero",
            "imagen",
            "numeros",
            "linea_en_blanco",
            "var",
            "promedio",
            "suma",
            "asignar"
        };

        internal static List<token> listaTokens = new List<token>();

        internal static List<Errores> listaErrores = new List<Errores>();

        internal static List<token> listaDecVariables = new List<token>();

        internal static List<variable> listaVariables = new List<variable>();

        public Form1()
        {
            InitializeComponent();
            this.CenterToScreen();
        }

        private void abrirToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog abrir = new OpenFileDialog()
            {
                Title = "Seleccione el Archivo",
                Filter = "Documantos de texto.TTI|*.TTI",
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

        private void guardarToolStripMenuItem_Click_1(object sender, EventArgs e)
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
                    Filter = "Documantos de texto.TTI|*.TTI",
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

        private void guargarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {

            SaveFileDialog guardarComo = new SaveFileDialog()
            {
                Title = "Seleccione el Destino",
                Filter = "Documantos de texto.TTI|*.TTI",
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

        private void acercaDeToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

            MessageBox.Show("Deivid Alexander lux Revolorio, 201549059" + "\n"
            + "Lenguajes Formales de Programacion, Seccion: A-" + "\n"
            + "Inga. Damaris Campos, Aux. Aylin Roche");

        }

        private void salirToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void analizarToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

            listaTokens = new List<token>();
            listaErrores = new List<Errores>();

            string lexema = "";
            int fila = 1;
            int columna = 1;

            int noToken = 0;
            int noError = 0;

            string cadena = editorTextoRichtxt.Text + " " + "\n";
            int estado = 0;
            char letra = ' ';

            for (int i = 0; i < cadena.Length; i++)
            {
                letra = Convert.ToChar(cadena.Substring(i, 1));

                switch (estado)
                {

                    case 0:

                        if (letra == 13 || letra == 9)
                        {
                            //Salto de Carro!

                        }
                        else if (letra == 10)//aumentar la fila para los saltos de linea 
                        {
                            columna = 1;
                            fila++;
                            //estado = 0;
                            //lexema = "";

                        }
                        else if (letra == 32)//para los espacios
                        {
                            estado = 0;
                            lexema = "";
                        }

                        else if (char.IsLetter(letra)) //para las letras (ya sean mayusculas o minusculas)
                        {

                            estado = 1;
                            lexema += letra;

                        }

                        else if (letra == 48) //para decimales menores que 1 y mayores que cero
                        {

                            estado = 2;
                            lexema += letra;

                        }

                        else if (letra == 45) //para decimales positivos y enteros negativos y positivos.
                        {

                            estado = 5;
                            lexema += letra;

                        }

                        else if (char.IsDigit(letra)) //para enteros y decimales positivos 
                        {

                            string numero = Convert.ToString(letra);
                            int num = Convert.ToInt32(numero);

                            if (num > 0)
                            {
                                estado = 6;
                                lexema += letra;

                            }
                            else
                            {
                                estado = 2;
                                lexema += letra;
                            }

                        }

                        else if (letra == 34) //para las comillas
                        {
                            estado = 7;
                            lexema += letra;

                        }

                        else if (letra == 91) // Para el corchete que abre
                        {
                            estado = 8;
                            lexema += letra;

                        }

                        else if (letra == 47)
                        {
                            estado = 12;
                            lexema += letra;

                        }

                        else if (letra == 123 | letra == 125 | letra == 40 | letra == 41 | letra == 44 | letra == 58 | letra == 59 | letra == 93
                            | letra == 61)
                        {
                            estado = 15;
                            lexema += letra;
                            i--;

                        }

                        else
                        {
                            estado = 0;
                            string error1 = Convert.ToString(letra);
                            guardarErrores(noError, error1, "lexico", "componente lexico desconocido", fila, columna);
                            noError++;
                            columna++;
                            lexema = "";

                        }

                        break;

                    case 1:

                        if (char.IsLetter(letra) || char.IsDigit(letra) || letra == 95)
                        {
                            estado = 1;
                            lexema += letra;

                        }
                        else
                        {

                            if (!validarPalabraReservada(lexema).Equals(""))
                            {
                                estado = 0;
                                string PalabraReservada = Convert.ToString(lexema);
                                GuadarToken(noToken, 1, PalabraReservada, "Palabra Reservada", fila, columna);
                                noToken++;
                                columna++;
                                lexema = "";
                                i--;

                            }
                            else
                            {

                                estado = 0;
                                string ID = Convert.ToString(lexema);
                                GuadarToken(noToken, 2, ID, "ID", fila, columna);
                                noToken++;
                                columna++;
                                lexema = "";
                                i--;

                            }


                        }

                        break;

                    case 2:

                        if (letra == 46)
                        {
                            estado = 3;
                            lexema += letra;

                        }
                        else
                        {

                            estado = 0;
                            string numero = Convert.ToString(lexema);
                            GuadarToken(noToken, 3, numero, "Numero", fila, columna);
                            noToken++;
                            columna++;
                            lexema = "";
                            i--;

                        }

                        break;

                    case 3:

                        if (char.IsDigit(letra))
                        {

                            estado = 4;
                            lexema += letra;

                        }
                        else
                        {

                            estado = 0;
                            string error1 = Convert.ToString(lexema);
                            guardarErrores(noError, error1, "lexico", "componente lexico desconocido", fila, columna);
                            noError++;
                            columna++;
                            lexema = "";
                            i--;

                        }

                        break;

                    case 4:

                        if (char.IsDigit(letra))
                        {
                            estado = 4;
                            lexema += letra;
                        }
                        else
                        {
                            if (lexema.Substring(0, 1).Equals("-"))
                            {
                                estado = 0;
                                string decim = Convert.ToString(lexema);
                                GuadarToken(noToken, 6, decim, "Decimal negativo", fila, columna);
                                noToken++;
                                columna++;
                                lexema = "";
                                i--;

                            }
                            else
                            {
                                estado = 0;
                                string decim = Convert.ToString(lexema);
                                GuadarToken(noToken, 5, decim, "Decimal", fila, columna);
                                noToken++;
                                columna++;
                                lexema = "";
                                i--;

                            }


                        }

                        break;

                    case 5:

                        if (char.IsDigit(letra))
                        {
                            string numero = Convert.ToString(letra);
                            int num = Convert.ToInt32(numero);

                            if (num > 0)
                            {
                                estado = 6;
                                lexema += letra;

                            }
                            else
                            {

                                estado = 16;
                                lexema += letra;

                            }

                        }
                        else
                        {

                            estado = 0;
                            string error1 = Convert.ToString(lexema);
                            guardarErrores(noError, error1, "lexico", "componente lexico desconocido", fila, columna);
                            noError++;
                            columna++;
                            lexema = "";
                            i--;

                        }

                        break;

                    case 6:

                        if (char.IsDigit(letra))
                        {
                            estado = 6;
                            lexema += letra;

                        }
                        else if (letra == 46)
                        {
                            estado = 3;
                            lexema += letra;

                        }
                        else
                        {


                            if (lexema.Substring(0, 1).Equals("-"))
                            {
                                estado = 0;
                                string numero = Convert.ToString(lexema);
                                GuadarToken(noToken, 4, numero, "Numero negativo", fila, columna);
                                noToken++;
                                columna++;
                                lexema = "";
                                i--;

                            }
                            else
                            {

                                estado = 0;
                                string numero = Convert.ToString(lexema);
                                GuadarToken(noToken, 3, numero, "Numero", fila, columna);
                                noToken++;
                                columna++;
                                lexema = "";
                                i--;

                            }


                        }

                        break;

                    case 7:

                        if (letra == 10)
                        {
                            estado = 0;
                            string error1 = Convert.ToString(lexema);
                            guardarErrores(noError, error1, "lexico", "componente lexico desconocido", fila, columna);
                            noError++;
                            columna++;
                            lexema = "";
                            i--;

                        }
                        else if (letra != 34)
                        {
                            estado = 7;
                            lexema += letra;

                        }
                        else if (letra == 34)
                        {
                            estado = 15;
                            lexema += letra;
                            i--;

                        }

                        break;

                    case 8:

                        if (letra == 42)
                        {
                            estado = 9;
                            lexema += letra;

                        }
                        else if (letra == 43)
                        {
                            estado = 10;
                            lexema += letra;

                        }
                        else
                        {
                            estado = 0;
                            string corchete = Convert.ToString(lexema);
                            GuadarToken(noToken, 7, corchete, "Corchete que abre", fila, columna);
                            noToken++;
                            columna++;
                            lexema = "";
                            i--;

                        }

                        break;

                    case 9:

                        if (letra == 10)
                        {
                            estado = 0;
                            string error1 = Convert.ToString(lexema);
                            guardarErrores(noError, error1, "lexico", "componente lexico desconocido", fila, columna);
                            noError++;
                            columna++;
                            lexema = "";
                            i--;

                        }
                        else if (letra != 42)
                        {

                            estado = 9;
                            lexema += letra;

                        }
                        else if (letra == 42)
                        {
                            estado = 11;
                            lexema += letra;

                        }

                        break;

                    case 10:

                        if (letra == 10)
                        {
                            estado = 0;
                            string error1 = Convert.ToString(lexema);
                            guardarErrores(noError, error1, "lexico", "componente lexico desconocido", fila, columna);
                            noError++;
                            columna++;
                            lexema = "";
                            i--;

                        }
                        else if (letra != 43)
                        {
                            estado = 10;
                            lexema += letra;

                        }
                        else if (letra == 43)
                        {
                            estado = 11;
                            lexema += letra;

                        }

                        break;

                    case 11:

                        if (letra == 93)
                        {
                            estado = 15;
                            lexema += letra;
                            i--;

                        }
                        else
                        {
                            estado = 0;
                            string error1 = Convert.ToString(lexema);
                            guardarErrores(noError, error1, "lexico", "componente lexico desconocido", fila, columna);
                            noError++;
                            columna++;
                            lexema = "";
                            i--;

                        }

                        break;

                    case 12:

                        if (letra == 42)
                        {
                            estado = 13;
                            lexema += letra;

                        }
                        else
                        {
                            estado = 0;
                            string error1 = Convert.ToString(lexema);
                            guardarErrores(noError, error1, "lexico", "componente lexico desconocido", fila, columna);
                            noError++;
                            columna++;
                            lexema = "";
                            i--;

                        }

                        break;

                    case 13:

                        /*  if (letra == 10)
                          {
                              estado = 0;
                              string error1 = Convert.ToString(lexema);
                              guardarErrores(noError, error1, "lexico", "componente lexico desconocido", fila, columna);
                              noError++;
                              columna++;
                              lexema = "";
                              i--;

                          }
                          else*/
                        if (letra != 42)
                        {
                            estado = 13;
                            lexema += letra;

                        }
                        else if (letra == 42)
                        {
                            estado = 14;
                            lexema += letra;

                        }

                        break;

                    case 14:

                        if (letra == 47)
                        {
                            estado = 15;
                            lexema += letra;
                            i--;
                        }
                        else
                        {

                            estado = 0;
                            string error1 = Convert.ToString(lexema);
                            guardarErrores(noError, error1, "lexico", "componente lexico desconocido", fila, columna);
                            noError++;
                            columna++;
                            lexema = "";
                            i--;

                        }

                        break;

                    case 15:

                        if (letra == 123)
                        {

                            estado = 0;
                            string corchete = Convert.ToString(lexema);
                            GuadarToken(noToken, 8, corchete, "llave que abre que abre", fila, columna);
                            noToken++;
                            columna++;
                            lexema = "";


                        }
                        else if (letra == 125)
                        {
                            estado = 0;
                            string corchete = Convert.ToString(lexema);
                            GuadarToken(noToken, 9, corchete, "llave que abre que cierra", fila, columna);
                            noToken++;
                            columna++;
                            lexema = "";


                        }
                        else if (letra == 40)
                        {

                            estado = 0;
                            string corchete = Convert.ToString(lexema);
                            GuadarToken(noToken, 10, corchete, "parentesis que abre que abre", fila, columna);
                            noToken++;
                            columna++;
                            lexema = "";


                        }
                        else if (letra == 41)
                        {
                            estado = 0;
                            string corchete = Convert.ToString(lexema);
                            GuadarToken(noToken, 11, corchete, "parentesis que abre que cierra", fila, columna);
                            noToken++;
                            columna++;
                            lexema = "";


                        }
                        else if (letra == 44)
                        {
                            estado = 0;
                            string corchete = Convert.ToString(lexema);
                            GuadarToken(noToken, 12, corchete, "Coma", fila, columna);
                            noToken++;
                            columna++;
                            lexema = "";

                        }
                        else if (letra == 58)
                        {
                            estado = 0;
                            string corchete = Convert.ToString(lexema);
                            GuadarToken(noToken, 13, corchete, "Dos puntos", fila, columna);
                            noToken++;
                            columna++;
                            lexema = "";

                        }
                        else if (letra == 59)
                        {
                            estado = 0;
                            string corchete = Convert.ToString(lexema);
                            GuadarToken(noToken, 14, corchete, "Punto Coma", fila, columna);
                            noToken++;
                            columna++;
                            lexema = "";

                        }
                        else if (letra == 93)
                        {
                            if (lexema.Length == 1)
                            {

                                estado = 0;
                                string corchete = Convert.ToString(lexema);
                                GuadarToken(noToken, 15, corchete, "Corchetre que cierra", fila, columna);
                                noToken++;
                                columna++;
                                lexema = "";
                            }
                            else
                            {

                                if (lexema.Substring(1, 1).Equals("*"))
                                {
                                    estado = 0;
                                    string corchete = Convert.ToString(lexema);
                                    GuadarToken(noToken, 17, corchete, "cadena subrayada", fila, columna);
                                    noToken++;
                                    columna++;
                                    lexema = "";

                                }
                                else
                                {
                                    estado = 0;
                                    string corchete = Convert.ToString(lexema);
                                    GuadarToken(noToken, 17, corchete, "cadena negrita", fila, columna);
                                    noToken++;
                                    columna++;
                                    lexema = "";

                                }

                            }

                        }
                        else if (letra == 34)
                        {
                            estado = 0;
                            string corchete = Convert.ToString(lexema);
                            GuadarToken(noToken, 16, corchete, "Cadena", fila, columna);
                            noToken++;
                            columna++;
                            lexema = "";

                        }
                        else if (letra == 47)
                        {
                            estado = 0;
                            string corchete = Convert.ToString(lexema);
                            GuadarToken(noToken, 18, corchete, "Comentario", fila, columna);
                            noToken++;
                            columna++;
                            lexema = "";

                        }
                        else if (letra == 61)
                        {
                            estado = 0;
                            string corchete = Convert.ToString(lexema);
                            GuadarToken(noToken, 19, corchete, "igual", fila, columna);
                            noToken++;
                            columna++;
                            lexema = "";

                        }

                        break;

                    case 16:

                        if (letra == 46)
                        {
                            estado = 3;
                            lexema += letra;

                        }
                        else
                        {

                            estado = 0;
                            string error1 = Convert.ToString(lexema);
                            guardarErrores(noError, error1, "lexico", "componente lexico desconocido", fila, columna);
                            noError++;
                            columna++;
                            lexema = "";
                            i--;

                        }

                        break;


                }

            }

            if (listaErrores.Count == 0)
            {
                if (listaTokens.Count != 0)
                {

                    MessageBox.Show("el analisis lexico ah sido exitoso");

                }
            }

            Bloque();

            if (listaErrores.Count == 0)
            {
                if (listaTokens.Count != 0)
                {
                    // aca si no hay error de nada y esta correcto todo el analisis sintactico
                    MessageBox.Show("el analisis sintactico ah sido exitoso");

                    for (int i = 0; i < listaTokens.Count; i++)// para los id y numeros y simbolos 
                    {

                        if (listaTokens[i].tipo.ToLower().Equals("numero") | listaTokens[i].tipo.ToLower().Equals("numero negativo") | listaTokens[i].tipo.ToLower().Equals("decimal"))
                        {
                            CambiarColor(listaTokens[i].Lexema, Color.Yellow, 0);

                        }
                        else if (listaTokens[i].tipo.ToLower().Equals("id"))
                        {
                            CambiarColor(listaTokens[i].Lexema, Color.Cyan, 0);

                        }
                        else
                        {
                            CambiarColor(listaTokens[i].Lexema, Color.Orange, 0);

                        }

                    }

                    for (int i = 0; i < listaTokens.Count; i++)//para palabras reservadas
                    {
                        if (listaTokens[i].tipo.ToLower().Equals("palabra reservada"))
                        {
                            CambiarColor(listaTokens[i].Lexema, Color.Blue, 0);

                        }

                    }

                    for (int i = 0; i < listaTokens.Count; i++)//para las cadenas
                    {
                        if (listaTokens[i].tipo.ToLower().Equals("cadena") | listaTokens[i].tipo.ToLower().Equals("cadena negrita") | listaTokens[i].tipo.ToLower().Equals("cadena subrayada"))
                        {
                            CambiarColor(listaTokens[i].Lexema, Color.Green, 0);

                        }

                    }

                    for (int i = 0; i < listaTokens.Count; i++)//para los comentarios
                    {

                        if (listaTokens[i].tipo.ToLower().Equals("comentario"))
                        {
                            CambiarColor(listaTokens[i].Lexema, Color.Silver, 0);

                        }

                    }

                    GnerarPDFTokens();

                    if (verificarVariableRepe())
                    {
                        generarPDF();

                    }
                    else
                    {
                        MessageBox.Show("corrige errores para que pueda generarse el pdf");

                    }



                }
                else if (listaTokens.Count == 0)
                {
                    // aca se muestra si las dos listas estan vacias
                    MessageBox.Show("no puedes analizar algo vacio");

                }
            }
            else if (listaErrores.Count != 0)
            {
                //aca es donde se muestra la tabla de errores
                MessageBox.Show("Se han encontrado errores");
                GnerarPDFErrores();
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

        public void GuadarToken(int noToken, int idToken, String lexema, String tipo, int fila, int columna)
        {

            token nuevoToken = new token(noToken, idToken, lexema, tipo, fila, columna);
            listaTokens.Add(nuevoToken);

        }

        public void guardarErrores(int noError, string error, string tipo, string descripcion, int filaError, int colunmaError)
        {

            Errores nuevoError = new Errores(noError, error, tipo, descripcion, filaError, colunmaError);
            listaErrores.Add(nuevoError);

        }

        string preanalisis = "";

        int noError = 0;
        int NoToken = 0;

        public void parea(string token)
        {

            if (token == preanalisis)
            {
                NoToken++;

                if (NoToken < listaTokens.Count)
                {
                    preanalisis = listaTokens[NoToken].Lexema.ToLower();
                }
            }

            else
            {
                string error1 = Convert.ToString(preanalisis);

                if (NoToken == listaTokens.Count) //error alfinal de todo
                {
                    guardarErrores(noError, error1, "sintactico", "le hace falta al final un: " + token, listaTokens[NoToken - 1].fila + 1
                    , listaTokens[NoToken - 1].columna + 1);
                    //noError++;
                    //NoToken++;
                }

                else if (NoToken < listaTokens.Count) //error comun 
                {
                    guardarErrores(noError, error1, "sintactico", "se esperaba: " + token, listaTokens[NoToken].fila
                    , listaTokens[NoToken].columna);
                    noError++;
                    //  NoToken++;
                    // preanalisis = listaTokens[NoToken].Lexema.ToLower();
                }

            }

        }

        public void Bloque()
        {
            if (NoToken == 0)
            {
                preanalisis = listaTokens[NoToken].Lexema.ToLower();
            }

            if (preanalisis.Equals("instrucciones") | preanalisis.Equals("variables") | preanalisis.Equals("texto"))
            {

                switch (preanalisis)
                {
                    case "instrucciones":

                        parea("instrucciones");
                        parea("{");
                        inst();
                        parea("}");
                        if (NoToken < listaTokens.Count)
                        {
                            masBlock();
                        }
                        break;

                    case "variables":

                        parea("variables");
                        parea("{");
                        dec();
                        parea("}");
                        if (NoToken < listaTokens.Count)
                        {
                            masBlock();
                        }

                        break;

                    case "texto":

                        parea("texto");
                        parea("{");
                        text();
                        parea("}");
                        if (NoToken < listaTokens.Count)
                        {
                            masBlock();
                        }

                        break;

                }


            }

            else if (preanalisis.Equals("{"))
            {

                if (noError == 0)
                {
                    string error1 = Convert.ToString(preanalisis);
                    guardarErrores(noError, error1, "sintactico", "se esperaba palabra reservada del segmento bloques", listaTokens[NoToken].fila
                    , listaTokens[NoToken].columna - 1);
                    noError++;

                }

                parea("{");

                bool bandera = true;

                do
                {

                    if (preanalisis.Equals("interlineado") | preanalisis.Equals("tamanio_letra") | preanalisis.Equals("nombre_archivo") | preanalisis.Equals("direccion_Archivo"))
                    {
                        bandera = false;
                        inst();
                        parea("}");
                        if (NoToken < listaTokens.Count)
                        {
                            masBlock();
                        }
                    }
                    else if (listaTokens[NoToken].tipo.Equals("ID"))
                    {
                        bandera = false;
                        dec();
                        parea("}");
                        if (NoToken < listaTokens.Count)
                        {
                            masBlock();
                        }

                    }
                    else if (preanalisis.Equals("imagen") | preanalisis.Equals("numeros") | preanalisis.Equals("var") | preanalisis.Equals("promedio") | preanalisis.Equals("suma")
                          | preanalisis.Equals("asignar") | listaTokens[NoToken].tipo.Equals("cadena subrayada") | listaTokens[NoToken].tipo.Equals("cadena negrita")
                          | listaTokens[NoToken].tipo.Equals("cadena"))
                    {
                        bandera = false;
                        //text();
                        parea("}");
                        if (NoToken < listaTokens.Count)
                        {
                            masBlock();
                        }

                    }
                    else
                    {
                        string error1 = Convert.ToString(preanalisis);
                        guardarErrores(noError, error1, "sintactico", "se esperaba palabra reservada", listaTokens[NoToken].fila
                        , listaTokens[NoToken].columna);
                        noError++;
                        NoToken++;
                        if (NoToken < listaTokens.Count)
                        {
                            preanalisis = listaTokens[NoToken].Lexema.ToLower();
                        }
                        bandera = true;


                    }

                } while (bandera);

            }

            else
            {

                string error1 = Convert.ToString(preanalisis);
                guardarErrores(noError, error1, "sintactico", " el token no se trata de alguna palabra reservada para los bloques, por lo tanto se encuentra fuera de rango", listaTokens[NoToken].fila
                , listaTokens[NoToken].columna);
                noError++;
                NoToken++;
                if (NoToken < listaTokens.Count)
                {
                    preanalisis = listaTokens[NoToken].Lexema.ToLower();
                    Bloque();
                }


            }

        }

        public void masBlock()
        {

            if (preanalisis.Equals("instrucciones") | preanalisis.Equals("variables") | preanalisis.Equals("texto"))
            {

                switch (preanalisis)
                {
                    case "instrucciones":

                        parea("instrucciones");
                        parea("{");
                        inst();
                        parea("}");
                        if (NoToken < listaTokens.Count)
                        {
                            masBlock();
                        };


                        break;

                    case "variables":

                        parea("variables");
                        parea("{");
                        dec();
                        parea("}");
                        masBlock();
                        if (NoToken < listaTokens.Count)
                        {
                            masBlock();
                        };


                        break;

                    case "texto":

                        parea("texto");
                        parea("{");
                        text();
                        parea("}");
                        if (NoToken < listaTokens.Count)
                        {
                            masBlock();
                        };

                        break;

                }

            }
            else if (preanalisis.Equals("{"))
            {

                if (noError == 0)
                {
                    string error1 = Convert.ToString(preanalisis);
                    guardarErrores(noError, error1, "sintactico", "se esperaba palabra reservada del segmento bloques", listaTokens[NoToken].fila
                    , listaTokens[NoToken].columna - 1);
                    noError++;

                }

                parea("{");

                bool bandera = true;

                do
                {

                    if (preanalisis.Equals("interlineado") | preanalisis.Equals("tamanio_letra") | preanalisis.Equals("nombre_archivo") | preanalisis.Equals("direccion_Archivo"))
                    {
                        bandera = false;
                        inst();
                        parea("}");
                        if (NoToken < listaTokens.Count)
                        {
                            masBlock();
                        }
                    }
                    else if (listaTokens[NoToken].tipo.Equals("ID"))
                    {
                        bandera = false;
                        dec();
                        parea("}");
                        if (NoToken < listaTokens.Count)
                        {
                            masBlock();
                        }

                    }
                    else if (preanalisis.Equals("imagen") | preanalisis.Equals("numeros") | preanalisis.Equals("var") | preanalisis.Equals("promedio") | preanalisis.Equals("suma")
                          | preanalisis.Equals("asignar") | listaTokens[NoToken].tipo.Equals("cadena subrayada") | listaTokens[NoToken].tipo.Equals("cadena negrita")
                          | listaTokens[NoToken].tipo.Equals("cadena"))
                    {
                        bandera = false;
                        //text();
                        parea("}");
                        if (NoToken < listaTokens.Count)
                        {
                            masBlock();
                        }

                    }
                    else
                    {
                        string error1 = Convert.ToString(preanalisis);
                        guardarErrores(noError, error1, "sintactico", "se esperaba palabra reservada", listaTokens[NoToken].fila
                        , listaTokens[NoToken].columna);
                        noError++;
                        NoToken++;
                        if (NoToken < listaTokens.Count)
                        {
                            preanalisis = listaTokens[NoToken].Lexema.ToLower();
                        }
                        bandera = true;


                    }

                } while (bandera);

            }

            else
            {
                if (NoToken < listaTokens.Count)
                {
                    string error1 = Convert.ToString(preanalisis);
                    guardarErrores(noError, error1, "sintactico", "no se permite componente fuera del rango de un bloque", listaTokens[NoToken].fila
                    , listaTokens[NoToken].columna);
                    noError++;
                    NoToken++;
                    if (NoToken < listaTokens.Count)
                    {
                        preanalisis = listaTokens[NoToken].Lexema.ToLower();
                        masBlock();
                    }
                }


            }


        }

        public void inst()
        {

            if (preanalisis.Equals("interlineado") | preanalisis.Equals("tamanio_letra") | preanalisis.Equals("nombre_archivo") | preanalisis.Equals("direccion_Archivo"))
            {

                switch (preanalisis)
                {
                    case "interlineado":

                        parea("interlineado");
                        parea("(");
                        valor();
                        parea(")");
                        parea(";");
                        if (NoToken < listaTokens.Count)
                        {
                            masInst();
                        };

                        break;

                    case "tamanio_letra":

                        parea("tamanio_letra");
                        parea("(");
                        valor();
                        parea(")");
                        parea(";");
                        if (NoToken < listaTokens.Count)
                        {
                            masInst();
                        };
                        break;

                    case "nombre_archivo":

                        parea("nombre_archivo");
                        parea("(");
                        valor();
                        parea(")");
                        parea(";");
                        if (NoToken < listaTokens.Count)
                        {
                            masInst();
                        };

                        break;

                    case "direccion_Archivo":

                        parea("direccion_Archivo");
                        parea("(");
                        valor();
                        parea(")");
                        parea(";");
                        if (NoToken < listaTokens.Count)
                        {
                            masInst();
                        };

                        break;

                }

            }

            else if (preanalisis.Equals("}"))
            {
                if (listaTokens[NoToken - 1].Lexema.Equals("{"))
                {
                    string error1 = Convert.ToString(preanalisis);
                    guardarErrores(noError, error1, "sintactica", "tiene que haber almenos una istruccione en el bloque", listaTokens[NoToken].fila
                    , listaTokens[NoToken].columna);
                    //ACA SE SALE AL BLOQUE Y VERIFICA LA LLAVE QUE CIERRA EN EL METODO BLOQUE
                }
            }

            else if (preanalisis.Equals("("))
            {

                string error1 = Convert.ToString(preanalisis);
                guardarErrores(noError, error1, "sintactico", "se esperaban alguna de las palabras reservadas que pertecence al bloque de instrucciones", listaTokens[NoToken].fila
                , listaTokens[NoToken].columna);
                noError++;

                parea("(");
                valor();
                parea(")");
                parea(";");
                if (NoToken < listaTokens.Count)
                {
                    masInst();
                };

            }

            else
            {
                string error1 = Convert.ToString(preanalisis);
                guardarErrores(noError, error1, "sintactico", "se esperaban alguna de las palabras reservadas que pertecence al bloque de instrucciones", listaTokens[NoToken].fila
                , listaTokens[NoToken].columna);
                noError++;
                NoToken++;
                if (NoToken < listaTokens.Count)
                {
                    preanalisis = listaTokens[NoToken].Lexema.ToLower();
                    inst();
                }

            }

        }

        public void masInst()
        {

            if (preanalisis.Equals("interlineado") | preanalisis.Equals("tamanio_letra") | preanalisis.Equals("nombre_archivo") | preanalisis.Equals("direccion_Archivo"))
            {

                switch (preanalisis)
                {
                    case "interlineado":

                        parea("interlineado");
                        parea("(");
                        valor();
                        parea(")");
                        parea(";");
                        if (NoToken < listaTokens.Count)
                        {
                            masInst();
                        };

                        break;

                    case "tamanio_letra":

                        parea("tamanio_letra");
                        parea("(");
                        valor();
                        parea(")");
                        parea(";");
                        if (NoToken < listaTokens.Count)
                        {
                            masInst();
                        };
                        break;

                    case "nombre_archivo":

                        parea("nombre_archivo");
                        parea("(");
                        valor();
                        parea(")");
                        parea(";");
                        if (NoToken < listaTokens.Count)
                        {
                            masInst();
                        };

                        break;

                    case "direccion_Archivo":

                        parea("direccion_Archivo");
                        parea("(");
                        valor();
                        parea(")");
                        parea(";");
                        if (NoToken < listaTokens.Count)
                        {
                            masInst();
                        };

                        break;

                }

            }

            else if (preanalisis.Equals("("))
            {

                string error1 = Convert.ToString(preanalisis);
                guardarErrores(noError, error1, "sintactico", "se esperaban alguna de las palabras reservadas que pertecence al bloque de instrucciones", listaTokens[NoToken].fila
                , listaTokens[NoToken].columna);
                noError++;

                parea("(");
                valor();
                parea(")");
                parea(";");
                if (NoToken < listaTokens.Count)
                {
                    masInst();
                };

            }

            else if (!preanalisis.Equals("}"))
            {

                string error1 = Convert.ToString(preanalisis);
                guardarErrores(noError, error1, "sintactico", "se esperaba una palabra reservada", listaTokens[NoToken].fila
                , listaTokens[NoToken].columna);
                noError++;
                NoToken++;
                if (NoToken < listaTokens.Count)
                {
                    preanalisis = listaTokens[NoToken].Lexema.ToLower();
                    masInst();
                };


            }


        }

        public void valor()
        {
            if (NoToken < listaTokens.Count)
            {

                switch (listaTokens[NoToken].tipo.ToLower())
                {

                    case ("cadena"):

                        NoToken++;

                        if (NoToken < listaTokens.Count)
                        {
                            preanalisis = listaTokens[NoToken].Lexema.ToLower();
                        }

                        break;

                    case ("numero"):

                        NoToken++;

                        if (NoToken < listaTokens.Count)
                        {
                            preanalisis = listaTokens[NoToken].Lexema.ToLower();
                        }

                        break;

                    case ("decimal"):

                        NoToken++;

                        if (NoToken < listaTokens.Count)
                        {
                            preanalisis = listaTokens[NoToken].Lexema.ToLower();
                        }

                        break;
                    default:

                        if (NoToken == listaTokens.Count)
                        {
                            string error1 = Convert.ToString(preanalisis);
                            guardarErrores(noError, error1, "sintactico", "se esperaba un numero entero o una cadena", listaTokens[NoToken - 1].fila + 1
                            , listaTokens[NoToken - 1].columna + 1);
                            noError++;


                        }
                        else
                        {

                            if (preanalisis == ")")
                            {
                                string error1 = Convert.ToString(preanalisis);
                                guardarErrores(noError, error1, "sintactico", "se esperaba un numero entero o una cadena", listaTokens[NoToken].fila
                                , listaTokens[NoToken].columna);
                                noError++;
                                //NoToken++;

                            }
                            else
                            {

                                string error1 = Convert.ToString(preanalisis);
                                guardarErrores(noError, error1, "sintactico", "se esperaba un numero entero o una cadena", listaTokens[NoToken].fila
                                , listaTokens[NoToken].columna);
                                noError++;
                                NoToken++;
                                if (NoToken < listaTokens.Count)
                                {
                                    preanalisis = listaTokens[NoToken].Lexema;
                                }

                            }
                        }

                        break;
                }

            }


        }

        public void dec()
        {

            if (preanalisis.Equals("}"))
            {

                if (listaTokens[NoToken - 1].Lexema.Equals("{"))
                {
                    string error1 = Convert.ToString(preanalisis);
                    guardarErrores(noError, error1, "sintactica", "tiene que haber almenos una istrucciones en el bloque", listaTokens[NoToken].fila
                    , listaTokens[NoToken].columna);
                    //ACA SE SALE AL BLOQUE Y VERIFICA LA LLAVE QUE CIERRA EN EL METODO BLOQUE
                }

            }

            else if (NoToken < listaTokens.Count)
            {

                if (listaTokens[NoToken].tipo.ToLower().Equals("id"))
                {
                    NoToken++;

                    if (NoToken < listaTokens.Count)
                    {
                        preanalisis = listaTokens[NoToken].Lexema.ToLower();

                    }

                    if (!preanalisis.Equals(":"))
                    {
                        if (NoToken < listaTokens.Count)
                        {
                            masID();
                        }
                    }
                    parea(":");
                    tipo();
                    asign();
                    if (NoToken < listaTokens.Count)
                    {
                        masDec();
                    }


                }

                else if (preanalisis.Equals(":"))
                {
                    string error1 = Convert.ToString(preanalisis);
                    guardarErrores(noError, error1, "sintactico", "se esperaban un id", listaTokens[NoToken].fila
                    , listaTokens[NoToken].columna);
                    noError++;

                    parea(":");
                    tipo();
                    asign();
                    if (NoToken < listaTokens.Count)
                    {
                        masDec();
                    }

                }

                else
                {

                    string error1 = Convert.ToString(preanalisis);
                    guardarErrores(noError, error1, "sintactico", "se esperaban un ID", listaTokens[NoToken].fila
                    , listaTokens[NoToken].columna);
                    noError++;
                    NoToken++;
                    if (NoToken < listaTokens.Count)
                    {
                        preanalisis = listaTokens[NoToken].Lexema.ToLower();
                        dec();
                    }


                }


            }


        }

        public void masDec()
        {

            if (listaTokens[NoToken].tipo.ToLower().Equals("id"))
            {
                NoToken++;

                if (NoToken < listaTokens.Count)
                {
                    preanalisis = listaTokens[NoToken].Lexema.ToLower();

                }

                if (!preanalisis.Equals(":"))
                {
                    if (NoToken < listaTokens.Count)
                    {
                        masID();
                    }
                }
                parea(":");
                tipo();
                asign();
                if (NoToken < listaTokens.Count)
                {
                    masDec();
                }


            }

            else if (preanalisis.Equals(":"))
            {
                string error1 = Convert.ToString(preanalisis);
                guardarErrores(noError, error1, "sintactico", "se esperaban un id", listaTokens[NoToken].fila
                , listaTokens[NoToken].columna);
                noError++;

                parea(":");
                tipo();
                asign();
                if (NoToken < listaTokens.Count)
                {
                    masDec();
                }

            }

            else if (!preanalisis.Equals("}"))
            {

                string error1 = Convert.ToString(preanalisis);
                guardarErrores(noError, error1, "sintactico", "se esperaban un ID", listaTokens[NoToken].fila
                , listaTokens[NoToken].columna);
                noError++;
                NoToken++;
                if (NoToken < listaTokens.Count)
                {
                    preanalisis = listaTokens[NoToken].Lexema.ToLower();
                    masDec();
                }


            }


        }

        public void masID()
        {

            parea(",");

            if (listaTokens[NoToken].tipo.ToLower().Equals("id"))
            {
                NoToken++;

                if (NoToken < listaTokens.Count)
                {
                    preanalisis = listaTokens[NoToken].Lexema.ToLower();

                }

            }
            else
            {
                string error1 = Convert.ToString(preanalisis);
                guardarErrores(noError, error1, "sintactico", "se esperaba  ID ", listaTokens[NoToken].fila
                , listaTokens[NoToken].columna);

            }

            if (!preanalisis.Equals(":"))
            {
                if (NoToken < listaTokens.Count)
                {
                    masID();
                }
            }

        }

        public void tipo()
        {
            switch (preanalisis)
            {
                case "entero":

                    parea("entero");

                    break;

                case "cadena":

                    parea("cadena");

                    break;

                default:

                    if (NoToken == listaTokens.Count)
                    {
                        string error1 = Convert.ToString(preanalisis);
                        guardarErrores(noError, error1, "sintactico", "se esperaba un tipo de dato", listaTokens[NoToken - 1].fila + 1
                        , listaTokens[NoToken - 1].columna + 1);
                        noError++;

                    }
                    else
                    {
                        string error1 = Convert.ToString(preanalisis);
                        guardarErrores(noError, error1, "sintactico", "se esperaba un tipo de dato", listaTokens[NoToken].fila
                        , listaTokens[NoToken].columna);
                        noError++;

                    }
                    break;

            }

        }

        public void asign()
        {

            switch (preanalisis)
            {
                case "=":

                    parea("=");
                    val();
                    parea(";");

                    break;

                case ";":

                    parea(";");

                    break;

                default:

                    if (NoToken == listaTokens.Count)
                    {
                        string error1 = Convert.ToString(preanalisis);
                        guardarErrores(noError, error1, "sintactico", "se esperaba un asignacion o un unto y coma", listaTokens[NoToken - 1].fila + 1
                        , listaTokens[NoToken - 1].columna + 1);
                        noError++;
                    }
                    else
                    {
                        string error1 = Convert.ToString(preanalisis);
                        guardarErrores(noError, error1, "sintactico", "se esperaba un asignacion o un unto y coma", listaTokens[NoToken].fila
                        , listaTokens[NoToken].columna);
                        noError++;
                    }

                    break;

            }

        }

        public void val()
        {
            if (NoToken < listaTokens.Count)
            {
                switch (listaTokens[NoToken].tipo.ToLower())
                {

                    case ("cadena"):

                        NoToken++;

                        if (NoToken < listaTokens.Count)
                        {
                            preanalisis = listaTokens[NoToken].Lexema.ToLower();
                        }

                        break;

                    case ("numero"):

                        NoToken++;

                        if (NoToken < listaTokens.Count)
                        {
                            preanalisis = listaTokens[NoToken].Lexema.ToLower();
                        }

                        break;

                    case ("numero negativo"):

                        NoToken++;

                        if (NoToken < listaTokens.Count)
                        {
                            preanalisis = listaTokens[NoToken].Lexema.ToLower();
                        }

                        break;

                    default:

                        if (NoToken == listaTokens.Count)
                        {
                            string error1 = Convert.ToString(preanalisis);
                            guardarErrores(noError, error1, "sintactico", "se esperaba un numero entero(pisitivo o negativo) o una cadena", listaTokens[NoToken - 1].fila + 1
                            , listaTokens[NoToken - 1].columna + 1);
                            noError++;

                        }
                        else
                        {

                            string error1 = Convert.ToString(preanalisis);
                            guardarErrores(noError, error1, "sintactico", "se esperaba un asignacion o un unto y coma", listaTokens[NoToken].fila
                            , listaTokens[NoToken].columna);
                            noError++;

                        }
                        break;

                }

            }

        }

        public void text()
        {

            if (preanalisis.Equals("}"))
            {

                if (listaTokens[NoToken - 1].Lexema.Equals("{"))
                {
                    string error1 = Convert.ToString(preanalisis);
                    guardarErrores(noError, error1, "sintactica", "tiene que haber almenos una istruccione en el bloque", listaTokens[NoToken].fila
                    , listaTokens[NoToken].columna);
                    //ACA SE SALE AL BLOQUE Y VERIFICA LA LLAVE QUE CIERRA EN EL METODO BLOQUE
                }

            }

            else
            {
                switch (preanalisis)
                {
                    case "imagen":

                        parea("imagen");
                        parea("(");

                        //Una cadena
                        if (NoToken < listaTokens.Count)
                        {
                            if (listaTokens[NoToken].tipo.ToLower().Equals("cadena"))
                            {
                                NoToken++;
                                preanalisis = listaTokens[NoToken].Lexema.ToLower();

                            }
                            else
                            {
                                string error1 = Convert.ToString(preanalisis);
                                guardarErrores(noError, error1, "sintactico", "se esperaba una cadena de parametro", listaTokens[NoToken].fila
                                , listaTokens[NoToken].columna);
                                noError++;

                            }

                        }
                        else
                        {
                            string error1 = Convert.ToString(preanalisis);
                            guardarErrores(noError, error1, "sintactico", "se esperaba una cadena de parametro", listaTokens[NoToken - 1].fila + 1
                            , listaTokens[NoToken - 1].columna + 1);
                        }

                        parea(",");
                        Dig();
                        parea(",");
                        Dig();
                        parea(")");
                        parea(";");
                        if (NoToken < listaTokens.Count)
                        {
                            masText();
                        }

                        break;

                    case "numeros":

                        parea("numeros");
                        parea("(");

                        //Una cadena
                        if (NoToken < listaTokens.Count)
                        {
                            if (listaTokens[NoToken].tipo.ToLower().Equals("cadena"))
                            {
                                NoToken++;
                                preanalisis = listaTokens[NoToken].Lexema.ToLower();

                            }
                            else
                            {
                                string error1 = Convert.ToString(preanalisis);
                                guardarErrores(noError, error1, "sintactico", "se esperaba almenos una cadena de parametro", listaTokens[NoToken].fila
                                , listaTokens[NoToken].columna);
                                noError++;

                            }

                        }
                        else
                        {
                            string error1 = Convert.ToString(preanalisis);
                            guardarErrores(noError, error1, "sintactico", "se esperaba una cadena de parametro", listaTokens[NoToken - 1].fila + 1
                            , listaTokens[NoToken - 1].columna + 1);
                        }


                        parea(")");
                        parea(";");
                        if (NoToken < listaTokens.Count)
                        {
                            masText();
                        }

                        break;

                    case "var":

                        parea("var");
                        parea("[");

                        //Un ID
                        if (NoToken < listaTokens.Count)
                        {
                            if (listaTokens[NoToken].tipo.ToLower().Equals("id"))
                            {
                                NoToken++;
                                preanalisis = listaTokens[NoToken].Lexema.ToLower();

                            }
                            else
                            {
                                string error1 = Convert.ToString(preanalisis);
                                guardarErrores(noError, error1, "sintactico", "se esperaba una ID de parametro", listaTokens[NoToken].fila
                                , listaTokens[NoToken].columna);
                                noError++;

                            }

                        }
                        else
                        {
                            string error1 = Convert.ToString(preanalisis);
                            guardarErrores(noError, error1, "sintactico", "se esperaba una ID como parametro", listaTokens[NoToken - 1].fila + 1
                            , listaTokens[NoToken - 1].columna + 1);

                        }

                        parea("]");
                        parea(";");
                        if (NoToken < listaTokens.Count)
                        {
                            masText();
                        }

                        break;

                    case "asignar":

                        parea("asignar");
                        parea("(");

                        //Un ID
                        if (NoToken < listaTokens.Count)
                        {
                            if (listaTokens[NoToken].tipo.ToLower().Equals("id"))
                            {
                                NoToken++;
                                preanalisis = listaTokens[NoToken].Lexema.ToLower();

                            }
                            else
                            {
                                string error1 = Convert.ToString(preanalisis);
                                guardarErrores(noError, error1, "sintactico", "se esperaba una ID de parametro", listaTokens[NoToken].fila
                                , listaTokens[NoToken].columna);
                                noError++;

                            }

                        }
                        else
                        {
                            string error1 = Convert.ToString(preanalisis);
                            guardarErrores(noError, error1, "sintactico", "se esperaba un ID como parametro", listaTokens[NoToken - 1].fila + 1
                            , listaTokens[NoToken - 1].columna + 1);
                        }

                        parea(",");
                        Valr();
                        parea(")");
                        parea(";");
                        if (NoToken < listaTokens.Count)
                        {
                            masText();
                        }

                        break;

                    case "suma":

                        parea("suma");
                        parea("(");
                        Num();
                        parea(")");
                        parea(";");
                        if (NoToken < listaTokens.Count)
                        {
                            masText();
                        }

                        break;

                    case "promedio":

                        parea("promedio");
                        parea("(");
                        Num();
                        parea(")");
                        parea(";");
                        if (NoToken < listaTokens.Count)
                        {
                            masText();
                        }

                        break;

                    case "linea_en_blanco":

                        parea("linea_en_blanco");
                        parea(";");
                        if (NoToken < listaTokens.Count)
                        {
                            masText();
                        }

                        break;

                    default:

                        if (NoToken < listaTokens.Count)
                        {
                            switch (listaTokens[NoToken].tipo.ToLower())
                            {
                                case "cadena":
                                    NoToken++;

                                    if (NoToken < listaTokens.Count)
                                    {
                                        preanalisis = listaTokens[NoToken].Lexema.ToLower();

                                    }
                                    if (NoToken < listaTokens.Count)
                                    {

                                        masText();
                                    }

                                    break;
                                case "cadena negrita":

                                    NoToken++;

                                    if (NoToken < listaTokens.Count)
                                    {
                                        preanalisis = listaTokens[NoToken].Lexema.ToLower();

                                    }
                                    parea(";");
                                    if (NoToken < listaTokens.Count)
                                    {

                                        masText();
                                    }

                                    break;
                                case "cadena subrayada":

                                    NoToken++;

                                    if (NoToken < listaTokens.Count)
                                    {
                                        preanalisis = listaTokens[NoToken].Lexema.ToLower();

                                    }
                                    parea(";");
                                    if (NoToken < listaTokens.Count)
                                    {

                                        masText();
                                    }

                                    break;
                                case "comentario":

                                    NoToken++;

                                    if (NoToken < listaTokens.Count)
                                    {
                                        preanalisis = listaTokens[NoToken].Lexema.ToLower();

                                    }
                                    if (NoToken < listaTokens.Count)
                                    {

                                        masText();
                                    }

                                    break;

                                default:

                                    if (NoToken == listaTokens.Count)
                                    {
                                        string error1 = Convert.ToString(preanalisis);
                                        guardarErrores(noError, error1, "sintactico", "se esperaba una intruccion del bloque", listaTokens[NoToken - 1].fila + 1
                                        , listaTokens[NoToken - 1].columna + 1);
                                        noError++;
                                    }
                                    else
                                    {
                                        string error1 = Convert.ToString(preanalisis);
                                        guardarErrores(noError, error1, "sintactico", "se esperaba un asignacion o un unto y coma", listaTokens[NoToken].fila
                                        , listaTokens[NoToken].columna);
                                        noError++;
                                    }

                                    break;

                            }

                        }

                        break;

                }

            }

        }

        public void masText()
        {

            if (!preanalisis.Equals("}"))
            {
                switch (preanalisis)
                {
                    case "imagen":

                        parea("imagen");
                        parea("(");

                        //Una cadena
                        if (NoToken < listaTokens.Count)
                        {
                            if (listaTokens[NoToken].tipo.ToLower().Equals("cadena"))
                            {
                                NoToken++;
                                preanalisis = listaTokens[NoToken].Lexema.ToLower();

                            }
                            else
                            {
                                string error1 = Convert.ToString(preanalisis);
                                guardarErrores(noError, error1, "sintactico", "se esperaba una cadena de parametro", listaTokens[NoToken].fila
                                , listaTokens[NoToken].columna);
                                noError++;

                            }

                        }
                        else
                        {
                            string error1 = Convert.ToString(preanalisis);
                            guardarErrores(noError, error1, "sintactico", "se esperaba una cadena de parametro", listaTokens[NoToken - 1].fila + 1
                            , listaTokens[NoToken - 1].columna + 1);
                        }

                        parea(",");
                        Dig();
                        parea(",");
                        Dig();
                        parea(")");
                        parea(";");
                        if (NoToken < listaTokens.Count)
                        {
                            masText();
                        }

                        break;

                    case "numeros":

                        parea("numeros");
                        parea("(");

                        //Una cadena
                        if (NoToken < listaTokens.Count)
                        {
                            if (listaTokens[NoToken].tipo.ToLower().Equals("cadena"))
                            {
                                NoToken++;
                                if (NoToken < listaTokens.Count)
                                {
                                    preanalisis = listaTokens[NoToken].Lexema.ToLower();
                                }
                            }
                            else
                            {
                                string error1 = Convert.ToString(preanalisis);
                                guardarErrores(noError, error1, "sintactico", "se esperaba una cadena de parametro", listaTokens[NoToken].fila
                                , listaTokens[NoToken].columna);
                                noError++;

                            }

                        }
                        else
                        {
                            string error1 = Convert.ToString(preanalisis);
                            guardarErrores(noError, error1, "sintactico", "se esperaba una cadena de parametro", listaTokens[NoToken - 1].fila + 1
                            , listaTokens[NoToken - 1].columna + 1);
                        }

                        masCade();
                        parea(")");
                        parea(";");
                        if (NoToken < listaTokens.Count)
                        {
                            masText();
                        }

                        break;

                    case "var":

                        parea("var");
                        parea("[");

                        //Un ID
                        if (NoToken < listaTokens.Count)
                        {
                            if (listaTokens[NoToken].tipo.ToLower().Equals("id"))
                            {
                                NoToken++;
                                preanalisis = listaTokens[NoToken].Lexema.ToLower();

                            }
                            else
                            {
                                string error1 = Convert.ToString(preanalisis);
                                guardarErrores(noError, error1, "sintactico", "se esperaba una ID de parametro", listaTokens[NoToken].fila
                                , listaTokens[NoToken].columna);
                                noError++;

                            }

                        }
                        else
                        {
                            string error1 = Convert.ToString(preanalisis);
                            guardarErrores(noError, error1, "sintactico", "se esperaba una ID como parametro", listaTokens[NoToken - 1].fila + 1
                            , listaTokens[NoToken - 1].columna + 1);

                        }

                        parea("]");
                        parea(";");
                        if (NoToken < listaTokens.Count)
                        {
                            masText();
                        }

                        break;

                    case "asignar":

                        parea("asignar");
                        parea("(");

                        //Un ID
                        if (NoToken < listaTokens.Count)
                        {
                            if (listaTokens[NoToken].tipo.ToLower().Equals("id"))
                            {
                                NoToken++;
                                preanalisis = listaTokens[NoToken].Lexema.ToLower();

                            }
                            else
                            {
                                string error1 = Convert.ToString(preanalisis);
                                guardarErrores(noError, error1, "sintactico", "se esperaba una ID de parametro", listaTokens[NoToken].fila
                                , listaTokens[NoToken].columna);
                                noError++;

                            }

                        }
                        else
                        {
                            string error1 = Convert.ToString(preanalisis);
                            guardarErrores(noError, error1, "sintactico", "se esperaba un ID como parametro", listaTokens[NoToken - 1].fila + 1
                            , listaTokens[NoToken - 1].columna + 1);
                        }

                        parea(",");
                        Valr();
                        parea(")");
                        parea(";");
                        if (NoToken < listaTokens.Count)
                        {
                            masText();
                        }

                        break;

                    case "suma":

                        parea("suma");
                        parea("(");
                        Num();
                        parea(")");
                        parea(";");
                        if (NoToken < listaTokens.Count)
                        {
                            masText();
                        }

                        break;

                    case "promedio":

                        parea("promedio");
                        parea("(");
                        Num();
                        parea(")");
                        parea(";");
                        if (NoToken < listaTokens.Count)
                        {
                            masText();
                        }

                        break;

                    case "linea_en_blanco":

                        parea("linea_en_blanco");

                        parea(";");

                        if (NoToken < listaTokens.Count)
                        {
                            masText();
                        }

                        break;

                    default:

                        if (NoToken < listaTokens.Count)
                        {
                            switch (listaTokens[NoToken].tipo.ToLower())
                            {
                                case "cadena":
                                    NoToken++;

                                    if (NoToken < listaTokens.Count)
                                    {
                                        preanalisis = listaTokens[NoToken].Lexema.ToLower();

                                    }
                                    if (NoToken < listaTokens.Count)
                                    {

                                        masText();
                                    }

                                    break;
                                case "cadena negrita":

                                    NoToken++;

                                    if (NoToken < listaTokens.Count)
                                    {
                                        preanalisis = listaTokens[NoToken].Lexema.ToLower();

                                    }
                                    parea(";");
                                    if (NoToken < listaTokens.Count)
                                    {

                                        masText();
                                    }

                                    break;
                                case "cadena subrayada":

                                    NoToken++;

                                    if (NoToken < listaTokens.Count)
                                    {
                                        preanalisis = listaTokens[NoToken].Lexema.ToLower();

                                    }
                                    parea(";");
                                    if (NoToken < listaTokens.Count)
                                    {

                                        masText();
                                    }

                                    break;
                                case "comentario":

                                    NoToken++;

                                    if (NoToken < listaTokens.Count)
                                    {
                                        preanalisis = listaTokens[NoToken].Lexema.ToLower();

                                    }
                                    if (NoToken < listaTokens.Count)
                                    {

                                        masText();
                                    }

                                    break;

                                default:

                                    if (NoToken == listaTokens.Count)
                                    {
                                        string error1 = Convert.ToString(preanalisis);
                                        guardarErrores(noError, error1, "sintactico", "se esperaba una intruccion del bloque", listaTokens[NoToken - 1].fila + 1
                                        , listaTokens[NoToken - 1].columna + 1);
                                        noError++;
                                    }
                                    else
                                    {
                                        string error1 = Convert.ToString(preanalisis);
                                        guardarErrores(noError, error1, "sintactico", "se esperaba un asignacion o un unto y coma", listaTokens[NoToken].fila
                                        , listaTokens[NoToken].columna);
                                        noError++;
                                    }

                                    break;

                            }

                        }

                        break;

                }
            }

        }

        public void masCade()
        {

            if (NoToken < listaTokens.Count)
            {
                if (!preanalisis.Equals(")"))
                {
                    parea(",");

                    if (listaTokens[NoToken].tipo.ToLower().Equals("cadena"))
                    {
                        NoToken++;
                        if (NoToken < listaTokens.Count)
                        {
                            preanalisis = listaTokens[NoToken].Lexema.ToLower();

                        }

                    }
                    else
                    {
                        string error1 = Convert.ToString(preanalisis);
                        guardarErrores(noError, error1, "sintactico", "se esperaba almenos una dadena", listaTokens[NoToken].fila
                        , listaTokens[NoToken].columna);

                    }

                    if (NoToken < listaTokens.Count)
                    {
                        masCade();
                    }

                }

            }


        }

        public void Dig()
        {
            if (NoToken < listaTokens.Count)
            {
                switch (listaTokens[NoToken].tipo.ToLower())
                {

                    case "numero":

                        NoToken++;
                        if (NoToken < listaTokens.Count)
                        {
                            preanalisis = listaTokens[NoToken].Lexema.ToLower();
                        }

                        break;
                    case "decimal":

                        NoToken++;
                        if (NoToken < listaTokens.Count)
                        {
                            preanalisis = listaTokens[NoToken].Lexema.ToLower();
                        }

                        break;

                    default:

                        string error1 = Convert.ToString(preanalisis);
                        guardarErrores(noError, error1, "sintactico", "se esperaba el valor del parametro", listaTokens[NoToken].fila
                        , listaTokens[NoToken].columna);
                        noError++;


                        break;

                }

            }
            else
            {

                string error1 = Convert.ToString(preanalisis);
                guardarErrores(noError, error1, "sintactico", "se esperaba el valor del parametro", listaTokens[NoToken - 1].fila + 1
                , listaTokens[NoToken - 1].columna + 1);
                noError++;

            }

        }

        public void Valr()
        {
            if (NoToken < listaTokens.Count)
            {

                switch (listaTokens[NoToken].tipo.ToLower())
                {
                    case "id":

                        NoToken++;
                        if (NoToken < listaTokens.Count)
                        {
                            preanalisis = listaTokens[NoToken].Lexema.ToLower();
                        }

                        break;

                    case "numero":

                        NoToken++;
                        if (NoToken < listaTokens.Count)
                        {
                            preanalisis = listaTokens[NoToken].Lexema.ToLower();
                        }

                        break;

                    case "numero negativo":

                        NoToken++;
                        if (NoToken < listaTokens.Count)
                        {
                            preanalisis = listaTokens[NoToken].Lexema.ToLower();
                        }

                        break;

                    case "cadena":

                        NoToken++;
                        if (NoToken < listaTokens.Count)
                        {
                            preanalisis = listaTokens[NoToken].Lexema.ToLower();
                        }

                        break;

                    default:

                        string error1 = Convert.ToString(preanalisis);
                        guardarErrores(noError, error1, "sintactico", "se esperaba el valor de asignacion", listaTokens[NoToken].fila
                        , listaTokens[NoToken].columna);
                        noError++;

                        break;

                }

            }
            else
            {
                string error1 = Convert.ToString(preanalisis);
                guardarErrores(noError, error1, "sintactico", "se esperaba el valor de aasignacion", listaTokens[NoToken - 1].fila + 1
                , listaTokens[NoToken - 1].columna + 1);
                noError++;

            }

        }

        public void Num()
        {
            if (NoToken < listaTokens.Count)
            {
                if (preanalisis.Equals(")"))
                {
                    string error1 = Convert.ToString(preanalisis);
                    guardarErrores(noError, error1, "sintactico", "se esperaba el valor numerico de parametros", listaTokens[NoToken - 1].fila + 1
                    , listaTokens[NoToken - 1].columna + 1);
                    noError++;


                }
                else
                {

                    switch (listaTokens[NoToken].tipo.ToLower())
                    {
                        case "numero":

                            NoToken++;
                            if (NoToken < listaTokens.Count)
                            {
                                preanalisis = listaTokens[NoToken].Lexema.ToLower();
                            }
                            masNum();

                            break;

                        case "numero negativo":

                            NoToken++;
                            if (NoToken < listaTokens.Count)
                            {
                                preanalisis = listaTokens[NoToken].Lexema.ToLower();
                            }
                            masNum();

                            break;

                        case "id":

                            NoToken++;
                            if (NoToken < listaTokens.Count)
                            {
                                preanalisis = listaTokens[NoToken].Lexema.ToLower();
                            }
                            masNum();

                            break;

                        default:

                            string error1 = Convert.ToString(preanalisis);
                            guardarErrores(noError, error1, "sintactico", "se esperaba un valor numerico de parametros ", listaTokens[NoToken].fila
                            , listaTokens[NoToken].columna);
                            noError++;

                            break;

                    }
                }

            }
            else
            {
                string error1 = Convert.ToString(preanalisis);
                guardarErrores(noError, error1, "sintactico", "se esperab un valor numerico de parametros", listaTokens[NoToken - 1].fila + 1
                , listaTokens[NoToken - 1].columna + 1);
                noError++;

            }


        }

        public void masNum()
        {
            if (!preanalisis.Equals(")"))
            {
                parea(",");

                switch (listaTokens[NoToken].tipo.ToLower())
                {
                    case "numero":

                        NoToken++;
                        if (NoToken < listaTokens.Count)
                        {
                            preanalisis = listaTokens[NoToken].Lexema.ToLower();
                        }
                        masNum();

                        break;

                    case "numero negativo":

                        NoToken++;
                        if (NoToken < listaTokens.Count)
                        {
                            preanalisis = listaTokens[NoToken].Lexema.ToLower();
                        }
                        masNum();

                        break;

                    case "id":

                        NoToken++;
                        if (NoToken < listaTokens.Count)
                        {
                            preanalisis = listaTokens[NoToken].Lexema.ToLower();
                        }
                        masNum();

                        break;

                    default:

                        string error1 = Convert.ToString(preanalisis);
                        guardarErrores(noError, error1, "sintactico", "se esperaba almenos el valor numerico de parametros ", listaTokens[NoToken].fila
                        , listaTokens[NoToken].columna);
                        noError++;


                        break;

                }

            }

        }

        public void GnerarPDFTokens()
        {
            string salida = ":(";

            for (int i = 0; i < listaTokens.Count; i++)
            {
                if (listaTokens[i].Lexema.ToLower().Equals("nombre_archivo"))
                {
                    salida = listaTokens[i + 2].Lexema;
                    salida = salida.Substring(1, salida.Length - 2);
                }

            }

            FileStream fs = new FileStream("C:\\Users\\alexl\\Desktop\\listadoTokens.pdf", FileMode.Create);
            Document documento = new Document(PageSize.LETTER);
            PdfWriter pw = PdfWriter.GetInstance(documento, fs);

            documento.Open();

            var MyFontBold = FontFactory.GetFont(FontFactory.TIMES_BOLD, 11);
            var MyFontBold1 = FontFactory.GetFont(FontFactory.TIMES, 10);
            var MyFontBold2 = FontFactory.GetFont(FontFactory.TIMES_BOLD, 18);

            documento.Add(new Paragraph("\nUNIVERSIDAD DE SAN CARLOS DE GUATEMALA \nFACULTAD DE INGENIERIA \nESCUELA DE CIENCIAS \nINGENIERIA EN CIENCIAS Y SISTEMAS \nLENGUAJES FORMALES Y DE PROGRAMACION\n", MyFontBold));
            iTextSharp.text.Image pic = iTextSharp.text.Image.GetInstance("C:\\Users\\alexl\\Desktop\\Usac_logo.png");
            pic.SetAbsolutePosition(450f, 625f);
            pic.ScalePercent(0.3f * 100);
            documento.Add(pic);


            string[] nombre = ruta.Split('\\');
            documento.Add(new Paragraph("\n\nArchivo Fuente: " + nombre[nombre.Length - 1]));
            documento.Add(new Paragraph("Archivo Salida: " + salida));

            documento.Add(new Paragraph("\n"));
            Paragraph parr = new Paragraph("Tabla de Token", MyFontBold2);
            parr.Alignment = Element.ALIGN_CENTER;
            documento.Add(parr);
            documento.Add(new Paragraph("\n"));

            PdfPTable tabla = new PdfPTable(6);
            tabla.WidthPercentage = 95f;

            tabla.AddCell(new Paragraph("No", MyFontBold));
            tabla.AddCell(new Paragraph("Token", MyFontBold));
            tabla.AddCell(new Paragraph("Lexema", MyFontBold));
            tabla.AddCell(new Paragraph("Tipo", MyFontBold));
            tabla.AddCell(new Paragraph("Fila", MyFontBold));
            tabla.AddCell(new Paragraph("Columna", MyFontBold));
            foreach (var item in listaTokens)
            {
                tabla.AddCell(new Paragraph(item.NoToken.ToString(), MyFontBold1));
                tabla.AddCell(new Paragraph(item.iDtoken.ToString(), MyFontBold1));
                tabla.AddCell(new Paragraph(item.Lexema, MyFontBold1));
                tabla.AddCell(new Paragraph(item.tipo, MyFontBold1));
                tabla.AddCell(new Paragraph(item.fila.ToString(), MyFontBold1));
                tabla.AddCell(new Paragraph(item.columna.ToString(), MyFontBold1));
            }

            documento.Add(tabla);

            documento.Close();

            Process.Start("C:\\Users\\alexl\\Desktop\\listadoTokens.pdf");

        }

        public void GnerarPDFErrores()
        {
            FileStream fs = new FileStream("C:\\Users\\alexl\\Desktop\\listadoErrores.pdf", FileMode.Create);
            Document documento = new Document(PageSize.LETTER);
            PdfWriter pw = PdfWriter.GetInstance(documento, fs);

            documento.Open();

            var MyFontBold = FontFactory.GetFont(FontFactory.TIMES_BOLD, 11);
            var MyFontBold1 = FontFactory.GetFont(FontFactory.TIMES, 10);
            var MyFontBold2 = FontFactory.GetFont(FontFactory.TIMES_BOLD, 18);

            documento.Add(new Paragraph("\nUNIVERSIDAD DE SAN CARLOS DE GUATEMALA \nFACULTAD DE INGENIERIA \nESCUELA DE CIENCIAS \nINGENIERIA EN CIENCIAS Y SISTEMAS \nLENGUAJES FORMALES Y DE PROGRAMACION\n", MyFontBold));
            iTextSharp.text.Image pic = iTextSharp.text.Image.GetInstance("C:\\Users\\alexl\\Desktop\\Usac_logo.png");
            pic.SetAbsolutePosition(450f, 625f);
            pic.ScalePercent(0.3f * 100);
            documento.Add(pic);


            string[] nombre = ruta.Split('\\');
            documento.Add(new Paragraph("\n\nArchivo Fuente: " + nombre[nombre.Length - 1]));

            documento.Add(new Paragraph("\n"));
            Paragraph parr = new Paragraph("Tabla de Errores", MyFontBold2);
            parr.Alignment = Element.ALIGN_CENTER;
            documento.Add(parr);
            documento.Add(new Paragraph("\n"));

            PdfPTable tabla = new PdfPTable(6);
            tabla.WidthPercentage = 95f;

            tabla.AddCell(new Paragraph("No", MyFontBold));
            tabla.AddCell(new Paragraph("Token", MyFontBold));
            tabla.AddCell(new Paragraph("Lexema", MyFontBold));
            tabla.AddCell(new Paragraph("Tipo", MyFontBold));
            tabla.AddCell(new Paragraph("Fila", MyFontBold));
            tabla.AddCell(new Paragraph("Columna", MyFontBold));
            foreach (var item in listaErrores)
            {
                tabla.AddCell(new Paragraph(item.noError.ToString(), MyFontBold1));
                tabla.AddCell(new Paragraph(item.error, MyFontBold1));
                tabla.AddCell(new Paragraph(item.tipo, MyFontBold1));
                tabla.AddCell(new Paragraph(item.descripcion, MyFontBold1));
                tabla.AddCell(new Paragraph(item.filaError.ToString(), MyFontBold1));
                tabla.AddCell(new Paragraph(item.colunmaError.ToString(), MyFontBold1));
            }

            documento.Add(tabla);

            documento.Close();

            Process.Start("C:\\Users\\alexl\\Desktop\\listadoErrores.pdf");

        }

        public bool verificarVariableRepe()
        {

            if (listaDecVariables.Count == 0)
            {

                for (int i = 0; i < listaTokens.Count; i++)
                {
                    string token = listaTokens[i].Lexema.ToLower();

                    if (token.Equals("variables"))
                    {
                        while (!token.Equals("}"))
                        {
                            if (listaTokens[i].tipo.ToLower().Equals("id"))
                            {
                                listaDecVariables.Add(listaTokens[i]);
                            }

                            i++;
                            token = listaTokens[i].Lexema.ToLower();
                        }

                    }

                }

            }

            int cont = 0;

            for (int i = 0; i < listaDecVariables.Count; i++)
            {
                for (int j = 0; j < listaDecVariables.Count; j++)
                {

                    if (listaDecVariables[i].Lexema.ToLower().Equals(listaDecVariables[j].Lexema.ToLower()))
                    {

                        cont++;

                        if (cont >= 2)
                        {

                            MessageBox.Show("Error, la variable:" + listaDecVariables[j].Lexema + "Se ah inicializado mas de una vez");
                            return false;
                        }

                    }


                }
                cont = 0;

            }

            listaDecVariables = new List<token>();
            return true;

        }

        public void generarPDF()
        {

            string salida = "Salida.pdf";
            float inter = 1.5F;
            int tamLetra = 11;
            string ubicacion = "C:\\Proyecto2\\";


            for (int i = 0; i < listaTokens.Count; i++)
            {
                string token = listaTokens[i].Lexema.ToLower();

                if (token.Equals("variables"))
                {

                    while (!token.Equals("}"))
                    {

                        if (listaTokens[i].tipo.ToLower().Equals("id"))
                        {
                            string ids = listaTokens[i].Lexema.ToLower();

                            if (listaTokens[i + 1].Lexema.ToLower().Equals(","))
                            {
                                i++;

                                while (!listaTokens[i].tipo.ToLower().Equals("dos puntos"))
                                {
                                    if (listaTokens[i].tipo.ToLower().Equals("id"))
                                    {

                                        ids += "," + listaTokens[i].Lexema.ToLower();
                                    }

                                    i++;
                                }

                                string[] idss = ids.Split(',');
                                if (idss.Length > 0)
                                {
                                    for (int j = 0; j < idss.Length; j++)
                                    {

                                        if (listaTokens[i + 2].Lexema.ToLower().Equals(";"))
                                        {
                                            listaVariables.Add(new variable(idss[j], listaTokens[i + 1].Lexema, "0"));

                                        }
                                        else
                                        {
                                            listaVariables.Add(new variable(idss[j], listaTokens[i + 1].Lexema, listaTokens[i + 3].Lexema));

                                        }


                                    }

                                }

                            }
                            else
                            {
                                i++;

                                if (listaTokens[i + 2].Lexema.ToLower().Equals(";"))
                                {
                                    listaVariables.Add(new variable(ids, listaTokens[i + 1].Lexema, "0"));

                                }
                                else
                                {
                                    listaVariables.Add(new variable(ids, listaTokens[i + 1].Lexema, listaTokens[i + 3].Lexema));

                                }

                            }

                        }

                        i++;
                        token = listaTokens[i].Lexema.ToLower();

                    }


                }

                else if (token.Equals("instrucciones"))
                {

                    while (!token.Equals("}"))
                    {
                        if (listaTokens[i].Lexema.ToLower().Equals("nombre_archivo"))
                        {
                            salida = listaTokens[i + 2].Lexema;
                            salida = salida.Substring(1, salida.Length - 2);

                        }
                        else if (listaTokens[i].Lexema.ToLower().Equals("interlineado"))
                        {
                            inter = Convert.ToSingle(listaTokens[i + 2].Lexema, CultureInfo.CreateSpecificCulture("en-US"));

                        }
                        else if (listaTokens[i].Lexema.ToLower().Equals("tamanio_letra"))
                        {

                            tamLetra = Convert.ToInt32(listaTokens[i + 2].Lexema);

                        }
                        else if (listaTokens[i].Lexema.ToLower().Equals("direccion_archivo"))
                        {

                            ubicacion = listaTokens[i + 2].Lexema;
                            ubicacion = salida.Substring(1, salida.Length - 2);
                        }

                        i++;
                        token = listaTokens[i].Lexema.ToLower();
                    }


                }


            }

            FileStream fs = new FileStream(ubicacion + salida, FileMode.Create);
            Document documento = new Document(PageSize.LETTER);
            PdfWriter pw = PdfWriter.GetInstance(documento, fs);

            documento.Open();

            BaseFont b = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, BaseFont.EMBEDDED);
            iTextSharp.text.Font fn = new iTextSharp.text.Font(b, tamLetra, 0, BaseColor.BLACK);
            iTextSharp.text.Font fn1 = new iTextSharp.text.Font(b, tamLetra, 8, BaseColor.BLACK);
            iTextSharp.text.Font fn2 = new iTextSharp.text.Font(b, tamLetra, 1, BaseColor.BLACK);

            Paragraph parr;

            for (int i = 0; i < listaTokens.Count; i++)
            {

                string token = listaTokens[i].Lexema.ToLower();


                if (token.Equals("texto"))
                {

                    while (!token.Equals("}"))
                    {

                        if (listaTokens[i].Lexema.ToLower().Equals("imagen"))
                        {

                            iTextSharp.text.Image pic = iTextSharp.text.Image.GetInstance(listaTokens[i + 2].Lexema.Replace("\"", ""));
                            pic.ScaleAbsoluteWidth(int.Parse(listaTokens[i + 4].Lexema));
                            pic.ScaleAbsoluteHeight(int.Parse(listaTokens[i + 6].Lexema));
                            documento.Add(pic);

                        }
                        else if (listaTokens[i].Lexema.ToLower().Equals("numeros"))
                        {
                            string listado = "";
                            int num = 1;

                            while (!listaTokens[i].Lexema.ToLower().Equals(")"))
                            {
                                if (listaTokens[i].tipo.ToLower().Equals("cadena"))
                                {

                                    listado += num + "." + "  " + listaTokens[i].Lexema.Substring(1, listaTokens[i].Lexema.Length - 2) + "\n";
                                    num++;

                                }

                                i++;
                            }

                            parr = new Paragraph(listado, fn);
                            parr.SetLeading(0, inter);
                            documento.Add(parr);

                        }
                        else if (listaTokens[i].Lexema.ToLower().Equals("asignar"))
                        {

                            if (listaTokens[i + 4].tipo.ToLower().Equals("id"))
                            {

                                buscarParametro(listaTokens[i + 2].Lexema).valor = buscarParametro(listaTokens[i + 4].Lexema).valor;

                            }
                            else if (listaTokens[i + 4].tipo.ToLower().Equals("cadena"))
                            {

                                buscarParametro(listaTokens[i + 2].Lexema).valor = listaTokens[i + 4].Lexema;

                            }
                            else if (listaTokens[i + 4].tipo.ToLower().Equals("numero") | listaTokens[i + 4].tipo.ToLower().Equals("numero negativo"))
                            {
                                buscarParametro(listaTokens[i + 2].Lexema).valor = listaTokens[i + 4].Lexema;

                            }


                        }
                        else if (listaTokens[i].Lexema.ToLower().Equals("suma"))
                        {
                            int sum = 0;

                            while (!listaTokens[i].Lexema.ToLower().Equals(")"))
                            {
                                if (listaTokens[i].tipo.ToLower().Equals("nuemro") | listaTokens[i].tipo.ToLower().Equals("nuemro negativo"))
                                {
                                    sum = sum + Convert.ToInt32(listaTokens[i].Lexema.ToLower());

                                }
                                else if (listaTokens[i].tipo.ToLower().Equals("id"))
                                {
                                    sum = sum + Convert.ToInt32(buscarParametro(listaTokens[i].Lexema).valor);

                                }

                                i++;
                            }

                            parr = new Paragraph(Convert.ToString(sum) + "\n", fn);
                            parr.SetLeading(0, inter);
                            documento.Add(parr);

                        }
                        else if (listaTokens[i].Lexema.ToLower().Equals("promedio"))
                        {
                            int sum = 0;
                            int div = 0;
                            int res = 0;

                            while (!listaTokens[i].Lexema.ToLower().Equals(")"))
                            {
                                if (listaTokens[i].tipo.ToLower().Equals("nuemro") | listaTokens[i].tipo.ToLower().Equals("nuemro negativo"))
                                {
                                    sum = sum + Convert.ToInt32(listaTokens[i].Lexema.ToLower());
                                    div++;
                                }
                                else if (listaTokens[i].tipo.ToLower().Equals("id"))
                                {
                                    sum = sum + Convert.ToInt32(buscarParametro(listaTokens[i].Lexema).valor);
                                    div++;
                                }

                            }
                            res = sum / div;

                            parr = new Paragraph(Convert.ToString(res) + "\n", fn);
                            parr.SetLeading(0, inter);
                            documento.Add(parr);


                        }
                        else if (listaTokens[i].Lexema.ToLower().Equals("var"))
                        {

                            parr = new Paragraph(Convert.ToString(buscarParametro(listaTokens[i + 2].Lexema).valor + "\n"), fn);
                            parr.SetLeading(0, inter);
                            documento.Add(parr);

                        }
                        else if (listaTokens[i].Lexema.ToLower().Equals("linea_en_blanco"))
                        {
                            parr = new Paragraph("\n", fn);
                            parr.SetLeading(0, inter);
                            documento.Add(parr);

                        }
                        else if (listaTokens[i].tipo.ToLower().Equals("cadena negrita"))
                        {
                            parr = new Paragraph(listaTokens[i].Lexema.Substring(2, listaTokens[i].Lexema.Length - 3) + "\n", fn2);
                            parr.SetLeading(0, inter);
                            documento.Add(parr);


                        }
                        else if (listaTokens[i].tipo.ToLower().Equals("cadena subrayada"))
                        {
                            parr = new Paragraph(listaTokens[i].Lexema.Substring(2, listaTokens[i].Lexema.Length - 3) + "\n", fn1);
                            parr.SetLeading(0, inter);
                            documento.Add(parr);

                        }
                        else if (listaTokens[i].tipo.ToLower().Equals("cadena"))
                        {
                            parr = new Paragraph(listaTokens[i].Lexema.Substring(1, listaTokens[i].Lexema.Length - 2), fn);
                            parr.SetLeading(0, inter);
                            documento.Add(parr);

                        }

                        i++;
                        token = listaTokens[i].Lexema.ToLower();

                    }


                }


            }

            documento.Close();
            MessageBox.Show("se el pdf se ah creado correctamente");
        }

        public variable buscarParametro(string parametro)
        {

            for (int i = 0; i < listaVariables.Count; i++)
            {

                if (listaVariables[i].id.ToLower().Equals(parametro.ToLower()))
                {

                    return listaVariables[i];

                }



            }
            return null;

        }



    }
}

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

namespace AnalizadorLexico
{
    public partial class Form1 : Form
    {
        private string componente_Acumulado = String.Empty;
        private int ini = 0;
        private Archivo ar = null;

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Metodo que verifica que un caracter sea letra o guion bajo
        /// </summary>
        private bool esLetra(char c)
        {
            return (((c >= 'a') && (c <= 'z')) || ((c >= 'A') && (c <= 'Z')) || (c == '_')) ? true : false;
        }

        /// <summary>
        /// Metodo que verifica que un caracter sea numero
        /// </summary>
        private bool esNumero(char c)
        {
            return ((c >= 0) && (c <= 9)) ? true : false;
        }

        /// <summary>
        /// Metodo que escribe en la tabla de simbolos dependiendo de que componente es
        /// </summary>
        private void escribe(string componente,int identificador)
        {
            //Componente Lexico + Lexema + Valor
            if (identificador == Interaccion.IDENTIFICADOR)
            {
                ComponenteLexico temp = new ComponenteLexico();

                String prueba = temp.esPalabraReservada(componente);
                if (prueba == String.Empty)
                {
                    string mete = "IDENTIFICADOR\t\t" + componente + "\t\t" + componente;
                    ar.appendTextToTabla(mete);
                }
                else
                {
                    string mete = "PALABRA RESERVADA\t\t" + temp + "\t\t" + temp;
                    ar.appendTextToTabla(mete);
                }
            }
            // SUMA
            else if (identificador == Interaccion.SUMA)
            {
                string mete = "OPERADOR ARITMÉTICO\t\t" + componente + "\t\t" + "SUMA";
                ar.appendTextToTabla(mete);
            }
            else if (identificador == Interaccion.SUMA_ASIGNACION)
            {
                string mete = "ASIGNACIÓN\t\t" + componente + "\t\t" + "SUMA-ASIGNACIÓN";
                ar.appendTextToTabla(mete);
            }
            else if (identificador == Interaccion.SUMA_INCREMENTO)
            {
                string mete = "INCREMENTO\t\t" + componente + "\t\t" + "INCREMENTO-POSITIVO";
                ar.appendTextToTabla(mete);
            }
            //RESTA
            else if (identificador == Interaccion.RESTA)
            {
                string mete = "OPERADOR ARITMÉTICO\t\t" + componente + "\t\t" + "RESTA";
                ar.appendTextToTabla(mete);
            }
            else if (identificador == Interaccion.RESTA_ASIGNACION)
            {
                string mete = "ASIGNACIÓN\t\t" + componente + "\t\t" + "RESTA-ASIGNACIÓN";
                ar.appendTextToTabla(mete);
            }
            else if (identificador == Interaccion.RESTA_INCREMENTO)
            {
                string mete = "INCREMENTO\t\t" + componente + "\t\t" + "INCREMENTO-NEGATIVO";
                ar.appendTextToTabla(mete);
            }
            //MULTIPLICACION
            else if (identificador == Interaccion.MULTI)
            {
                string mete = "OPERADOR ARITMÉTICO\t\t" + componente + "\t\t" + "MULTIPLICACIÓN";
                ar.appendTextToTabla(mete);
            }
            else if (identificador == Interaccion.MULTI_ASIGNACION)
            {
                string mete = "ASIGNACIÓN\t\t" + componente + "\t\t" + "MULTIPLICACIÓN-ASIGNACIÓN";
                ar.appendTextToTabla(mete);
            }
            //DIVISION
            else if (identificador == Interaccion.DIVIDE)
            {
                string mete = "OPERADOR ARITMÉTICO\t\t" + componente + "\t\t" + "DIVISIÓN";
                ar.appendTextToTabla(mete);
            }
            else if (identificador == Interaccion.DIVIDE_ASIGNACION)
            {
                string mete = "ASIGNACIÓN\t\t" + componente + "\t\t" + "DIVISIÓN-ASIGNACIÓN";
                ar.appendTextToTabla(mete);
            }
            //MODULO
            else if (identificador == Interaccion.MOD)
            {
                string mete = "OPERADOR ARITMÉTICO\t\t" + componente + "\t\t" + "MÓDULO";
                ar.appendTextToTabla(mete);
            }
            else if (identificador == Interaccion.MOD_ASIGNACION)
            {
                string mete = "ASIGNACIÓN\t\t" + componente + "\t\t" + "MÓDULO-ASIGNACIÓN";
                ar.appendTextToTabla(mete);
            }
            //IGUAL
            else if (identificador == Interaccion.IGUAL_ASIGNACION)
            {
                string mete = "ASIGNACIÓN\t\t" + componente + "\t\t" + "ASIGNACIÓN";
                ar.appendTextToTabla(mete);
            }
            else if (identificador == Interaccion.IGUAL_IGUAL)
            {
                string mete = "OPERADOR RELACIONAL\t\t" + componente + "\t\t" + "IGUAL";
                ar.appendTextToTabla(mete);
            }
            //MENOR
            else if (identificador == Interaccion.MENOR)
            {
                string mete = "OPERADOR RELACIONAL\t\t" + componente + "\t\t" + "MENOR";
                ar.appendTextToTabla(mete);
            }
            else if (identificador == Interaccion.MENOR_IGUAL)
            {
                string mete = "OPERADOR RELACIONAL\t\t" + componente + "\t\t" + "MENOR-IGUAL";
                ar.appendTextToTabla(mete);
            }
            //MAYOR
            else if (identificador == Interaccion.MAYOR)
            {
                string mete = "OPERADOR RELACIONAL\t\t" + componente + "\t\t" + "MAYOR";
                ar.appendTextToTabla(mete);
            }
            else if (identificador == Interaccion.MAYOR_IGUAL)
            {
                string mete = "OPERADOR RELACIONAL\t\t" + componente + "\t\t" + "MAYOR-IGUAL";
                ar.appendTextToTabla(mete);
            }
            // (
            else if (identificador == Interaccion.PARENTESIS_ABIERTO)
            {
                string mete = "SIMBOLO ESPECIAL\t\t" + componente + "\t\t" + "PARENTESIS-ABIERTO";
                ar.appendTextToTabla(mete);
            }
            // )
            else if (identificador == Interaccion.PARENTESIS_CERRADO)
            {
                string mete = "SIMBOLO ESPECIAL\t\t" + componente + "\t\t" + "PARENTESIS-CERRADO";
                ar.appendTextToTabla(mete);
            }
            // }
            else if (identificador == Interaccion.LLAVE_CERRADA)
            {
                string mete = "SIMBOLO ESPECIAL\t\t" + componente + "\t\t" + "LLAVE-CERRADA";
                ar.appendTextToTabla(mete);
            }
            // {
            else if (identificador == Interaccion.LLAVE_ABIERTO)
            {
                string mete = "SIMBOLO ESPECIAL\t\t" + componente + "\t\t" + "LLAVE-ABIERTA";
                ar.appendTextToTabla(mete);
            }
            // [
            else if (identificador == Interaccion.CORCHETE_ABIERTO)
            {
                string mete = "SIMBOLO ESPECIAL\t\t" + componente + "\t\t" + "CORCHETE-ABIERTO";
                ar.appendTextToTabla(mete);
            }
            // ]
            else if (identificador == Interaccion.CORCHETE_CERRADO)
            {
                string mete = "SIMBOLO ESPECIAL\t\t" + componente + "\t\t" + "CORCHETE-CERRADO";
                ar.appendTextToTabla(mete);
            }
            // ;
            else if (identificador == Interaccion.PUNTO_COMA)
            {
                string mete = "SIMBOLO ESPECIAL\t\t" + componente + "\t\t" + "PUNTO-Y-COMA";
                ar.appendTextToTabla(mete);
            }
            // ,
            else if (identificador == Interaccion.COMA)
            {
                string mete = "SIMBOLO ESPECIAL\t\t" + componente + "\t\t" + "COMA";
                ar.appendTextToTabla(mete);
            }
            // .
            else if (identificador == Interaccion.DOS_PUNTOS)
            {
                string mete = "SIMBOLO ESPECIAL\t\t" + componente + "\t\t" + "DOS-PUNTOS";
                ar.appendTextToTabla(mete);
            }
            // #
            else if (identificador == Interaccion.NUMERAL)
            {
                string mete = "SIMBOLO ESPECIAL\t\t" + componente + "\t\t" + "NUMERAL";
                ar.appendTextToTabla(mete);
            }
            // ,
            else if (identificador == Interaccion.PUNTO)
            {
                string mete = "SIMBOLO ESPECIAL\t\t" + componente + "\t\t" + "PUNTO";
                ar.appendTextToTabla(mete);
            }
            // '
            else if (identificador == Interaccion.COMILLA_SENCILLA)
            {
                string mete = "SIMBOLO ESPECIAL\t\t" + componente + "\t\t" + "COMILLA-SENCILLA";
                ar.appendTextToTabla(mete);
            }
            //  "
            else if (identificador == Interaccion.COMILLA_DOBLE)
            {
                string mete = "SIMBOLO ESPECIAL\t\t" + componente + "\t\t" + "COMILLA-DOBLE";
                ar.appendTextToTabla(mete);
            }
            //AND
            else if (identificador == Interaccion.AND_BIT)
            {
                string mete = "OPERADOR BIT\t\t" + componente + "\t\t" + "AND";
                ar.appendTextToTabla(mete);
            }
            else if (identificador == Interaccion.AND_LOGICO)
            {
                string mete = "OPERADOR LÓGICO\t\t" + componente + "\t\t" + "Y";
                ar.appendTextToTabla(mete);
            }
            //OR
            else if (identificador == Interaccion.OR_BIT)
            {
                string mete = "OPERADOR BIT\t\t" + componente + "\t\t" + "OR";
                ar.appendTextToTabla(mete);
            }
            else if (identificador == Interaccion.OR_LOGICO)
            {
                string mete = "OPERADOR LÓGICO\t\t" + componente + "\t\t" + "O";
                ar.appendTextToTabla(mete);
            }
            //!=
            else if (identificador == Interaccion.NEGACION)
            {
                string mete = "OPERADOR LÓGICO\t\t" + componente + "\t\t" + "NEGACIÓN";
                ar.appendTextToTabla(mete);
            }
            else if (identificador == Interaccion.DIFERENTE)
            {
                string mete = "OPERADOR LÓGICO\t\t" + componente + "\t\t" + "DIFERENTE";
                ar.appendTextToTabla(mete);
            }
        }
        /// <summary>
        /// Metodo que analiza caracter a caracter para despues de haberlo clasificado escribirlo en el archivo
        /// </summary>
        private void Clasifica(string linea)
        {
            ComponenteLexico cl = new ComponenteLexico();
            int tam = linea.Length;
            for (int i = 0; i < tam; i++)
            {
                char c = linea[i];
                if (esLetra(c))
                {
                    int j;
                    bool flag = false;
                    for (j = i;esLetra(c) && j<tam && cl.automataIdentificador(c); ++j)
                    {
                        componente_Acumulado += c;
                        if (j + 1 == tam) {
                            flag = true;
                            break;
                        }
                        c = linea[j + 1];

                    }
                    i=j -1;
                    //Mandamos el valor de 4 porque coincide con la interaccion 4 de identificador
                    escribe(componente_Acumulado,4);
                    componente_Acumulado = String.Empty;
                    if (flag)
                        break;
                }
                else if (esNumero(c))
                {

                }
                else
                {
                    //Aqui agregar preguntas si es +,-,/,*,etc.
                    if (c == '+')
                    {
                        int res = Interaccion.SUMA;
                        componente_Acumulado += c;
                        if (i + 1 <= tam - 1) {
                            c = linea[i + 1];
                            res = cl.automataSuma(c);
                            if (res == Interaccion.SUMA_ASIGNACION)
                            {
                                componente_Acumulado += c;
                                i++;
                            }
                            else if (res == Interaccion.SUMA_INCREMENTO)
                            {
                                componente_Acumulado += c;
                                i++;
                            }
                        }
                        //Mandamos el valor de 4 porque coincide con la interaccion 4 de identificador
                        escribe(componente_Acumulado, res);
                        componente_Acumulado = String.Empty;
                    }
                    else if (c == '-')
                    {
                        int res = Interaccion.RESTA;
                        componente_Acumulado += c;
                        if (i + 1 <= tam - 1)
                        {
                            c = linea[i + 1];
                            res = cl.automataResta(c);
                            if (res == Interaccion.RESTA_ASIGNACION)
                            {
                                componente_Acumulado += c;
                                i++;
                            }
                            else if (res == Interaccion.RESTA_INCREMENTO)
                            {
                                componente_Acumulado += c;
                                i++;
                            }
                        }
                        //Mandamos el valor de 4 porque coincide con la interaccion 4 de identificador
                        escribe(componente_Acumulado, res);
                        componente_Acumulado = String.Empty;
                    }
                    else if (c == '*')
                    {
                        int res = Interaccion.MULTI;
                        componente_Acumulado += c;
                        if (i + 1 <= tam - 1)
                        {
                            c = linea[i + 1];
                            res = cl.automataMultiplicacion(c);
                            if (res == Interaccion.MULTI_ASIGNACION)
                            {
                                componente_Acumulado += c;
                                i++;
                            }
                        }
                        //Mandamos el valor de 4 porque coincide con la interaccion 4 de identificador
                        escribe(componente_Acumulado, res);
                        componente_Acumulado = String.Empty;
                    }
                    else if (c == '/')
                    {
                        int res = Interaccion.DIVIDE;
                        componente_Acumulado += c;
                        if (i + 1 <= tam - 1)
                        {
                            c = linea[i + 1];
                            res = cl.automataDivision(c);
                            if (res == Interaccion.DIVIDE_ASIGNACION)
                            {
                                componente_Acumulado += c;
                                i++;
                            }
                        }
                        //Mandamos el valor de 4 porque coincide con la interaccion 4 de identificador
                        escribe(componente_Acumulado, res);
                        componente_Acumulado = String.Empty;
                    }
                    else if (c == '%')
                    {
                        int res = Interaccion.MOD;
                        componente_Acumulado += c;
                        if (i + 1 <= tam - 1)
                        {
                            c = linea[i + 1];
                            res = cl.automataModulo(c);
                            if (res == Interaccion.MOD_ASIGNACION)
                            {
                                componente_Acumulado += c;
                                i++;
                            }
                        }
                        //Mandamos el valor de 4 porque coincide con la interaccion 4 de identificador
                        escribe(componente_Acumulado, res);
                        componente_Acumulado = String.Empty;
                    }
                    else if (c == '=')
                    {
                        int res = Interaccion.IGUAL_ASIGNACION;
                        componente_Acumulado += c;
                        if (i + 1 <= tam - 1)
                        {
                            c = linea[i + 1];
                            res = cl.automataIgual(c);
                            if (res == Interaccion.IGUAL_IGUAL)
                            {
                                componente_Acumulado += c;
                                i++;
                            }
                        }
                        //Mandamos el valor de 4 porque coincide con la interaccion 4 de identificador
                        escribe(componente_Acumulado, res);
                        componente_Acumulado = String.Empty;
                    }
                    else if (c == '<')
                    {
                        int res = Interaccion.MENOR;
                        componente_Acumulado += c;
                        if (i + 1 <= tam - 1)
                        {
                            c = linea[i + 1];
                            res = cl.automataMenor(c);
                            if (res == Interaccion.MENOR_IGUAL)
                            {
                                componente_Acumulado += c;
                                i++;
                            }
                        }
                        //Mandamos el valor de 4 porque coincide con la interaccion 4 de identificador
                        escribe(componente_Acumulado, res);
                        componente_Acumulado = String.Empty;
                    }
                    else if (c == '>')
                    {
                        int res = Interaccion.MAYOR;
                        componente_Acumulado += c;
                        if (i + 1 <= tam - 1)
                        {
                            c = linea[i + 1];
                            res = cl.automataMayor(c);
                            if (res == Interaccion.MAYOR_IGUAL)
                            {
                                componente_Acumulado += c;
                                i++;
                            }
                        }
                        //Mandamos el valor de 4 porque coincide con la interaccion 4 de identificador
                        escribe(componente_Acumulado, res);
                        componente_Acumulado = String.Empty;
                    }
                    else if (c == '(')
                    {
                        componente_Acumulado += c;
                        escribe(componente_Acumulado, Interaccion.PARENTESIS_ABIERTO);
                        componente_Acumulado = String.Empty;
                    }
                    else if (c == ')')
                    {
                        componente_Acumulado += c;
                        escribe(componente_Acumulado, Interaccion.PARENTESIS_CERRADO);
                        componente_Acumulado = String.Empty;
                    }
                    else if (c == '{')
                    {
                        componente_Acumulado += c;
                        escribe(componente_Acumulado, Interaccion.LLAVE_ABIERTO);
                        componente_Acumulado = String.Empty;
                    }
                    else if (c == '}')
                    {
                        componente_Acumulado += c;
                        escribe(componente_Acumulado, Interaccion.LLAVE_CERRADA);
                        componente_Acumulado = String.Empty;
                    }
                    else if (c == '[')
                    {
                        componente_Acumulado += c;
                        escribe(componente_Acumulado, Interaccion.CORCHETE_ABIERTO);
                        componente_Acumulado = String.Empty;
                    }
                    else if (c == ']')
                    {
                        componente_Acumulado += c;
                        escribe(componente_Acumulado, Interaccion.CORCHETE_CERRADO);
                        componente_Acumulado = String.Empty;
                    }
                    else if (c == ';')
                    {
                        componente_Acumulado += c;
                        escribe(componente_Acumulado, Interaccion.PUNTO_COMA);
                        componente_Acumulado = String.Empty;
                    }
                    else if (c == ',')
                    {
                        componente_Acumulado += c;
                        escribe(componente_Acumulado, Interaccion.COMA);
                        componente_Acumulado = String.Empty;
                    }
                    else if (c == '.')
                    {
                        componente_Acumulado += c;
                        escribe(componente_Acumulado, Interaccion.PUNTO);
                        componente_Acumulado = String.Empty;
                    }
                    else if (c == ':')
                    {
                        componente_Acumulado += c;
                        escribe(componente_Acumulado, Interaccion.DOS_PUNTOS);
                        componente_Acumulado = String.Empty;
                    }
                    else if (c == '#')
                    {
                        componente_Acumulado += c;
                        escribe(componente_Acumulado, Interaccion.NUMERAL);
                        componente_Acumulado = String.Empty;
                    }
                    else if (c == '\'')
                    {
                        componente_Acumulado += c;
                        escribe(componente_Acumulado, Interaccion.COMILLA_SENCILLA);
                        componente_Acumulado = String.Empty;
                    }
                    else if (c == '\"')
                    {
                        componente_Acumulado += c;
                        escribe(componente_Acumulado, Interaccion.COMILLA_DOBLE);
                    }
                    else if (c == '&')
                    {
                        int res = Interaccion.AND_BIT;
                        componente_Acumulado += c;
                        if (i + 1 <= tam - 1)
                        {
                            c = linea[i + 1];
                            res = cl.automataAnd(c);
                            if (res == Interaccion.AND_LOGICO)
                            {
                                componente_Acumulado += c;
                                i++;
                            }
                        }
                        //Mandamos el valor de 4 porque coincide con la interaccion 4 de identificador
                        escribe(componente_Acumulado, res);
                        componente_Acumulado = String.Empty;
                    }
                    else if (c == '|')
                    {
                        int res = Interaccion.OR_BIT;
                        componente_Acumulado += c;
                        if (i + 1 <= tam - 1)
                        {
                            c = linea[i + 1];
                            res = cl.automataOr(c);
                            if (res == Interaccion.OR_LOGICO)
                            {
                                componente_Acumulado += c;
                                i++;
                            }
                        }
                        //Mandamos el valor de 4 porque coincide con la interaccion 4 de identificador
                        escribe(componente_Acumulado, res);
                        componente_Acumulado = String.Empty;
                    }
                    else if (c == '!')
                    {
                        int res = Interaccion.NEGACION;
                        componente_Acumulado += c;
                        if (i + 1 <= tam - 1)
                        {
                            c = linea[i + 1];
                            res = cl.automataNegacion(c);
                            if (res == Interaccion.DIFERENTE)
                            {
                                componente_Acumulado += c;
                                i++;
                            }
                        }
                        //Mandamos el valor de 4 porque coincide con la interaccion 4 de identificador
                        escribe(componente_Acumulado, res);
                        componente_Acumulado = String.Empty;
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamReader sr = new StreamReader(openFileDialog1.FileName);

                string tabla = textBoxTabla.Text;
                string errores = textBoxErrores.Text;
                string posibleSimbolo = String.Empty;
                
                ar = new Archivo(tabla, errores);
                ar.creaArchivoTabla();
                ar.creaArchivoErrores();
                
                while (!sr.EndOfStream){
                    string linea = sr.ReadLine();
                    Clasifica(linea);
                }
                
                sr.Close();
            }
        }
    }
}

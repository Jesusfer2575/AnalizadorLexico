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
                string mete = "IDENTIFICADOR\t\t" + componente + "\t\t" +componente;  
                ar.appendTextToTabla(mete);
            }
            else if(identificador == Interaccion.SUMA)
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
                            c = linea[i+1];
                            res = cl.automataSuma(c);
                            if(res == Interaccion.SUMA_ASIGNACION)
                            {
                                componente_Acumulado += c;
                                i++;
                            }
                            else if(res == Interaccion.SUMA_INCREMENTO)
                            {
                                componente_Acumulado += c;
                                i++;
                            }
                        }
                        //Mandamos el valor de 4 porque coincide con la interaccion 4 de identificador
                        escribe(componente_Acumulado, res);
                        componente_Acumulado = String.Empty;
                    }
                    if (c == '-')
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

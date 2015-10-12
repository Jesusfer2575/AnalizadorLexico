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

        private void escribe(string componente,int identificador)
        {
            switch (identificador)
            {
                case 4:

            }
        }

        private void Clasifica(string linea)
        {
            ComponenteLexico cl = new ComponenteLexico();
            int tam = linea.Length;
            for (int i = ini; i < tam; i++)
            {
                char c = linea[i];
                if (esLetra(c))
                {
                    int j;
                    for (j = i;esLetra(c) && cl.automataIdentificador(c);j++)
                    {
                        componente_Acumulado += c;
                    }
                    i = j;
                    escribe(componente_Acumulado,identificador);
                    componente_Acumulado = String.Empty;
                }
                else if (esNumero(c))
                {

                }
                else
                {
                    //Aqui agregar preguntas si es +,-,/,*,etc.
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
                
                Archivo ar = new Archivo(tabla, errores);
                ar.creaArchivoTabla();
                ar.creaArchivoErrores();
                
                while (!sr.EndOfStream){
                    string  linea = sr.ReadLine();
                    int tam = linea.Length;
                    Clasifica(linea);
                }
                
                sr.Close();
            }
        }
    }
}

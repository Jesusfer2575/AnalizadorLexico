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
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int indice_automata = 0;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamReader sr = new StreamReader(openFileDialog1.FileName);

                string tabla = textBoxTabla.Text;
                string errores = textBoxErrores.Text;
                string posibleSimbolo = String.Empty;
                ComponenteLexico cl = new ComponenteLexico();

                Archivo ar = new Archivo(tabla, errores);
                ar.creaArchivoTabla();
                ar.creaArchivoErrores();

                bool flag1 = true;
                while (!sr.EndOfStream){
                    string  linea = sr.ReadLine();
                    int tam = linea.Length;
                    for(int i=0;i<tam;i++)
                    {
                        flag1 = (cl.automataIdentificador(linea[i]) == true) ? true : false;
                        /*if (indice_automata == 0)
                        {
                            
                            if (!flag1)
                                indice_automata += 1;
                        }
                        else if (indice_automata == 1) {
                            flag1 = (cl.automataEntero(linea[i]) == true) ? true : false;
                            if (!flag1)
                                indice_automata += 1;
                        }*/
                        
                    }

                    if (flag1)
                        Console.WriteLine("Cadena Aceptada!");
                    else
                        Console.WriteLine("Cadena Rechazada");
                }
                
                sr.Close();
            }
        }
    }
}

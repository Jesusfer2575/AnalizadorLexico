using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalizadorLexico
{
    class Archivo
    {
        private string path = Directory.GetCurrentDirectory() + "\\" + "..\\..\\Archivos\\";
        private string tablaSimbolos = String.Empty;
        private string archivoErrores = String.Empty;

        /// <summary>
        /// Constructor de la clase Archivo 
        /// </summary>
        public Archivo(string tablaSimbolos,string archivoErrores)
        {
            this.archivoErrores = archivoErrores;
            this.tablaSimbolos = tablaSimbolos;
        }

        /// <summary>
        /// Metodo que verifica la existencia de un archivo 
        /// </summary>
        public bool existeArchivo(string nombre)
        {
            return (File.Exists(path + nombre)) ? true:false;
        }

        /// <summary>
        /// Metodo que crea el Archivo se sugiere preguntar con el metodo
        /// existeArchivo() si ya existe primero 
        /// </summary>
        public void creaArchivoTabla()
        {
            string ruta = path + this.tablaSimbolos + ".txt";
            if (!File.Exists(ruta))
            {
                using (FileStream fs = File.Create(ruta))
                {
                    Byte[] info = new UTF8Encoding(true).GetBytes("");
                    fs.Write(info, 0, info.Length);
                }
            }

        }

        public void creaArchivoErrores()
        {
            string ruta = path + this.archivoErrores + ".txt";
            if (!File.Exists(ruta))
            {
                using (FileStream fs = File.Create(ruta))
                {
                    Byte[] info = new UTF8Encoding(true).GetBytes("");
                    fs.Write(info, 0, info.Length);
                }
            }

        }

        /// <summary>
        /// Metodo que añade texto a la tabla de Simbolos
        /// </summary>
        public void appendTextToTabla(string s)
        {
            StreamWriter sw = File.AppendText(path+tablaSimbolos);
            sw.WriteLine(s);
            sw.Close();
        }

        /// <summary>
        /// Metodo que añade texto al Archivo de Errores
        /// </summary>
        public void appendTextToErrors(string s)
        {
            StreamWriter sw = File.AppendText(path + archivoErrores);
            sw.WriteLine(s);
            sw.Close();
        }
    }
}

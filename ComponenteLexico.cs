using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalizadorLexico
{
    class ComponenteLexico
    {
        private []palabrasReservadas = {"if","else","do","while","switch","case","for","void","int","float","double","char",
                                        "short","long","signed","unsigned","include","define","return","main"};
        private string Lexema = String.Empty;
        private string Componente = String.Empty;
        private string Valor = String.Empty;
        private int estado_Identificador = 1;
        private int estado_And = 1,estado_Or = 1,estado_Not = 1;
        private int estado_Mod = 1;

        /// <summary>
        /// Constructor de la clase ComponenteLexico 
        /// </summary>
        public ComponenteLexico()
        {

        }

        /// <summary>
        /// Metodo que verifica si un identificador es Palabra Reservada
        /// </summary>
        private bool esPalabraReservada(string palabra)
        {
            int tam = palabrasReservadas.Length;
            for(int i=0;i<tam;i++)
            {
                if (palabrasReservadas[i] == palabra) return true;
            }
            return false;
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
        /// Metodo que simula el Automata de un identificador
        /// </summary>
        public bool automataIdentificador(char c)
        {
            switch (estado_Identificador)
            {
                case 1:
                    if (esLetra(c))
                        estado_Identificador = 2;
                    else
                        estado_Identificador = 0;
                    break;
                case 2:
                    if (esNumero(c))
                        estado_Identificador = 4;
                    else if (esLetra(c))
                        estado_Identificador = 3;
                    break;
                case 3:
                    if (esNumero(c))
                        estado_Identificador = 4;
                    else if (esLetra(c))
                        estado_Identificador = 3;
                    break;
                case 4:
                    if (esNumero(c))
                        estado_Identificador = 4;
                    else if (esLetra(c))
                        estado_Identificador = 3;
                    break;
            }
            return (estado_Identificador == 2 || estado_Identificador == 3 || estado_Identificador == 4) ? true : false;
        }

        /// <summary>
        /// Metodo que simula el Automata de un Operador Lógico &&
        /// </summary>
        public bool automataLogicoAnd(char c)
        {
            switch (estado_And)
            {
                case 1:
                    if (c == '&')
                        estado_And = 2;
                    else
                        estado_And = 0;
                    break;
                case 2:
                    if (c == '&')
                        estado_And = 3;
                    else
                        estado_And = 0;
                    break;
            }
            return (estado_And == 3) ? true : false;
        }

        /// <summary>
        /// Metodo que simula el Automata de un Operador Lógico ||
        /// </summary>
        public bool automataLogicoOr(char c)
        {
            switch (estado_Or)
            {
                case 1:
                    if (c == '|')
                        estado_Or = 2;
                    else
                        estado_Or = 0;
                    break;
                case 2:
                    if (c == '|')
                        estado_Or = 3;
                    else
                        estado_Or = 0;
                    break;
            }
            return (estado_Or == 3) ? true : false;
        }

        /// <summary>
        /// Metodo que simula el Automata de un Operador Lógico !
        /// </summary>
        public bool automataLogicoNot(char c)
        {
            switch (estado_Not)
            {
                case 1:
                    if (c == '!')
                        estado_Not = 2;
                    else
                        estado_Not = 0;
                    break;
            }
            return (estado_Not == 2) ? true : false;
        }

        /// <summary>
        /// Metodo que simula el Automata de un Operador Aritmetico %
        /// </summary>
        public bool automataAritmeticoMod(char c)
        {
            switch (estado_Mod)
            {
                case 1:
                    if (c == '%')
                        estado_Mod = 2;
                    else
                        estado_Mod = 0;
                    break;
            }
            return (estado_Mod == 2) ? true : false;
        }
    }
}

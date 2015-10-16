using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalizadorLexico
{
    class ComponenteLexico
    {
        private string []palabrasReservadas = {"if","else","do","while","switch","case","for","void","int","float","double","char",
                                        "short","long","signed","unsigned","include","define","return","main"};
        private string Lexema = String.Empty;
        private string Componente = String.Empty;
        private string Valor = String.Empty;
        private int estado_Identificador = 1;
        private int estado_And = 1,estado_Or = 1,estado_Not = 1;
        private int estado_Mod = 1;
        public int estado_int = 1,estado_float = 1;

        /// <summary>
        /// Constructor de la clase ComponenteLexico
        /// </summary>
        public ComponenteLexico()
        {

        }

        /// <summary>
        /// Metodo que verifica si un identificador es Palabra Reservada
        /// </summary>
        /// <param name="palabra"></param>
        /// <returns></returns>
        public string esPalabraReservada(string palabra)
        {
            int tam = palabrasReservadas.Length;
            for(int i=0;i<tam;i++)
            {
                if (palabrasReservadas[i] == palabra) return palabrasReservadas[i];
            }
            return String.Empty;
        }

        /// <summary>
        /// Metodo que verifica que un caracter sea letra o guion bajo
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        private bool esLetra(char c)
        {
            return (((c >= 'a') && (c <= 'z')) || ((c >= 'A') && (c <= 'Z')) || (c == '_')) ? true : false;
        }

        /// <summary>
        /// Metodo que verifica que un caracter sea numero
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        private bool esNumero(char c)
        {
            return ((c >= '0') && (c <= '9')) ? true : false;
        }

        /// <summary>
        /// Metodo que simula el Automata de un Numero Entero
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public bool automataInt(char c)
        {
            switch (estado_int)
            {
                case 1:
                    if (esNumero(c))
                        estado_int = 2;
                    break;
                case 2:
                    if (esNumero(c))
                        estado_int = 2;
                    break;
            }
            return (estado_int == 2) ? true : false;
        }

        /// <summary>
        /// Metodo que simula el Automata de un Numero Float
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public bool automataFloat(char c)
        {
            switch (estado_float)
            {
                case 1:
                    if (c == '.' && estado_int==2)
                        estado_float = 3;
                    else if(c == '.')
                        estado_float = 2;
                    break;
                case 2:
                    if (esNumero(c))
                        estado_float = 3;
                    break;
                case 3:
                    if (esNumero(c))
                        estado_float = 3;
                    break;
            }
            return (estado_float == 3) ? true : false;
        }

        /// <summary>
        /// Metodo que simula el Automata de un identificador
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
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
        /// <param name="c"></param>
        /// <returns></returns>
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
        /// <param name="c"></param>
        /// <returns></returns>
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
        /// <param name="c"></param>
        /// <returns></returns>
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
        /// <param name="c"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Metodo que simula el Automata de caracter especial (
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public bool caracterParenAbre(char c)
        {
            return (c=='(') ? true:false;
        }

        /// <summary>
        /// Metodo que simula el Automata de caracter especial )
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public bool caracterParenCierra(char c)
        {
            return (c == ')') ? true : false;
        }

        /// <summary>
        /// Metodo que simula el Automata de caracter especial [
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public bool caracterCorchAbre(char c)
        {
            return (c == '[') ? true : false;
        }

        /// <summary>
        /// Metodo que simula el Automata de caracter especial ]
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public bool caracterCorchCierra(char c)
        {
            return (c == ']') ? true : false;
        }

        /// <summary>
        /// Metodo que simula el Automata de caracter especial {
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public bool caracterLlaveAbre(char c)
        {
            return (c == '{') ? true : false;
        }

        /// <summary>
        /// Metodo que simula el Automata de caracter especial }
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public bool caracterLlaveCierra(char c)
        {
            return (c == '}') ? true : false;
        }

        /// <summary>
        /// Metodo que simula el Automata de caracter especial .
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public bool caracterPunto(char c)
        {
            return (c == '.') ? true : false;
        }

        /// <summary>
        /// Metodo que simula el Automata de caracter especial ,
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public bool caracterComa(char c)
        {
            return (c == ',') ? true : false;
        }

        /// <summary>
        /// Metodo que simula el Automata de caracter especial ;
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public bool caracterPuntoComa(char c)
        {
            return (c == ';') ? true : false;
        }

        /// <summary>
        /// Metodo que simula el Automata de caracter especial :
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public bool caracterPuntos(char c)
        {
            return (c == ':') ? true : false;
        }

        /// <summary>
        /// Metodo que simula el Automata de caracter especial #
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public bool caracterGato(char c)
        {
            return (c == '#') ? true : false;
        }

        /// <summary>
        /// Metodo que simula el Automata de caracter especial '
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public bool caracterComilla(char c)
        {
            return (c == '\'') ? true : false;
        }

        /// <summary>
        /// Metodo que simula el Automata de caracter especial "
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public bool caracterComillas(char c)
        {
            return (c == '\"') ? true : false;
        }

        /// <summary>
        /// Metodo que simula el Automata del simbolo + y sus derivados ++ y +=
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public int automataSuma(char c)
        {
            if (c == '=')
                return Interaccion.SUMA_ASIGNACION;
            else if (c == '+')
                return Interaccion.SUMA_INCREMENTO;
            return Interaccion.SUMA;
        }

        /// <summary>
        /// Metodo que simula el Automata del simbolo - y sus derivados -- y -=
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public int automataResta(char c)
        {
            if (c == '=')
                return Interaccion.RESTA_ASIGNACION;
            else if (c == '-')
                return Interaccion.RESTA_INCREMENTO;
            return Interaccion.RESTA;
        }

        /// <summary>
        /// Metodo que simula el Automata del simbolo * y *=
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public int automataMultiplicacion(char c)
        {
            if (c == '=')
                return Interaccion.MULTI_ASIGNACION;
            return Interaccion.MULTI;
        }

        /// <summary>
        /// Metodo que simula el Automata del simbolo / y /=
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public int automataDivision(char c)
        {
            if (c == '=')
                return Interaccion.DIVIDE_ASIGNACION;
            return Interaccion.DIVIDE;
        }

        /// <summary>
        /// Metodo que simula el Automata del simbolo % y %=
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public int automataModulo(char c)
        {
            if (c == '=')
                return Interaccion.MOD_ASIGNACION;
            return Interaccion.MOD;
        }

        /// <summary>
        /// Metodo que simula el Automata del simbolo = y ==
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public int automataIgual(char c)
        {
            if (c == '=')
                return Interaccion.IGUAL_IGUAL;
            return Interaccion.IGUAL_ASIGNACION;
        }

        /// <summary>
        /// Metodo que simula el Automata del simbolo < y <=
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public int automataMenor(char c)
        {
            if (c == '=')
                return Interaccion.MENOR_IGUAL;
            return Interaccion.MENOR;
        }

        /// <summary>
        /// Metodo que simula el Automata del simbolo > y >=
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public int automataMayor(char c)
        {
            if (c == '=')
                return Interaccion.MAYOR_IGUAL;
            return Interaccion.MAYOR;
        }

        /// <summary>
        /// Metodo que simula el Automata del simbolo & y &&
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public int automataAnd(char c)
        {
            if (c == '&')
                return Interaccion.AND_LOGICO;
            return Interaccion.AND_BIT;
        }

        /// <summary>
        /// Metodo que simula el Automata del simbolo | y ||
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public int automataOr(char c)
        {
            if (c == '|')
                return Interaccion.OR_LOGICO;
            return Interaccion.OR_BIT;
        }

        /// <summary>
        /// Metodo que simula el Automata del simbolo ! y !=
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public int automataNegacion(char c)
        {
            if (c == '=')
                return Interaccion.DIFERENTE;
            return Interaccion.NEGACION;
        }
    }
}

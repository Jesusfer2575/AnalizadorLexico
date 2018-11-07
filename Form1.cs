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
        /// <summary>
        /// Atributos de la clase
        /// </summary>
        private string componente_Acumulado = String.Empty;
        private int linea_actual = 0;
        private Archivo ar = null;

        public Form1()
        {
            InitializeComponent();
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
            return (c >= '0' && (c <= '9')) ? true : false;
        }

        /// <summary>
        /// Metodo que identifica que tipo de palabra reservbada es
        /// </summary>
        /// <param name="cad"></param>
        /// <returns></returns>
        private string tipoPalabraReservada(string cad)
        {
            if(cad == "if" || cad == "else" || cad == "switch" || cad == "case")
            {
                return "ESTRUCTURA SELECTIVA";
            }
            else if(cad == "for" || cad == "while" || cad == "do")
            {
                return "ESTRUCTURA REPETITIVA";
            }
            else if (cad == "int" || cad == "float" || cad == "double" || cad == "char" || cad == "void")
            {
                return "TIPO DE DATO";
            }
            else if (cad == "short" || cad == "long" || cad == "signed" || cad == "unsigned")
            {
                return "CALIFICADOR DE TIPO DE DATO";
            }
            else if (cad == "include" || cad == "define" || cad == "inline")
            {
                return "INSTRUCCION DEL PROCESADOR";
            }
            else if (cad == "return")
            {
                return "RETURN";
            }
            else
            {
                return "MAIN";
            }
        }

        /// <summary>
        /// Metodo que escribe en la tabla de simbolos dependiendo de que componente es
        /// </summary>
        /// <param name="componente"></param>
        /// <param name="identificador"></param>
        private void escribe(string componente,int identificador)
        {
            //Componente Lexico + Lexema + Valor
            if (identificador == Interaccion.IDENTIFICADOR)
            {
                ComponenteLexico temp = new ComponenteLexico();

                String prueba = temp.esPalabraReservada(componente);
                if (prueba == String.Empty)
                {
                    string cadena_insertar = "IDENTIFICADOR\t\t" + componente + "\t\t" + componente;
                    ar.appendTextToTabla(cadena_insertar);
                }
                else
                {
                    tipoPalabraReservada(prueba);
                    string cadena_insertar = tipoPalabraReservada(prueba)+"\t\t" + prueba + "\t\t" + prueba.ToUpper();
                    ar.appendTextToTabla(cadena_insertar);
                }
            }
            //ENTERO
            else if (identificador == Interaccion.ENTERO)
            {
                string cadena_insertar = "NÚMERO ENTERO\t\t"+ componente + "\t\t" + componente;
                ar.appendTextToTabla(cadena_insertar);
            }
            //FLOAT
            else if (identificador == Interaccion.FLOTANTE)
            {
                string cadena_insertar = "NÚMERO FLOTANTE\t\t" + componente + "\t\t" + componente;
                ar.appendTextToTabla(cadena_insertar);
            }
            // SUMA
            else if (identificador == Interaccion.SUMA)
            {
                string cadena_insertar = "OPERADOR ARITMÉTICO\t\t" + componente + "\t\t" + "SUMA";
                ar.appendTextToTabla(cadena_insertar);
            }
            else if (identificador == Interaccion.SUMA_ASIGNACION)
            {
                string cadena_insertar = "ASIGNACIÓN\t\t" + componente + "\t\t" + "SUMA-ASIGNACIÓN";
                ar.appendTextToTabla(cadena_insertar);
            }
            else if (identificador == Interaccion.SUMA_INCREMENTO)
            {
                string cadena_insertar = "INCREMENTO\t\t" + componente + "\t\t" + "INCREMENTO-POSITIVO";
                ar.appendTextToTabla(cadena_insertar);
            }
            //RESTA
            else if (identificador == Interaccion.RESTA)
            {
                string cadena_insertar = "OPERADOR ARITMÉTICO\t\t" + componente + "\t\t" + "RESTA";
                ar.appendTextToTabla(cadena_insertar);
            }
            else if (identificador == Interaccion.RESTA_ASIGNACION)
            {
                string cadena_insertar = "ASIGNACIÓN\t\t" + componente + "\t\t" + "RESTA-ASIGNACIÓN";
                ar.appendTextToTabla(cadena_insertar);
            }
            else if (identificador == Interaccion.RESTA_INCREMENTO)
            {
                string cadena_insertar = "INCREMENTO\t\t" + componente + "\t\t" + "INCREMENTO-NEGATIVO";
                ar.appendTextToTabla(cadena_insertar);
            }
            //MULTIPLICACION
            else if (identificador == Interaccion.MULTI)
            {
                string cadena_insertar = "OPERADOR ARITMÉTICO\t\t" + componente + "\t\t" + "MULTIPLICACIÓN";
                ar.appendTextToTabla(cadena_insertar);
            }
            else if (identificador == Interaccion.MULTI_ASIGNACION)
            {
                string cadena_insertar = "ASIGNACIÓN\t\t" + componente + "\t\t" + "MULTIPLICACIÓN-ASIGNACIÓN";
                ar.appendTextToTabla(cadena_insertar);
            }
            //DIVISION
            else if (identificador == Interaccion.DIVIDE)
            {
                string cadena_insertar = "OPERADOR ARITMÉTICO\t\t" + componente + "\t\t" + "DIVISIÓN";
                ar.appendTextToTabla(cadena_insertar);
            }
            else if (identificador == Interaccion.DIVIDE_ASIGNACION)
            {
                string cadena_insertar = "ASIGNACIÓN\t\t" + componente + "\t\t" + "DIVISIÓN-ASIGNACIÓN";
                ar.appendTextToTabla(cadena_insertar);
            }
            //MODULO
            else if (identificador == Interaccion.MOD)
            {
                string cadena_insertar = "OPERADOR ARITMÉTICO\t\t" + componente + "\t\t" + "MÓDULO";
                ar.appendTextToTabla(cadena_insertar);
            }
            else if (identificador == Interaccion.MOD_ASIGNACION)
            {
                string cadena_insertar = "ASIGNACIÓN\t\t" + componente + "\t\t" + "MÓDULO-ASIGNACIÓN";
                ar.appendTextToTabla(cadena_insertar);
            }
            //IGUAL
            else if (identificador == Interaccion.IGUAL_ASIGNACION)
            {
                string cadena_insertar = "ASIGNACIÓN\t\t" + componente + "\t\t" + "ASIGNACIÓN";
                ar.appendTextToTabla(cadena_insertar);
            }
            else if (identificador == Interaccion.IGUAL_IGUAL)
            {
                string cadena_insertar = "OPERADOR RELACIONAL\t\t" + componente + "\t\t" + "IGUAL";
                ar.appendTextToTabla(cadena_insertar);
            }
            //MENOR
            else if (identificador == Interaccion.MENOR)
            {
                string cadena_insertar = "OPERADOR RELACIONAL\t\t" + componente + "\t\t" + "MENOR";
                ar.appendTextToTabla(cadena_insertar);
            }
            else if (identificador == Interaccion.MENOR_IGUAL)
            {
                string cadena_insertar = "OPERADOR RELACIONAL\t\t" + componente + "\t\t" + "MENOR-IGUAL";
                ar.appendTextToTabla(cadena_insertar);
            }
            //MAYOR
            else if (identificador == Interaccion.MAYOR)
            {
                string cadena_insertar = "OPERADOR RELACIONAL\t\t" + componente + "\t\t" + "MAYOR";
                ar.appendTextToTabla(cadena_insertar);
            }
            else if (identificador == Interaccion.MAYOR_IGUAL)
            {
                string cadena_insertar = "OPERADOR RELACIONAL\t\t" + componente + "\t\t" + "MAYOR-IGUAL";
                ar.appendTextToTabla(cadena_insertar);
            }
            // (
            else if (identificador == Interaccion.PARENTESIS_ABIERTO)
            {
                string cadena_insertar = "SIMBOLO ESPECIAL\t\t" + componente + "\t\t" + "PARENTESIS-ABIERTO";
                ar.appendTextToTabla(cadena_insertar);
            }
            // )
            else if (identificador == Interaccion.PARENTESIS_CERRADO)
            {
                string cadena_insertar = "SIMBOLO ESPECIAL\t\t" + componente + "\t\t" + "PARENTESIS-CERRADO";
                ar.appendTextToTabla(cadena_insertar);
            }
            // }
            else if (identificador == Interaccion.LLAVE_CERRADA)
            {
                string cadena_insertar = "SIMBOLO ESPECIAL\t\t" + componente + "\t\t" + "LLAVE-CERRADA";
                ar.appendTextToTabla(cadena_insertar);
            }
            // {
            else if (identificador == Interaccion.LLAVE_ABIERTO)
            {
                string cadena_insertar = "SIMBOLO ESPECIAL\t\t" + componente + "\t\t" + "LLAVE-ABIERTA";
                ar.appendTextToTabla(cadena_insertar);
            }
            // [
            else if (identificador == Interaccion.CORCHETE_ABIERTO)
            {
                string cadena_insertar = "SIMBOLO ESPECIAL\t\t" + componente + "\t\t" + "CORCHETE-ABIERTO";
                ar.appendTextToTabla(cadena_insertar);
            }
            // ]
            else if (identificador == Interaccion.CORCHETE_CERRADO)
            {
                string cadena_insertar = "SIMBOLO ESPECIAL\t\t" + componente + "\t\t" + "CORCHETE-CERRADO";
                ar.appendTextToTabla(cadena_insertar);
            }
            // ;
            else if (identificador == Interaccion.PUNTO_COMA)
            {
                string cadena_insertar = "SIMBOLO ESPECIAL\t\t" + componente + "\t\t" + "PUNTO-Y-COMA";
                ar.appendTextToTabla(cadena_insertar);
            }
            // ,
            else if (identificador == Interaccion.COMA)
            {
                string cadena_insertar = "SIMBOLO ESPECIAL\t\t" + componente + "\t\t" + "COMA";
                ar.appendTextToTabla(cadena_insertar);
            }
            // :
            else if (identificador == Interaccion.DOS_PUNTOS)
            {
                string cadena_insertar = "SIMBOLO ESPECIAL\t\t" + componente + "\t\t" + "DOS-PUNTOS";
                ar.appendTextToTabla(cadena_insertar);
            }
            // #
            else if (identificador == Interaccion.NUMERAL)
            {
                string cadena_insertar = "SIMBOLO ESPECIAL\t\t" + componente + "\t\t" + "NUMERAL";
                ar.appendTextToTabla(cadena_insertar);
            }
            // .
            else if (identificador == Interaccion.PUNTO)
            {
                string cadena_insertar = "SIMBOLO ESPECIAL\t\t" + componente + "\t\t" + "PUNTO";
                ar.appendTextToTabla(cadena_insertar);
            }
            // '
            else if (identificador == Interaccion.COMILLA_SENCILLA)
            {
                string cadena_insertar = "SIMBOLO ESPECIAL\t\t" + componente + "\t\t" + "COMILLA-SENCILLA";
                ar.appendTextToTabla(cadena_insertar);
            }
            //  "
            else if (identificador == Interaccion.COMILLA_DOBLE)
            {
                string cadena_insertar = "SIMBOLO ESPECIAL\t\t" + componente + "\t\t" + "COMILLA-DOBLE";
                ar.appendTextToTabla(cadena_insertar);
            }
            //AND
            else if (identificador == Interaccion.AND_BIT)
            {
                string cadena_insertar = "OPERADOR BIT\t\t" + componente + "\t\t" + "AND";
                ar.appendTextToTabla(cadena_insertar);
            }
            else if (identificador == Interaccion.AND_LOGICO)
            {
                string cadena_insertar = "OPERADOR LÓGICO\t\t" + componente + "\t\t" + "Y";
                ar.appendTextToTabla(cadena_insertar);
            }
            //OR
            else if (identificador == Interaccion.OR_BIT)
            {
                string cadena_insertar = "OPERADOR BIT\t\t" + componente + "\t\t" + "OR";
                ar.appendTextToTabla(cadena_insertar);
            }
            else if (identificador == Interaccion.OR_LOGICO)
            {
                string cadena_insertar = "OPERADOR LÓGICO\t\t" + componente + "\t\t" + "O";
                ar.appendTextToTabla(cadena_insertar);
            }
            //!=
            else if (identificador == Interaccion.NEGACION)
            {
                string cadena_insertar = "OPERADOR LÓGICO\t\t" + componente + "\t\t" + "NEGACIÓN";
                ar.appendTextToTabla(cadena_insertar);
            }
            else if (identificador == Interaccion.DIFERENTE)
            {
                string cadena_insertar = "OPERADOR LÓGICO\t\t" + componente + "\t\t" + "DIFERENTE";
                ar.appendTextToTabla(cadena_insertar);
            }
        }

        /// <summary>
        /// Metodo que analiza caracter a caracter para despues de haberlo clasificado escribirlo en el archivo
        /// </summary>
        /// <param name="linea"></param>
        /// <param name="num_linea"></param>
        private void Clasifica(string linea,int num_linea)
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
                    for (j = i;(esLetra(c) || esNumero(c)) && j<tam && cl.automataIdentificador(c); ++j)
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
                    escribe(componente_Acumulado,Interaccion.IDENTIFICADOR);
                    componente_Acumulado = String.Empty;
                    if (flag)
                        break;
                }
                else if (esNumero(c) || c == '.')
                {
                    cl.estado_int = 1;
                    cl.estado_float = 1;
                    int j;
                    bool flag = false;
                    for (j = i; esNumero(c) && j < tam && cl.automataInt(c); ++j)
                    {
                        componente_Acumulado += c;
                        if (j + 1 == tam)
                        {
                            flag = true;
                            break;
                        }
                        c = linea[j + 1];
                    }
                    if (c == '.')
                    {
                        cl.automataFloat(c);
                        componente_Acumulado += c;
                        if (j + 1 < tam)
                        {
                            c = linea[j + 1];
                        }
                        for (j=j+1; esNumero(c) && j < tam && cl.automataFloat(c); ++j)
                        {
                            componente_Acumulado += c;
                            if (j + 1 == tam)
                            {
                                flag = true;
                                break;
                            }
                            c = linea[j + 1];
                        }
                    }  
                    if(cl.estado_float == 3)
                        escribe(componente_Acumulado, Interaccion.FLOTANTE);
                    else if (cl.estado_int == 2)
                        escribe(componente_Acumulado, Interaccion.ENTERO);
                    else
                    {
                        escribe(componente_Acumulado, Interaccion.PUNTO);
                    }
                    i = j - 1;
                    //Mandamos el valor de 4 porque coincide con la interaccion 4 de identificador
                    componente_Acumulado = String.Empty;
                    if (flag)
                        break;
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
                        componente_Acumulado = String.Empty;
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
                    else if(c!=' ' && c !='\n' && c != '\t')
                    {
                        ar.appendTextToErrors(c.ToString(),num_linea);
                    }
                }
            }
        }

        /// <summary>
        /// Método que se ejecuta al darle clic al botón y lanza el programa
        /// Analiza linea a linea 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamReader sr = new StreamReader(openFileDialog1.FileName);

                string tabla = textBoxTabla.Text;
                string errores = textBoxErrores.Text;
                string posibleSimbolo = String.Empty;
                
                ar = new Archivo(tabla, errores);
                //ar.creaArchivoTabla();
                //ar.creaArchivoErrores();
                
                while (!sr.EndOfStream){
                    this.linea_actual++;
                    string linea = sr.ReadLine();
                    Clasifica(linea,linea_actual);
                }
                
                sr.Close();
            }
            this.Close();
        }
    }
}

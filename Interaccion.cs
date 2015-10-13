using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalizadorLexico
{
    public class Interaccion
    {
        public static int SUMA = 1;
        public static int SUMA_ASIGNACION = 2;
        public static int SUMA_INCREMENTO = 3;
        public static int IDENTIFICADOR = 4;
        public static int ENTERO = 5;
        public static int FLOTANTE = 6;
        public static int RESTA = 7;
        public static int RESTA_ASIGNACION = 8;
        public static int RESTA_INCREMENTO = 9;
        public static int MULTI = 10;
        public static int MULTI_ASIGNACION = 11;
        public static int DIVIDE = 12;
        public static int DIVIDE_ASIGNACION = 13;
        public static int MOD = 14;
        public static int MOD_ASIGNACION = 15;
        public static int IGUAL_IGUAL = 16;
        public static int IGUAL_ASIGNACION = 17;
        public static int MENOR = 18;
        public static int MENOR_IGUAL = 19;
        public static int MAYOR = 20;
        public static int MAYOR_IGUAL = 21;
<<<<<<< HEAD
        public static int PARENTESIS_ABIERTO = 28;
        public static int PARENTESIS_CERRADO = 29;
        public static int LLAVE_ABIERTO = 30;
        public static int LLAVE_CERRADA = 31;
        public static int CORCHETE_ABIERTO = 32;
        public static int CORCHETE_CERRADO = 33;
        public static int PUNTO_COMA = 34;
        public static int COMA = 35;
        public static int PUNTO = 36;
        public static int DOS_PUNTOS = 37;
        public static int NUMERAL = 38;
        public static int COMILLA_SENCILLA = 39;
        public static int COMILLA_DOBLE = 40;
=======
        public static int AND_LOGICO = 22;
        public static int AND_BIT = 23;
        public static int OR_LOGICO = 24;
        public static int OR_BIT = 25;
        public static int NEGACION = 26;
        public static int DIFERENTE = 27;
>>>>>>> 96918e8934eff671f2004759e224e0dcfd29303f

    }
}

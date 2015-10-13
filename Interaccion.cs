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

        public static int DIVIDE = 12;
        public static int DIVIDE_ASIGNACION = 13;
        
    }
}

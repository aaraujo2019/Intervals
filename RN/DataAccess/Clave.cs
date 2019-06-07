
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RN
{
    public static class Clave
    {
        public static string Encriptar(this string _cadenaAencriptar)
        {
            string result = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(_cadenaAencriptar);
            result = Convert.ToBase64String(encryted);
            return result;
        }

        /// Esta función desencripta la cadena que le envíamos en el parámentro de entrada.
        public static string DesEncriptar(this Byte[] _cadenaAdesencriptar)
        {
            string result = string.Empty;
            byte[] decryted = _cadenaAdesencriptar;
            //result = Encoding.ASCII.GetChars(decryted).ToString();
            result = System.Text.Encoding.Unicode.GetString(decryted);
            return result;
        }

    }
}

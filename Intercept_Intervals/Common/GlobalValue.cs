using Entities.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intervals.Common
{
    public class GlobalValue
    {
        public static string Dominio = "grancolombia.local";
        //Aquí va el path URL del servicio de directorio LDAP

        public static string path = "LDAP://grancolombia.local/DC=grancolombia, DC=local";

        public static List<Rol_Permission> Permissions;

    }
}

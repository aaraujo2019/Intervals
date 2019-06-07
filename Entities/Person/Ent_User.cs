using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Person
{
   public class Ent_User
    {
        public int id_User { get; set; }
        public string nombre_User { get; set; }
        public string login_User { get; set; }
        public string passwd_User { get; set; }
        public bool admin_user { get; set; }
        public int idGrupo_User { get; set; }
        public bool activo_User { get; set; }
        public string Project { get; set; }
        public int id_Grupo { get; set; }
        public string nombre_Grupo { get; set; }
        public string code   { get; set; }

    }
}

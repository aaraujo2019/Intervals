using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Person
{
  public  class Rol_Permission
    {
        public string Name { get; set; }
        public string RoleName { get; set; }
        public string fkpage { get; set; }
        public string fkcontrolid { get; set; }
        public int Invisible { get; set; }
        public int Disabled { get; set; }
        public int ContenedorPeqMineria { get; set; }
        public int ContenedorZandor { get; set; }
        public int ContenedorOtros { get; set; }

    }
}

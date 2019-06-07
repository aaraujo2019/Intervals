using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Topography
{
public   class MasterTopography
    {
        /// <summary>
        /// Codigo que ingresa para el maestro
        /// </summary>
       public string Code { get; set; }
        /// <summary>
        /// Descripcion asociada al còdigo
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Còdigo generico del maestro al cual se hace referencia en base de datos
        /// </summary>
        public string CodeMaster { get; set; }
        /// <summary>
        /// Tipo generico de tipo de fente asignado por el Topografi
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Tipo de condiciòn para agregar al maestro, definicion del topografo
        /// </summary>
        public string ConditionId { get; set; }
        /// <summary>
        /// Comentarios asociados al maestro IdSelection
        /// </summary>
        public string Comment { get; set; }
        
        //p
    }
}

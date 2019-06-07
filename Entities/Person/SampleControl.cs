using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Person
{
public    class SampleControl
    {
        public int SKInvSamplesControls { get; set; }
        public int? Id_User { get; set; }
        public string nombre_User { get; set; }
        public DateTime Date_Assign { get; set; }
        public string FromAssign { get; set; }

        public string SampleFrom { get; set; }

        public string ToAssign { get; set; }

        public string SampleTo { get; set; }


        public long SKInvSamples { get; set; }
        public string Sample { get; set; }
        public bool? Used { get; set; }
        public string Type { get; set; }


        public string Code { get; set; }

        public string Description { get; set; }

    }
}

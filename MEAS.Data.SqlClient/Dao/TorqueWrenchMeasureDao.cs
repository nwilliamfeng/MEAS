using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace MEAS.Data
{
    [Table("TorqueWrenchMeasures")]
    [Serializable]
    public class TorqueWrenchMeasureDao : MeasureDaoBase
    {
     
        public virtual string Data { get; set; }

        public virtual TorqueWrench Measurand { get; set; }

        public virtual TorqueStandard Standard { get; set; }

        public DateTime AcceptTime { get; set; }
    }
}

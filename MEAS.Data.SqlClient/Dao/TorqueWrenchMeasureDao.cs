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
         public virtual TorqueWrenchMeasureSettingDao Setting { get; set; } 

    }
}

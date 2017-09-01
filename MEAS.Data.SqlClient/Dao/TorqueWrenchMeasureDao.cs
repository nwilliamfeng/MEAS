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
        public override string ToString()
        {
            return string.Format("{0}:{1}:{2}", this.Id, this.TestCode, this.TestDate);
        }

      

    }
}

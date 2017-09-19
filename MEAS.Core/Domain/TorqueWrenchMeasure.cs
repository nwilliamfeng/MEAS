using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAS
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class TorqueWrenchMeasure : MeasureBase
    {
        /// <summary>
        /// 测试数据
        /// </summary>
        public class TorqueWrenchMeasureData
        {
            public TorqueWrenchMeasureData()
            {
                this.GagingPoints = new List<TorqueMeasurePoint>(); 
            }

            public double ZeroPoint { get; set; }

            public IList<TorqueMeasurePoint> GagingPoints { get; set; }
        }

       

        public TorqueWrenchMeasure()
        {
            this.Data = new TorqueWrenchMeasureData();
        }

        public TorqueWrench Measurand { get; set; }

        public TorqueStandard Standard { get; set; }


        public TorqueWrenchMeasureData Data { get; private set; }

    }
}

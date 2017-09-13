using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MEAS.Data
{
    [Serializable]
    public class MeasureDaoBase : DaoBase
    {
        [Column(Order = 1)]
        [MaxLength(12)]
        public string TestCode { get; set; }


        [Required]
        [Column(Order = 2)]
        public string Tester { get; set; }


        [Column(Order = 3)]
        public string Checker { get; set; }

        [Column(Order = 4)]
        public virtual Environment Environment { get; set; }
    }
}

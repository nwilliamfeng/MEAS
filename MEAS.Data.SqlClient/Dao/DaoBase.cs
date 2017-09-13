using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MEAS.Data
{
    public abstract class DaoBase
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order =0)]
        public int Id { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (this.GetType() != obj.GetType())
                return false;

            return this.Id == (obj as DaoBase).Id;
        }

        [Timestamp]
        [Column(Order = int.MaxValue)]
        public byte[] Timestamp { get; set; }

        public override int GetHashCode()
        {
            return  this.Id.GetHashCode()*37+13;
        }
    }
}

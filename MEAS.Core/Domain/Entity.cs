using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAS
{
    public abstract class Entity:IEntity
    {
        public int Id { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (this.GetType() != obj.GetType())
                return false;

            return this.Id == (obj as Entity).Id;
        }

        public byte[] Timestamp { get; set; }

        public override int GetHashCode()
        {
            return  this.Id.GetHashCode();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAS
{
    public interface IEntity
    {
        int Id { get; set; }

        byte[] Timestamp { get; set; }
    }
}

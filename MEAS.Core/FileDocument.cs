using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAS
{
    public sealed class FileDocument :Entity
    {
        public string FileName { get; set; }

        public string ContentType { get; set; }

        public byte[] Data { get; set; }
    }
}

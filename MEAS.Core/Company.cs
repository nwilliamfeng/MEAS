﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAS
{
    [Serializable]
    public abstract class Company:Entity
    {
        public string Name { get; set; }
    }
}

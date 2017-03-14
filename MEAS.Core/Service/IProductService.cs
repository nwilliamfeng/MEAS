﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAS.Service
{
    public interface IProductService
    {
        

        Task<IEnumerable<Product>> FindWithCategory(string category);
    }
}

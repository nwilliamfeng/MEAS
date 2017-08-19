﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MEAS.Data
{
    public interface ITorqueWrenchMeasureRepository
    {

        Task<IEnumerable<TorqueWrenchMeasureDao>> FindWithCode(string code);

        Task<bool> Delete(int id);

        Task<TorqueWrenchMeasureDao> FindWithId(int id);

        Task<SearchResult<TorqueWrenchMeasureDao>> Find(DateTime start, DateTime end, int pagesize = 3, int pageidx = 0);
    }
}
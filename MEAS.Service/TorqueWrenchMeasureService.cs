﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEAS.Data;
using AutoMapper;

namespace MEAS.Service
{
    public class TorqueWrenchMeasureService : ITorqueWrenchMeasureService
    {
        private ITorqueWrenchMeasureRepository _testRepository;

        public TorqueWrenchMeasureService(ITorqueWrenchMeasureRepository testRepository)
        {
            this._testRepository = testRepository;
        }

        public async Task<bool> Delete(int id)
        {
            return await this._testRepository.Delete(id);
        }

        public async Task<SearchResult<TorqueWrenchMeasure>> Find(DateTime start, DateTime end, int pagesize = 3, int pageIdx = 0)
        {
            var sr =await this._testRepository.Find(start,end,pagesize,pageIdx);
            return new SearchResult<TorqueWrenchMeasure>(sr.Data.Select(x => Mapper.Map<TorqueWrenchMeasure>(x)), sr.TotalCount);
        }

        public async Task<IEnumerable<TorqueWrenchMeasure>> FindWithCode(string code)
        {
            var daos= await this._testRepository.FindWithCode(code);
            return daos.Select(x => Mapper.Map<TorqueWrenchMeasure>(x));
        }

        public async Task<TorqueWrenchMeasure> FindWithId(int id)
        {
            var dao = await this._testRepository.FindWithId(id);
            return Mapper.Map<TorqueWrenchMeasure>(dao);
        }
    }
}
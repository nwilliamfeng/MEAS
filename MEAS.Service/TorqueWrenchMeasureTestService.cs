using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEAS.Data;
using AutoMapper;

namespace MEAS.Service
{
    public class TorqueWrenchMeasureTestService : ITorqueWrenchMeasureTestService
    {
        private ITorqueWrenchMeasureTestRepository _testRepository;

        public TorqueWrenchMeasureTestService(ITorqueWrenchMeasureTestRepository testRepository)
        {
            this._testRepository = testRepository;
        }

        public async Task<bool> Delete(int id)
        {
            return await this._testRepository.Delete(id);
        }

        public async Task<IEnumerable<TorqueWrenchMeasureTest>> FindWithCode(string code)
        {
            var daos= await this._testRepository.FindWithCode(code);
            return daos.Select(x => Mapper.Map<TorqueWrenchMeasureTest>(x));
        }

        public async Task<TorqueWrenchMeasureTest> FindWithId(int id)
        {
            var dao = await this._testRepository.FindWithId(id);
            return Mapper.Map<TorqueWrenchMeasureTest>(dao);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAS.Service
{
    public interface ITorqueWrenchMeasureTestService
    {
        Task<IEnumerable<TorqueWrenchMeasureTest>> FindWithCode(string code);

        Task<bool> Delete(int id);

        Task<TorqueWrenchMeasureTest> FindWithId(int id);
    }
}

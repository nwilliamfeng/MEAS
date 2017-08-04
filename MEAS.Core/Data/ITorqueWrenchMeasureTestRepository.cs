using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAS.Data
{
    public interface ITorqueWrenchMeasureTestRepository
    {

        Task<IEnumerable<TorqueWrenchMeasureTestDao>> FindWithCode(string code);

        Task<bool> Delete(int id);

        Task<TorqueWrenchMeasureTestDao> FindWithId(int id);
    }
}

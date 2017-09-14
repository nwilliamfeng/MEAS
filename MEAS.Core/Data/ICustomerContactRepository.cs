using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAS.Data
{
    public interface ICustomerContactRepository
    {
        Task<bool> Add(CustomerContact contact);

        Task<bool> Update(CustomerContact contact);

        Task<bool> Remove(int id);

        Task<SearchResult<CustomerContact>> Find(string name,int pageSize=5,int pageNumber=0);

        Task<CustomerContact> Find(int id);
    }
}

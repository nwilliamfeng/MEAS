using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAS.Data
{
    public interface ICustomerRepository
    {
        Task<bool> Add(Customer contact);

        Task<bool> Update(Customer  contact);

        Task<bool> Remove(int id);

        Task<SearchResult<Customer>> Find(string name,int pageSize=5,int pageNumber=0);

        Task<Customer> Find(int id);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAS.Data.SqlServer
{
    public class ProductRepository : IProductRepository
    {
        private List<Product> lst = new List<Product>();

        public ProductRepository()
        {
            lst.Add(new Product {Id=1,  Category="Chess" , Name="Name1_Chess", Model="Model1_Chess",Price=23.25m});
            lst.Add(new Product { Id = 2, Category = "Chess", Name = "Name2_Chess", Model = "Model2_Chess", Price = 55.03m });
            lst.Add(new Product { Id = 3, Category = "Chess", Name = "Name3_Chess", Model = "Model3_Chess", Price = 63.89m });
            lst.Add(new Product { Id = 4, Category = "Chess", Name = "Name4_Chess", Model = "Model4_Chess", Price = 450.2m });
            lst.Add(new Product { Id = 5, Category = "Soccer", Name = "Name1_Soccer", Model = "Model1_Soccer", Price = 120.65m });
            lst.Add(new Product { Id = 6, Category = "Soccer", Name = "Name2_Soccer", Model = "Model2_Soccer", Price = 211.0m });
            lst.Add(new Product { Id = 7, Category = "Soccer", Name = "Name3_Soccer", Model = "Model3_Soccer", Price = 699.99m });
            lst.Add(new Product { Id = 8, Category = "Watersports", Name = "Name1_Watersports", Model = "Model1_Watersports", Price = 99.99m });
            lst.Add(new Product { Id = 9, Category = "Watersports", Name = "Name2_Watersports", Model = "Model2_Watersports", Price = 299.09m });
            lst.Add(new Product { Id = 10, Category = "Watersports", Name = "Name3_Watersports", Model = "Model3_Watersports", Price = 199.99m });
        }

        public Task<IEnumerable<Product>> FindWithCategory(string category)
        {
            return Task.Run(() =>
            {
                return lst.Where(x => x.Category == category);
            });
        }

        public Task<Product> FindWithId(int productId)
        {
            return Task.Run(() =>
            {
                return lst.FirstOrDefault(x => x.Id == productId);
            });
        }
    }
}

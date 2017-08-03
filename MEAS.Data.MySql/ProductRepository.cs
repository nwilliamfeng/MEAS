using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAS.Data.MySql
{
    public class ProductRepository : IProductRepository
    {
        private static List<Product> lst  ;

        public ProductRepository()
        {
            if (lst == null)
            {
                lst = new List<Product>();
                lst.Add(new Product { Id = 1, Category = "Chess", Name = "Name1_Chess", Model = "Model1_Chess", Price = 23.25m });
                lst.Add(new Product { Id = 2, Category = "Chess", Name = "Name2_Chess", Model = "Model2_Chess", Price = 55.03m });
                lst.Add(new Product { Id = 3, Category = "Chess", Name = "Name3_Chess", Model = "Model3_Chess", Price = 63.89m });
                lst.Add(new Product { Id = 4, Category = "Chess", Name = "Name4_Chess", Model = "Model4_Chess", Price = 450.2m });
                lst.Add(new Product { Id = 5, Category = "Soccer", Name = "Name1_Soccer", Model = "Model1_Soccer", Price = 120.65m });
                lst.Add(new Product { Id = 6, Category = "Soccer", Name = "Name2_Soccer", Model = "Model2_Soccer", Price = 211.0m });
                lst.Add(new Product { Id = 7, Category = "Soccer", Name = "Name3_Soccer", Model = "Model3_Soccer", Price = 699.99m });
                lst.Add(new Product { Id = 8, Category = "Watersports", Name = "Name1_Watersports", Model = "Model1_Watersports", Price = 99.99m });
                lst.Add(new Product { Id = 9, Category = "Watersports", Name = "Name2_Watersports", Model = "Model2_Watersports", Price = 299.09m });
                lst.Add(new Product { Id = 10, Category = "Watersports", Name = "Name3_Watersports", Model = "Model3_Watersports", Price = 199.99m });
                for (int i = 0; i < 10; i++)
                    lst.Add(new Product { Id = 11 + i, Category = "Chess", Name = "Name" + i.ToString() + "_Chess", Model = "Model1_Chess", Price = i * 10m });
                for (int i = 0; i < 10; i++)
                    lst.Add(new Product { Id = 30 + i, Category = "Soccer", Name = "Name" + i.ToString() + "_Soccer", Model = "Model1_Soccer", Price = i * 5m });
                for (int i = 0; i < 10; i++)
                    lst.Add(new Product { Id = 40 + i, Category = "Watersports", Name = "Watersports" + i.ToString() + "_Watersports", Model = "Model1_Watersports", Price = i * 12m });
            };
        }

        public Task<bool> Delete(int id)
        {
            return Task.Run(() =>
            {
                var old = lst.FirstOrDefault(x => x.Id == id);
                if (old == null)
                    return false;
                lst.Remove(old);
                return true;
            });
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

        public  Task<IEnumerable<Product>> LoadAll()
        {
            return Task.Run<IEnumerable<Product>>(() =>
            {
                return lst;
            });
        }
    }
}

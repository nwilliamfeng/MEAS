using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dapper;
using MEAS.Data.MySql;

namespace MEAS.Tests
{
    public class Category:Entity
    {
     

        public string Title { get; set; }

        public IList<Article> Articles { get; set; }

        public void Dump()
        {
               Console.WriteLine("id "+this.Id +" category title "+Title);
            foreach (var a in Articles)
                Console.WriteLine("article : "+a.Id +"article title :"+a.Title);
        }

    }

    public class Article:Entity
    {
        

        public string Title { get; set; }
 
    }

    public class CategoryRepository
    {
        private const string connstr = @"Data Source=DESKTOP-07EN549\LVB;Initial Catalog=testdb;MultipleActiveResultSets=true;User ID=sa;Password=2004v704";
        public IEnumerable<Category> LoadAll()
        {
            var conn = new SqlConnection(connstr);
            //var lookup = new Dictionary<int, Category>();
            //conn.Query<Category, Article, Category>(@"
            //        SELECT c.*, a.Id,a.Title
            //        FROM Categorys  c
            //        JOIN Articles a ON c.Id = a.CategoryId                    
            //        ", (a, b) =>
            //    {
            //        Category category;
            //        if (!lookup.TryGetValue(a.Id, out category))
            //            lookup.Add(a.Id, category = a);
            //        if (category.Articles == null)
            //            category.Articles = new List<Article>();
            //        (category.Articles as IList<Article>).Add(b);
            //        return category;
            //    }).AsQueryable();
            //var resultList = lookup.Values;
            //return resultList;
   
            return conn.QueryOneToMany<Category, Article>("select c.*,a.Id,a.Title from categorys c join articles a on c.Id=a.CategoryId", (a, b) =>
            {
                if (a.Articles == null)
                    a.Articles = new List<Article>();
                a.Articles.Add(b);
            });
        }

       


    }



    
}

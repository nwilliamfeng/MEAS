using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MEAS.Data;
using MEAS.Data.MySql;
using System.Diagnostics;

namespace MEAS.Tests.Data
{
    [TestClass]
    public class CategoryTableTest
    {
        [TestMethod]
        public void  TestAppendMeasure()
        {
            CategoryRepository cr = new Tests.CategoryRepository();
           var results= cr.LoadAll();
            foreach (var d in results)
                d.Dump();
        }



        [TestMethod]
        public void TestFindWithId2()
        {
            TestDb td = new Data.TestDb();
            td.DoTest6();
        }

        [TestMethod]
        public void TestInsert()
        {
            CategoryRepository cr = new Tests.CategoryRepository();
            Category category = new Tests.Category();
            category.Title = "new category";
            Article article = new Article();
            article.Id = 15;
            article.Title = "new article";
            article.Text = "new text";
            category.Articles = new List<Article>();
            category.Articles.Add(article);
            cr.Insert(category);
        }



    }
}

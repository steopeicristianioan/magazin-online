using generics.NETFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.model;
using Store.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    [TestClass]
    public class Product_CategoryRepositoryTest
    {
        [TestMethod]
        public void testGetAll()
        {
            Product_CategoryRepository repository = new Product_CategoryRepository();
            List<Product_Category> all = repository.AllProductCategories;

            Assert.AreEqual(26, all.Count);
        }
        [TestMethod]
        public void testGetByCategoryID()
        {
            Product_CategoryRepository repository = new Product_CategoryRepository();

            Assert.AreEqual(3, repository.getProductCategoriesByCategory(16).Count);
            Assert.AreEqual(0, repository.getProductCategoriesByCategory(210).Count);
        }
        [TestMethod]
        public void testGroupByCategories()
        {
            Product_CategoryRepository repository = new Product_CategoryRepository();
            CategoryRepository categoryRepository = new CategoryRepository();
            ChainedHashtable<Category, List<Product>> hashtable = repository.groupByCategories(categoryRepository);
            
            Assert.AreEqual(3, hashtable.get(new Category(16, "aaaaa")).Count);
            Assert.AreEqual(null, hashtable.get(new Category(45, "aaaaa")));
            Assert.AreEqual(null, hashtable.get(new Category(16, "baab")));
        }
    }
}

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
    public class CategoryRepositoryTest
    {
        [TestMethod]
        public void testGetAllCategories()
        {
            CategoryRepository repository = new CategoryRepository();
            List<Category> categories = repository.AllCategories;
            Assert.AreEqual(9, categories.Count);
            Assert.AreEqual("Tech", categories[0].Name);
            Assert.AreEqual("Samsung Products", categories[categories.Count - 1].Name);
        }
        [TestMethod]
        public void testGetById()
        {
            CategoryRepository repository = new CategoryRepository();
            Assert.AreEqual(new Category(13, ""), repository.getById(13));
            Assert.AreEqual(new Category(-1, ""), repository.getById(30));
            Assert.AreEqual("Sport", repository.getById(16).Name);
        }
    }
}

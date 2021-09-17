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
    public class ProductRepositoryTest
    {
        [TestMethod]
        public void testGetAllProducts()
        {
            ProductRepository repository = new ProductRepository();
            List<Product> products = repository.AllProducts;

            Assert.AreEqual(9, products.Count);
            Assert.AreEqual("Apple Iphone XR", products[4].Name);
            Assert.AreEqual(300, products[3].Stock);
        }
        [TestMethod]
        public void testGetById()
        {
            ProductRepository product = new ProductRepository();

            Assert.AreEqual(-1, product.getById(30).ID);
            Assert.AreEqual(5, product.getById(5).ID);
            Assert.AreEqual(300, product.getById(8).Stock);
        }
    }
}

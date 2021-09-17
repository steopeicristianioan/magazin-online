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
    public class OrderDetailRepositoryTest
    {
        [TestMethod]
        public void getAllOrderDetailsTest()
        {
            OrderDetailRepository repository = new OrderDetailRepository();
            List<OrderDetail> all = repository.AllOrderDetails;

            Assert.AreEqual(5, all.Count);
        }
        [TestMethod]
        public void getByIdTest()
        {
            OrderDetailRepository repository = new OrderDetailRepository();

            Assert.AreEqual(5, repository.getById(1).Product_ID);
            Assert.AreEqual(0, repository.getById(100).Product_ID);
            Assert.AreEqual(1299.99, repository.getById(5).Price);
        }
    }
}

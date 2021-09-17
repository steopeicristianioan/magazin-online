using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.repository;
using Store.model;

namespace Test
{
    [TestClass]
    public class OrderRepositoryTest
    {
        [TestMethod]
        public void testGetAllOrders()
        {
            OrderRepository repository = new OrderRepository();
            List<Order> allOrders = repository.AllOrders;
            Assert.AreEqual(4, allOrders[allOrders.Count - 1].ID);
            Assert.AreEqual(3, allOrders[0].Ammount);
            Assert.AreEqual(3, allOrders.Count);
        }
        [TestMethod]
        public void testGetUnfinishedOrder()
        {
            OrderRepository repository = new OrderRepository();
            Assert.AreEqual(4, repository.getlastUnfinishedOrder(1).ID);
            //Console.WriteLine(order == null);
        }
    }
}

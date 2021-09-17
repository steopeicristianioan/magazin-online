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
    public class OptionRepositoryTest
    {
        [TestMethod]
        public void getAllOptionsTest()
        {
            OptionRepository repository = new OptionRepository();
            List<Option> options = repository.AllOptions;

            Assert.AreEqual(89, options.Count);
        }
        [TestMethod]
        public void getByIdTest()
        {
            OptionRepository repository = new OptionRepository();

            Assert.AreEqual("Shoe Size 30", repository.getById(62).Name);
            Assert.AreEqual(-1, repository.getById(250).ID);
        }
        [TestMethod]
        public void getFromOrderDetailTest()
        {
            OptionRepository repository = new OptionRepository();
            OrderDetailRepository repository1 = new OrderDetailRepository();
            Assert.AreEqual(2, repository.getOptionsFromOrderDetail(repository1.getById(1)).Count);
            Assert.AreEqual(0, repository.getOptionsFromOrderDetail(repository1.getById(-2)).Count);
        }
    }
}

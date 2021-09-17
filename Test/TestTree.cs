using generics.NETFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Test
{
    [TestClass]
    public class TestTree
    {
        //private readonly ITestOutputHelper output;

        //public TestTree(ITestOutputHelper output)
        //{

        //    this.output = output;
        //}
        [TestMethod]
        public void testAdd()
        {
            PriceTree priceTree = new PriceTree();
            priceTree.Tree.prettyPrint();

            //Assert.AreEqual(tree.find(tree.Root, 3).Data, 3);
        }
    }
}

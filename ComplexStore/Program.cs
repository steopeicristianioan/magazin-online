using ComplexStore.model;
using ComplexStore.repository;
using ComplexStore.staticMethods;
using generics.NETFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComplexStore
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.Run(new MainForm());
            //Product_CategoryRepository repository = new Product_CategoryRepository();
            //CategoryRepository categoryRepository = new CategoryRepository();
            //ChainedHashtable<Category, List<Product>> hashtable = repository.groupByCategories(categoryRepository);

            //foreach(Category category in categoryRepository.AllCategories)
            //{
            //    Console.WriteLine(category + "\n");
            //    List<Product> products = hashtable.get(category);
            //    foreach(Product product in products)
            //        Console.WriteLine(product);
            //}

            
        }
    }
}

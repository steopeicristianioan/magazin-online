using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using generics.NETFramework;
using Store;
using Store.model;
using Store.repository;
using Store.staticMethods;

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

            //Category_OptionRepository category_OptionRepository = new Category_OptionRepository();
            //OptionRepository optionRepository = new OptionRepository();
            //ChainedHashtable<string, List<Option>> hashtable = optionRepository.groupByIdentifiers(15,
            //    category_OptionRepository);
            //foreach (string identifier in StaticMethods.identifiers)
            //{
            //    Console.WriteLine("\t" + identifier);
            //    List<Option> options = hashtable.get(identifier);
            //    if (options != null)
            //    {
            //        foreach (Option option in options)
            //            Console.WriteLine(option);
            //    }
            //}

        }
    }
}

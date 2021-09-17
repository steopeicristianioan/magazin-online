using Store.model;
using Store.staticMethods;
using generics.NETFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Store.staticMethods;

namespace Store.repository
{
    class Product_OptionRepository : Repository
    {
        private List<Product_Option> allProductOptions;
        public Product_OptionRepository() 
        {
            allProductOptions = getallProductOptions();
        }

        public List<Product_Option> getallProductOptions()
        {
            string sql = "select * from product_options";
            return db.LoadData<Product_Option, dynamic>(sql, new { }, connection);
        }
        public List<Product_Option> getProductOptionsByProductId(int id)
        {
            List<Product_Option> res = new List<Product_Option>();
            foreach (Product_Option option in allProductOptions)
                if (option.Product_ID.Equals(id))
                    res.Add(option);
            return res;
        }
        public ChainedHashtable<string, List<Option>> groupByIdentifiers(int productID)
        {
            string[] identifiers = StaticMethods.identifiers;
            List<Product_Option> optionsByID = getProductOptionsByProductId(productID);

            ChainedHashtable<string, List<Option>> hashtable = new ChainedHashtable<string, List<Option>>(10);
            List<Option>[] options = new List<Option>[identifiers.Length];
            OptionRepository optionRepository = new OptionRepository();

            foreach(Product_Option pr_option in optionsByID)
            {
                Option option = optionRepository.getById(pr_option.Option_ID);
                for (int i = 0; i < identifiers.Length; i++)
                    if (option.Name.Contains(identifiers[i]))
                    {
                        if (options[i] == null)
                            options[i] = new List<Option>();
                        options[i].Add(option);
                    }
            }
            
            for(int i = 0; i<identifiers.Length; i++)
            {
                if(options[i] != null)
                    hashtable.put(identifiers[i], options[i]);
            }

            return hashtable;
        }
    }
}

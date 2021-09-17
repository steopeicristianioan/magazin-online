using generics.NETFramework;
using Store.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.repository
{
    public class OptionRepository : Repository
    {
        private List<Option> allOptions;
        public List<Option> AllOptions { get => this.allOptions; }
        public OptionRepository() 
        {
            allOptions = getAllOptions();
        }

        public List<Option> getAllOptions()
        {
            string sqlScript = "select * from options";
            return db.LoadData<Option, dynamic>(sqlScript, new { }, connection);
        }
        public Option getById(int id)
        {
            foreach (Option option in allOptions)
                if (option.ID == id)
                    return option;
            return new Option(-1, "", 0, 0);
        }
        public List<Option> getOptionsFromOrderDetail(OrderDetail detail)
        {
            List<Option> res = new List<Option>();
            string []options = detail.Selected_Options.Split(',');
            foreach (Option option in allOptions)
                if (options.Contains(option.ID.ToString()))
                    res.Add(option);
            return res;
        }
        public List<Option> getByCategory(Category_OptionRepository category_OptionRep, int category_ID)
        {
            List<Category_Option> category_Options = category_OptionRep.getOptionsByCategory(category_ID);
            List<Option> res = new List<Option>();
            foreach (Category_Option category_Option in category_Options)
                res.Add(getById(category_Option.Option_ID));
            return res;
        }
        public ChainedHashtable<string, List<Option>> groupByIdentifiers(int categoryID, 
            Category_OptionRepository category_OptionRepository)
        {
            ChainedHashtable<string, List<Option>> hashtable = new ChainedHashtable<string, List<Option>>(10);
            List<Option> allOptions = getByCategory(category_OptionRepository, categoryID);
            string[] identifiers = staticMethods.StaticMethods.identifiers;

            foreach(string identifier in identifiers)
            {
                List<Option> options = new List<Option>();
                foreach (Option option in allOptions)
                    if (option.Name.Contains(identifier))
                        options.Add(option);
                hashtable.put(identifier, options);
            }

            return hashtable;
        }
        public List<Option> getFromProduct_OptionList(List<Product_Option> product_Options)
        {
            List<Option> res = new List<Option>();
            foreach (Product_Option option in product_Options)
                res.Add(getById(option.Option_ID));
            return res;
        }
    }
}

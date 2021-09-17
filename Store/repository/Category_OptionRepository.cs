using generics.NETFramework;
using Store.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.repository
{
    public class Category_OptionRepository : Repository
    {
        private List<Category_Option> allCategoryOptions;
        public int LastID;

        public Category_OptionRepository()
        {
            getAllCategory_Options();
        }

        public void getAllCategory_Options()
        {
            string sql = "select * from category_options";
            allCategoryOptions = db.LoadData<Category_Option, dynamic>(sql, new { }, connection);
            LastID = allCategoryOptions[allCategoryOptions.Count - 1].ID;
        }

        public List<Category_Option> getOptionsByCategory(int category_ID)
        {
            List<Category_Option> res = new List<Category_Option>();
            foreach (Category_Option category_Option in allCategoryOptions)
                if (category_Option.Category_ID == category_ID)
                    res.Add(category_Option);
            return res;
        }
        
    }
}

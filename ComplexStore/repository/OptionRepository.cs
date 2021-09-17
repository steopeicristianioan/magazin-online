using ComplexStore.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexStore.repository
{
    class OptionRepository : Repository
    {
        private List<Option> allOptions;
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
            return new Option(-1, "", 0);
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
    }
}

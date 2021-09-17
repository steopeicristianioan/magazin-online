using Store.model;
using DataAccessLibrary.NETFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.repository
{
    public class CategoryRepository : Repository
    {
        private List<Category> allCategories;
        public List<Category> AllCategories { get => this.allCategories; }
        public CategoryRepository() 
        {
            allCategories = getAllCategories();
        }

        public List<Category> getAllCategories()
        {
            string sqlScript = "select * from categories";
            return db.LoadData<Category, dynamic>(sqlScript, new { }, connection);
        }
        public Category getById(int id)
        {
            foreach (Category category in allCategories)
                if (category.ID == id)
                    return category;
            return new Category(-1, "");
        }
    }
}

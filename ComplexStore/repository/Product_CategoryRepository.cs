using ComplexStore.model;
using generics.NETFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexStore.repository
{
    class Product_CategoryRepository : Repository
    {
        private List<Product_Category> allProductCategories;
        public Product_CategoryRepository() 
        {
            allProductCategories = getAllProductCategories();
        }

        public List<Product_Category> getAllProductCategories()
        {
            string sql = "select * from product_categories";
            return db.LoadData<Product_Category, dynamic>(sql, new { }, connection);
        }

        public List<Product_Category> getProductCategoriesByCategory(int categoryID)
        {
            List<Product_Category> res = new List<Product_Category>();
            foreach (Product_Category pr_category in allProductCategories)
                if (pr_category.Category_ID == categoryID)
                    res.Add(pr_category);
            return res;
        }
        public ChainedHashtable<Category, List<Product>> groupByCategories(CategoryRepository categoryRepository)
        {
            ProductRepository productRepository = new ProductRepository();
            ChainedHashtable<Category, List<Product>> hashtable = new ChainedHashtable<Category, List<Product>>(10);

            List<Category> allCategories = categoryRepository.AllCategories;

            foreach(Category category in allCategories)
            {
                List<Product_Category> productCategoriesByCategoryID = getProductCategoriesByCategory(category.ID);
                List<Product> products = new List<Product>();
                foreach (Product_Category pr_cat in productCategoriesByCategoryID)
                    products.Add(productRepository.getById(pr_cat.Product_ID));
                hashtable.put(category, products);
            }

            return hashtable;
        }
    }
}

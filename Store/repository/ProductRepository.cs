using Store.model;
using generics.NETFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.repository
{
    public class ProductRepository : Repository
    {
        private List<Product> allProducts;
        public List<Product> AllProducts { get => this.allProducts; }
        public ProductRepository() 
        {
            this.allProducts = getAllProducts();
        }

        public List<Product> getAllProducts() 
        {           
            string sql = "select * from products";
            return db.LoadData<Product, dynamic>(sql, new { }, connection);
        }
        public Product getById(int id)
        {
            foreach (Product product in allProducts)
                if (product.ID == id)
                    return product;
            return new Product(-1, "", 0, null, 0, DateTime.Now, "");
        }

        public List<Product> getFilteredByCategory(ChainedHashtable<Category, List<Product>> hashtable, 
            Category category,
            StandardSearch search)
        {
            List<Product> all = hashtable.get(category);
            List<Product> res = new List<Product>();
            foreach (Product product in all)
                if (product.Price >= search.MinPrice && product.Price <= search.MaxPrice)
                    res.Add(product);
            return res;
        }
    }
}

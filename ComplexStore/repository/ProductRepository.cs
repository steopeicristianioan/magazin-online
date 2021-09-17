using ComplexStore.model;
using generics.NETFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexStore.repository
{
    class ProductRepository : Repository
    {
        private List<Product> allProducts;
        public ProductRepository() 
        {
            this.allProducts = getAllProducts();
        }

        public List<Product> getAllProducts() ///!!!
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

    }
}

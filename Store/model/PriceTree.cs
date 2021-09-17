using generics.NETFramework;
using Store.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.model
{
    public class PriceTree
    {
        private ProductRepository productRepository;
        private BinaryTree<Product> tree;
        public BinaryTree<Product> Tree { get => this.tree; }
        private List<Product> all;

        public PriceTree()
        {
            productRepository = new ProductRepository();
            all = productRepository.AllProducts;
            all.Sort();
            loadTree();
        }

        public void loadTree()
        {
            tree = new BinaryTree<Product>(all[all.Count - 1]);
            Queue<int> roots = new Queue<int>();
            int ct = 0;
            roots.Enqueue(all.Count - 1);
            for(int i = all.Count - 2; i>=0; i--)
            {
                ct++;
                if (ct % 2 == 1 && ct > 1)
                    roots.Dequeue();
                tree.add(all[roots.Peek()], all[i]);
                roots.Enqueue(i);            
            }
        }
    }
}

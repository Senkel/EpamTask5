using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpamTask4.Repository
{
    public class ProductRepository : BasicRepository<Product>
    {
        public Product GetByTitle(string title)
        {
            using (var entities = new MyDBEntities())
            {
                return entities.Product.FirstOrDefault(m => m.Title == title);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpamTask4.Repository
{
   public class ManagerRepository:BasicRepository<Manager>
    {
        public Manager GetByName(string name)
        {
            using (var entities = new MyDBEntities())
            {
                return entities.Manager
                    .FirstOrDefault(m => m.ManagerName == name);
            }
        }

        public decimal GetSumById(int id)
        {
            using (var context = new MyDBEntities())
            {
                return context.Manager
                    .FirstOrDefault(m => m.Id == id)
                    .InfoSale.Sum(x => x.Sum);
            }
        }

        public IEnumerable<InfoSale> GetSalesByManagerId(int id)
        {
            using (var context = new MyDBEntities())
            {
                return context.Manager
                    .FirstOrDefault(m => m.Id == id)
                    .InfoSale
                    .ToList();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpamTask4.Repository
{
    public class BasicRepository<T> where T : class, IEntity
    {
        public IEnumerable<T> GetById(int id)
        {
            using (var entities = new MyDBEntities())
            {
                return entities.Set<T>().Where(x => x.Id == id).ToList();
            }
        }

        public int Insert(T item)
        {
            using (var context = new MyDBEntities())
            {
                var entities = context.Set<T>().Add(item);
                context.SaveChanges();
                return item.Id;
            }
        }

        public IEnumerable<T> GetAll()
        {
            using (var context = new MyDBEntities())
            {
                var entities = context.Set<T>().ToList();
                return entities;
            }
        }

        public void Remove(T item)
        {
            using (var context = new MyDBEntities())
            {
                //context.Set<T>().Remove(item);
                context.Entry(item).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public void Update(T item)
        {
            using (var context = new MyDBEntities())
            {
                //context.Set<T>().Attach(item);
                context.Entry(item).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}

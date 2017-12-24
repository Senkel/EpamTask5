using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpamTask4.Repository
{
    public class ClientRepository : BasicRepository<Client>
    {
        public Client GetByName(string name)
        {
            using (var entities = new MyDBEntities())
            {
                return entities.Client
                    .FirstOrDefault(m => m.ClientName == name);
            }
        }
    }
}

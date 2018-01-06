using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EpamTask5.Models
{
    public class SaleInfo
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public decimal Sum { get; set; }

        public int IdManager { get; set; }

        public int IdClient { get; set; }
         
        public int IdProduct { get; set; }
    }
}
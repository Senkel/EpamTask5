using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EpamTask5.Models
{
    public class Product
    {
        [MaxLength(10, ErrorMessage = "Too many chars")]
        [StringLength(10, ErrorMessage = "Too many chars")]
        public string Name { get; set; }

        public int Id { get; set; }
    }
}
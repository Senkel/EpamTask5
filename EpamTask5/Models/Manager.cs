﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EpamTask5.Models
{
    public class Manager
    {
        [MaxLength (10)]
        [StringLength(10)]
        public string Name { get; set; }

        public int Id { get; set; }
    }
}
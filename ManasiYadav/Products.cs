﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace ManasiYadav
{
    public class Products
    {
       [Key]
       public int ProductId { get; set; }
       public string Name { get; set; }
       public string Description { get; set; }
       public float Price { get; set; }
       public string Category { get; set; } 
        
    }
}

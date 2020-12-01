using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PruebaApi.Models
{
    public class ProductoViewModel
    {
         public int IdProducto { get; set; }
        public string Tipo { get; set; }
         
        
        public ColorViewModel Color { get; set; }
        public TalleViewModel Talle { get; set; }
    }
}